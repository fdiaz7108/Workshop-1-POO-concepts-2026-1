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

        // Public constructors
        public Time() : this(0, 0, 0, 0) { }

        public Time(int hour) : this(hour, 0, 0, 0) { }

        public Time(int hour, int minute) : this(hour, minute, 0, 0) { }

        public Time(int hour, int minute, int second) : this(hour, minute, second, 0) { }

        public Time(int hour, int minute, int second, int millisecond)
        {
            // Compute total milliseconds from provided components (can be negative or > day)
            long totalMs = ((long)hour * 60L * 60L + (long)minute * 60L + (long)second) * 1000L + millisecond;

            // dayOffset: floor division to handle negatives correctly
            double dayOffsetDouble = Math.Floor(totalMs / (double)MsPerDay);
            _dayOffset = (long)dayOffsetDouble;

            long rem = totalMs - _dayOffset * MsPerDay; // remainder in [0, MsPerDay)
            if (rem < 0)
            {
                // Defensive: ensure remainder non-negative
                rem += MsPerDay;
                _dayOffset -= 1;
            }

            _milliseconds = (int)(rem % 1000);
            rem /= 1000;
            _seconds = (int)(rem % 60);
            rem /= 60;
            _minutes = (int)(rem % 60);
            rem /= 60;
            _hours = (int)(rem % 24);
        }

        // Private convenience constructor for already-normalized components + explicit day offset
        private Time(int hours, int minutes, int seconds, int milliseconds, long dayOffset)
        {
            _hours = hours;
            _minutes = minutes;
            _seconds = seconds;
            _milliseconds = milliseconds;
            _dayOffset = dayOffset;
        }

        // Public methods (accessible from Program)
        public long ToMilliseconds()
        {
            long ms = (((_hours * 60L + _minutes) * 60L + _seconds) * 1000L) + _milliseconds;
            return _dayOffset * MsPerDay + ms;
        }

        public long ToSeconds() => ToMilliseconds() / 1000L;

        public long ToMinutes() => ToMilliseconds() / (60L * 1000L);

        public Time Add(Time other)
        {
            long sumMs = this.ToMilliseconds() + other.ToMilliseconds();

            // Compute new day offset and normalized components
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

        public bool IsOtherDay(Time other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            return this._dayOffset != other._dayOffset;
        }

        public override string ToString()
        {
            string baseTime = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", _hours, _minutes, _seconds, _milliseconds);
            if (_dayOffset == 0) return baseTime;
            return $"{baseTime} (dayOffset:{(_dayOffset >= 0 ? "+" : "")}{_dayOffset})";
        }



        // Optional: expose read-only properties if needed
        public int Hours => _hours;
        public int Minutes => _minutes;
        public int Seconds => _seconds;
        public int Milliseconds => _milliseconds;
        public long DayOffset => _dayOffset;
    }
}
