using System.Collections.Generic;

public class Day
{

    // private string name;
    private List<TimeSlot> timeSlot = new List<TimeSlot>();

    public List<TimeSlot> getTimeSlot()
    {
        return this.timeSlot;
    }

    public void setTimeSlot(List<TimeSlot> timeSlot)
    {
        this.timeSlot = timeSlot;
    }

    public Day(string inputname)
    {
        //         this.setName(inputname);
        for (int i = 9; (i < 16); i++)
        {
            if ((i != 12))
            {
                TimeSlot ts = new TimeSlot();
                this.timeSlot.Add(ts);
            }

        }

        //         TimeSlot ts1=new TimeSlot(12);
        //         ts1.setSubject("BREAK");
        //          timeSlot.Add(ts1);
        //         for(int i=1; i<4; i++){
        //             TimeSlot ts=new TimeSlot(i);
        //             timeSlot.Add(ts);
        //         }
    }
}