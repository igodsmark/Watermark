using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace Watermark
{
    internal static class Utilities
    {
        internal static void WatermarkedImage(string path, string fileName, string message, string outputFolder)
        {
            bool debug = false;
            if(!Directory.Exists(outputFolder + @"\Watermarked"))
            {
                Directory.CreateDirectory(outputFolder + @"\Watermarked");
            }
            string outputPath = outputFolder + @"\Watermarked\";
            using (Image image = Image.FromFile(path + "\\" + fileName))
            {
                Graphics graphics = Graphics.FromImage(image);
                int fontSize = 25;
                if(image.Width > 1500)
                {
                    fontSize = 85;
                }
                Font font = new Font("Arial", 80, FontStyle.Bold);
                SolidBrush brush = new SolidBrush(Color.FromArgb(40, 255, 255, 255));
                SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(40, 0, 0, 0));


                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;

                int index = 0;
                int divisor = 10;
                float offsetX = 0;
                float offsetY = 0;
                SizeF textSize = graphics.MeasureString(message, font);

                int increment = Convert.ToInt32(textSize.Width);

                while(offsetX < image.Width)
                {
                    Console.WriteLine("***** New Column *****");
                    while (offsetY <= image.Height)
                    {
                        Console.WriteLine($"offsetX: {offsetX}, offsetY: {offsetY}");
                        Matrix matrix = new Matrix();
                        matrix.Translate(offsetX, offsetY);
                        matrix.Rotate(-45.0f);

                        graphics.Transform = matrix;
                        string messageText = message;
                        if (debug)
                        {
                            messageText += $" {offsetX} {offsetY}";
                        }
                        graphics.DrawString(messageText, font, shadowBrush, 2, 52, format);
                        graphics.DrawString(messageText, font, brush, 0, 50, format);

                        offsetY += increment;
                        index++;
                    }
                    offsetY = 0;
                    offsetX += increment;
                }


                
                string outputFileName = System.IO.Path.GetFileNameWithoutExtension(fileName) + System.IO.Path.GetExtension(fileName) ;
                image.Save(outputPath + outputFileName);
            }
        }
    }
}
