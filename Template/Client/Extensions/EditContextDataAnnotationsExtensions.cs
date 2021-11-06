using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Template.Shared.Services;

namespace Template.Client.Extensions;

public static class EditContextDataAnnotationsExtensions
{
    private static readonly ConcurrentDictionary<(Type ModelType, string FieldName), PropertyInfo> _propertyInfoCache = new();

    public static EditContext AddLocalizedDataAnnotationsValidation(this EditContext editContext, Localizer localizer)
    {
        if (editContext == null)
        {
            throw new ArgumentNullException(nameof(editContext));
        }

        var messages = new ValidationMessageStore(editContext);

        // Perform object-level validation on request
        editContext.OnValidationRequested +=
            (sender, eventArgs) => ValidateModel((EditContext)sender, messages, localizer);

        // Perform per-field validation on each field edit
        editContext.OnFieldChanged +=
            (sender, eventArgs) => ValidateField(editContext, messages, eventArgs.FieldIdentifier, localizer);

        return editContext;
    }

    private static void ValidateModel(EditContext editContext, ValidationMessageStore messages, Localizer localizer)
    {
        var validationContext = new ValidationContext(editContext.Model);
        var validationResults = new List<ValidationResult>();

        Validator.TryValidateObject(editContext.Model, validationContext, validationResults, true);

        // Transfer results to the ValidationMessageStore
        messages.Clear();

        foreach (var validationResult in validationResults)
        {
            foreach (var memberName in validationResult.MemberNames)
            {
                messages.Add(editContext.Field(memberName), localizer[validationResult.ErrorMessage]);
            }
        }

        editContext.NotifyValidationStateChanged();
    }

    private static void ValidateField(EditContext editContext, ValidationMessageStore messages, in FieldIdentifier fieldIdentifier, Localizer localizer)
    {
        if (!TryGetValidatableProperty(fieldIdentifier, out var propertyInfo))
        {
            return;
        }

        var propertyValue = propertyInfo.GetValue(fieldIdentifier.Model);

        var validationContext = new ValidationContext(fieldIdentifier.Model)
        {
            MemberName = propertyInfo.Name
        };

        var results = new List<ValidationResult>();

        Validator.TryValidateProperty(propertyValue, validationContext, results);

        messages.Clear(fieldIdentifier);
        messages.Add(fieldIdentifier, results.Select(result => localizer[result.ErrorMessage]));

        editContext.NotifyValidationStateChanged();
    }

    private static bool TryGetValidatableProperty(in FieldIdentifier fieldIdentifier, out PropertyInfo propertyInfo)
    {
        var cacheKey = (ModelType: fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName);

        if (!_propertyInfoCache.TryGetValue(cacheKey, out propertyInfo))
        {
            // DataAnnotations only validates public properties, so that's all we'll look for
            // If we can't find it, cache 'null' so we don't have to try again next time
            propertyInfo = cacheKey.ModelType.GetProperty(cacheKey.FieldName);

            // No need to lock, because it doesn't matter if we write the same value twice
            _propertyInfoCache[cacheKey] = propertyInfo;
        }

        return propertyInfo != null;
    }
}
