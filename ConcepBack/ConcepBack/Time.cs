using System;
using System.Collections.Generic;
using System.Text;

namespace tiempo
{
    internal class Time
    {
        // Propiedades privadas
        private int _hour;
        private int _minute;
        private int _second;
        private int _millisecond;
        private int time;


        public Time() : this(0, 0, 0, 0) { }


        public Time(int hour) : this(hour, 0, 0, 0) { }


        public Time(int hour, int minute) : this(hour, minute, 0, 0) { }


        public Time(int hour, int minute, int second) : this(hour, minute, second, 0) { }


        public Time(int hour, int minute, int second, int millisecond)
        {
            if (hour < 0 || hour > 23) throw new ArgumentOutOfRangeException(nameof(hour), "La hora debe estar entre 0 y 23.");
            if (minute < 0 || minute > 59) throw new ArgumentOutOfRangeException(nameof(minute), "Los minutos deben estar entre 0 y 59.");
            if (second < 0 || second > 59) throw new ArgumentOutOfRangeException(nameof(second), "Los segundos deben estar entre 0 y 59.");
            if (millisecond < 0 || millisecond > 999) throw new ArgumentOutOfRangeException(nameof(millisecond), "Los milisegundos deben estar entre 0 y 999.");

            _hour = hour;
            _minute = minute;
            _second = second;
            _millisecond = millisecond;
        }

        public long ToMilliseconds()
        {
            long time = (_hour * 3600000L) + (_minute * 60000L) + (_second * 1000L) + _millisecond;
            return time;
        }

        public double ToSeconds()
        {
            double time = ToMilliseconds() / 1000.0;
            return time;
        }

        public double ToMinutes()
        {
            double time = ToMilliseconds() / 60000.0;
            return time;

        }

        public bool IsOtherDay(Time other)
        {
            long totalMs = this.ToMilliseconds() + other.ToMilliseconds();
            return totalMs >= 86400000; // 24 horas en ms
        }

        public Time Add(Time other)
        {
            long totalMs = this.ToMilliseconds() + other.ToMilliseconds();

            // Usamos módulo para "reiniciar" el tiempo si se pasa de las 24 horas
            long msInADay = 86400000;
            long remainingMs = totalMs % msInADay;

            int h = (int)(remainingMs / 3600000);
            int m = (int)((remainingMs % 3600000) / 60000);
            int s = (int)((remainingMs % 60000) / 1000);
            int ms = (int)(remainingMs % 1000);

            return new Time(h, m, s, ms);
        }

        public override string ToString()
        {

            int displayHour = _hour % 12;
            if (displayHour == 0) displayHour = 12;
            string tt = _hour < 12 ? "AM" : "PM";

            return $"{displayHour:D2}:{_minute:D2}:{_second:D2}.{_millisecond:D3} {tt}";
        }
    }
}
