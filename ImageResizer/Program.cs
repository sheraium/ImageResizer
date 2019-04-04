using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;

            ImageProcess imageProcess = new ImageProcess();

            imageProcess.Clean(destinationPath);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
            sw.Stop();
            var nonAsyncMS = sw.ElapsedMilliseconds;
            Console.WriteLine($"花費時間 non-Async: {nonAsyncMS} ms");

            sw.Restart();
            await imageProcess.ResizeImagesAsync(sourcePath, destinationPath, 2.0);
            sw.Stop();
            double asyncMS = sw.ElapsedMilliseconds;
            Console.WriteLine($"花費時間 Async: {asyncMS} ms, {nonAsyncMS/asyncMS*100:F1}%");

            sw.Restart();
            imageProcess.ResizeImagesParallel(sourcePath, destinationPath, 2.0);
            sw.Stop();
            double parallelMS = sw.ElapsedMilliseconds;
            Console.WriteLine($"花費時間 Parallel: {parallelMS} ms, {nonAsyncMS / parallelMS * 100:F1}%");

            Console.ReadLine();
        }
    }
}
