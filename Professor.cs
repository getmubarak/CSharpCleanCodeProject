using System.Collections.Generic;

public class Professor
{

    private int professorID;

    private string professorName;

    private List<string> subjectsTaught = new List<string>();

    public Professor(int id, string name, string subj)
    {
        this.professorID = id;
        this.professorName = name;
        string[] subjectNames = subj.Split('/');
        for (int i = 0; (i < subjectNames.Length); i++)
        {
            this.subjectsTaught.Add(subjectNames[i]);
        }

    }

    public int getProfessorID()
    {
        return this.professorID;
    }

    public void setProfessorID(int professorID)
    {
        this.professorID = professorID;
    }

    public string getProfessorName()
    {
        return this.professorName;
    }

    public void setProfessorName(string professorName)
    {
        this.professorName = professorName;
    }

    public List<string> getSubjectTaught()
    {
        return this.subjectsTaught;
    }

    public void setSubjectTaught(List<string> subjectTaught)
    {
        this.subjectsTaught = subjectTaught;
    }
}