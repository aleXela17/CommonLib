using CommonLib.Interfaces.Entities.Base;

namespace CommonLib.Data.Entities.Base;

/// <summary>Сущность, снабжённая комментарием</summary>
public abstract class DescriptedEntity : Entity, IDescriptedEntity
{
   /// <summary>Комментарий</summary>
   public string? Description { get; set; }
}