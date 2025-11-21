using System.Globalization;
using System.Reflection;

namespace Despro.Blazor.Base.BaseGenerals
{
    public static class DatePersian
    {
        public class Cultures
        {
            public static void InitializePersianCulture()
            {
                InitializeCulture("fa-ir", new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" },
                                  new[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" },
                                  new[]
                                      {
                                      "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی",
                                      "بهمن", "اسفند", ""
                                      },
                                  new[]
                                      {
                                      "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی",
                                      "بهمن", "اسفند", ""
                                      }, "ق.ظ. ", "ب.ظ. ", "yyyy/MM/dd", new PersianCalendar());
            }

            public static void InitializeCulture(string culture, string[] abbreviatedDayNames, string[] dayNames,
                                                 string[] abbreviatedMonthNames, string[] monthNames, string amDesignator,
                                                 string pmDesignator, string shortDatePattern, Calendar calendar)
            {
                CultureInfo calture = new(culture);
                DateTimeFormatInfo info = calture.DateTimeFormat;
                info.AbbreviatedDayNames = abbreviatedDayNames;
                //info.DayNames = dayNames;
                info.AbbreviatedMonthNames = abbreviatedMonthNames;
                info.MonthNames = monthNames;
                info.AMDesignator = amDesignator;
                info.PMDesignator = pmDesignator;
                info.ShortDatePattern = shortDatePattern;
                info.FirstDayOfWeek = DayOfWeek.Saturday;
                Calendar cal = calendar;
                Type type = typeof(DateTimeFormatInfo);
                FieldInfo fieldInfo = type.GetField("calendar", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo?.SetValue(info, cal);
                FieldInfo field = typeof(CultureInfo).GetField("calendar", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
                field?.SetValue(calture, cal);
                Thread.CurrentThread.CurrentCulture = calture;
                Thread.CurrentThread.CurrentUICulture = calture;
                CultureInfo.CurrentCulture.DateTimeFormat = info;
                CultureInfo.CurrentUICulture.DateTimeFormat = info;
                CultureInfo cultureInfo = new("fa-IR");
                cultureInfo.NumberFormat.CurrencySymbol = "ريال";

                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            }
        }
    }

    public class PersianCulture : CultureInfo
    {
        private readonly Calendar _calendar;
        private readonly Calendar[] _optionalCalendars;
        private DateTimeFormatInfo _dateTimeFormatInfo;

        public PersianCulture() : base("fa-IR")
        {
            _calendar = new PersianCalendar();

            _optionalCalendars = new List<Calendar>
            {
                new PersianCalendar(),
                new GregorianCalendar()
            }.ToArray();

            DateTimeFormatInfo dateTimeFormatInfo = CreateSpecificCulture("fa-IR").DateTimeFormat;
            dateTimeFormatInfo.Calendar = _calendar;
            _dateTimeFormatInfo = dateTimeFormatInfo;
        }

        public override Calendar Calendar => _calendar;

        public override Calendar[] OptionalCalendars => _optionalCalendars;

        public override DateTimeFormatInfo DateTimeFormat
        {
            get => _dateTimeFormatInfo;
            set => _dateTimeFormatInfo = value;
        }
    }
}
