﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace TileMerge
{
    class Program
    {
        static void Main(string[] args)
        {
            var images = Directory.GetFiles(".", "*.png").OrderBy(s => s).ToList();

            int numberOfTilesPerSize = (int)Math.Ceiling(Math.Sqrt(images.Count()));

            var offsetOfCurrentImage = Point.Empty;

            Bitmap firstBitmapFileAsSizeReference = (Bitmap)Bitmap.FromFile(images.First());
            var size = firstBitmapFileAsSizeReference.Width;
            
            Bitmap tileSetImage = new Bitmap(size * numberOfTilesPerSize, size * numberOfTilesPerSize, PixelFormat.Format32bppArgb);   
            Graphics tileSetGraphicsObject = Graphics.FromImage(tileSetImage);
            tileSetImage.SetResolution(firstBitmapFileAsSizeReference.HorizontalResolution, firstBitmapFileAsSizeReference.VerticalResolution);

            Bitmap infoImage = new Bitmap(size * numberOfTilesPerSize, size * numberOfTilesPerSize, PixelFormat.Format32bppArgb);
            Graphics infoGraphicsObject = Graphics.FromImage(infoImage);      
            infoImage.SetResolution(firstBitmapFileAsSizeReference.HorizontalResolution, firstBitmapFileAsSizeReference.VerticalResolution);

            firstBitmapFileAsSizeReference.Dispose();

            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
           
            for (int index = 0; index < images.Count; index++)
            {
                var imageBitMap = (Bitmap)Bitmap.FromFile(images[index]);
                tileSetGraphicsObject.DrawImage(imageBitMap, offsetOfCurrentImage);
                infoGraphicsObject.DrawImage(imageBitMap, offsetOfCurrentImage);
                infoGraphicsObject.DrawString(index.ToString(), drawFont, drawBrush, new PointF(offsetOfCurrentImage.X + 5, offsetOfCurrentImage.Y + 5));
                imageBitMap.Dispose();

                if (offsetOfCurrentImage.X + size < size * numberOfTilesPerSize)
                {
                    offsetOfCurrentImage.X += (int)size;
                }
                else
                {
                    offsetOfCurrentImage.X = 0;
                    offsetOfCurrentImage.Y += size;
                }
            }
           
                    
            tileSetGraphicsObject.Dispose();
            tileSetImage.Save(@"c:\output\tileSet.png", ImageFormat.Png);
            tileSetImage.Dispose();

            infoGraphicsObject.Dispose();
            infoImage.Save(@"c:\output\tileSet_meta.png", ImageFormat.Png);
            infoImage.Dispose();
        }
    }
}
