using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4
{
    internal class LargeObject : IDisposable
    {
        private int[] largeArray;
        private bool disposed = false;

        public LargeObject()
        {
            largeArray = new int[100_000_000];
            Console.WriteLine("Великий об'єкт створено.");
        }

        public void PerformOperation()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("LargeObject");
            }

            // Імітація операцій над масивом
            for (int i = 0; i < largeArray.Length; i++)
            {
                largeArray[i] = i+5;
            }
            Console.WriteLine("Операція над масивом виконана.");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Зупинка виклику фіналізатора
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    largeArray = null;
                    Console.WriteLine("Керовані ресурси очищено.");
                }

                disposed = true;
            }
        }

        ~LargeObject()
        {
            Dispose(false);
        }

    }
}
