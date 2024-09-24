using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;

namespace Task_2
{
    public class ResourceMonitor
    {
        private readonly System.Timers.Timer _timer;
        private readonly long _maxMemoryUsage;
        private readonly Process _process;

        public event Action<string> OnWarning;  // Подія для попередження

        public ResourceMonitor(int processId, long maxMemoryUsage, double checkIntervalInSeconds)
        {
            _maxMemoryUsage = maxMemoryUsage;

            try
            {
                // Отримуємо процес за його PID
                _process = Process.GetProcessById(processId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не вдалося знайти процес із PID {processId}. Помилка: {ex.Message}");
                return;
            }

            // Налаштування таймера для перевірки використання ресурсів через інтервал часу
            _timer = new System.Timers.Timer(checkIntervalInSeconds * 1000);  // Інтервал у мілісекундах
            _timer.Elapsed += CheckResources;
            _timer.AutoReset = true;
        }

        // Запуск моніторингу
        public void StartMonitoring()
        {
            if (_process != null)
            {
                _timer.Start();
            }
        }

        // Зупинка моніторингу
        public void StopMonitoring()
        {
            _timer.Stop();
        }

        // Метод для перевірки споживання ресурсів
        private void CheckResources(object sender, ElapsedEventArgs e)
        {
            if (_process == null || _process.HasExited)
            {
                Console.WriteLine("Процес завершив свою роботу або не доступний.");
                StopMonitoring();
                return;
            }

            // Отримання фактичного використання пам'яті (у байтах)
            long memoryUsage = _process.PrivateMemorySize64;

            // Якщо використання пам'яті наближається до максимального порогу, викликаємо попередження
            if (memoryUsage >= _maxMemoryUsage * 0.9)  // Попередження при досягненні 90% порогу
            {
                OnWarning?.Invoke($"Попередження: використання пам'яті наближається до допустимого рівня ({memoryUsage / (1024 * 1024)} MB з дозволених {_maxMemoryUsage / (1024 * 1024)} MB).");
            }
        }
    }
}
