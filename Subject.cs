public class Subject
{

    private int subjectID;

    private string subjectName;

    private int numberOfLecturesPerWeek;

    private bool islab;

    private string department;

    public Subject(int id, string name, int lectures, bool lab, string dept)
    {
        this.subjectID = id;
        this.subjectName = name;
        this.numberOfLecturesPerWeek = lectures;
        this.islab = lab;
        this.department = dept;
    }

    public string getSubjectName()
    {
        return this.subjectName;
    }

    public void setSubjectName(string subjectName)
    {
        this.subjectName = subjectName;
    }

    public int getNumberOfLecturesPerWeek()
    {
        return this.numberOfLecturesPerWeek;
    }

    public void setNumberOfLecturesPerWeek(int numberOfLecturesPerWeek)
    {
        this.numberOfLecturesPerWeek = numberOfLecturesPerWeek;
    }

    public int getSubjectID()
    {
        return this.subjectID;
    }

    public void setSubjectID(int subjectID)
    {
        this.subjectID = subjectID;
    }

    public bool isIslab()
    {
        return this.islab;
    }

    public void setIslab(bool islab)
    {
        this.islab = islab;
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