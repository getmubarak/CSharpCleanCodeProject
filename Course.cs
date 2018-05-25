using System.Collections.Generic;

public class Course
{

    // represents each course
    private int courseID;

    private string courseName;

    private List<Subject> subjectsIncluded = new List<Subject>();

    private List<Combination> combinations = new List<Combination>();

    public List<Combination> getCombinations()
    {
        return this.combinations;
    }

    public void setCombinations(List<Combination> combinations)
    {
        this.combinations = combinations;
    }

    private List<StudentGroups> studentGroups = new List<StudentGroups>();

    public List<StudentGroups> getStudentGroups()
    {
        return this.studentGroups;
    }

    public void setStudentGroups(List<StudentGroups> studentGroups)
    {
        this.studentGroups = studentGroups;
    }

    public Course(int id, string name, List<Subject> subjects)
    {
        System.Console.WriteLine("creating new course.......");
        this.courseID = id;
        this.courseName = name;
        this.subjectsIncluded = subjects;
    }

    public void createStudentGroups()
    {
        int size = 0;
        List<Combination> combs = new List<Combination>();
        List<Subject>.Enumerator subjectIterator = subjectsIncluded.GetEnumerator();
        while (subjectIterator.MoveNext())
        {
            Subject subject = subjectIterator.Current;
            List<Combination>.Enumerator combIterator = combinations.GetEnumerator();
            while (combIterator.MoveNext())
            {
                Combination combination = (Combination)combIterator.Current;
                List<string> subjects = combination.getSubjects();
                List<string>.Enumerator subjectItr = subjects.GetEnumerator();
                while (subjectItr.MoveNext())
                {
                    if (subjectItr.Current.Equals(subject.getSubjectName(),System.StringComparison.OrdinalIgnoreCase))
                    {
                        size = size + combination.getSizeOfClass();
                        if (!combs.Contains(combination))//.getSubjects()))
                        {
                            combs.Add(combination);
                        }
                    }
                }
            }
            StudentGroups studentGroup = new StudentGroups(this.courseName + "/" + subject.getSubjectName(), subject.getNumberOfLecturesPerWeek(), size, combs, subject.getSubjectName(), subject.isIslab(), subject.getDepartment());
            studentGroups.Add(studentGroup);
            size = 0;
        }

    }

    // creates all possible professor x subject he teaches combinations and saves as lecture objects
    public void createCombination(string subjects, int size)
    {
        Combination combination = new Combination(subjects, size);
        this.combinations.Add(combination);
    }

    public int getCourseID()
    {
        return this.courseID;
    }

    public void setCourseID(int courseID)
    {
        this.courseID = courseID;
    }

    public string getCourseName()
    {
        return this.courseName;
    }

    public void setCourseName(string courseName)
    {
        this.courseName = courseName;
    }

    //     public List<Professor> getProfessorsTeaching() {
    //         return professorsTeaching;
    //     }
    //     public void setProfessorsTeaching(List<Professor> professorsTeaching) {
    //         this.professorsTeaching = professorsTeaching;
    //     }
    public List<Subject> getSubjectsTaught()
    {
        return this.subjectsIncluded;
    }

    public void setSubjectsTaught(List<Subject> subjectsTaught)
    {
        this.subjectsIncluded = subjectsTaught;
    }
}