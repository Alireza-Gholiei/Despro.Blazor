using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Despro.Blazor.Layout.Components.Dropdowns;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Despro.Blazor.Form.Components.Datepickers;

public partial class Datepicker<TValue> : BaseComponent
{
    [Parameter] public bool Inline { get; set; }
    [Parameter] public string Format { get; set; }
    [Parameter] public bool DatePicker { get; set; } = true;
    [Parameter] public bool TimePicker { get; set; } = false;
    [Parameter] public TValue SelectedDate { get; set; }
    [Parameter] public EventCallback<TValue> SelectedDateChanged { get; set; }
    [Parameter] public EventCallback Updated { get; set; }
    [Parameter] public string Label { get; set; }
    [Parameter] public bool IsDisabled { get; set; } = false;
    [Parameter] public RenderFragment ChildContent { get; set; }

    private TValue _value;
    private DateTimeOffset? _selectedDate;
    private DateTimeOffset _currentDate = DateTimeOffset.Now;
    private readonly BaseColor _selectedColor = BaseColor.Primary;

    private string hours
    {
        get => _currentDate.ToString("HH", Culture.DateTimeFormat);
        set
        {
            try
            {
                if (int.TryParse(value, out var newValue))
                {
                    if (newValue > 24)
                    {
                        newValue = 0;
                    }
                }
                else
                {
                    newValue = 0;
                }

                _currentDate = _currentDate.Date.AddHours(newValue).AddMinutes(_currentDate.Minute);
                _ = SetSelectedHorse(_currentDate);
            }
            catch (Exception)
            {
            }
        }
    }
    private string minute
    {
        get => _currentDate.ToString("mm", Culture.DateTimeFormat);
        set
        {
            try
            {
                if (int.TryParse(value, out var newValue))
                {
                    if (newValue > 60)
                    {
                        newValue = 0;
                    }
                }
                else
                {
                    newValue = 0;
                }

                _currentDate = _currentDate.Date.AddHours(_currentDate.Hour).AddMinutes(newValue);
                _ = SetSelectedHorse(_currentDate);
            }
            catch (Exception)
            {
            }
        }
    }

    private bool _allYear = false;
    private bool _allMonth = false;

    private CultureInfo Culture => CultureInfo.CurrentCulture;
    private Dropdown _dropdown;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (string.IsNullOrWhiteSpace(Format))
        {
            Format = DatePicker switch
            {
                true when TimePicker => "HH:mm yyyy/MM/dd",
                true when !TimePicker => "yyyy/MM/dd",
                false when TimePicker => "HH:mm",
                _ => Format
            };
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (!EqualityComparer<TValue>.Default.Equals(_value, SelectedDate))
        {
            _value = SelectedDate;

            await SetSelectedDay(ConvertToDateTimeOffset(SelectedDate));
        }
    }

    private TValue ConvertToTValue(DateTimeOffset? value)
    {
        var type = typeof(TValue);

        if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
        {
            return (TValue)(object)value;
        }
        else if (type == typeof(DateTime) || type == typeof(DateTime?))
        {
            return (TValue)(object)value?.DateTime;
        }
        else if (type == typeof(long) || type == typeof(long?))
        {
            if (TimePicker && DatePicker)
            {
                return (TValue)(object)value?.Ticks;
            }
            else if (DatePicker)
            {
                return (TValue)(object)value?.Date.Ticks;
            }
            else if (TimePicker)
            {
                return (TValue)(object)value?.TimeOfDay.Ticks;
            }

            return (TValue)(object)value?.Ticks;
        }
        else if (type == typeof(string))
        {
            if (TimePicker && DatePicker)
            {
                return (TValue)(object)value?.ToString("yyyy/MM/dd HH:mm:ss");
            }
            else if (DatePicker)
            {
                return (TValue)(object)value?.ToString("yyyy/MM/dd");
            }
            else if (TimePicker)
            {
                return (TValue)(object)value?.ToString("HH:mm:ss");
            }
        }
        else if (type == typeof(TimeSpan) || type == typeof(TimeSpan?))
        {
            return (TValue)(object)(value?.TimeOfDay ?? TimeSpan.Zero);
        }

        return default!;
    }

    private DateTimeOffset? ConvertToDateTimeOffset(TValue value)
    {
        var type = typeof(TValue);
        if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
        {
            return value as DateTimeOffset?;
        }
        else if (type == typeof(DateTime) || type == typeof(DateTime?))
        {
            var dateTime = value as DateTime?;
            DateTimeOffset? newDate = dateTime;
            return newDate;
        }
        else if (type == typeof(long) || type == typeof(long?))
        {
            var dateTime = value as long?;
            DateTimeOffset? newDate = new DateTime(dateTime.GetValueOrDefault(0));
            return newDate;
        }
        else if (type == typeof(string))
        {
            var dateTime = value as string;
            DateTimeOffset? newDate = DateTime.Parse(dateTime);
            return newDate;
        }
        else if (type == typeof(TimeSpan) || type == typeof(TimeSpan?))
        {
            if (value is TimeSpan timeSpan)
            {
                return DateTimeOffset.Now.Date + timeSpan;
            }
        }

        return default!;
    }

    private string[] GetWeekdays()
    {
        var names = Culture.DateTimeFormat.AbbreviatedDayNames;
        var first = (int)Culture.DateTimeFormat.FirstDayOfWeek;

        return names.Skip(first).Take(names.Length - first).Concat(names.Take(first)).ToArray();
    }

    private string GetCurrentMonth()
    {
        return _currentDate.ToString("MMMM", Culture.DateTimeFormat);
    }

    private string GetCurrentYear()
    {
        return _currentDate.ToString("yyyy", Culture.DateTimeFormat);
    }

    private string GetCurrentHorse()
    {
        return _currentDate.ToString("HH", Culture.DateTimeFormat);
    }

    private string GetCurrentMinutes()
    {
        return _currentDate.ToString("mm", Culture.DateTimeFormat);
    }

    private void SetPreviousMonth()
    {
        _currentDate = _currentDate.AddMonths(-1);
    }

    private void SetNextMonth()
    {
        _currentDate = _currentDate.AddMonths(1);
    }

    private void SetPreviousYear()
    {
        _currentDate = _currentDate.AddYears(-20);
    }

    private void SetNextYear()
    {
        _currentDate = _currentDate.AddYears(20);
    }

    private void SetHorseChange()
    {

    }

    private void SetMinuteChange()
    {

    }

    private void SetTime()
    {
        _ = SetSelectedHorse(_currentDate);

        if (!Inline && _dropdown != null)
        {
            _dropdown.Close();
        }
    }

    private void SetPreviousHorse()
    {
        _currentDate = _currentDate.AddHours(-1);

        _ = SetSelectedHorse(_currentDate);
    }

    private void SetNextHorse()
    {
        _currentDate = _currentDate.AddHours(1);

        _ = SetSelectedHorse(_currentDate);
    }

    private void SetPreviousMinutes()
    {
        _currentDate = _currentDate.AddMinutes(-1);

        _ = SetSelectedMinutes(_currentDate);
    }

    private void SetNextMinutes()
    {
        _currentDate = _currentDate.AddMinutes(1);

        _ = SetSelectedMinutes(_currentDate);
    }

    private DateTimeOffset FirstDateInWeek(DateTimeOffset dt)
    {
        while (dt.DayOfWeek != Culture.DateTimeFormat.FirstDayOfWeek)
            dt = dt.AddDays(-1);

        return dt;
    }

    private DateTimeOffset FirstYearInTen(DateTime dt)
    {
        PersianCalendar persianCalendar = new();

        var yearSt = persianCalendar.GetYear(dt).ToString();

        var year = int.Parse(yearSt.Substring(yearSt.Length - 1, 1));

        dt = persianCalendar.ToDateTime(int.Parse(yearSt) - year, persianCalendar.GetMonth(dt), persianCalendar.GetDayOfMonth(dt), 0, 0, 0, 0);

        return dt;
    }

    private List<DateTimeOffset> GetDates()
    {
        PersianCalendar persianCalendar = new();

        List<DateTimeOffset> dates = [];
        var firstDayOfMonth = _currentDate.Date.AddDays(1 - persianCalendar.GetDayOfMonth(_currentDate.DateTime));
        var firstDate = FirstDateInWeek(firstDayOfMonth);

        for (var i = 0; i < 42; i++)
        {
            dates.Add(firstDate);
            firstDate = firstDate.AddDays(1);
        }

        return dates;
    }

    private List<DateTimeOffset> GetMounts()
    {
        PersianCalendar persianCalendar = new();

        var firstYear = _currentDate;

        List<DateTimeOffset> mounts = [];

        firstYear = firstYear.AddMonths(1 - persianCalendar.GetMonth(firstYear.DateTime));

        var daysOfMonth = persianCalendar.GetDayOfMonth(firstYear.DateTime);
        var daysInMonthCur = persianCalendar.GetDaysInMonth(firstYear.Year, firstYear.Month);

        firstYear = firstYear.AddDays(daysInMonthCur - daysOfMonth - daysInMonthCur + 1);

        mounts.Add(firstYear);

        while (persianCalendar.GetMonth(firstYear.DateTime) != 12)
        {
            var daysInMonth = persianCalendar.GetDaysInMonth(persianCalendar.GetYear(firstYear.DateTime), persianCalendar.GetMonth(firstYear.DateTime));
            firstYear = firstYear.AddDays(daysInMonth);
            mounts.Add(firstYear);
        }

        return mounts;
    }

    private List<DateTimeOffset> GetYears()
    {
        List<DateTimeOffset> dates = [];

        var firstYear = FirstYearInTen(_currentDate.Date);

        for (var i = 0; i < 20; i++)
        {
            dates.Add(firstYear);
            firstYear = firstYear.AddYears(1);
        }

        return dates;
    }

    private async Task SetSelectedYear(DateTimeOffset? date)
    {
        _selectedDate = date;

        if (date != null)
        {
            _currentDate = (DateTimeOffset)date;
        }

        _value = ConvertToTValue(_selectedDate);

        await SelectedDateChanged.InvokeAsync(_value);

        await Updated.InvokeAsync(_value);

        _allYear = false;
        _allMonth = true;
    }

    private async Task SetSelectedMonth(DateTimeOffset? date)
    {
        _selectedDate = date;

        if (date != null)
        {
            _currentDate = (DateTimeOffset)date;
        }

        _value = ConvertToTValue(_selectedDate);

        await SelectedDateChanged.InvokeAsync(_value);

        await Updated.InvokeAsync(_value);

        _allYear = false;
        _allMonth = false;
    }

    private async Task SetSelectedDay(DateTimeOffset? date)
    {
        _selectedDate = date;

        if (date != null)
        {
            _currentDate = (DateTimeOffset)date;
        }

        _value = ConvertToTValue(_selectedDate);

        await SelectedDateChanged.InvokeAsync(_value);

        await Updated.InvokeAsync(_value);
        if (!Inline && _dropdown != null && !TimePicker)
        {
            _dropdown.Close();
        }
    }

    private async Task SetSelectedHorse(DateTimeOffset? date)
    {
        _selectedDate = date;

        if (date != null && !IsCurrentMonth(date))
        {
            _currentDate = (DateTimeOffset)date;
        }

        _value = ConvertToTValue(_selectedDate);

        await SelectedDateChanged.InvokeAsync(_value);

        await Updated.InvokeAsync(_value);
    }

    private async Task SetSelectedMinutes(DateTimeOffset? date)
    {
        _selectedDate = date;

        if (date != null && !IsCurrentMonth(date))
        {
            _currentDate = (DateTimeOffset)date;
        }

        _value = ConvertToTValue(_selectedDate);

        await SelectedDateChanged.InvokeAsync(_value);

        await Updated.InvokeAsync(_value);
    }

    private bool IsCurrentMonth(DateTimeOffset? date)
    {
        PersianCalendar persianCalendar = new();

        return persianCalendar.GetMonth(date.GetValueOrDefault(DateTimeOffset.Now).DateTime) ==
               persianCalendar.GetMonth(_currentDate.DateTime);
    }

    private bool IsSelectedDay(DateTimeOffset? date)
    {
        if (_selectedDate == null || date == null) { return false; }

        PersianCalendar persianCalendar = new();

        return persianCalendar.GetDayOfYear(_selectedDate.GetValueOrDefault(DateTimeOffset.Now).DateTime) ==
               persianCalendar.GetDayOfYear(date.GetValueOrDefault(DateTimeOffset.Now).DateTime);
    }

    private bool IsSelectedMount(DateTimeOffset? date)
    {
        if (_selectedDate == null || date == null) { return false; }

        PersianCalendar persianCalendar = new();

        return persianCalendar.GetMonth(_selectedDate.GetValueOrDefault(DateTimeOffset.Now).DateTime) ==
               persianCalendar.GetMonth(date.GetValueOrDefault(DateTimeOffset.Now).DateTime);
    }

    private bool IsSelectedYear(DateTimeOffset? date)
    {
        if (_selectedDate == null || date == null) { return false; }

        PersianCalendar persianCalendar = new();

        return persianCalendar.GetYear(_selectedDate.GetValueOrDefault(DateTimeOffset.Now).DateTime) ==
               persianCalendar.GetYear(date.GetValueOrDefault(DateTimeOffset.Now).DateTime);
    }

    private string YearCss(DateTimeOffset? date)
    {
        PersianCalendar persianCalendar = new();

        var cssClass = new ClassBuilder()
            .Add("datepicker-year")
            .AddIf("datepicker-day-dropdown", !Inline)
            .AddIf("datepicker-current", persianCalendar.GetYear(date.GetValueOrDefault(DateTimeOffset.Now).DateTime) == persianCalendar.GetYear(DateTimeOffset.Now.DateTime))
            .AddIf(_selectedColor.GetColorClass("bg") + " text-white", IsSelectedYear(date))
            .ToString();

        return cssClass;
    }

    private string MountCss(DateTimeOffset? date)
    {
        PersianCalendar persianCalendar = new();

        var cssClass = new ClassBuilder()
            .Add("datepicker-year")
            .AddIf("datepicker-day-dropdown", !Inline)
            .AddIf("datepicker-current", persianCalendar.GetMonth(date.GetValueOrDefault(DateTimeOffset.Now).DateTime) == persianCalendar.GetMonth(DateTimeOffset.Now.DateTime))
            .AddIf(_selectedColor.GetColorClass("bg") + " text-white", IsSelectedMount(date))
            .ToString();

        return cssClass;
    }

    private string DayCss(DateTimeOffset? date)
    {
        PersianCalendar persianCalendar = new();

        return new ClassBuilder()
            .Add("datepicker-day")
            .AddIf("datepicker-not-month", !IsCurrentMonth(date))
            .AddIf("datepicker-day-dropdown", !Inline)
            .AddIf("datepicker-current", persianCalendar.GetDayOfYear(date.GetValueOrDefault(DateTimeOffset.Now).DateTime) == persianCalendar.GetDayOfYear(DateTimeOffset.Now.DateTime))
            .AddIf(_selectedColor.GetColorClass("bg") + " text-white", IsSelectedDay(date))
            .ToString();
    }

    private void SelectDateClick()
    {
        if (!_allYear && !_allMonth)
        {
            _allYear = true;
            _allMonth = false;
        }
        else
        {
            _allYear = false;
            _allMonth = false;
        }
    }

    private void Close()
    {
        //if (!Inline && _dropdown != null)
        //{
        //    _dropdown.Close();
        //}
    }
}