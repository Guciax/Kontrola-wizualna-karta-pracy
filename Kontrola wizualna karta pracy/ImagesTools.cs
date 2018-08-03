using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy
{
    class ImagesTools
    {
        public static List<Image> CreateListOfImages(string Folderpath)
        {
            List<Image> result = new List<Image>();
            string[] extensionFilter = new string[] { ".png", ".bmp", ".jpg" };
            var dir = new DirectoryInfo(Folderpath);
            FileInfo[] files = dir.GetFiles();

            foreach (var file in files)
            {
                Debug.WriteLine("Image: "+file.Name);

                string ext = Path.GetExtension(file.Name);
                if (!extensionFilter.Contains(ext)) continue;
                Image newImg = Bitmap.FromFile(file.FullName);
                newImg.Tag = Path.GetFileNameWithoutExtension(file.Name);
                result.Add(newImg);
            }

            return result;
        }

        public static Image GetImageByName(List<Image> imgList, string name)
        {
            Image result = null;

            foreach (var img in imgList)
            {
                string tagname = img.Tag.ToString();
                if (tagname==name)
                {
                    result = img;
                    break;
                }
            }
            return result;
        }
    }
}
