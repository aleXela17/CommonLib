using CommonLib.Interfaces.Entities.Base;

namespace CommonLib.Data.Entities.Base;

/// <summary>Сущность</summary>
public abstract class Entity : IEntity
{
   /// <summary>Первичный ключ</summary>
   public int Id { get; set; }
}