namespace TDDBank
{
    public class OpeningHours
    {
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
