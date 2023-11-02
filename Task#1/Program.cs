namespace TaskOne
{
    class Program
    {
        public static void Main()
        {
            string path = "E:\\Games\\7 Days To Die";
            ClearDirectory CDirectory = new(30);
            CDirectory.Checked(path);
        }
    }
    class ClearDirectory
    {
        TimeSpan Time;

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
                            Remove($"Файл на удаление {file.Name}");
                        }
                    }
                    var directories = dir.GetDirectories();
                    foreach (var directory in directories)
                    {
                        Checked(directory.FullName);
                        if ((DateTime.Now - directory.LastWriteTime) >= Time)
                        {
                            Remove($"Директория на удаление {directory.Name}");
                        }
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

        private void Remove(string path)
        {
            Console.WriteLine(path);
        }

    }
}