using System;

public class ClassRoom
{

    // represents each classroom
    private string roomID;

    private int size;

    private bool islaboratory;

    private Week week;

    private string department;

    public ClassRoom(string id, int i, bool islab, string dept)
    {
        this.roomID = id;
        this.setWeek(new Week());
        this.setSize(i);
        this.islaboratory = islab;
        this.department = dept;
    }

    public bool isLaboratory()
    {
        return this.islaboratory;
    }

    public void setLaboratory(bool laboratory)
    {
        this.islaboratory = laboratory;
    }

    public string getRoomNo()
    {
        return this.roomID;
    }

    public void setRoomNo(string roomNo)
    {
        this.roomID = roomNo;
    }

    public Week getWeek()
    {
        return this.week;
    }

    public void setWeek(Week week)
    {
        this.week = week;
    }

    public int getSize()
    {
        return this.size;
    }

    public void setSize(int size)
    {
        this.size = size;
    }

    public string getDepartment()
    {
        return this.department;
    }

    public void setDepartment(string department)
    {
        this.department = department;
    }
}