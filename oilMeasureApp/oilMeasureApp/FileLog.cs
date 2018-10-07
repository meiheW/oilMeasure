using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace oilMeasure
{
    class FileLog
    {
        private string fullPath;

        public FileLog()
        {
            fullPath = "E:\\save.csv";//you can modify the pisiton of the save file.
        }

        public bool LogToFile( double lon,double lat, double measure )
        {
            //文件管理，可以将数据保存在一个文件中，这个文件格式为csv就行
            Boolean fileExits=true;
            if (!File.Exists(fullPath))
                fileExits = false;
            FileStream fs = new FileStream(fullPath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            //write
            if (fileExits == false) sw.Write("lon,lat,measure"+"\r\n");
            sw.Write(lon.ToString()+","+lat.ToString()+","+measure.ToString()+"\r\n");
            //clear and close
            sw.Flush();
            sw.Close();
            fs.Close();

            return true;
        }
    }
}
