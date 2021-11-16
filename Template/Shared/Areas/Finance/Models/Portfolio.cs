using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Shared.Areas.Finance.Models;

public class Portfolio
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }

    [NotMapped]
    public Asset[] Assets { get; set; }

    [NotMapped]
    public Asset[] DisplayedAssets { get; set; }
}
