namespace Program
{
    class Program
    {
        public static void Main(string[] args)
        {
            string path = "E:\\Games\\7 Days To Die";
            ClearDirectory cD = new(30);
            cD.Checked(path);
            Console.WriteLine($"По пути {path}" +
                              $"\nИсходный размер папки {CheckedWeightDir.GetWeightDir(path)} байт" +
                              $"\nФайлов на удаление {ClearDirectory.FilePath.Count}");
            if(ClearDirectory.FilePath.Count > 1)
            {
                Console.WriteLine("Совершить очистку y/n");
                if("y" == Console.ReadLine())
                {
                    cD.DeleteFile(ClearDirectory.FilePath);
                }
                else
                {
                    Console.WriteLine("))");
                }
            }
            Console.WriteLine($"Текщий размер папки {CheckedWeightDir.GetWeightDir(path)} байт");
        }
    }

    class ClearDirectory
    {
        TimeSpan Time;
        public static List<string> FilePath = new List<string>();

        public ClearDirectory(byte time)
        {
            Time = TimeSpan.FromMinutes(time);
        }

        public void Checked(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            try
            {
                if (dir.Exists)
                {
                    var files = dir.GetFiles();
                    foreach (var file in files)
                    {
                        if ((DateTime.Now - file.LastWriteTime) >= Time)
                        {
                            FilePath.Add(file.FullName);
                        }
                    }
                    var directories = dir.GetDirectories();
                    foreach (var directory in directories)
                    {
                        Checked(directory.FullName);
                    }
                }
                else
                {
                    throw new Exception("Не верно указан путь");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteFile(List<string> Paths)
        {
            try
            {
                foreach(var path in Paths)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    throw new Exception("Не верный путь");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Remove(string path)
        {
            Console.WriteLine(path);
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