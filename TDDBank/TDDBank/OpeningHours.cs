namespace TDDBank
{
    public class OpeningHours
    {
        public bool IsWeekend()
        {
            return DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday;
        }

        public bool IsNowOpen() //:-(
        {
            // Öffnungszeiten Montag bis Freitag
            if (DateTime.Now.DayOfWeek >= DayOfWeek.Monday && DateTime.Now.DayOfWeek <= DayOfWeek.Friday)
            {
                if (DateTime.Now.TimeOfDay >= new TimeSpan(10, 30, 0) && DateTime.Now.TimeOfDay < new TimeSpan(19, 0, 0))
                {
                    return true;
                }
            }
            // Öffnungszeiten Samstag
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                if (DateTime.Now.TimeOfDay >= new TimeSpan(10, 30, 0) && DateTime.Now.TimeOfDay < new TimeSpan(14, 0, 0))
                {
                    return true;
                }
            }

            // Sonntag geschlossen
            return false;
        }


        public bool IsOpen(DateTime zeit)
        {
            // Öffnungszeiten Montag bis Freitag
            if (zeit.DayOfWeek >= DayOfWeek.Monday && zeit.DayOfWeek <= DayOfWeek.Friday)
            {
                if (zeit.TimeOfDay >= new TimeSpan(10, 30, 0) && zeit.TimeOfDay < new TimeSpan(19, 0, 0))
                {
                    return true;
                }
            }
            // Öffnungszeiten Samstag
            else if (zeit.DayOfWeek == DayOfWeek.Saturday)
            {
                if (zeit.TimeOfDay >= new TimeSpan(10, 30, 0) && zeit.TimeOfDay < new TimeSpan(14, 0, 0))
                {
                    return true;
                }
            }

            // Sonntag geschlossen
            return false;
        }
    }
}
