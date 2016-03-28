using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class BitmapCollection
    {
        private Dictionary<String, Dictionary<String, Bitmap>> data;
        public BitmapCollection()
        {
            data = new Dictionary<string, Dictionary<string, Bitmap>>();
            DirectoryInfo dir = new DirectoryInfo("Images");
            if(dir.Exists)
            {
                DirectoryInfo[] di = dir.GetDirectories();
                foreach(var subdir in di)
                {
                    FileInfo[] files = subdir.GetFiles();
                    String category = subdir.Name;
                    foreach(var file in files)
                    {
                        String name = Path.GetFileNameWithoutExtension(file.FullName);
                        if (!data.ContainsKey(category)) data[category] = new Dictionary<string, Bitmap>();
                        data[category][name] = (Bitmap)Image.FromFile(file.FullName);
                        data[category][name].MakeTransparent(Color.White);
                    }
                }
            }

        }

        public Dictionary<String, Bitmap> this[String category]
        {
            get
            {
                return data[category];
            }
        }
    }
}
