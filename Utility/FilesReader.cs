using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace Utility
{
    public class FilesReader
    {
        static public ArrayList loadAlerts(){
            StreamReader objReader = new StreamReader(System.IO.Directory.GetCurrentDirectory()+"//SourceData//snort alerts.txt");
            string sLine = "";
            ArrayList LineList = new ArrayList();
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                    LineList.Add(sLine);
            }
            objReader.Close();
            MessageBox.Show(LineList.ToString());
           
            return LineList;
        }
    }

}
