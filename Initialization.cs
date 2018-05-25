using System.Collections.Generic;

public class Initialization
{

    // this class takes all inputs from a file. courseID, courseName, roomID's, subjects and professors associated with course
    // currently hardcoded by taking one course with 6 subjects and 6 teachers
    private List<Subject> subjects = new List<Subject>();

    private List<Professor> professors = new List<Professor>();

    private List<TimeTable> timetables = new List<TimeTable>();

    private List<Lecture> classes = new List<Lecture>();

    private List<Combination> combinations = new List<Combination>();

    // reads input from a file.
    public void readInput()
    {
        List<ClassRoom> classroom = new List<ClassRoom>();
        ClassRoom room1 = new ClassRoom("D101", 20, false, "Common");
        classroom.Add(room1);
        ClassRoom room2 = new ClassRoom("E101", 20, false, "ComputerScience");
        classroom.Add(room2);
        ClassRoom room3 = new ClassRoom("LAB1", 20, true, "ComputerScience");
        classroom.Add(room3);
        //         ClassRoom room4 = new ClassRoom("LAB2", 20, true);
        //         classroom.Add(room4);
        //         ClassRoom room5 = new ClassRoom("G101", 20, false);
        //         classroom.Add(room5);
        //         ClassRoom room6 = new ClassRoom("H101", 20, false);
        //         classroom.Add(room6);
        //         ClassRoom room6 = new ClassRoom("I101", 60, false);
        //         classroom.Add(room6);
        this.professors.Add(new Professor(1, "Shruti", "IR/IRlab/DM"));
        this.professors.Add(new Professor(2, "Snehal", "P&S"));
        this.professors.Add(new Professor(3, "Ramrao", "DS"));
        this.professors.Add(new Professor(4, "Ranjit", "WR"));
        this.professors.Add(new Professor(5, "Shekhar", "TOC"));
        this.professors.Add(new Professor(6, "Monica", "SS"));
        this.professors.Add(new Professor(7, "Ravi", "R"));
        this.professors.Add(new Professor(8, "Amit", "ML/MLlab"));
        this.professors.Add(new Professor(9, "Rama", "DAA/UML"));
        this.createLectures(this.professors);
        TimeTable timetb1 = new TimeTable(classroom, this.classes);
        // , professors);
        // timetb1.initialization(classroom, classes);
        // TimeTable timetb2=new TimeTable(classroom, classes);
        // TimeTable timetb3=new TimeTable(classroom, classes);
        int courseid = 1;
        string courseName = "MSc.I.T. Part I";
        System.Console.WriteLine("reading input.......");
        this.subjects.Add(new Subject(1, "IR", 4, false, "ComputerScience"));
        this.subjects.Add(new Subject(2, "P&S", 4, false, "ComputerScience"));
        this.subjects.Add(new Subject(3, "DS", 4, false, "ComputerScience"));
        this.subjects.Add(new Subject(4, "WR", 1, false, "Common"));
        this.subjects.Add(new Subject(5, "TOC", 4, false, "ComputerScience"));
        this.subjects.Add(new Subject(6, "IRlab", 3, true, "ComputerScience"));
        this.subjects.Add(new Subject(7, "JAVA", 3, true, "ComputerScience"));
        System.Console.WriteLine("new course creation.......");
        Course course1 = new Course(courseid, courseName, this.subjects);
        course1.createCombination("IR/P&S/DS/WR/TOC/IRlab/JAVA/", 20);
        course1.createStudentGroups();
        List<StudentGroups> studentGroups = course1.getStudentGroups();
        timetb1.addStudentGroups(studentGroups);
        // combinations.AddAll(course1.getCombinations());
        // timetb2.AddStudentGroups(studentGroups);
        /// timetb3.AddStudentGroups(studentGroups);
        this.subjects.Clear();
        this.subjects.Add(new Subject(8, "DM", 4, false, "ComputerScience"));
        this.subjects.Add(new Subject(9, "DAA", 4, false, "ComputerScience"));
        this.subjects.Add(new Subject(10, "SS", 1, false, "ComputerScience"));
        this.subjects.Add(new Subject(11, "ML", 4, false, "Common"));
        this.subjects.Add(new Subject(12, "UML", 4, false, "ComputerScience"));
        this.subjects.Add(new Subject(13, "MLlab", 3, true, "ComputerScience"));
        this.subjects.Add(new Subject(14, "R", 3, true, "ComputerScience"));
        Course course2 = new Course(2, "MSc.I.T. Part II", this.subjects);
        course2.createCombination("DM/DAA/SS/ML/UML/MLlab/R/", 20);
        course2.createStudentGroups();
        studentGroups = course2.getStudentGroups();
        timetb1.addStudentGroups(studentGroups);
        // combinations.AddAll(course2.getCombinations());
        // timetb2.AddStudentGroups(studentGroups);
        // timetb3.AddStudentGroups(studentGroups);
        System.Console.WriteLine("Setting tt.......");
        System.Console.WriteLine("Adding tt.......");
        timetb1.initializeTimeTable();
        // timetb2.initializeTimeTable();
        // timetb3.initializeTimeTable();
        this.timetables.Add(timetb1);
        // timetable.Add(timetb2);
        // timetable.Add(timetb3);
        System.Console.WriteLine("populating.......");
        // display();
        this.populateTimeTable(timetb1);
        GeneticAlgorithm ge = new GeneticAlgorithm();
        // ge.fitness(timetb1);
        //         timetb1.createTimeTableGroups(combinations);
        ge.populationAccepter(timetables);
        //         //ge.fitness(timetb2);
        // ge.fitness(timetb3);
        // populateTimeTable();
    }

    public void populateTimeTable(TimeTable timetb1)
    {
        int i = 0;
        System.Console.WriteLine("populating started.......");
        while ((i < 3))
        {
            TimeTable tempTimetable = timetb1;
            List<ClassRoom> allrooms = tempTimetable.getRoom();
            var allroomsIterator = allrooms.GetEnumerator();
            while (allroomsIterator.MoveNext())
            {
                ClassRoom room = allroomsIterator.Current;
                List<Day> weekdays = room.getWeek().getWeekDays();
                weekdays.Shuffle();
                if (!room.isLaboratory())
                {
                    var daysIterator = weekdays.GetEnumerator();
                    while (daysIterator.MoveNext())
                    {
                        Day day = daysIterator.Current;
                        day.getTimeSlot().Shuffle();
                    }

                }

            }

            this.timetables.Add(tempTimetable);
            i++;
        }

        System.Console.WriteLine("populating done.......");
        System.Console.WriteLine("display called.......");
        this.display();
    }

    private void createLectures(List<Professor> professors)
    {
        //  TODO Auto-generated method stub
       var professorIterator = this.professors.GetEnumerator();
        while (professorIterator.MoveNext())
        {
            Professor professor = professorIterator.Current;
            List<string> subjectsTaught = professor.getSubjectTaught();
            var subjectIterator = subjectsTaught.GetEnumerator();
            while (subjectIterator.MoveNext())
            {
                string subject = subjectIterator.Current;
                this.classes.Add(new Lecture(professor, subject));
            }

        }

    }

    // creates another 3 timetable objects for population by taking first yimetable and shuffling it.
    //     public void populateTimeTable(){
    //         int i=0;
    //         System.Console.WriteLine("populating started.......");
    //         while(i<6){
    //             TimeTable tempTimetable = timetbl;
    //             List<ClassRoom> allrooms = tempTimetable.getRoom();
    //             Iterator<ClassRoom> allroomsIterator = allrooms.iterator();
    //             while(allroomsIterator.hasNext()){
    //                 ClassRoom room = allroomsIterator.next();
    //                 List<Day> weekdays = room.getWeek().getWeekDays();
    //                 Iterator<Day> daysIterator=weekdays.iterator();
    //                 while(daysIterator.hasNext()){
    //                     Day day = daysIterator.next();
    //                     Collections.shuffle(day.getTimeSlot());
    //                 }
    //             }
    //             timetable.Add(tempTimetable);
    //             i++;
    //         }
    //         System.Console.WriteLine("populating done.......");
    //         System.Console.WriteLine("display called.......");
    //         display();
    //         
    //         GeneticAlgorithm.populationAccepter(timetable);
    //     }
    // displays all timetables
    private void display()
    {
        //  TODO Auto-generated method stub
        int i = 1;
        System.Console.WriteLine("displaying all tt\'s.......");
        var timetableIterator = this.timetables.GetEnumerator();
        while (timetableIterator.MoveNext())
        {
            System.Console.WriteLine(("+++++++++++++++++++++++++++++++++++++++++\nTime Table no. " + i));
            TimeTable currentTimetable = timetableIterator.Current;
            System.Console.WriteLine(("Score : " + currentTimetable.getFittness()));
            List<ClassRoom> allrooms = currentTimetable.getRoom();
            var allroomsIterator = allrooms.GetEnumerator();
            while (allroomsIterator.MoveNext())
            {
                ClassRoom room = allroomsIterator.Current;
                System.Console.WriteLine(("Room: " + room.getRoomNo()));
                List<Day> weekdays = room.getWeek().getWeekDays();
                var daysIterator = weekdays.GetEnumerator();
                while (daysIterator.MoveNext())
                {
                    Day day = daysIterator.Current;
                    List<TimeSlot> timeslots = day.getTimeSlot();
                    var timeslotIterator = timeslots.GetEnumerator();
                    //System.Console.Write(""+day.getName()+": ");
                    while (timeslotIterator.MoveNext())
                    {
                        TimeSlot lecture = ((TimeSlot)(timeslotIterator.Current));
                        if ((lecture.getLecture() != null))
                        {
                            //System.Console.Write(" (Subject: "+lecture.getLecture().getSubject()+" --> Professor: "+lecture.getLecture().getProfessor().getProfessorName()+" GrpName: "+lecture.getLecture().getStudentGroup().getName()+")");
                           System.Console.Write(("("
                                            + (lecture.getLecture().getSubject() + ("#"
                                            + (lecture.getLecture().getProfessor().getProfessorName() + ("#"
                                            + (lecture.getLecture().getStudentGroup().getName().Split('/')[0] + ")")))))));
                        }
                        else
                        {
                           System.Console.Write("   free   ");
                        }

                    }

                   System.Console.Write("\n");
                }

               System.Console.Write("\n\n");
            }

            i++;
        }

    }
}