using Back;
using System;

try
{
    // Ejemplos válidos
    Time t1 = new Time();
    Time t2 = new Time(14);
    Time t3 = new Time(9, 34);
    Time t4 = new Time(19, 45, 56);
    Time t5 = new Time(23, 3, 45, 678);

    var times = new List<Time> { t1, t2, t3, t4, t5 };
    foreach (Time time in times)
    {
        Console.WriteLine($"Time: {time}");
        Console.WriteLine($"\tMilliseconds: {time.ToMilliseconds(),15:N0}");
        Console.WriteLine($"\tSeconds: {time.ToSeconds(),15:N0}");
        Console.WriteLine($"\tMinutes: {time.ToMinutes(),15:N0}");
        Console.WriteLine($"\tAdd con t3: {time.Add(t3)}");
        Console.WriteLine($"\tIsOtherDay con t4: {time.IsOtherDay(t4)}");
    }

    // Esto debería lanzar excepción
    Time t6 = new Time(45, -7, 90, -87);
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Error de validación: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error inesperado: {ex.Message}");
}