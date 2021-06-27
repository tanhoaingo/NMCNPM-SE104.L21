using System;

namespace BookStore.Tools
{
    public class Rules
    {
        private static Rules _Instance;

        public static Rules Instance
        {
            get { if (_Instance == null) _Instance = new Rules(); return _Instance; }
            set { _Instance = value; }
        }

        public Int64 ConvertStringAmountToInt64(string amount)
        {
            if (amount == null || amount == "")
            {
                return 0;
            }
                return Int64.Parse(amount);
        }
        public long ConvertDecimal_nullToInt64(decimal? number)
        {
            if (number == null)
            {
                return 0;
            }
            decimal tmp = number ?? 0;
            return Convert.ToInt64(tmp);
        }
        public long ConvertInt_nullToInt64(int? number)
        {
            if (number == null)
            {
                return 0;
            }
            int tmp = number ?? 0;
            return Convert.ToInt64(tmp);
        }

    }
}
