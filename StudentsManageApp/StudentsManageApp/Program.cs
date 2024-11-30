using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace StudentsManageApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FetchStudentsListFromDatabase();
            List<int> menuCalls = new List<int>() { 1,2,3,4,5,6,7 };
            int userCall;


            DisplayMenu();
            UserCall();
            while (userCall != 7 )
            {
                Console.WriteLine("---------------------------------------");
                switch (userCall)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2:
                        Student.DisplayAllStudents();
                        break;
                    case 3:
                        Student.DisplayAllStudentsWithInfo();
                        break;
                    case 4:
                        FindStudentCall();
                        break;
                    case 5:
                        UpdateStudentGradeCall();
                        break;
                    case 6:
                        RemoveStudent();
                        break;
                }
                Console.WriteLine();
                Console.WriteLine();
                DisplayMenu();
                UserCall();
            }


            // Displaying menu
            void DisplayMenu()
            {
                Console.WriteLine("         Menu");
                Console.WriteLine("1: Add new student.");
                Console.WriteLine("2: See all students.");
                Console.WriteLine("3: See all students with info.");
                Console.WriteLine("4: Find student by roll number.");
                Console.WriteLine("5: Update student grade.");
                Console.WriteLine("6: Remove Student.");
                Console.WriteLine("7: Exit.");
                Console.Write("Enter number of operation u want to make: ");
            }

            // Finding student in students list
            Student FindStudent(int rollNumber, bool display = true)
            {
                var student = Student.StudentsList.FirstOrDefault(student => student.RollNumber == rollNumber);
                if (student == null)
                {
                    Console.WriteLine($"We dont have student with this roll number({rollNumber}).");
                }
                else if (student != null && display == true)
                {
                    Console.WriteLine($"Here is the info about student with Roll Number: {rollNumber}:");
                    Console.WriteLine($"Student: {student.Name}; Roll Number: {student.RollNumber}; Grade: {student.Grade}");
                }
                return student;
            }

            void UserCall()
            {
                while (!int.TryParse(Console.ReadLine(), out userCall) || !menuCalls.Contains(userCall))
                {
                    Console.Write("Wrong menu call. Enter correct number for operation: ");
                }
            }

            void AddStudent()
            {
                Console.Write("Enter name of new Student: ");
                string studentName = Console.ReadLine();
                var newStudent = new Student(studentName);
                UpdateStudentsListInDatabase();
                Console.WriteLine($"{newStudent.Name} was added succesfuly!");
            }

            void RemoveStudent()
            {
                Console.Write("Enter Student's roll number: ");
                int rollNumber;
                while (!int.TryParse(Console.ReadLine(), out rollNumber))
                {
                    Console.Write("Invalid roll number. Enter again: ");
                }
                var student = FindStudent(rollNumber, false);
                Student.StudentsList.Remove(student);
                UpdateStudentsListInDatabase();
                Console.WriteLine($"Student {student.Name}({student.RollNumber}) was deleted succesfully");
            }

            void FindStudentCall()
            {
                Console.Write("Enter Student's roll number: ");
                int rollNumber;
                while (!int.TryParse(Console.ReadLine(), out rollNumber))
                {
                    Console.Write("Invalid roll number. Enter again: ");
                }
                FindStudent(rollNumber);
            }

            void UpdateStudentGradeCall()
            {
                int rollNumber;
                while (!int.TryParse(Console.ReadLine(), out rollNumber))
                {
                    Console.Write("Invalid roll number. Enter again: ");
                }
                var student = FindStudent(rollNumber, false);
                student.SetOrUpdateGrade();
            }

            void UpdateStudentsListInDatabase()
            {
                var data = JsonSerializer.Serialize(Student.StudentsList);
                var path = "C:\\Users\\Lenovo\\Desktop\\Students-list-database\\data.txt";

                using (StreamWriter sw = new StreamWriter(path,false))
                {
                    sw.WriteLine(data);
                }
            }

            void FetchStudentsListFromDatabase()
            {
                var path = "C:\\Users\\Lenovo\\Desktop\\Students-list-database\\data.txt";
                using (StreamReader sr = new StreamReader(path))
                {
                    string data = sr.ReadToEnd();
                    if(data != "")
                    {
                        List<Student> tmpListOfStudents = JsonSerializer.Deserialize<List<Student>>(data);
                        tmpListOfStudents?.ForEach(student => Student.StudentsList.Add(student));
                    }
                }
            }
        }
        class Student
        {
            public string Name { get; set; }
            public int RollNumber { get; private set; }
            public char Grade { get; private set; }
            public static List<Student> StudentsList { get; private set; } = new List<Student>();
            public Student(string name)
            {
                Name = name;
                RollNumber = GenerateRollNumber();
                SetOrUpdateGrade();
                StudentsList.Add(this);
            }

            [JsonConstructor]
            public Student(string name, int rollNumber, char grade)
            {
                Name = name;
                RollNumber = rollNumber;
                Grade = grade;
            }

            // Generating and set rollnumber
            private int GenerateRollNumber()
            {
                var random = new Random();
                int generatedRollNumber = random.Next(1000, 9999);
                if (StudentsList.Count != 0)
                {
                    while (StudentsList.Any(student => student.RollNumber == generatedRollNumber))
                    {
                        generatedRollNumber = random.Next(1000, 9999);
                    }
                }
                return generatedRollNumber;
            }
            // Set or update grade
            public void SetOrUpdateGrade()
            {
                Console.Write("Enter student's grade: ");
                int grade;
                while(!int.TryParse(Console.ReadLine(), out grade) || (grade > 100 || grade < 0))
                {
                    Console.WriteLine("Grade was invalid. Please write valid grade:");
                }
                if (grade >= 90)
                    Grade = 'A';
                else if (grade >= 80 && grade <= 89)
                    Grade = 'B';
                else if (grade >= 70 && grade <= 79)
                    Grade = 'C';
                else if (grade >= 60 && grade <= 69)
                    Grade = 'D';
                else
                    Grade = 'F';
            }
            // Display all registered students
            public static void DisplayAllStudents()
            {
                StudentsList.ForEach(student => Console.WriteLine($"Student: {student.Name};"));
            }
            // Display all registered students with their info
            public static void DisplayAllStudentsWithInfo()
            {
                StudentsList.ForEach(student => Console.WriteLine($"Student: {student.Name}; Roll Number: {student.RollNumber}; Grade: {student.Grade}"));
            }
        }
    }
}