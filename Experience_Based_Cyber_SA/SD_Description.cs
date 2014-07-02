using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
   
    public class SD_Description : SD
    {
        public String text = "";

    

        public SD_Description()
            : base(Enum_SDType.SDType.DESCRIPTION)
        { }

        public SD_Description(string txt)
            : base(Enum_SDType.SDType.DESCRIPTION)
        {
            text = txt;
        }
        public override string toObsString()
        {
            string output = "TEXT-[" + text + "]";
            return output;
        }
        public override string toActString()
        {
            return "Write [DESCRIPTION] of [" + text + "]";
        }
        public override string toOriginalString()
        {
            string output = this.id + ";";

            output += text + ";";

            return output;
        }
        

        
    }
}
