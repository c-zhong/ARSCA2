using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experience_Based_Cyber_SA
{
    public class Con_Operation
    {

        #region Hypothesis Operation
        public enum Enum_Hypo_Op
        {
            NEW_2,  //the last digital number refers to the number of parameters
            ADD_CHILD_2,
            ADD_SIBLING_2,
            JUMP_FROM_TO_2,
            CHANGE_CONTENT_3,
            CHANGE_TRUTH_VALUE_3,
            MOVE_FROM_TO_3
        }
        public static string Enum_Hypo_Op_To_String(Enum_Hypo_Op e)
        {
            switch (e)
            {
                case Enum_Hypo_Op.NEW_2: return "NEW";
                case Enum_Hypo_Op.ADD_CHILD_2: return "ADD_CHILD";
                case Enum_Hypo_Op.ADD_SIBLING_2: return "ADD_SIBLING";
                case Enum_Hypo_Op.JUMP_FROM_TO_2: return "JUMP_FROM_TO";
                case Enum_Hypo_Op.CHANGE_CONTENT_3: return "CHANGE_CONTENT";
                case Enum_Hypo_Op.CHANGE_TRUTH_VALUE_3: return "CHANGE_TRUTH_VALUE";
                case Enum_Hypo_Op.MOVE_FROM_TO_3: return "MOVE_FROM_TO";
            }
            return "OTHER";
        }
        /*
        public static string Hypo_Op_String(Enum_Hypo_Op op, Con_Hypo hypo)
        {
            return Enum_Hypo_Op_To_String(op) + " (\n" + hypo.id + " " + hypo.hypo + "\n)";
        }
         * */

        public static string Hypo_Op_String(Enum_Hypo_Op op, Con_Hypo hypo1, Con_Hypo hypo2)
        {
            if (hypo1 == null)
            {
                return Enum_Hypo_Op_To_String(op) + " (\n" + "HYPO_ROOT" + " " + "HYPO_ROOT" + ",\n" + hypo2.id + " " + hypo2.hypo + "\n)";
            }
            return Enum_Hypo_Op_To_String(op) + " (\n" + hypo1.id + " " + hypo1.hypo + ",\n" + hypo2.id + " " + hypo2.hypo + "\n)";
        }
        public static string Hypo_Op_String(Enum_Hypo_Op op, Con_Hypo hypo, string original_value, string updated_value)
        {
            return Enum_Hypo_Op_To_String(op) + " (\n" + hypo.id + " " + hypo.hypo + ",\n" + original_value + ",\n" + updated_value + "\n)";
        }
        #endregion


        #region Observation and Action

        
        public enum Enum_ObAc_Op
        {
            CHECKING, //action: check
            FINDING, //observation
            SEARCH_2, //data source, keyword
            FILTER_2,//data source and filter_by
            LINK, //field_n, obs_n
            LOOKUP, //port or term
        }
        public enum Enum_LookUp { PORT, TERM}
        public static string Enum_ObAc_Op_To_String(Enum_ObAc_Op e)
        {
            switch (e)
            {
                case Enum_ObAc_Op.CHECKING: return "CHECKING";
                case Enum_ObAc_Op.FINDING: return "FINDING";
                case Enum_ObAc_Op.SEARCH_2: return "SEARCH";
                case Enum_ObAc_Op.FILTER_2: return "FILTER";
                case Enum_ObAc_Op.LINK: return "LINK";
                case Enum_ObAc_Op.LOOKUP: return "LOOKUP";
              
            }
            return "OTHER";
        }

        public static string Search_2_String(string datasource, string keyword)
        {
            string str = Enum_ObAc_Op_To_String(Enum_ObAc_Op.SEARCH_2) + "(\n "+datasource+",\n"+keyword +"\n)";
            return str;
        }
        public static string Filter_2_String(string datasource, string sql)
        {
            string str = Enum_ObAc_Op_To_String(Enum_ObAc_Op.FILTER_2) + "(\n " + sql + ",\n" + datasource+"\n)";
            return str;
        }
        public static string Link_2_String(SD_Link sd)
        {
            string str = Enum_ObAc_Op_To_String(Enum_ObAc_Op.LINK) + " (\n";
            foreach (String s in sd.reasons)
            {
                str += s;
                if (s != sd.reasons.Last())
                {
                    str += "&";
                }
            }
            str += ",\n";

            foreach (SD sdi in sd.related_sds)
            {
                str += sdi.toObsString();
                if (sdi != sd.related_sds.Last())
                {
                    str += ",";
                }
                str += "\n";
            }
            str += ")";
            return str;
        }
        public static string lookup_2_string(Enum_LookUp type, string keyword)
        {
            //type : PORT or TERM
            string str = Enum_ObAc_Op_To_String(Enum_ObAc_Op.LOOKUP) + "( " + type.ToString() + ", " + keyword + ")";
            return str; 
        }

        public static string Act_Op_String(Enum_ObAc_Op op, Con_Actions act)
        {
            String str = Enum_ObAc_Op_To_String(op) + " (\n";
            foreach (SD sd in act.sds)
            {
                str += sd.toActString();
                if(sd != act.sds.Last())
                {
                    str+=",";
                }
                str += "\n";
            }
            str += ")";
            return str;
        }

        public static string Obs_Op_String(Enum_ObAc_Op op, Con_Observation obs)
        {
            String str = Enum_ObAc_Op_To_String(op) + " (\n";
            foreach (SD sd in obs.sds)
            {
                str += sd.toObsString();
                if (sd != obs.sds.Last())
                {
                    str += ",";
                }
                str += "\n";
            }
            str += ")";
            return str;
        }

        public static string SDs_Op_String(Enum_ObAc_Op op, List<SD> sds)
        {
            String str = Enum_ObAc_Op_To_String(op) + " (\n";
            foreach (SD sd in sds)
            {
                str += sd.toObsString();
                if (sd != sds.Last())
                {
                    str += ",";
                }
                str += "\n";
            }
            str += ")";
            return str;
        }

        #endregion


        #region Tool Function Endorsed by user

        public enum Enum_Sys_Endorse
        {
            LIKE_SCREENSHOT,
            LIKE_HTREE,
            LIKE_HTREE_DETAIL,
            LIKE_ETREE,
            LIKE_ETREE_DETAIL,
            LIKE_OBSERVATION_SCREENSHOT,
            LIKE_OBSSERVATION_RAW,
            LIKE_FIND,
        }
        public static string Enum_Sys_Endorse_To_String(Enum_Sys_Endorse e)
        {
            switch (e)
            {
                case Enum_Sys_Endorse.LIKE_SCREENSHOT: return "lIKE_SHOWEDIN_SCREEN:";
                case Enum_Sys_Endorse.LIKE_HTREE: return "LIKE_HTREE";
                case Enum_Sys_Endorse.LIKE_HTREE_DETAIL: return "LIKE_HTREE_DETAIL";
                case Enum_Sys_Endorse.LIKE_ETREE: return "LIKE_ETREE";
                case Enum_Sys_Endorse.LIKE_ETREE_DETAIL: return "LIKE_ETREE_DETAIL";
                case Enum_Sys_Endorse.LIKE_OBSERVATION_SCREENSHOT: return "LIKE_OBS_BY_SCREEN";
                case Enum_Sys_Endorse.LIKE_OBSSERVATION_RAW: return "LIKE_OBS_BY_RAW";
                case Enum_Sys_Endorse.LIKE_FIND: return "LIKE_FIND";
            }
            return "OTHER";
        }
        /*
        public static string Hypo_Op_String(Enum_Hypo_Op op, Con_Hypo hypo)
        {
            return Enum_Hypo_Op_To_String(op) + " (\n" + hypo.id + " " + hypo.hypo + "\n)";
        }
         * */


        public static string Sys_Endorse_String(Enum_Sys_Endorse type, string path)
        {
            String str = Enum_Sys_Endorse_To_String(type) + " (\n";
            if (!path.Trim().Equals(""))
            {
                str += "; ";
            }
            return str;
        }

        #endregion

    }
}
