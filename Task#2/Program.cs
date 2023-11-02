namespace Program
{
    class Program
    {
        public static void Main(string[] args)
        {
            string path = "E:\\Games\\7 Days To Die";
            Console.WriteLine($"Вес в байтах {CheckedWeightDir.GetWeightDir(path)} байтов");
        }
    }
    class CheckedWeightDir
    {
        public static long GetWeightDir(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            long weight = 0;
            try
            {
                if (dir.Exists)
                {
                    var files = dir.GetFiles();
                    foreach (var file in files)
                    {
                        weight += file.Length;
                    }
                    var directorys = dir.GetDirectories();
                    foreach (var directory in directorys)
                    {
                        weight += GetWeightDir(directory.FullName);
                    }
                    return weight;
                }
                throw new Exception("Не верно указан путь");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return weight;
            }
        }
    }
}