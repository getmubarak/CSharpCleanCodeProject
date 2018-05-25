
using System.Collections.Generic;

public class Combination
{

    private int sizeOfClass;

    private List<string> subjectCombination = new List<string>();

    public Combination(string subjects, int size)
    {
        //  TODO Auto-generated constructor stub
        this.setSizeOfClass(size);
        string[] subj = subjects.Split('/');
        for (int i = 0; (i < subj.Length); i++)
        {
            this.subjectCombination.Add(subj[i]);
        }

    }

    public int getSizeOfClass()
    {
        return this.sizeOfClass;
    }

    public void setSizeOfClass(int sizeOfClass)
    {
        this.sizeOfClass = sizeOfClass;
    }

    public List<string> getSubjects()
    {
        return this.subjectCombination;
    }

    public void setSubjects(List<string> subjects)
    {
        this.subjectCombination = subjects;
    }
}