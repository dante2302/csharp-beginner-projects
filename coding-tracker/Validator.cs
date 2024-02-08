    public class Validator
    {
        public static bool ValidateTime(string time)
        {
            return TimeOnly.TryParseExact(time, "HH:mm", out _);
        }

        public static bool ValidateDate(string date)
        {
            return DateOnly.TryParseExact(date, "dd/MM/yyyy", out _);
        }

    }
