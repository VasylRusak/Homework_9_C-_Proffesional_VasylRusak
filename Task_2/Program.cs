using System.Text;

namespace Task_2
{


    internal class Program
    {
        static void Main()
        {

            Console.OutputEncoding = Encoding.Unicode;

            // Запитуємо у користувача PID процесу, який потрібно моніторити
            Console.Write("Введіть PID процесу, який потрібно моніторити: ");
            int processId;
            if (!int.TryParse(Console.ReadLine(), out processId))
            {
                Console.WriteLine("Невірний формат PID.");
                return;
            }

            // Встановлюємо допустимий максимум використання пам'яті
            long maxMemoryUsage = 400 * 1024 * 1024;

            // Створюємо об'єкт моніторингу ресурсів з інтервалом 
            ResourceMonitor monitor = new ResourceMonitor(processId, maxMemoryUsage, 5);

            // Підписуємося на подію 
            monitor.OnWarning += message => Console.WriteLine(message);

            // Запускаємо моніторинг
            monitor.StartMonitoring();

            // Імітуємо роботу програми
            Console.WriteLine("Моніторинг процесу. Натисніть будь-яку клавішу для завершення.");
            Console.ReadKey();

            // Зупинка моніторингу перед завершенням програми
            monitor.StopMonitoring();
        }
    }
}
