/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      SD_Alerts_Rela.cs
/// Function:   
/// Note:    
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Experience_Based_Cyber_SA
{
    public class SD_Alerts_Rela : SD
    {
        public List<SD_IDSlog> alertList = new List<SD_IDSlog>();
        public static List<string> missed_ids_list = new List<string>();

        public SD_Alerts_Rela(List<SD_IDSlog> list)
            : base(Enum_SDType.SDType.ALERTS_RELA)
        {
            alertList = list;
        }

        public override string toObsString()
        {
            string output = "";
            foreach (SD_IDSlog str in alertList)
            {
                output += "A" + str.sid;
                if (str != alertList.Last())
                    output += " < ";
            }
            return output;
        }

        public override string toActString()
        {
            return "Check ALERT CORRELATION";
        }

        public override string toOriginalString()
        {
            string output = this.id+";";
            foreach (SD_IDSlog str in alertList)
            {
                output += str.sid;
                if (str != alertList.Last())
                    output += ",";
            }
            output += ";";
            return output;
        }

        public SD_Alerts_Rela(string str)
            : base(Enum_SDType.SDType.ALERTS_RELA)
        {
            string[] array = str.Split(';');
            string[] alerts = array[0].Split(',');
            foreach (string item in alerts)
            {
                SD_IDSlog ids = new SD_IDSlog();
                ids.sid = item;
                alertList.Add(ids);
            }
        }

        public static void find_missed_alerts(List<SD_IDSlog> ids_list, SD_Alerts_Rela rel)
        {
            //MessageBox.Show("Inside func " + ids_list[0].sid + " " + ids_list[1].sid);


            List<SD_IDSlog> actual_order = rel.alertList;
            List<string> actual_ids_order = new List<string>();

            //List<string> missed_ids_list = new List<string>();

            foreach (SD_IDSlog i in actual_order)
                actual_ids_order.Add(i.sid);

            for(int k =0; k<ids_list.Count() - 1; k++)
            {
                for (int j = k+1; j< ids_list.Count(); j++)
                {
                    int start = -1, end = -1;

                    if (actual_ids_order.Contains(ids_list[k].sid) && actual_ids_order.Contains(ids_list[j].sid))
                    {
                        start = actual_ids_order.IndexOf(ids_list[k].sid);
                        end = actual_ids_order.IndexOf(ids_list[j].sid);

                        if ((end - start > 1) && start!=-1 && end!=-1)
                        {
                            for (int index = start + 1; index < end; index++)
                                missed_ids_list.Add(actual_ids_order[index]);
                        }
                    }
                }
            }

            missed_ids_list = missed_ids_list.Distinct().ToList();
            
        }

    }
}
