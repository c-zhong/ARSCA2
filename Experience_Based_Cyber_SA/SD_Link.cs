using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class SD_Link : SD
    {
        public List<SD> related_sds = new List<SD>();

        public List<String> reasons = new List<String>();

        public SD_Link()
            : base(Enum_SDType.SDType.LINK)
        { }

        public SD_Link(List<SD> sdlist,List<String> reasonlist )
            : base(Enum_SDType.SDType.LINK)
        {
            /*
            foreach (SD sd in sdlist)
            {
                if (sd.type != Enum_SDType.SDType.LINK)
                {
                    SD newsd = new SD(sd);
                    related_sds.Add(newsd);
                }
            }
             * */
            related_sds=sdlist;
            reasons = reasonlist;
        }

        public override string toObsString()
        {
            string output ="";
            if(reasons.Count>0){
            output = "LINK-["+related_sds.Count+" observations] for [";
            for(int i =0; i<reasons.Count-1; i++){
                output += reasons[i]+", ";
            }
            output += reasons.Last()+"]";
            }

            return output;
        }
        public override string toActString()
        {
            string output ="";
            if(reasons.Count>0){
            output = "LINK-["+related_sds.Count+" observations] for [";
            for(int i =0; i<reasons.Count-1; i++){
                output += reasons[i]+", ";
            }
            output += reasons.Last()+"]";
            }

            return output;
        }

        public override string toOriginalString()
        {
             string output = this.id+";";
            foreach (SD sd in related_sds)
            {
                output += sd.id;
                if (sd != related_sds.Last())
                    output += ",";
            }
            output += ";";
            return output;
        }

       

    }
}
