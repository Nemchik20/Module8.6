using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    class Program
    {
        public static void Main(string[] args)
        {
            string path = @"C:\Users\EternalYouth\Desktop\Students.dat";

            foreach(var st in ReadFile(path))
            {
                Console.WriteLine(st);
            }
        }
        private static List<Student> ReadFile(string path)
        {
            List<Student> students = new List<Student>();

            BinaryFormatter format = new BinaryFormatter();
            try
            {
                using (FileStream FR = new FileStream(path, FileMode.Open))
                {
                    students.Add((Student)format.Deserialize(FR));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return students;
        }
    }

    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}