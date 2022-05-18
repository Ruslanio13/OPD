[System.Serializable]
public class Calendar
{
    public int Day {get; private set;}
    public int Month { get; private set; }
    public int Year { get; private set; }
    public int AllDays { get; private set; }

    public Calendar(int day, int month, int year)
    {
        Day = day;
        Month = month;
        Year = year;
        AllDays = 0;
    }

    public Calendar()
    {
        Day = 1;
        Month = 1;
        Year = 2022;
        AllDays = 0;
    }

    public bool IsTimeToDividends() => Month == 1 && Day == 1;

    public string GetStrDate()
    {
        string date;
        date = Day.ToString("00") + "." + Month.ToString("00") + "." + Year.ToString();
        return date;
    }

    public void UpdateDate()
    {
        if ((Month == 1 || Month == 3 || Month == 5 || Month == 8 || Month == 10 || Month == 12) && Day == 31)
        {
            GoToNextMonth();
            if (Month == 13)
            {
                Year += 1;
                Month = 1;
            }
        }
        else if (((Month == 4 || Month == 6 || Month == 7 || Month == 9 || Month == 11) && Day == 30) ||
            ((Month == 2 && Day == 28 && Year % 4 != 0) || (Month == 2 && Day == 29 && Year % 4 == 0)))
            GoToNextMonth();
        else
            Day += 1;
        AllDays += 1;
    }

    private void GoToNextMonth()
    {
        Day = 1;
        Month += 1;
    }
}