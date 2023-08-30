using StudentRandomizer_V0._0._1;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using System;
using System.Data.SqlTypes;

string fileName = "";
List<int> currentCohortIds = new List<int>();
List<Cohort> currentCohorts= new List<Cohort>();
List<Cohort> currentCohortsUsed = new List<Cohort>();
List<Team> currentTeamList = new List<Team>();
Cohort addStudentTo;

FileInput();
MainMenu();

void FileInput()
{
    Console.Clear();
    Console.WriteLine("Good morning, afternoon, or evening, Matt.\n" +
                      "Please enter the directory of the file you would like me to pull from: (Example: /../../../FileName.csv)");
    var parseFileName = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(parseFileName))
    {
        Console.WriteLine("Error: Invalid File Directory. Please try again.");
        parseFileName = Console.ReadLine();
    }
    fileName = parseFileName;
    try
    {
        using (StreamReader newReader = new StreamReader(fileName))
        {
            List<string> lines = new List<string>();

            while (!newReader.EndOfStream)
            {
                lines.Add(newReader.ReadLine());
            }

            for (int i = 1; i < lines.Count; i++)
            {
                string nextStudent = lines[i];
                string[] studentAttributes = nextStudent.Split(',');

                Student newStudent = new Student(studentAttributes[0], studentAttributes[1], Convert.ToInt32(studentAttributes[2]));

                if (currentCohortIds.Contains(Convert.ToInt32(studentAttributes[2])) != true)
                {
                    currentCohorts.Add(new Cohort(Convert.ToInt32(studentAttributes[2]), new List<Student>()));
                    currentCohortIds.Add(Convert.ToInt32(studentAttributes[2]));
                }
                // Find what cohort new student needs to be added to
                addStudentTo = currentCohorts.Find(cohort => cohort.cohortId == Convert.ToInt32(studentAttributes[2]));
                // Add the student
                addStudentTo.students.Add(newStudent);
            }
            currentCohorts.Sort((left, right) => left.cohortId.CompareTo(right.cohortId));
            newReader.Close();
        }
    }
    catch
    {
        Console.WriteLine("Error: File read error.");
    }
}
void MainMenu()
{
    Console.Clear();
    int userInput = 0;
    Console.WriteLine(" Matt's Student Team Creator (V0.0.0.1) \n" +
                      "----------------------------------------\n" +
                      "[1] Create Teams\n" +
                      "[2] Exit");
    Console.WriteLine("Please select an option: (1 - 2)");
    var parseUserInput = Console.ReadLine();
    while (!Int32.TryParse(parseUserInput, out userInput) || userInput > 2 || userInput < 1)
    {
        Console.WriteLine("Error: Invalid Input. Please Try Again.");
        parseUserInput = Console.ReadLine();
    }
    userInput = Convert.ToInt32(parseUserInput);
    switch (userInput)
    {
        case 1:
            TeamCreation();
            break;
        case 2:
            Console.WriteLine("Thank you for using the program.");
            break;
        default: 
            Console.WriteLine("Error: Unacceptable Option.");
            break;
    }
}

void TeamCreation()
{
    bool exitLoop = false;
    int cohortSelect;
    int teamAmount;
    int teamSize;
    List<Cohort> currentCohortsCopy = currentCohorts;
    List<int> currentCohortIdsUsed = new List<int>();
    Cohort cohortSelected = new Cohort();
    string msg = "Out of all cohorts listed, which ones would you like to include in team creation?\n";
    List<Student> studentsSelected = new List<Student>();
    List<Team> studentTeams = new List<Team>();
    Random randomStudent = new Random();
    int studentPosition = randomStudent.Next(0, studentsSelected.Count());

    Console.Clear();
    while (exitLoop == false)
    {
        Console.WriteLine(msg);
        foreach (Cohort chrt in currentCohortsCopy)
        {
            Console.WriteLine("Cohort #" + chrt.cohortId);
        }
        var parseCohortSelect = Console.ReadLine();
        while (!Int32.TryParse(parseCohortSelect, out cohortSelect) || !currentCohortIds.Contains(cohortSelect) && cohortSelect != 0 || currentCohortIdsUsed.Contains(cohortSelect))
        {
            Console.WriteLine("Error: Invalid Input. Please Try Again.");
            parseCohortSelect = Console.ReadLine();
        }
        if (cohortSelect == 0)
        {
            exitLoop = true;
        }
        else
        {
            cohortSelected = currentCohorts.Find(chrt => chrt.cohortId == cohortSelect);
            currentCohortsUsed.Add(cohortSelected);
            currentCohortIdsUsed.Add(cohortSelect);
            currentCohortsCopy.Remove(cohortSelected);
        }
        if (!currentCohortsCopy.Any())
        {
            exitLoop = true;
        }
        msg = "Out of all cohorts listed, which ones would you like to include in team creation?\n(Please enter 0 to leave the selection process.)";
        Console.Clear();
    }
    foreach(Cohort cohort in currentCohortsUsed)
    {
        foreach (Student stu in cohort.students)
        {
            studentsSelected.Add(stu);
        }
    }
    Console.WriteLine("Here are the Current Students Selected:\n");
    foreach (Student stu in studentsSelected)
    {
        Console.WriteLine(stu.firstName + " " + stu.lastName);
    }

    Console.WriteLine("\nHow many teams would you like to create?");
    var parseTeamAmount = Console.ReadLine();
    while (!Int32.TryParse(parseTeamAmount, out teamAmount) || teamAmount > studentsSelected.Count || teamAmount <= 1)
    {
        Console.WriteLine("Error: Invalid Input. Please Try Again.");
        parseTeamAmount = Console.ReadLine();
    }
    double teamSizeNotRounded = studentsSelected.Count() / teamAmount;
    teamSize = Convert.ToInt32(Math.Round(teamSizeNotRounded, 1));
    for (int i = 0; i < teamAmount; i++)
    {
        List<Student> teamStudents = new List<Student>();
        for (int j = 0; j < teamSize; j++)
        {
            teamStudents.Add(studentsSelected[studentPosition]);
            studentsSelected.RemoveAt(studentPosition);
            studentPosition = randomStudent.Next(0, studentsSelected.Count());
        }
        studentTeams.Add(new Team(i, teamSize, teamStudents));
    }
    while (studentsSelected.Any())
    {
        int counter = 0;
        for (int i = 0; i < studentsSelected.Count();)
        {
            studentTeams[counter].teamList.Add(studentsSelected[i]);
            studentTeams[counter].teamSize += 1;
            studentsSelected.RemoveAt(i);
            counter++;
        }
    }

    Console.Clear();
    Console.WriteLine("Your teams are: ");
    foreach (Team tm in studentTeams)
    {
        tm.teamNum += 1;
        Console.WriteLine(tm);
    }

    Console.WriteLine("Thank you for using the program!");
}