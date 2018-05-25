using System;
using System.Collections.Generic;

public class TimeTable
{

    private List<ClassRoom> rooms = new List<ClassRoom>();

    private int fittness;

    private List<Lecture> classes = new List<Lecture>();

    private List<StudentGroups> studentGroups = new List<StudentGroups>();

    private List<ClassRoom> practicalRooms = new List<ClassRoom>();

    private List<ClassRoom> theoryRooms = new List<ClassRoom>();

    private List<StudentGroups> theoryStudentGroups = new List<StudentGroups>();

    private List<StudentGroups> practicalStudentGroups = new List<StudentGroups>();

    private Dictionary<Combination, Week> personalTimeTable = new Dictionary<Combination, Week>();

    // private List<Professor> professors=new List<>();
    // adds more rooms to timetable
    public TimeTable(List<ClassRoom> classroom, List<Lecture> lectures)
    {
        // , List<Professor> professors){
        this.rooms = classroom;
        this.classes = lectures;
        this.fittness = 999;
        //         this.professors=professors;
    }

    //     public void initialization(List<ClassRoom> classroom, List<Lecture> lectures){
    //         this.rooms=classroom;
    //         this.classes=lectures;
    //         this.fittness=999;
    //     }
    public int getFittness()
    {
        return this.fittness;
    }

    public void setFittness(int fittness)
    {
        this.fittness = fittness;
    }

    public void addStudentGroups(List<StudentGroups> studentgrps)
    {
        //  TODO Auto-generated method stub
        this.studentGroups.AddRange(studentgrps);
    }

    public void initializeTimeTable()
    {
        for (var roomsIterator = this.rooms.GetEnumerator(); roomsIterator.MoveNext();
        )
        {
            ClassRoom room = roomsIterator.Current;
            if (room.isLaboratory())
            {
                this.practicalRooms.Add(room);
            }
            else
            {
                this.theoryRooms.Add(room);
            }

        }

        for (var studentGroupIterator = this.studentGroups.GetEnumerator(); studentGroupIterator.MoveNext();
        )
        {
            StudentGroups studentGroup = studentGroupIterator.Current;
            if (studentGroup.IsPractical())
            {
                this.practicalStudentGroups.Add(studentGroup);
            }
            else
            {
                this.theoryStudentGroups.Add(studentGroup);
            }

        }

        this.rooms.Clear();
        // studentGroups.clear();
        setTimeTable(this.practicalStudentGroups, this.practicalRooms, "practical");
        setTimeTable(this.theoryStudentGroups, this.theoryRooms, "theory");
        this.rooms.AddRange(this.practicalRooms);
        this.rooms.AddRange(this.theoryRooms);
        // studentGroups.addAll(practicalStudentGroups);
        // studentGroups.addAll(theoryStudentGroups);
    }

    public void setTimeTable(List<StudentGroups> studentGroups2, List<ClassRoom> rooms2, string str)
    {
        //  TODO Auto-generated method stub
        studentGroups2.Shuffle();
        Stack<Lecture> lecturesStack = new Stack<Lecture>();
        for (var sdtGrpIterator = studentGroups2.GetEnumerator(); sdtGrpIterator.MoveNext();
        )
        {
            StudentGroups studentGrp = sdtGrpIterator.Current;
            string subject = studentGrp.getSubjectName();
            int noOfLectures = studentGrp.getNoOfLecturePerWeek();
            for (int i = 0; (i < noOfLectures); i++)
            {
                classes.Shuffle();
                var classIterator = this.classes.GetEnumerator();
                while (classIterator.MoveNext())
                {
                    Lecture lecture = classIterator.Current;
                    if (lecture.getSubject().Equals(subject,System.StringComparison.InvariantCultureIgnoreCase))
                    {
                        Lecture mainLecture = new Lecture(lecture.getProfessor(), lecture.getSubject());
                        mainLecture.setStudentGroup(studentGrp);
                        lecturesStack.Push(mainLecture);
                        break;
                    }

                }

            }

        }

        while (lecturesStack.Count > 0)
        {
            lecturesStack.Shuffle();
            Lecture lecture2 = lecturesStack.Pop();
            if (str.Equals("theory",StringComparison.InvariantCultureIgnoreCase))
            {
                this.placeTheoryLecture(lecture2, rooms2);
            }

            if (str.Equals("practical",StringComparison.InvariantCultureIgnoreCase))
            {
                this.placePracticalLecture(lecture2, rooms2);
            }

        }

    }

    private void placePracticalLecture(Lecture lecture2, List<ClassRoom> rooms2)
    {
        //  TODO Auto-generated method stub
        int size = lecture2.getStudentGroup().getSize();
        String dept = lecture2.getStudentGroup().getDepartment();
        int i = 0;
        bool invalid = true;
        ClassRoom room = null;
        rooms2.Shuffle();
        while (invalid)
        {
            room = this.getBestRoom(size, rooms2);
            if (room.getDepartment().Equals(dept,StringComparison.InvariantCultureIgnoreCase))
            {
                invalid = false;
                rooms2.Shuffle();
            }
            else
            {
                rooms2.Shuffle();
            }

        }

        List<Day> weekdays = room.getWeek().getWeekDays();
        var daysIterator = weekdays.GetEnumerator();
        while ((daysIterator.MoveNext()
                    && (i < 3)))
        {
            Day day = daysIterator.Current;
            List<TimeSlot> timeslots = day.getTimeSlot();
            var timeslotIterator = timeslots.GetEnumerator();
            while ((timeslotIterator.MoveNext()
                        && (i < 3)))
            {
                TimeSlot lecture3 = timeslotIterator.Current;
                if ((lecture3.getLecture() == null))
                {
                    lecture3.setLecture(lecture2);
                    i++;
                }

            }

        }

    }

    private void placeTheoryLecture(Lecture lecture, List<ClassRoom> rooms2)
    {
        //  TODO Auto-generated method stub
        int size = lecture.getStudentGroup().getSize();
        String dept = lecture.getStudentGroup().getDepartment();
        bool invalid = true;
        ClassRoom room = null;
        rooms2.Shuffle();
        while (invalid)
        {
            room = this.getBestRoom(size, rooms2);
            if (room.getDepartment().Equals(dept,StringComparison.InvariantCultureIgnoreCase))
            {
                invalid = false;
                rooms2.Shuffle();
            }
            else
            {
                rooms2.Shuffle();
            }

        }

        List<Day> weekdays = room.getWeek().getWeekDays();
        var daysIterator = weekdays.GetEnumerator();
        while (daysIterator.MoveNext())
        {
            Day day = daysIterator.Current;
            List<TimeSlot> timeslots = day.getTimeSlot();
            var timeslotIterator = timeslots.GetEnumerator();
            while (timeslotIterator.MoveNext())
            {
                TimeSlot lecture2 = timeslotIterator.Current;
                if ((lecture2.getLecture() == null))
                {
                    lecture2.setLecture(lecture);
                    return;
                }

            }

        }

    }

    private bool checkOccupiedRoom(ClassRoom tempRoom, List<ClassRoom> rooms2)
    {
        //  TODO Auto-generated method stub
        for (var roomsIterator = rooms2.GetEnumerator(); roomsIterator.MoveNext();
        )
        {
            ClassRoom room = roomsIterator.Current;
            if (room.Equals(tempRoom))
            {
                List<Day> weekdays = room.getWeek().getWeekDays();
                var daysIterator = weekdays.GetEnumerator();
                while (daysIterator.MoveNext())
                {
                    Day day = daysIterator.Current;
                    List<TimeSlot> timeslots = day.getTimeSlot();
                    var timeslotIterator = timeslots.GetEnumerator();
                    while (timeslotIterator.MoveNext())
                    {
                        TimeSlot lecture = timeslotIterator.Current;
                        if ((lecture.getLecture() == null))
                        {
                            return false;
                        }

                    }

                }

                return true;
            }

        }

        return false;
    }

    private ClassRoom getBestRoom(int size, List<ClassRoom> rooms2)
    {
        //  TODO Auto-generated method stub
        int delta = 1000;
        ClassRoom room = null;
        for (var roomsIterator = rooms2.GetEnumerator(); roomsIterator.MoveNext();
        )
        {
            ClassRoom tempRoom = roomsIterator.Current;
            if (!this.checkOccupiedRoom(tempRoom, rooms2))
            {
                int tmp = Math.Abs((size - tempRoom.getSize()));
                if ((tmp < delta))
                {
                    delta = tmp;
                    room = tempRoom;
                }

            }

        }

        return room;
    }

    //     public void createTimeTableGroups(List<Combination> combinations2){
    /// /        List<Combination> combinations=new List<>();
    /// /        
    /// /            for(Iterator<Combination> combItr = combinations2.iterator(); combItr.hasNext();){
    /// /                 Combination comb = combItr.next();
    /// /                if(!combinations.contains(comb)){
    /// /                    combinations.add(comb);
    /// /                }
    /// /            }
    //         
    //         
    /// /        for(Iterator<Combination> combIterator = combinations2.iterator(); combIterator.hasNext();){
    /// /            Combination combtn = combIterator.next();
    /// /            personalTimeTable.put(combtn, new Week());
    /// /        }
    //         
    //         for(Iterator<Combination> combIterator = combinations2.iterator(); combIterator.hasNext();){
    //             Combination combtn = combIterator.next();
    //             for (Iterator<ClassRoom> roomsIterator = theoryRooms.iterator(); roomsIterator.hasNext();){
    //                 ClassRoom room=roomsIterator.next();
    //                 Iterator<Day> daysIterator = room.getWeek().getWeekDays().iterator();
    //                 while(daysIterator.hasNext()){
    //                     Day day = daysIterator.next();
    //                     List<TimeSlot> timeslots = day.getTimeSlot();
    //                     Iterator<TimeSlot> timeslotIterator= timeslots.iterator();
    //                     while(timeslotIterator.hasNext()){
    //                         TimeSlot lecture = timeslotIterator.next();
    //                         if(lecture.getLecture()==null){
    //                             System.out.print(" free ");
    //                         }
    //                         else if(lecture.getLecture().getStudentGroup().getCombination().contains(combtn)){
    //                             System.out.print("###Room="+room.getRoomNo()/*+" Day="+day.getName()+" Time="+lecture.getSlotTime()*/+" Professor="+lecture.getLecture().getProfessor()+" Subject="+lecture.getLecture().getSubject());
    //                         }
    //                         else{
    //                             System.out.print(" free ");
    //                         }
    //                     }
    //                     System.out.print("\n");
    //                 }
    //             }
    //         }
    //     }
    //     private void putInPersonalTimeTable(Combination combtn, String roomNo, String name, TimeSlot lecture) {
    //         // TODO Auto-generated method stub
    //         Week week = personalTimeTable.get(combtn);
    //         Iterator<Day> daysIterator=week.getWeekDays().iterator();
    //         while(daysIterator.hasNext()){
    //             Day day = daysIterator.next();
    //             if(day.getName().equalsIgnoreCase(name)){
    //                 Iterator<TimeSlot> timeslotIterator=day.getTimeSlot().iterator();
    //                 while(timeslotIterator.hasNext()){
    //                     TimeSlot lecture2 = (TimeSlot) timeslotIterator.next();
    //                     if(lecture2)
    //                 }
    //             }
    //         }
    //     }
    // creates random assignment of lecture using lecture objects, subjects and number of lectures per week to a room
    //     private ClassRoom randomTimetable(ClassRoom room, List<Subject> subjectsTaught, List<Lecture> lectureList) {
    //         Iterator subIterator=subjectsTaught.iterator();
    //         Stack<Lecture> lecturesStack=new Stack();
    //         while(subIterator.hasNext()){
    //             Subject subject = (Subject) subIterator.next();
    //             int noOfLecturesPerWeek = subject.getNumberOfLecturesPerWeek();
    //             for(int i=0; i<noOfLecturesPerWeek; i++){
    //                 Collections.shuffle(lectureList);
    //                 Iterator<Lecture> classIterator = lectureList.iterator();
    //                 while(classIterator.hasNext()){
    //                     Lecture getLecture = classIterator.next();
    //                     if(getLecture.getSubject().equalsIgnoreCase(subject.getSubjectName())){
    //                         lecturesStack.push(getLecture);
    //                         break;
    //                     }
    //                 }
    //             }
    //         }
    //         
    //         Collections.shuffle(lecturesStack);
    //         List<Day> weekdays = room.getWeek().getWeekDays();
    //         Iterator<Day> daysIterator=weekdays.iterator();
    //         while(daysIterator.hasNext()){
    //             Day day = daysIterator.next();
    //             List<TimeSlot> timeslots = day.getTimeSlot();
    //             Iterator timeslotIterator= timeslots.iterator();
    //             while(timeslotIterator.hasNext() && !(lecturesStack.isEmpty())){
    //                 TimeSlot lecture = (TimeSlot) timeslotIterator.next();
    //                 lecture.setLecture(lecturesStack.pop());
    //                 Collections.shuffle(lecturesStack);
    //             }
    //         }        
    //         return room;
    //     }
    public List<ClassRoom> getRoom()
    {
        return this.rooms;
    }

    public void setRoom(List<ClassRoom> room)
    {
        this.rooms = room;
    }

    public List<ClassRoom> getPracticalRooms()
    {
        return this.practicalRooms;
    }

    public void setPracticalRooms(List<ClassRoom> practicalRooms)
    {
        this.practicalRooms = practicalRooms;
    }

    public List<ClassRoom> getTheoryRooms()
    {
        return this.theoryRooms;
    }

    public void setTheoryRooms(List<ClassRoom> theoryRooms)
    {
        this.theoryRooms = theoryRooms;
    }

    public List<StudentGroups> getTheoryStudentGroups()
    {
        return this.theoryStudentGroups;
    }

    public void setTheoryStudentGroups(List<StudentGroups> theoryStudentGroups)
    {
        this.theoryStudentGroups = theoryStudentGroups;
    }

    public List<StudentGroups> getPracticalStudentGroups()
    {
        return this.practicalStudentGroups;
    }

    public void setPracticalStudentGroups(List<StudentGroups> practicalStudentGroups)
    {
        this.practicalStudentGroups = practicalStudentGroups;
    }
}