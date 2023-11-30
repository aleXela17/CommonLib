using System;

namespace CommonLib.Extensions;

public static class RandomDateExt
{
   //private DateTime start = new DateTime(2003, 10, 1);
   public static DateTime RndDateTime(this Random rnd)
   {
      DateTime start = new(2003, 1, 1);
      int range = (DateTime.Today - start).Days;
      return start.AddDays(rnd.Next(range));
   }
}