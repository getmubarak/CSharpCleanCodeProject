
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class GeneticAlgorithm
{

    // private int numberOfRooms;
    // private List <string> subjects=new List();
    // private List <ClassRoom> rooms=new List();
    // static HashMap<Integer,TimeTable> ttscore=new HashMap();
    private static TimeTable GlobalBestTimetable;

    private static int min = 1000;

    private static List<string> weekDayNames = new List<string>();

    private static List<string> lectureTimings = new List<string>();

    public  void populationAccepter(List<TimeTable> timeTableCollection)
    {
        //  randomly got population from the initialization class
        List<TimeTable>.Enumerator timetableIterator = timeTableCollection.GetEnumerator();
        for (var iterator = timeTableCollection.GetEnumerator(); iterator.MoveNext();)
        {
            TimeTable tt = iterator.Current;
            GeneticAlgorithm.fitness(tt);
        }

        GeneticAlgorithm.createWeek();
        GeneticAlgorithm.createLectureTime();
        GeneticAlgorithm.selection(timeTableCollection);
    }

    private static void createWeek()
    {
        string[] weekDaysName = DateTimeFormatInfo.CurrentInfo.DayNames;// (new DateFormatSymbols() + getWeekdays());
        for (int i = 0; (i < weekDaysName.Length); i++)
        {
            System.Console.WriteLine(("weekday = " + weekDaysName[i]));
            // if(!(weekDaysName[i].equalsIgnoreCase("Sunday"))){
            if (!(weekDaysName[ i].Equals("Sunday")))
            {
                weekDayNames.Add(weekDaysName[i]);
            }

        }

    }

    private static void createLectureTime()
    {
        for (int i = 9; (i < 16); i++)
        {
            // if(i!=12){
            lectureTimings.Add((i + (":00" + (" TO "
                            + ((i + 1)
                            + ":00")))));
            // }            
        }

    }

    public static void selection(List<TimeTable> timetables)
    {
        int iterations = 50;
        int i = 1;
        List<TimeTable> mutants = new List<TimeTable>();
        var ttItr = timetables.GetEnumerator();
        while (ttItr.MoveNext())
        {
            GeneticAlgorithm.fitness(ttItr.Current);
        }

        while ((iterations != 0))
        {
            // Iterator<Integer> scoreIterator=ttscore.keySet().iterator();
            var timetableIterator = timetables.GetEnumerator();
            // Iterator<TimeTable> tempIterator=timetableIterator;        
            // min= timetableIterator.next().getFittness();
            while (timetableIterator.MoveNext())
            {
                TimeTable tt = timetableIterator.Current;
                int score = tt.getFittness();
                if ((score < min))
                {
                    min = score;
                    GlobalBestTimetable = tt;
                    GeneticAlgorithm.display();
                    GeneticAlgorithm.writeToExcelFile();
                }

            }

            if ((min == 0))
            {
                // List<TimeTable> timeTable=new List();
                // timeTable.Add(GlobalBestTimetable);
                GeneticAlgorithm.display();
                //System.exit(0);
                Environment.Exit(0);
            }
            else
            {
                System.Console.WriteLine(("Iteration :" + i));
                i++;
                iterations--;
                // timetables.remove(GlobalBestTimetable);            
                for (var iterator = timetables.GetEnumerator(); iterator.MoveNext();
                )
                {
                    TimeTable timetable1 = iterator.Current;
                    // TimeTable timetable2 = (TimeTable) iterator.next();                
                    //                 SingleTimeTable timetable1=ttscore.get(key1);
                    //                 SingleTimeTable timetable2=ttscore.get(key2);                
                    TimeTable childTimetable = GeneticAlgorithm.crossOver(timetable1);
                    //                 if(childTimetable.getFittness()< GlobalBestTimetable.getFittness()){
                    //                     GlobalBestTimetable=childTimetable;
                    //                 }    
                    //                 for (int j = 0; j < arr.length; j++) {
                    //                     TimeTable singleTimeTable = arr[j];                    
                    TimeTable mutant = GeneticAlgorithm.Mutation(childTimetable);
                    //                     if(childTimetable.getFittness()< GlobalBestTimetable.getFittness()){
                    //                         GlobalBestTimetable=childTimetable;
                    //                     }
                    mutants.Add(mutant);
                    // }        
                }

                timetables.Clear();
                for (int j = 0; (j < mutants.Count); j++)
                {
                    GeneticAlgorithm.fitness(mutants[j]);
                    timetables.Add(mutants[j]);
                }

                mutants.Clear();
            }

        }

        GeneticAlgorithm.display();
    }

    public static void fitness(TimeTable timetable)
    {
        List<ClassRoom> rooms = timetable.getRoom();
        var roomIterator1 = rooms.GetEnumerator();
        while (roomIterator1.MoveNext())
        {
            int score = 0;
            ClassRoom room1 = roomIterator1.Current;
            var roomIterator2 = rooms.GetEnumerator();
            while (roomIterator2.MoveNext())
            {
                ClassRoom room2 = roomIterator2.Current;
                if ((room2 != room1))
                {
                    List<Day> weekdays1 = room1.getWeek().getWeekDays();
                    List<Day> weekdays2 = room2.getWeek().getWeekDays();
                    var daysIterator1 = weekdays1.GetEnumerator();
                    var daysIterator2 = weekdays2.GetEnumerator();
                    while ((daysIterator1.MoveNext() && daysIterator2.MoveNext()))
                    {
                        Day day1 = daysIterator1.Current;
                        Day day2 = daysIterator2.Current;
                        List<TimeSlot> timeslots1 = day1.getTimeSlot();
                        List<TimeSlot> timeslots2 = day2.getTimeSlot();
                        var timeslotIterator1 = timeslots1.GetEnumerator();
                        var timeslotIterator2 = timeslots2.GetEnumerator();
                        while ((timeslotIterator1.MoveNext() && timeslotIterator2.MoveNext()))
                        {
                            TimeSlot lecture1 = timeslotIterator1.Current;
                            TimeSlot lecture2 = timeslotIterator2.Current;
                            if (((lecture1.getLecture() != null)
                                        && (lecture2.getLecture() != null)))
                            {
                                //                             string subject1=lecture1.getLecture().getSubject();
                                //                             string subject2=lecture2.getLecture().getSubject();                            
                                string professorName1 = lecture1.getLecture().getProfessor().getProfessorName();
                                string professorName2 = lecture2.getLecture().getProfessor().getProfessorName();
                                string stgrp1 = lecture1.getLecture().getStudentGroup().getName();
                                string stgrp2 = lecture2.getLecture().getStudentGroup().getName();
                                if ((stgrp1.Equals(stgrp2) || professorName1.Equals(professorName2)))
                                {
                                    score = (score + 1);
                                }

                                List<Combination> stcomb1 = lecture1.getLecture().getStudentGroup().getCombination();
                               var stcombItr = stcomb1.GetEnumerator();
                                while (stcombItr.MoveNext())
                                {
                                    if (lecture2.getLecture().getStudentGroup().getCombination().Contains(stcombItr.Current))
                                    {
                                        score = (score + 1);
                                        break;
                                    }

                                }

                            }

                        }

                    }

                }

            }

            timetable.setFittness(score);
            // ttscore.put(score,timetable);
            // System.Console.WriteLine("\nScore : "+score);
        }

        System.Console.WriteLine(("Score..................................." + timetable.getFittness()));
        //         Iterator iterator = ttscore.keySet().iterator(); 
        //         while (iterator.hasNext()) {  
        //                ClassRoom key = (ClassRoom) iterator.next();  
        //                int value = (int) ttscore.get(key);  
        //                
        //                System.Console.WriteLine("\nScore : "+value);  
        //             }  
    }

    private static TimeTable Mutation(TimeTable parentTimetable)
    {
        TimeTable mutantTimeTable = parentTimetable;
        int rnd2;
        int rnd1;
        Random randomGenerator = new Random();
        List<ClassRoom> presentClassroom = mutantTimeTable.getRoom();
        for (var iterator = presentClassroom.GetEnumerator(); iterator.MoveNext();
        )
        {
            ClassRoom classRoom = iterator.Current;
            // for (Iterator <Day> iterator2 = classRoom.getWeek().getWeekDays().iterator(); iterator2.hasNext();) {
            //  i have got the two days here which i have to exchange... but wat i actually 
            // want to shuffle is not the days but the schedule for the day!                
            rnd1 = randomGenerator.Next(5);
            rnd2 = -1;
            while ((rnd1 != rnd2))
            {
                rnd2 = randomGenerator.Next(5);
            }

            List<Day> weekDays = classRoom.getWeek().getWeekDays();
            Day day1 = weekDays[rnd1];
            Day day2 = weekDays[rnd2];
            List<TimeSlot> timeSlotsOfday1 = day1.getTimeSlot();
            List<TimeSlot> timeSlotsOfday2 = day2.getTimeSlot();
            day1.setTimeSlot(timeSlotsOfday2);
            day2.setTimeSlot(timeSlotsOfday1);
            //  if i am limiting this to two days i am breaking out... 
            // or else all the days will get exchanged in a sorted order
            // like monday-tue,wed thu,fri sat in pairs!
            break;
            // }            
        }

        //  apply repairstrategy here! check whether mutant 
        // better than parent and vice versa and choose the best        
        return mutantTimeTable;
    }

    private static TimeTable crossOver(TimeTable fatherTimeTable)
    {
        //  let us say that we give father the priority to stay as the checker!
        //  in the outer loop        
        Random randomGenerator = new Random();
        var parentTimeTableClassRooms = fatherTimeTable.getRoom().GetEnumerator();
        while (parentTimeTableClassRooms.MoveNext())
        {
            ClassRoom room = parentTimeTableClassRooms.Current;
            if (!room.isLaboratory())
            {
                List<Day> days = room.getWeek().getWeekDays();
                int i = 0;
                while ((i < 3))
                {
                    int rnd = randomGenerator.Next(5);
                    Day day = days[rnd];
                    day.getTimeSlot().Shuffle();
                   // Collections.shuffle(day.getTimeSlot());
                    i++;
                }

            }

        }

        return fatherTimeTable;
    }

    private static void writeToExcelFile()
    {
       StreamWriter  writer = new StreamWriter("timetable.csv");
        // PrintWriter pw = new PrintWriter(writer);
        int i = 0;
        writer.Write(("\n\nMinimum : " + min));
        writer.Write(("\n\nScore : " + GlobalBestTimetable.getFittness()));
        writer.Write("\n\n (Subject#Professor#Student Group)");
        List<ClassRoom> allrooms = GlobalBestTimetable.getRoom();
        var allroomsIterator = allrooms.GetEnumerator();
        while (allroomsIterator.MoveNext())
        {
            ClassRoom room = allroomsIterator.Current;
            writer.Write(("\n\nRoom Number: " + room.getRoomNo()));
            List<Day> weekdays = room.getWeek().getWeekDays();
            var daysIterator = weekdays.GetEnumerator();
            var lectTimeItr = lectureTimings.GetEnumerator();
            writer.Write("\n\nTimings: ,");
            while (lectTimeItr.MoveNext())
            {
                writer.Write((lectTimeItr.Current + ","));
            }

            i = 0;
            writer.Write("\nDays\n");
            while (daysIterator.MoveNext())
            {
                Day day = daysIterator.Current;
                writer.Write((""
                                + (weekDayNames[i] + ",")));
                List<TimeSlot> timeslots = day.getTimeSlot();
                i++;
                for (int k = 0; (k < timeslots.Count); k++)
                {
                    if ((k == 3))
                    {
                        writer.Write("BREAK,");
                    }

                    TimeSlot lecture = timeslots[k];
                    if ((lecture.getLecture() != null))
                    {
                        writer.Write(("("
                                        + (lecture.getLecture().getSubject() + ("#"
                                        + (lecture.getLecture().getProfessor().getProfessorName() + ("#"
                                        + (lecture.getLecture().getStudentGroup().getName().Split('/')[0] + (")" + ","))))))));
                    }
                    else
                    {
                        writer.Write("FREE LECTURE,");
                    }

                }

                writer.Write("\n");
            }

            writer.Write("\n");
        }

        //             i++;            
        // writer.append("This is grahesh&Shridatt copyright @");
        writer.Flush();
        writer.Close();
    }

    private static void display()
    {
        //  TODO Auto-generated method stub
        int j = 0;
        int i = 0;
        System.Console.WriteLine(("Minimum : " + min));
        System.Console.WriteLine(("\nScore : " + GlobalBestTimetable.getFittness()));
        List<ClassRoom> allrooms = GlobalBestTimetable.getRoom();
        var allroomsIterator = allrooms.GetEnumerator();
        while (allroomsIterator.MoveNext())
        {
            ClassRoom room = allroomsIterator.Current;
            System.Console.WriteLine(("\nRoom: " + room.getRoomNo()));
            List<Day> weekdays = room.getWeek().getWeekDays();
            var daysIterator = weekdays.GetEnumerator();
            var lectTimeItr = lectureTimings.GetEnumerator();
           System.Console.Write("\nTimings:    ");
            while (lectTimeItr.MoveNext())
            {
                System.Console.WriteLine((" "
                                + (lectTimeItr.Current + " ")));
            }

            i = 0;
            System.Console.WriteLine("\n");
            while (daysIterator.MoveNext())
            {
                Day day = daysIterator.Current;
                System.Console.WriteLine(("Day: " + weekDayNames[i]));
                List<TimeSlot> timeslots = day.getTimeSlot();
                // Iterator<TimeSlot> timeslotIterator= timeslots.iterator();
                i++;
                //System.Console.Write(""+day.getName()+": ");
                for (int k = 0; (k < timeslots.Count); k++)
                {
                    if ((k == 3))
                    {
                       System.Console.Write("       BREAK       ");
                    }

                    TimeSlot lecture = timeslots[k];
                    if ((lecture.getLecture() != null))
                    {
                        //System.Console.Write(" (Subject: "+lecture.getLecture().getSubject()+" --> Professor: "+lecture.getLecture().getProfessor().getProfessorName()+" GrpName: "+lecture.getLecture().getStudentGroup().getName()+")");
                        System.Console.WriteLine(("  ("
                                        + (lecture.getLecture().getSubject() + ("#"
                                        + (lecture.getLecture().getProfessor().getProfessorName() + ("#"
                                        + (lecture.getLecture().getStudentGroup().getName().Split('/')[0] + ")")))))));
                    }
                    else
                    {
                        System.Console.WriteLine(" FREE LECTURE ");
                    }

                }

                System.Console.WriteLine("\n");
            }

            System.Console.WriteLine("\n");
        }

        // System.Console.WriteLine("This is grahesh&Shridatt copyright @");
    }
}
//                     while(timeslotIterator.hasNext()){
//                         
//                         TimeSlot lecture = timeslotIterator.next();
//                         
//                         if(lecture.getLecture()!=null){
//                             //System.out.print(" (Subject: "+lecture.getLecture().getSubject()+" --> Professor: "+lecture.getLecture().getProfessor().getProfessorName()+" GrpName: "+lecture.getLecture().getStudentGroup().getName()+")");
//                                System.Console.Write("  ("+lecture.getLecture().getSubject()+"#"+lecture.getLecture().getProfessor().getProfessorName()+"#"+lecture.getLecture().getStudentGroup().getName().split("/")[0]+")");
//                             }
//                         
//                             else{
//                                System.Console.Write("       FREE LECTURE       ");
//                             }
//                         
//                     }
// }
// incomplete class. Not used till now. Working on this. You also start working
// use Elitism for crossover where best 2 timetables are kept and other two are used for crossover
// crossover done by replacing random days of each room in timetable with same room in other timetable
// Mutation : exchange two random days from a room
// Repair Strategy : take clashing lectures, change teacher or take and place at null lecture position
// private static TimeTable[] crossOver(TimeTable fatherTimeTable,TimeTable motherTimeTable){
//     // let us say that we give father the priority to stay as the checker!
//     // in the outer loop        
//     Iterator<ClassRoom> parentTimeTableClassRooms=fatherTimeTable.getRoom().iterator();        
//     while(parentTimeTableClassRooms.hasNext()) {
//         ClassRoom fathersClassRoom = (ClassRoom) parentTimeTableClassRooms.next();            
//         string parentClassId=fathersClassRoom.getRoomNo();            
//         Iterator<ClassRoom> motherTimeTableClassRooms=motherTimeTable.getRoom().iterator();            
//         while(motherTimeTableClassRooms.hasNext()){
//             ClassRoom mothersClassRoom = (ClassRoom) motherTimeTableClassRooms.next();                
//             string motherClassId=mothersClassRoom.getRoomNo();                
//             // if both are same classes then
//             if(motherClassId.equals(parentClassId)){
//                 // change days in them randomly!                    
//                 int crossoverPoint=0;                    
//                 Random r=new Random();                    
//                 while(crossoverPoint==0||crossoverPoint==5){
//                     crossoverPoint=r.nextInt(5);
//                 }                    
//                 List<Day> fatherTTDays= fathersClassRoom.getWeek().getWeekDays();
//                 List<Day> motherTTDays= mothersClassRoom.getWeek().getWeekDays();
//                 
//                 List<Day> tempExchange1=new List<Day>();
//                 List<Day> tempExchange2=new List<Day>();                    
//                 for(int i=0;i<crossoverPoint;i++){
//                     tempExchange1.Add(fatherTTDays.get(i));
//                     
//                 }                    
//                 // assuming till 6 days
//                 for (int i = crossoverPoint; i < 6; i++) {
//                     tempExchange2.Add(motherTTDays.get(i));
//                 }                    
//                 fatherTTDays.removeAll(tempExchange1);
//                 motherTTDays.removeAll(tempExchange2);                    
//                 fatherTTDays.AddAll(motherTTDays);
//                 tempExchange1.AddAll(tempExchange2);                    
//                 motherTTDays.clear();
//                 motherTTDays.AddAll(tempExchange1);                    
//                 mothersClassRoom.getWeek().setWeekDays(motherTTDays);
//                 fathersClassRoom.getWeek().setWeekDays(fatherTTDays);                    
//             }
//         }        
//     }        
//     TimeTable[] offsprings={fatherTimeTable,motherTimeTable};
//     return offsprings;
// }
// public ClassRoom initialRandom(ClassRoom room){
// List<Day> weekdays = room.getWeek().getWeekDays();
// Iterator<Day> daysIterator=weekdays.iterator();
// while(daysIterator.hasNext()){
//     Collections.shuffle(subjects);
//     Day day = daysIterator.next();
//     List<TimeSlot> timeslots = day.getTimeSlot();
//     Iterator timeslotIterator= timeslots.iterator();
//     Iterator subIterator=subjects.iterator();
//     while(timeslotIterator.hasNext() && subIterator.hasNext()){
//         TimeSlot lecture = (TimeSlot) timeslotIterator.next();
/// /        if(!(lecture.getSlotTime()==12)){
/// /        lecture.setSubject((string)subIterator.next());
/// /        }
//     }
// }
// 
// return room;
// }