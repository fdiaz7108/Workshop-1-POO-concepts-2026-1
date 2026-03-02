using System.Threading;
using System.Timers;


try
{
    // Ejemplo solicitado
    Time t1 = new Time();
    Time t2 = new Time(14);
    Time t3 = new Time(9, 34);
    Time t4 = new Time(19, 45, 56);
    Time t5 = new Time(23, 3, 45, 678);


    var times = new List<Time> { t1, t2, t3, t4, t5 };
    foreach (Time time in times)
    {
        Console.WriteLine($"Time: {time}");
        Console.WriteLine($"\tMilliseconds 1: {time.ToMilliseconds(),15:N0}");
        Console.WriteLine($"\tSeconds: {time.ToSeconds(),15:N0}");
        Console.WriteLine($"\tMinutes: {time.ToMinutes(),15:N0}");
        Console.WriteLine($"\tAdd: {time.Add(t3)}");
        Console.WriteLine($"\tIsOtherDay: {time.IsOtherDay(t4)}");

    }
    var t6 = new Time(45, -7, 90, -87);


}
catch (Exception ex)
{
    Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
}
