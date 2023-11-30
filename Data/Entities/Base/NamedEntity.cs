using System.ComponentModel.DataAnnotations;

namespace CommonLib.Data.Entities.Base;

public abstract class NamedEntity : Entity
{
   [Required]
   public string? Name { get; set; }
   
}