namespace HomeWork_8_2;

class Program
{
    static void Main(string[] args)
    {
        MainAsync().GetAwaiter().GetResult();
    }

    private static async Task MainAsync()
    {
        // Входящая папка Volume размещена на рабочем столе
        // string directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Volume";

        //Для проверки большого расчета.. 
        string directory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

        Console.WriteLine("Идет процесс подсчета.");

        //Когда коту делать нечего..
        TimerCallback tm = new TimerCallback((object obj) => Console.Write("."));
        // создаем таймер
        Timer timer = new Timer(tm, null, 0, 100);

        // Неправильно делать долгие операции в одном потоке
        await Task.Run(() =>
        {
            Console.WriteLine($"{Environment.NewLine} Объем папки: {GetVolume(directory)}");
        });
    }


    static long GetVolume(string directory, long size = 0)
    {
        // Проверяем наличие папки
        if (Directory.Exists(directory))
        {
            try
            {
                DirectoryInfo infoDir = new(directory);
                foreach (var itemFiles in infoDir.GetFiles())
                {
                    FileInfo info = new(itemFiles.ToString());
                    size += info.Length;
                }
                foreach (var itemDir in infoDir.GetDirectories())
                {
                    size = GetVolume(itemDir.ToString(), size);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine($"Произошла ошибка, {Environment.NewLine} {e.Message}");
            }
        }
        else
        {
            Console.WriteLine($"Указанной папки не существует");
        }
        return size;
    }
}