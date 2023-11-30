using System;

namespace CommonLib.Extensions;
/// <summary>
/// Получение рандомного значения из массива
/// </summary>
public static class RandomItemExt
{
   public static T NextItem<T>(this Random rnd, params T[] items) => items[rnd.Next(items.Length)];
}