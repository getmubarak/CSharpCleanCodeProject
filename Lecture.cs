public class Lecture
{

    private Professor professor;

    private string subject;

    private StudentGroups studentGroup;

    // represents one lecture having professor and subject
    public Lecture(Professor prof, string sub)
    {
        this.professor = prof;
        this.subject = sub;
    }

    public Professor getProfessor()
    {
        return this.professor;
    }

    public void setProfessor(Professor professor)
    {
        this.professor = professor;
    }

    public string getSubject()
    {
        return this.subject;
    }

    public void setSubject(string subject)
    {
        this.subject = subject;
    }

    public StudentGroups getStudentGroup()
    {
        return this.studentGroup;
    }

    public void setStudentGroup(StudentGroups studentGroup)
    {
        this.studentGroup = studentGroup;
    }
}