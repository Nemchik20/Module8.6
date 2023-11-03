using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    class Program
    {
        public static void Main(string[] args)
        {
            DirectoryInfo directory = new DirectoryInfo(@"C:\Users\EternalYouth\Desktop\ProjectsFinal");
            List<Student> students = ReadFile(directory.FullName + @"\Students.dat"); // Коллекция студентов

            Dictionary<string, List<Student>> groups = new Dictionary<string, List<Student>>(); // Ключ группа студента 

            foreach(var student in students) // Chat gpt Помог с этив блоком текста но я понял как с ним работать!
            {

                if (groups.ContainsKey(student.Group))
                {
                    groups[student.Group].Add(student);
                }
                else
                {
                    List<Student> studentsGroup = new List<Student> { student };
                    groups.Add(student.Group, studentsGroup);
                }
            }
            foreach(var group in groups) 
            {
                CreateAndWrite(directory.FullName + @"\FinalTask", group.Value, group.Key);
            }

            string[] paths = Directory.GetFiles(directory.FullName + @"\FinalTask");
            foreach (string path in paths)
            {
                List<Student> stu = ReadFile(path);
                foreach (Student student in stu)
                {
                    Console.WriteLine($"Группа -> {student.Group}  Имя - > {student.Name} Возраст -> {student.DateOfBirth}");
                }
            }
        }
        private static void CreateAndWrite(string path, List<Student> lsStuent, string group)
        {
            Student[] student = lsStuent.ToArray();
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (var Fs = new FileStream(path + @"\"+  group + ".dat", FileMode.OpenOrCreate))
                {
                    bf.Serialize(Fs, student);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        } // Запись в группы 

        private static List<Student> ReadFile(string path)
        {
            List<Student> students = new List<Student>();

            BinaryFormatter format = new BinaryFormatter();
            try
            {
                using (var FR = new FileStream(path, FileMode.Open))
                {
                    var st = (Student[])format.Deserialize(FR);
                    foreach (var student in st)
                    {
                        students.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return students;
        } //Deserialized students[]
    }
}