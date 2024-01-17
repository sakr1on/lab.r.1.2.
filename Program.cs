namespace StudentsList
{
    /// <summary>
    /// Класс Person. Хранит и выводит информацию о человеке (возраст, имя)
    /// </summary>
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public virtual void Display()
        {
            Console.WriteLine($"Имя: {FirstName}");
            Console.WriteLine($"Фамилия: {LastName}");
            Console.WriteLine($"Возраст: {Age}");
        }
    }

    /// <summary>
    /// Наследуемый класс Student от Person, хранит и выводит информацию о студенте и его группе.
    /// </summary>
    public class Student : Person
    {
        public int StudentID { get; set; }
        public string Group { get; set; }

        public Student(string firstName, string lastName, int age, int studentId, string group) : base(firstName, lastName, age)
        {
            StudentID = studentId;
            Group = group;
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"StudentID: {StudentID}");
            Console.WriteLine($"Номер группы: {Group}");
        }
    }

    /// <summary>
    /// Класс Group. Хранит и выводит информацию о группе и студентах, состоящие в группе
    /// </summary>
    public class Group
    {
        public string GroupName { get; set; }
        public List<Student> Students { get; set; }
        public Student Monitor { get; set; }

        public Group(string groupName, List<Student> students, Student monitor)
        {
            GroupName = groupName;
            Students = students;
            Monitor = monitor;
        }

        public void Display()
        {
            Console.WriteLine($"Рассматриваемая группа: {GroupName}");
            Console.WriteLine("Данные о студентах");
            foreach (var student in Students)
            {
                student.Display();
            }

            Console.WriteLine("Староста: ");
            Monitor.Display();
        }
    }

    /// <summary>
    /// Класс Viewer, позволяет пользователю посмотреть студента(ов) и группу(ы)
    /// </summary>
    public class Viewer
    {
        public void ViewStudentInfo(Student student)
        {
            student.Display();
        }

        public void ViewGroupInfo(Group group)
        {
            group.Display();
        }
    }

    /// <summary>
    /// Наследуемый класс Editor от Viewer, позволяет внести нового студента
    /// </summary>
    public class Editor : Viewer
    {
        private int _nextStudentID = 4; // Начальное значение ID.

        public void AddNewStudent(Group group)
        {
            Console.WriteLine("Введите имя нового студента:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Введите фамилию нового студента:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Введите возраст нового студента:");
            int age;
            while (!int.TryParse(Console.ReadLine(), out age))
            {
                Console.WriteLine("Некорректный возраст. Попробуйте ещё раз:");
            }

            Console.WriteLine("Введите номер группы нового студента:");
            string groupNumber = Console.ReadLine();

            var newStudent = new Student(firstName, lastName, age, _nextStudentID++, groupNumber);
            group.Students.Add(newStudent);
        }
    }

    /// <summary>
    /// Главный класс MainProgram, предоставляющий интерфейс пользователя для управления студентами и группами
    /// </summary>
    public class MainProgram
    {
        static void Main()
        {
            var student1 = new Student("Иван", "Иванов", 21, 1, "1");
            var student2 = new Student("Мария", "Кузнецова", 19, 2, "2");
            var student3 = new Student("Аркадий", "Котов", 23, 3, "1");

            var group1 = new Group("Группа 1", new List<Student> { student1, student3 }, student1);
            var group2 = new Group("Группа 2", new List<Student> { student3 }, student3);

            var editor = new Editor();
            var isProgramRunning = true;

            while (isProgramRunning)
            {
                Console.WriteLine("Выберите группу 1 или 2 (fingerprint)");
                string groupChoice = Console.ReadLine();

                if (groupChoice == "1")
                {
                    editor.ViewGroupInfo(group1);
                }
                else if (groupChoice == "2")
                {
                    editor.ViewGroupInfo(group2);
                }

                Console.WriteLine("Желаете внести нового студента? y/n");
                string addStudentChoice = Console.ReadLine();

                if (addStudentChoice == "y")
                {
                    Console.WriteLine("Выберите группу 1 или 2 (fingerprint)");
                    string chosenGroup = Console.ReadLine();

                    if (chosenGroup == "1")
                    {
                        editor.AddNewStudent(group1);
                    }
                    else if (chosenGroup == "2")
                    {
                        editor.AddNewStudent(group2);
                    }
                }
                else
                {
                    isProgramRunning = false;
                }
            }
        }
    }
}