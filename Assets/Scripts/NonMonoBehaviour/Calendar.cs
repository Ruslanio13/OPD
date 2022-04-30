[System.Serializable]
public class Calendar
{
    public int Day{get; private set;}
    private int _month;
    private int _year;
    public int AllDays { get; private set; }

    public Calendar(int day, int month, int year)
    {
        Day = day;
        _month = month;
        _year = year;
        AllDays = 0;
    }

    public Calendar()
    {
        Day = 1;
        _month = 1;
        _year = 2022;
        AllDays = 0;
    }

    public bool IsTimeToDividends() => _month == 1 && Day == 10;

    public string GetStrDate()
    {
        string date;
        date = Day.ToString("00") + "." + _month.ToString("00") + "." + _year.ToString();
        return date;
    }

    public void UpdateDate()
    {
        if ((_month == 1 || _month == 3 || _month == 5 || _month == 8 || _month == 10 || _month == 12) && Day == 31)
        {
            GoToNextMonth();
            if (_month == 13)
            {
                _year += 1;
                _month = 1;
            }
        }
        else if (((_month == 4 || _month == 6 || _month == 7 || _month == 9 || _month == 11) && Day == 30) ||
            ((_month == 2 && Day == 28 && _year % 4 != 0) || (_month == 2 && Day == 29 && _year % 4 == 0)))
            GoToNextMonth();
        else
            Day += 1;
        AllDays += 1;
    }

    private void GoToNextMonth()
    {
        Day = 1;
        _month += 1;
    }
}