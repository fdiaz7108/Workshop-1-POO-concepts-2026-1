namespace Back
{
    public class Time
    {
        private const long MsPerDay = 24L * 60 * 60 * 1000;
        private readonly int _hours;
        private readonly int _minutes;
        private readonly int _seconds;
        private readonly int _milliseconds;
        private readonly long _dayOffset;

        // Constructores con validación
        public Time() : this(0, 0, 0, 0) { }

        public Time(int hour) : this(hour, 0, 0, 0) { }

        public Time(int hour, int minute) : this(hour, minute, 0, 0) { }

        public Time(int hour, int minute, int second) : this(hour, minute, second, 0) { }

        public Time(int hour, int minute, int second, int millisecond)
        {
            // VALIDACIÓN DE RANGOS
            if (hour < 0 || hour > 23)
                throw new ArgumentOutOfRangeException(nameof(hour), "Las horas deben estar entre 0 y 23");
            if (minute < 0 || minute > 59)
                throw new ArgumentOutOfRangeException(nameof(minute), "Los minutos deben estar entre 0 y 59");
            if (second < 0 || second > 59)
                throw new ArgumentOutOfRangeException(nameof(second), "Los segundos deben estar entre 0 y 59");
            if (millisecond < 0 || millisecond > 999)
                throw new ArgumentOutOfRangeException(nameof(millisecond), "Los milisegundos deben estar entre 0 y 999");

            _hours = hour;
            _minutes = minute;
            _seconds = second;
            _milliseconds = millisecond;
            _dayOffset = 0;
        }

        // Private constructor for normalized values
        private Time(int hours, int minutes, int seconds, int milliseconds, long dayOffset)
        {
            _hours = hours;
            _minutes = minutes;
            _seconds = seconds;
            _milliseconds = milliseconds;
            _dayOffset = dayOffset;
        }

        // Conversiones
        public long ToMilliseconds()
        {
            return ((_hours * 60L + _minutes) * 60L + _seconds) * 1000L + _milliseconds;
        }

        public long ToSeconds() => ToMilliseconds() / 1000L;
        public long ToMinutes() => ToMilliseconds() / (60L * 1000L);

        // Add - mantiene la lógica original pero usando el constructor privado
        public Time Add(Time other)
        {
            long sumMs = this.ToMilliseconds() + other.ToMilliseconds();
            double dayOffsetDouble = Math.Floor(sumMs / (double)MsPerDay);
            long newDayOffset = (long)dayOffsetDouble;
            long rem = sumMs - newDayOffset * MsPerDay;

            if (rem < 0)
            {
                rem += MsPerDay;
                newDayOffset -= 1;
            }

            int newMilliseconds = (int)(rem % 1000);
            rem /= 1000;
            int newSeconds = (int)(rem % 60);
            rem /= 60;
            int newMinutes = (int)(rem % 60);
            rem /= 60;
            int newHours = (int)(rem % 24);

            return new Time(newHours, newMinutes, newSeconds, newMilliseconds, newDayOffset);
        }

        // IsOtherDay - AHORA VERIFICA LA SUMA
        public bool IsOtherDay(Time other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            Time sum = this.Add(other);
            return sum._dayOffset > 0;
        }

        // ToString con formato AM/PM
        public override string ToString()
        {
            int hour12 = _hours % 12;
            if (hour12 == 0) hour12 = 12;
            string ampm = _hours < 12 ? "AM" : "PM";
            return $"{hour12:D2}:{_minutes:D2}:{_seconds:D2}.{_milliseconds:D3} {ampm}";
        }

        // Propiedades
        public int Hours => _hours;
        public int Minutes => _minutes;
        public int Seconds => _seconds;
        public int Milliseconds => _milliseconds;
        public long DayOffset => _dayOffset;
    }
}