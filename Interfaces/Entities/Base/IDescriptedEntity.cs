namespace CommonLib.Interfaces.Entities.Base;

/// <summary>Сущность, снабжённая комментарием</summary>
public interface IDescriptedEntity : IEntity
{
    /// <summary>Комментарий</summary>
    public string? Description { get; set; }
}