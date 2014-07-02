using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    
    public class SD_Image : SD
    {
        public String imagepath = "";

        public String note = "";

        //Which data source the image is snipped?
        public String imageSource="";

        public SD_Image(String path, string  note1, string imgsrc)
            : base(Enum_SDType.SDType.IMAGE)
        {
            imagepath = path;
            note = note1;
            imageSource = imgsrc;
        }

        public void setSDImage(String path, string note1, string imgsrc)
        {
            imagepath = path;
            note = note1;
            imageSource = imgsrc;
        }

        public override string toObsString()
        {
         
            //return imageSource+"\n\r"+note+"\n\r"+imagepath;
            return "[" + imageSource + "]-[" + Util_All.getImageName(imagepath) + "]";
        }

        public override string toActString()
        {
            return "Check IMAGE FROM ["+imageSource+"]";
        }

        public override string toOriginalString()
        {
            string output = this.id + ";";
            output += imageSource+";"+note+";"+imagepath;
            return output;
        }

        public SD_Image(string str)
            : base(Enum_SDType.SDType.IMAGE)
        {
            string[] array = str.Split(';');

            imageSource = array[0];
            note = array[1];
            imagepath = array[2];
        }


    }
}
