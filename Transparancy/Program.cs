using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Bulk color transparency");
            int red, green, blue;

            Console.WriteLine("Red:");
            red = int.Parse(Console.ReadLine());
            Console.WriteLine("Green:");
            green = int.Parse(Console.ReadLine());
            Console.WriteLine("Blue:");
            blue = int.Parse(Console.ReadLine());

            var images = Directory.GetFiles(".", "*.bmp");

            foreach (var image in images)
            {
                Bitmap bmp = (Bitmap)Bitmap.FromFile(image);
                Bitmap outImg = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);
                outImg.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
                Graphics g = Graphics.FromImage(outImg);
                g.DrawImage(bmp, Point.Empty);
                g.Dispose();
                bmp.Dispose();

                for (int x = 0; x < outImg.Width; x++)
                {
                    for (int y = 0; y < outImg.Height; y++)
                    {
                        if (outImg.GetPixel(x, y) == Color.FromArgb(255, red, green, blue))
                        {
                            outImg.SetPixel(x, y, Color.Transparent);
                        }
                    }
                }

                outImg.Save(@"c:\output\" + Path.GetFileNameWithoutExtension(image) + ".png", ImageFormat.Png);
                outImg.Dispose();
            }
            Console.WriteLine("Conversion completed");
            Console.ReadKey();
        }
    }
}
