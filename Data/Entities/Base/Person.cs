using System.ComponentModel.DataAnnotations;

namespace CommonLib.Data.Entities.Base;

public abstract class Person : Entity
{
   /// <summary>Фамилия</summary>
   [MaxLength(30)]
   public string LastName { get; set; }

   /// <summary>Имя</summary>
   [MaxLength(20)]
   public string FirstName { get; set; }

   /// <summary>Отчество</summary>
   [MaxLength(30)]
   public string? Patronymic { get; set; }

   protected Person()
   {
      LastName = string.Empty;
      FirstName = string.Empty;
   }
   protected Person(string lastName, string firstName, string patronymic)
   {
      LastName = lastName;
      FirstName = firstName;
      Patronymic = patronymic;
   }

   public override string ToString() => $"[id:{Id}] {string.Join(' ', LastName, FirstName, Patronymic)}";

   public string ShortName => $"{FirstName} {LastName[..1]}. {(string.IsNullOrEmpty(Patronymic)
      ? ""
      : Patronymic?[..1] + ".")}";
}