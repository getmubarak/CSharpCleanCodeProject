
using System;
using System.Collections.Generic;
using System.Globalization;

public class Week
{
    private string[] weekDaysName;
    private List<Day> weekDays = new List<Day>();

    public List<Day> getWeekDays()
    {
        return weekDays;
    }

    public void setWeekDays(List<Day> weekDays)
    {
        this.weekDays = weekDays;
    }

    public Week()
    {
        this.weekDaysName = DateTimeFormatInfo.CurrentInfo.DayNames; //new DateFormatSymbols().getWeekdays();
        for (int i = 1; i < weekDaysName.Length; i++)
        {
            //System.out.println("weekday = " + weekDaysName[i]);
            if (!(weekDaysName[i].Equals("Sunday", StringComparison.InvariantCultureIgnoreCase)))
            {
                Day newday = new Day(weekDaysName[i]);
                weekDays.Add(newday);
            }
        }
    }
}
