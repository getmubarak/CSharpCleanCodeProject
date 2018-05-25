using System.Collections.Generic;

public class StudentGroups
{

    private string name;

    private int noOfLecturePerWeek;

    private List<Combination> combinations = new List<Combination>();

    private int size;

    private string subjectName;

    private bool isPractical;

    private string department;

    public StudentGroups(string str, int numberOfLectures, int i, List<Combination> combs, string subject, bool lab, string dept) 
    {
        //  TODO Auto-generated constructor stub
        this.setName(str);
        this.setNoOfLecturePerWeek(numberOfLectures);
        this.setCombination(combs);
        this.setSize(i);
        this.subjectName = subject;
        this.isPractical = lab;
        this.setDepartment(dept);
    }

    public int getSize()
    {
        return this.size;
    }

    public void setSize(int size)
    {
        this.size = size;
    }

    public List<Combination> getCombination()
    {
        return this.combinations;
    }

    public void setCombination(List<Combination> combination)
    {
        this.combinations = combination;
    }

    public int getNoOfLecturePerWeek()
    {
        return this.noOfLecturePerWeek;
    }

    public void setNoOfLecturePerWeek(int noOfLecturePerWeek)
    {
        this.noOfLecturePerWeek = noOfLecturePerWeek;
    }

    public string getName()
    {
        return this.name;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public string getSubjectName()
    {
        return this.subjectName;
    }

    public void setSubjectName(string subjectName)
    {
        this.subjectName = subjectName;
    }

    public bool IsPractical()
    {
        return this.isPractical;
    }

    public void setPractical(bool isPractical)
    {
        this.isPractical = isPractical;
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