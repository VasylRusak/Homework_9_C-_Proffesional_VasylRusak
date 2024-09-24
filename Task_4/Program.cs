using System.Text;

namespace Task_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            using (LargeObject largeObject = new LargeObject())
            {
                largeObject.PerformOperation();
            }

            Console.WriteLine("Програма завершена. Натисніть будь-яку клавішу для виходу.");
            Console.ReadKey();

        }
    }
}
