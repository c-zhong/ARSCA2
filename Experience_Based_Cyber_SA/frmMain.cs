/// <summary>
/// Copyright (c) CHEN ZHONG / czz111@psu.edu
/// File:      frmMain.cs
/// Function:   main window
/// Note:   Tag on the left: [Monitor]- to view all data sources  [Analysis]- operations
/// </summary>
/// Deepak -> update maching algorithm
/***
 * UPDATES:
 * [TRACE 3-24] -> audit the key operations.
 * add file:
 *  -- Con_Operation.cs, 
 *  -- Con_Observable_Trace.cs, 
 *  -- Con_Observable_Trace_Item.cs
 * update files:
 *  -- frmMain.cs
 * 
 * [VAST-DATA 4-1] -> add firewall 
 * 
 * [4-07 REMOVE MISSING ALERT] -> change UI, remove missing alert window, and attack chain
 * 
 * [4-08 GRAPHVIZ]
 * 
 * [6-01 UI]
 * 1) change UI -> integrete H-Tree window in main.fm
 * 
 * [6-01 SNIP]
 * 1) enable snipping, user can add an observation by snip the screenshot just like QQ
 *
 * [6-11 UI ADV]
 * More user friendly
 * 
 * [6-15 UI ILIKE]
 * add like function to endorse tool's functions
 * 
 * [6-15 UI SEARCH-DGV]
 * add search function. users can do keyword-based search in datagridview
 * 
 * [6-15 NEW IDS]
 * add more fields to IDS alerts
 * 
 * [6-24 ADD_LINE_NUM]
 * add line num to DGV
 * 
 * [6-25 TERM/PORT LOOKUP ]
 * enable users to lookup the meaning of a port number and a term
 * 
 * [6-27 FRM-OBV]
 * Change observation confirmation UI. 
 * 
 * [6-27 FILTER-DGV]
 * User can select a filed, relation and input a filter value. The DGV will only show the filtered data. 
 * 
 * [6-27 LINK_OBS]
 * user can group observations
 * 
 * [6-29 TRACE]
 * Trace include 
 *  Normal Obs/Act, e.g. Find and Check. 
 *  Hypothesis Op
 *  Endorse
 *  Search
 *  Filter
 *  Link
 *  Lookup
 *  (Con_Operation.cs)
 *  
 * [7-16 REDUCE_LOAD_FILTER_TIME]
 * 
 * [7-17 DB_SQL]
 * 
 * [7-19 VIRTUAL MODE]
 * set firewall log as virtual mode to load large data
 * 
 * [7-23 Auto Complete]
 * filter value textbox can auto complete
 * 
 * [7-24 ONGOING OBS]
 * add frmCurrentObservation.
 * to show current ongoing observation as a seperate window.
 * 
 * [7-25 CORELATE ETREE HTREE]
 * correlate E-Tree and H-Tree
 * 
 * [7-26 HIGHLIGHT PATH]
 * 
 * [7-28 ETREE STRUCTURE CHANGE]
 * In order to have multiple first-level EU, add an pre-defined Root EU in Tree.
 * Whenever add a first-level EU, create a init_hypo as its parent hypo
 * and link it to the pre-defined Root EU. 
 * There is no hypo between first-level EU with pre-defined Root EU
 * 
 * [7-29 DISPLAY FILTER CONDITION]
 * Add a button with tip bubble icon that show current filter condition.
 * 
 * [8-20 ADD PORT IN LINK]
 * In link confirmation windown, add Src Port and Des Port.
 * 
 * [8-20 MEANINGFUL NODE]
 * make each node in H-Tree meaningful
 * 
 * [8-22 CREATE OBS BY DESCR]
 * add 3th option for creating observation: describe it by words
/*
 * 
 * [8-31 NODE OP]
 * when right click a node in H-Tree, a menu shows up, 
 * let user to delete, cut, paste
 * 
 * [14-5-19 Test Time]
 * Measure loading time
 * "Load_Source-0,Filter-1,Search_Yes-2,Search_No-3,Inquire_Yes-4, Inquire_No-5";
 * 
 * PictureBox.SizeMode 属性
 * http://lunax.info/archives/355.html
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;

//[6-01-SNIP]
using CSharpWin_JD.CaptureImage;

namespace Experience_Based_Cyber_SA
{

    public partial class frmMain : Form
    {
        #region Fields

        //All about Current E-Tree and H-Tree in memory
        //These are used in both [capturing] and [using]
        public Con_Observation confirmedObs = new Con_Observation();
        public Con_Actions confirmedAct = new Con_Actions();
        public Con_Hypo confirmedHypo = new Con_Hypo();
        public Con_Observation tempObs = new Con_Observation();
        public Con_Actions tempAct = new Con_Actions();

        public Con_Experience_Tree exTree = new Con_Experience_Tree();
        public Con_Hypo lastHypo = null;
        public List<TreeNode> lastETreeNodeList = new List<TreeNode>();
        //public TreeNode lastNode = null;
        public Con_Hypo_Tree hypoTree = new Con_Hypo_Tree();

        //Searched E-Tree list
        public List<Util_ETN_Score> ETNList = new List<Util_ETN_Score>();
        public bool isOutofTree = true;


        public bool isObsPoped = false;
        public bool isfrmHypothesisDone = false;
        //status
        //public int statusObs = 0; //1: hasn't begined 2: begined 3: finished
        //public int statusAct = 0; //1: is not acting, 2: acting

        //list of all EUs
        public static List<Con_Experience_Unit> EUList = new List<Con_Experience_Unit>();

        private Font f = new Font("Segoe UI", 12);

        //differentiate 
        public int isCapture = 1; //1: iscapture -1: is using 0:nothing

        //using Experience
        public bool isSearchReady = false;

        //search button has been clicked:
        public bool isSearchClicked = false;

        //MissingALert Table
        public DataTable missingAlertDt = new DataTable();
        public List<Con_Hypo> trueMissingHypos = new List<Con_Hypo>();

        public static List<Con_Experience_Unit> all_matches = new List<Con_Experience_Unit>();
        public static List<Con_Experience_Unit> all_matched_eus = new List<Con_Experience_Unit>();

        public static List<List<Con_Experience_Unit>> prev_results = new List<List<Con_Experience_Unit>>();
        public static List<double> prev_weights = new List<double>();


        public static List<SD_IDSlog> current_sds_list = new List<SD_IDSlog>();


        //begin [TRACE 3-24] Trace
        public Con_Observable_Trace trace = new Con_Observable_Trace();
        //end [TRACE 3-24]

        //[4-08 GRAPHVIZ]
        Util_GraphvizGenerator graph = new Util_GraphvizGenerator();

        //[6-15 UI SEARCH-DGV]
        Util_DGV_Searcher ids_searcher = null;
        Util_DGV_Searcher firewall_searcher = null;

        //[6-27 FILTER-DGV]
        public Boolean firstPopTip = false;
        public Boolean firstPopTip_selectobs = false;

        //[7-19 VIRTUAL MODE]
        DataTable firewall_dt = null;
        DataTable ids_dt = null;


        //[7-23 Auto Complete]
        AutoCompleteStringCollection acsc = new AutoCompleteStringCollection();


        //[7-24 ONGOING OBS]
        public frmCurrentObservation currentobs = null;

        //[7-25 CORELATE ETREE HTREE]
        Dictionary<string, TreeNode> id2et = new Dictionary<string, TreeNode>();
        Dictionary<string, TreeNode> id2ht = new Dictionary<string, TreeNode>();

        //[7-27]
        Con_Hypo init_hypo = new Con_Hypo();
        #endregion

        //[14-5-19 Test Time]
        List<string> test_time_str = new List<string>();
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

        string head = "Load_Source,Filter,Search_Yes,Search_No,Inquire_Yes, Inquire_No";
        private enum test_index
        {
            LOAD_SRC,
            FILTER,
            SEARCH_Y,
            SEARCH_N,
            INQUIRE_Y,
            INQUIRE_N,
        }
       
        private void test_time_add(int index)
        {
            string new_str = "";
            int i =0;
            for(;i<index;i++)
            {
                new_str += "0,";
            }
            new_str += watch.ElapsedMilliseconds+"";
            for(;i<6-1; i++)
            {
                new_str += ",0";
            }
            test_time_str.Add(new_str);
        }
        public frmMain()
        {
            InitializeComponent();
            f = Font;

            tab.SelectedIndex = 1;

            //[14-5-19 Test Time]
            test_time_str.Add(head);

            watch.Start();

            loadVAST_IDS();
            loadVAST_Firewall();
            loadVAST_Typology_Img();

            watch.Stop();

            //[14-5-19 Test Time]
            test_time_add((int)test_index.LOAD_SRC);

            //Util_FileReader.load_etree();

            resetAll();

            isCapture = 1;
            setCaptureUseVisualbility();

            //[6-15 UI SEARCH-DGV]
            ids_searcher = new Util_DGV_Searcher(this.dataGridViewAlerts);
            firewall_searcher = new Util_DGV_Searcher(this.dataGridViewFirewall);

            //[6-27 FILTER-DGV]
            this.cmbToolRela.SelectedIndex = 0;

        }

        public void loadMissingDTColumn()
        {
            Util_All.addMissingColumns(missingAlertDt);
        }


        #region Load Source Data


        //[VAST-DATA 4-1]
        public void loadVAST_Typology_Img()
        {
            string img_path = Util_ConfigData.getFilePath(Util_ConfigData.Enum_Scenario.VAST, Util_ConfigData.Enum_FileType.TOPOLOGY_IMG);
            Image image = Image.FromFile(img_path);
            imgBoxTypology.Image = image;
            imgBoxTypology.SizeMode = PictureBoxSizeMode.Zoom;
        }
        //[VAST-DATA 4-1]
        public void loadVAST_IDS()
        {
            string idspath = Util_ConfigData.getFilePath(Util_ConfigData.Enum_Scenario.VAST, Util_ConfigData.Enum_FileType.IDS);

            // dataGridViewAlerts.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //this.dataGridViewAlerts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ids_dt = Util_FileReader.dbLoadData(Util_ConfigData.getCurrentIDS());
            this.dataGridViewAlerts.DataSource = ids_dt;
            this.dataGridViewAlerts.RowsDefaultCellStyle.BackColor = Color.Ivory;
            this.dataGridViewAlerts.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;
            dataGridViewAlerts.Columns[0].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridViewAlerts.Columns[1].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewAlerts.Columns[2].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewAlerts.Columns[4].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewAlerts.Columns[6].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridViewAlerts.Columns[7].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewAlerts.Columns[3].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewAlerts.Columns[5].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewAlerts.ClearSelection();
        }

        private void setFirewallDGV_format()
        {
            this.dataGridViewFirewall.RowsDefaultCellStyle.BackColor = Color.Ivory;
            this.dataGridViewFirewall.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;
            //dataGridViewFirewall.Columns[0].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridViewFirewall.Columns[1].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridViewFirewall.Columns[2].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            //dataGridViewFirewall.Columns[3].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridViewFirewall.Columns[4].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            //dataGridViewFirewall.Columns[5].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            //dataGridViewFirewall.Columns[6].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridViewFirewall.Columns[7].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridViewFirewall.Columns[8].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridViewFirewall.Columns[9].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridViewFirewall.Columns[10].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridViewFirewall.Columns[11].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;

        }


        #endregion

        #region Capture Observations and Actions

        //[VAST-DATA 4-1]
        private List<SD> generateFirewallActObs(DataGridView dgv)
        {
            try
            {
                //[TRACE 3-24]
                List<SD> newSDs = new List<SD>();

                foreach (DataGridViewRow row in dgv.SelectedRows)
                {
                    String time = row.Cells["DataTime"].Value.ToString();
                    String src = row.Cells["SrcIP"].Value.ToString();
                    String sp = row.Cells["SrcPort"].Value.ToString();
                    String des = row.Cells["DesIP"].Value.ToString();
                    String dp = row.Cells["DesPort"].Value.ToString();
                    String proto = row.Cells["Protocol"].Value.ToString();
                    String prior = row.Cells["Priority"].Value.ToString();
                    String op = row.Cells["Operation"].Value.ToString();
                    String proc = row.Cells["Protocol"].Value.ToString();
                    String serv = row.Cells["DestService"].Value.ToString();
                    String dir = row.Cells["Direction"].Value.ToString();
                    String msg = row.Cells["MsgCode"].Value.ToString();
                    SD_FirewallLog newFirewall = new SD_FirewallLog(src, sp, des, dp, time, prior, op, msg, proc, serv, dir);
                    tempObs.sds.Add(newFirewall);
                    tempAct.sds.Add(newFirewall);

                    //[TRACE 3-24]
                    newSDs.Add(newFirewall);

                }

                return newSDs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[VAST-DATA 4-1]
        private List<SD> generateNetworkTypologyActObs(string host)
        {
            //[TRACE 3-24]
            List<SD> newSDs = new List<SD>();

            SD_NetCon newNetCon = new SD_NetCon(host, 0);
            tempObs.sds.Add(newNetCon);
            tempAct.sds.Add(newNetCon);

            newSDs.Add(newNetCon);

            return newSDs;
        }

        private List<SD> generateIDSLogActObs(DataGridView dgv)
        {

            try
            {
                List<SD_IDSlog> newIDSList = new List<SD_IDSlog>();
                foreach (DataGridViewRow dgvRow in dgv.SelectedRows)
                {
                    String src = Convert.ToString(((DataGridViewTextBoxCell)dgvRow.Cells["SourceIP"]).Value).Trim();
                    String des = Convert.ToString(((DataGridViewTextBoxCell)dgvRow.Cells["DestIP"]).Value).Trim();
                    String description = Convert.ToString(((DataGridViewTextBoxCell)dgvRow.Cells["Description"]).Value).Trim();
                    String[] array = Regex.Split(description, @"\s+");
                    String sid = array[0];
                    newIDSList.Add(new SD_IDSlog(src, des, sid));
                }
                tempObs.sds.AddRange(newIDSList);
                tempAct.sds.AddRange(newIDSList);

                SD_Alerts_Rela newAlertRela = null;
                if (newIDSList.Count > 1)
                {
                    newAlertRela = new SD_Alerts_Rela(newIDSList);
                    tempObs.sds.Add(newAlertRela);

                }

                //[TRACE 3-24]
                List<SD> newSDs = new List<SD>();
                newSDs.AddRange(newIDSList);
                return newSDs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<SD> generateNetConActObs(DataGridView dgv)
        {

            //[TRACE 3-24]
            List<SD> newSDs = new List<SD>();

            foreach (DataGridViewCell item in dgv.SelectedCells)
            {
                int srcIndex = item.RowIndex;
                int desIndex = item.ColumnIndex - 1;
                String src = Util_ConfigData.getNode(srcIndex);
                String des = Util_ConfigData.getNode(desIndex);
                String sid = item.Value.ToString();
                SD_NetCon newNetCon = new SD_NetCon(src, des, sid);
                tempObs.sds.Add(newNetCon);
                tempAct.sds.Add(newNetCon);

                //[TRACE 3-24]
                newSDs.Add(newNetCon);
            }

            return newSDs;

        }

        private List<SD> generatePacketDumpActObs(DataGridView dgv)
        {
            //[TRACE 3-24]
            List<SD> newSDs = new List<SD>();

            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                String time = row.Cells["Date"].Value + " " + row.Cells["Time"].Value;
                String src = row.Cells["SourceIP"].Value.ToString(); ;
                String des = row.Cells["DestinationIP"].Value.ToString(); ;
                String proto = row.Cells["Proto"].Value.ToString(); ;
                SD_PacketDump newPacket = new SD_PacketDump(src, des, time, proto);
                tempObs.sds.Add(newPacket);
                tempAct.sds.Add(newPacket);

                //[TRACE 3-24]
                newSDs.Add(newPacket);

            }

            return newSDs;
        }

        private List<SD> generateWebLogActObs(DataGridView dgv)
        {
            List<SD> newSDs = new List<SD>();

            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                String time = row.Cells["Date"].Value + " " + row.Cells["Time"].Value;
                String src = row.Cells["IP"].Value.ToString(); ;
                SD_WebServerLog newlog = new SD_WebServerLog(time, src);
                tempObs.sds.Add(newlog);
                tempAct.sds.Add(newlog);

                newSDs.Add(newlog);
            }

            return newSDs;
        }

        private List<SD> generateAuthLogActObs(DataGridView dgv)
        {
            List<SD> newSDs = new List<SD>();

            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                String time = row.Cells["Date"].Value + " " + row.Cells["Time"].Value;
                String type = row.Cells["Type"].Value.ToString(); ;
                String src = row.Cells["IP"].Value.ToString();
                String info = row.Cells["Information"].Value.ToString(); ;
                SD_AuthLog newlog = new SD_AuthLog(time, type, src, info);
                tempObs.sds.Add(newlog);
                tempAct.sds.Add(newlog);

                newSDs.Add(newlog);
            }

            return newSDs;
        }

        private List<SD> generateVulActObs(DataGridView dgv, int nodeIndex)
        {
            List<SD> newSDs = new List<SD>();

            String ip = Util_ConfigData.getIP(nodeIndex);
            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                String vulID = row.Cells["Vul ID"].Value.ToString(); ;
                SD_Vul newlog = new SD_Vul(ip, vulID);
                tempObs.sds.Add(newlog);
                tempAct.sds.Add(newlog);

                newSDs.Add(newlog);
            }

            return newSDs;
        }

        private List<SD> generatePortActObs(DataGridView dgv, int nodeIndex)
        {
            List<SD> newSDs = new List<SD>();

            String ip = Util_ConfigData.getIP(nodeIndex);
            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                String port = row.Cells["Port"].Value.ToString(); ;
                String state = row.Cells["State"].Value.ToString(); ;
                String serv = row.Cells["Service"].Value.ToString(); ;
                String vers = row.Cells["Version"].Value.ToString(); ;
                SD_Port newlog = new SD_Port(ip, port, state, serv, vers);
                tempObs.sds.Add(newlog);
                tempAct.sds.Add(newlog);

                newSDs.Add(newlog);
            }

            return newSDs;
        }

        private List<SD> generateVirActObs(DataGridView dgv, int nodeIndex)
        {
            List<SD> newSDs = new List<SD>();

            String ip = Util_ConfigData.getIP(nodeIndex);
            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                String time = row.Cells["Date"].Value.ToString(); ;
                String info = row.Cells["InforMation"].Value.ToString(); ;
                SD_VirLog newlog = new SD_VirLog(ip, time, info);
                tempObs.sds.Add(newlog);
                tempAct.sds.Add(newlog);

                newSDs.Add(newlog);
            }

            return newSDs;
        }

        private bool checkCaptureOrUsingIsSelected()
        {
            if (isCapture != 1 && isCapture != -1)
            {
                MessageBox.Show("First select [Captureing experience] or [Using experience] please.");
                return false;
            }
            return true;
        }


        //1: rootHT, 2:exist eu 0:can create observation
        private int canCreateObservation()
        {
           // bool result = true;

            if (lastHypo != null)
            {
                if (Util_All.isHTRoot(lastHypo.id))
                {
                    return 1;
                }


                if (lastHypo.next_unit != null)
                {
                    if (!lastHypo.next_unit.isEmpty())
                    {
                        return 2;
                    }
                }

            }
            return 0;
        }

        //[VAST-DATA 4-1]
        private void dataGridViewFirewall_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == ' ')
            {
                if (!checkCaptureOrUsingIsSelected())
                    return;

                if (this.dataGridViewFirewall.SelectedCells.Count > 0 && !isObsPoped)
                {
                    int canobs = canCreateObservation();
                    if (canobs == 2)
                    {
                        MessageBox.Show(
                            CanNotCreateObsMsg());
                        return;
                    }
                    else if (canobs == 1)
                    {
                        Con_Hypo newInitHypo = new Con_Hypo();
                        newInitHypo.id = Util_All.generateID(Util_All.init_h);
                        exTree.root.nextHypoList.Add(newInitHypo);
                        newInitHypo.pre_unit = exTree.root;
                        newInitHypo.parentHypo = hypoTree.root;
                        hypoTree.root.addChildrenHypo(newInitHypo);
                        lastHypo = newInitHypo;
                        canobs = 0;
                    }

                    if (canobs == 0)
                    {
                        List<SD> newSDs = generateFirewallActObs(dataGridViewFirewall);

                        //[TRACE 3-24]
                        addTraceItem(newSDs);

                        isObsPoped = true;
                        frmObservation fob = new frmObservation(this, tempObs, tempAct);
                        fob.ShowDialog();
                    }
                }
            }

        }

        //[VAST-DATA 4-1]
        public string getClickedHost(int x, int y)
        {
            int diff_range = 80;

            if (Math.Abs(x - 340) <= diff_range && Math.Abs(y - 336) <= diff_range)
            {
                return "DNS Root Servers";
            }
            else if (Math.Abs(x - 340) <= diff_range && Math.Abs(y - 517) <= diff_range)
            {
                return "Websites";
            }
            else if (Math.Abs(x - 543) <= diff_range && Math.Abs(y - 700) <= diff_range)
            {
                return "Workstation";
            }
            else if (Math.Abs(x - 702) <= diff_range && Math.Abs(y - 700) <= diff_range)
            {
                return "Financial Server";
            }
            else if (Math.Abs(x - 816) <= diff_range && Math.Abs(y - 700) <= diff_range)
            {
                return "IDS";
            }
            else if (Math.Abs(x - 942) <= diff_range && Math.Abs(y - 700) <= diff_range)
            {
                return "Log Server";
            }
            else if (Math.Abs(x - 1085) <= diff_range && Math.Abs(y - 700) <= diff_range)
            {
                return "DNS";
            }
            else if (Math.Abs(x - 928) <= diff_range && Math.Abs(y - 424) <= diff_range)
            {
                return "Corporate HQ Data Center";
            }
            else if (Math.Abs(x - 796) <= diff_range && Math.Abs(y - 428) <= diff_range)
            {
                return "Firewall 172.25.0.1";
            }
            else if (Math.Abs(x - 620) <= diff_range && Math.Abs(y - 521) <= diff_range)
            {
                return "Firewall 172.23.0.1";
            }
            return "";

        }

        private void dataGridViewAlerts_KeyPress(object sender, KeyPressEventArgs e)
        {
            // MessageBox.Show("key press");
            if (e.KeyChar == ' ')
            {
                if (!checkCaptureOrUsingIsSelected())
                    return;

                if (dataGridViewAlerts.SelectedRows.Count != 0 && !isObsPoped)
                {
                    int canobs = canCreateObservation();
                    if (canobs == 2)
                    {
                        MessageBox.Show(
                                CanNotCreateObsMsg());
                        return;
                    }
                    else if (canobs == 1)
                    {
                        Con_Hypo newInitHypo = new Con_Hypo();
                        newInitHypo.id = Util_All.generateID(Util_All.init_h);
                        exTree.root.nextHypoList.Add(newInitHypo);
                        newInitHypo.pre_unit = exTree.root;
                        newInitHypo.parentHypo = hypoTree.root;
                        hypoTree.root.addChildrenHypo(newInitHypo);
                        lastHypo = newInitHypo;
                        canobs = 0;
                    }

                    if (canobs == 0)
                    {

                        List<SD> newSDs = generateIDSLogActObs(dataGridViewAlerts);

                        //[TRACE 3-24]
                        addTraceItem(newSDs);

                        isObsPoped = true;
                        frmObservation fob = new frmObservation(this, tempObs, tempAct);
                        fob.ShowDialog();
                    }
                }
            }
        }


        #endregion

        /// <summary>
        /// [Trace 3-24] Add a new item in trace based on selected SDs
        /// </summary>
        /// <param name="newSDs"></param>
        private void addTraceItem(List<SD> newSDs)
        {
            try
            {
                if (newSDs.Count > 0)
                {
                    string op1 = Con_Operation.SDs_Op_String(Con_Operation.Enum_ObAc_Op.CHECKING, newSDs);
                    trace.AddItem(Util_All.currentTime(), op1);
                    string op2 = Con_Operation.SDs_Op_String(Con_Operation.Enum_ObAc_Op.FINDING, newSDs);
                    trace.AddItem(Util_All.currentTime(), op2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //reset all trees and obs and acts in memory
        private void resetAll()
        {
            try
            {
                resetObsAct();
                lastHypo = null;
                exTree = new Con_Experience_Tree();
                hypoTree = new Con_Hypo_Tree();
                init_hypo = hypoTree.root;
                if (hypoTree.root != null && exTree.root != null)
                {

                    exTree.root.preHypo = hypoTree.root;
                    hypoTree.root.next_unit = exTree.root;
                }

                //H-Tree
                initHTree();

                //begin [TRACE 3-24]
                trace = new Con_Observable_Trace();
                //end [TRACE 3-24]

                //isCapture = 0;
                //setCaptureUseVisualbility();
                isSearchReady = false;
                isOutofTree = true;
                isSearchClicked = false;
                ETNList.Clear();
                lastETreeNodeList.Clear();

                missingAlertDt = new DataTable();
                loadMissingDTColumn();

                btnHTContext.Visible = false;
                btnETContext.Visible = false;

                //[7-29]
                btnFilterRule.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void resetObsAct()
        {
            //statusAct = 2;
            confirmedAct = new Con_Actions();
            tempAct = new Con_Actions();
            confirmedObs = new Con_Observation();
            tempObs = new Con_Observation();
            confirmedHypo = new Con_Hypo();

            //[7-24 ONGOING OBS]
            if (currentobs != null)
                currentobs.clearList();
        }

        public Con_Experience_Unit newExperienceUnit(Con_Observation obs, Con_Actions act, Con_Hypo preHypo)
        {
            try
            {
                Con_Experience_Unit exU = new Con_Experience_Unit();

                if (exTree.isNull())
                {
                    //obs = new Con_Observation();
                    obs.sds.AddRange(tempObs.sds);
                    obs.sds.AddRange(confirmedObs.sds);
                    //exU = new Con_Experience_Unit(act, obs);
                    exU.setEU(null, obs);
                    Con_Hypo iniHypo = exTree.addFirstLevelEU(exU);

                    init_hypo.next_unit = exTree.root;
                    init_hypo.addChildrenHypo(iniHypo);
                }
                else
                {
                    if (preHypo != null)
                    {
                        obs.sds.AddRange(confirmedObs.sds);
                        act.sds.AddRange(confirmedAct.sds);
                        exU.setEU(act, obs);
                        exU.setPreHypo(preHypo);
                        preHypo.next_unit = exU;
                    }

                }
                return exU;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[TRACE 3-24] add a parameter :  hypoop/ this is used to distinguish the call from NEW hypo and the call from ADD A CHILD
        public Con_Hypo newHypo(Con_Experience_Unit preEx, Con_Experience_Unit nextEx, Con_Hypo hypo, Con_Operation.Enum_Hypo_Op hypoop)
        {
            try
            {
                //set up E-tree
                Con_Hypo newhp = new Con_Hypo(hypo);
                newhp.pre_unit = preEx;
                newhp.next_unit = nextEx;
                preEx.nextHypoList.Add(newhp);
                if (nextEx != null)
                    nextEx.preHypo = newhp;


                //begin [TRACE 3-24]
                string op = "";
                if (lastHypo == null || lastHypo.id.Contains("HT"))
                {
                    op = Con_Operation.Hypo_Op_String(hypoop, null, newhp);
                }
                else
                {
                    op = Con_Operation.Hypo_Op_String(hypoop, lastHypo, newhp);
                }
                trace.AddItem(Util_All.currentTime(), op);
                //end 

                lastHypo = newhp;

                //set up H-tree

                if (newhp.next_unit != null && nextEx != null)
                {
                    newhp.setChildrenHypo(nextEx.nextHypoList);
                }


                if (preEx != null)
                {
                    Con_Hypo parentHypo = preEx.preHypo;
                    if (parentHypo == hypoTree.root)
                    {
                        //parentHypo = hypoTree.root;
                        hypoTree.root.next_unit = preEx;
                    }

                    parentHypo.addChildrenHypo(newhp);
                    newhp.setParentHypo(parentHypo);

                }

                return newhp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //click Hypothesis
        public void actionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                bool euExist = false;
                bool initht = false;
                bool isHTRoot = false;

                //new generated eu or existing eu
                if (lastHypo == null || hypoTree.isNull())
                {
                    initht = true;
                }
                //case 1: eu exist
                if (lastHypo != null)
                {
                    if (lastHypo.next_unit != null)
                    {
                        if (!lastHypo.next_unit.isEmpty())
                        {
                            euExist = true;
                        }
                    }

                    if (Util_All.isHTRoot(lastHypo.id))
                    {
                        isHTRoot = true;
                    }
                }
                //case 2: new generated eu
                if (!euExist && !isHTRoot)
                {
                    if (confirmedObs == null)
                    {
                        MessageBox.Show("Sorry, hypothesis can't be made without observation");
                        return;
                    }
                    else
                    {
                        if (confirmedObs.isNull())
                        {
                            MessageBox.Show("Sorry, hypothesis can't be made without observation");
                            return;
                        }
                    }
                }


                Con_Experience_Unit exU = null;

                //lastHypo == init

                //confirmedObs can't be null because of the previous if else
                if (isHTRoot)
                {
                   // exU = exTree.root;
                    Con_Hypo new_init_hypo = new Con_Hypo();
                    new_init_hypo.id = Util_All.generateID(Util_All.init_h);
                    new_init_hypo.pre_unit = exTree.root;
                    exTree.root.nextHypoList.Add(new_init_hypo);
                    new_init_hypo.parentHypo = lastHypo;
                    lastHypo.childrenHypos.Add(new_init_hypo);
                    lastHypo = new_init_hypo;
                    
                    exU = null;

                    
                    frmHypothesis fhy = new frmHypothesis(this, confirmedObs, confirmedHypo);
                    fhy.ShowDialog();
                }
                else
                {
                    if (!euExist)
                    {
                        exU = null;
                        frmHypothesis fhy = new frmHypothesis(this, confirmedObs, confirmedHypo);
                        fhy.ShowDialog();
                    }
                    else //eu exist: lasthypo != null && lasthypo.next_unit != empty
                    {
                        exU = lastHypo.next_unit;
                        frmHypothesis fhy = new frmHypothesis(this, exU.obs, confirmedHypo);
                        fhy.ShowDialog();
                    }
                }

                if (!isfrmHypothesisDone)
                    return;
                isfrmHypothesisDone = false;

                if (exU == null)
                {

                    Con_Observation obs = new Con_Observation();
                    Con_Actions act = new Con_Actions();
                    exU = newExperienceUnit(obs, act, lastHypo);

                    if (exU == null || exU.isEmpty())
                    {
                        MessageBox.Show(
@"No parent hypothsis found. 

Please double click a node in the H-Tree to make 
it be the parent of the new hypothesis.");
                        return;
                    }
                }



                //new Hypothesis
                Con_Hypo thenewhypo = newHypo(exU, null, confirmedHypo, Con_Operation.Enum_Hypo_Op.NEW_2);

                //if (isHTRoot)
                //{
                //    thenewhypo.id = "INIT-H-" + thenewhypo.id;
                //    thenewhypo.parentHypo = hypoTree.root;
                //    hypoTree.root.addChildrenHypo(thenewhypo);

                //    hypoTree.root.next_unit = exTree.root;

                //}
                if (ETNList.Count < 1)
                {
                    //Search(Con_Hypo.getContextObs(lastHypo));
                }
                else if (lastHypo != null)
                {
                    sortETNList(Con_Hypo.getContextObs(lastHypo));
                }


                if (isOutofTree && isCapture == -1 && ETNList.Count > 0)
                {
                    MessageBox.Show("Your action is out of the experience.");
                    isOutofTree = false;
                }


                //Clear
                resetObsAct();

                if (initht)
                {
                    initHTree();
                }
                else
                {
                    loadHypoTree();
                }
                refreshETree();

                updateLastHypoLocation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        #region [06-1 UI][06-1 SNIP]
        public void refreshETree()
        {
            try
            {

                if (exTree.isNull())
                    return;

                treeViewTree.Nodes.Clear();
                treeViewTree.BeginUpdate();

                Con_Experience_Unit treeroot = exTree.root;
                TreeNode treerootNode = new TreeNode();
                treerootNode.Text = treeroot.id;

                //[7-25]
                id2et.Remove(treeroot.id);
                id2et.Add(treeroot.id, treerootNode);

                treerootNode.Tag = treeroot;
                treerootNode.ToolTipText = "N";

                addETreeChildren(treerootNode, treeroot);

                treeViewTree.Nodes.Add(treerootNode);

                updateTreeViewFont();
                treeViewTree.EndUpdate();
                treeViewTree.Refresh();
                treeViewTree.ExpandAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addETreeChildren(TreeNode parentNode, Con_Experience_Unit parent)
        {
            try
            {
                foreach (Con_Hypo hypo in parent.nextHypoList)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Text = hypo.id;

                    //[7-25]

                    id2et.Remove(hypo.id);
                    id2et.Add(hypo.id, childNode);

                    childNode.Tag = hypo;
                    childNode.ToolTipText = hypo.truthValue + "";

                    Con_Experience_Unit nextEU = hypo.next_unit;
                    if (nextEU != null)
                    {
                        TreeNode nextEUNode = new TreeNode();
                        nextEUNode.Text = nextEU.id;

                        //[7-25]
                        id2et.Remove(nextEU.id);
                        id2et.Add(nextEU.id, nextEUNode);


                        nextEUNode.Tag = nextEU;
                        nextEUNode.ToolTipText = "N";

                        addETreeChildren(nextEUNode, nextEU);

                        //Util_All.setTreeNodeFont(nextEUNode);
                        childNode.Nodes.Add(nextEUNode);
                    }

                    //Util_All.setTreeNodeFont(childNode);
                    parentNode.Nodes.Add(childNode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //private void showTreeView()
        //{
        //    treeViewTree.Nodes.Clear();
        //    treeViewTree.BeginUpdate();
        //    Util_FileReader.loadExperienceTree(treeViewTree);
        //    updateTreeViewFont();
        //    treeViewTree.EndUpdate();
        //    treeViewTree.Refresh();
        //}




        //E-tree TreeView
        private void treeViewTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode currentNode = treeViewTree.SelectedNode;

                if (currentNode != null)
                {
                    txtCurrentNodeID.Text = currentNode.Text;

                    string nodeType = Util_All.checkNodeType(currentNode.Text);

                    if (currentNode.Tag != null)
                    {
                        if (nodeType.Equals("EU"))
                        {
                            updateDetailVisibility("TreeView");
                            Util_FileReader.loadExperienceDetail(this.treeViewDetail, (Con_Experience_Unit)currentNode.Tag);
                            this.treeViewDetail.ExpandAll();

                        }
                        else if (nodeType.Equals("H"))
                        {
                            updateDetailVisibility("TextBox");
                            this.textBoxDetail.Text = ((Con_Hypo)currentNode.Tag).hypo;

                        }
                    }
                    else
                    {
                        if (nodeType.Equals("EU"))
                        {
                            updateDetailVisibility("TreeView");
                            Util_FileReader.loadExperienceDetail(this.treeViewDetail, Util_All.trimMarker(currentNode.Text));
                            this.treeViewDetail.ExpandAll();
                        }
                        else if (nodeType.Equals("H"))
                        {
                            updateDetailVisibility("TextBox");
                            this.textBoxDetail.Text = ((Con_Hypo)currentNode.Tag).hypo;
                        }
                    }
                }
                else
                {
                    txtCurrentNodeID.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Detail View
        private void updateDetailVisibility(string option)
        {
            if (option.Equals("TreeView"))
            {
                this.treeViewDetail.Visible = true;
                this.textBoxDetail.Visible = false;
            }
            else if (option.Equals("TextBox"))
            {
                this.treeViewDetail.Visible = false;
                this.textBoxDetail.Visible = true;
            }
        }




        //E-tree
        public void updateTreeViewFont()
        {
            try
            {
                this.treeViewTree.Visible = true;
                this.textBoxDetail.Visible = false;
                lastETreeNodeList.Clear();
                updateTreeNodeFont(this.treeViewTree.Nodes);
                //treeViewTree.ExpandAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void updateTreeNodeFont(TreeNodeCollection list)
        {

            try
            {
                foreach (TreeNode node in list)
                {
                    #region Current Marker

                    node.Text = Util_All.trimMarker(node.Text);
                    if (node.ImageIndex == 9)
                    {
                        lastETreeNodeList.Add(node);
                        node.Text += Util_All.getCurrentMarker();
                    }
                    if (node.Text.Substring(0, 1).Equals("H"))
                    {
                        Con_Hypo hypo = (Con_Hypo)node.Tag;
                        if (hypo.type == 1)
                        {
                            node.Text += Util_All.getMissingMarker();
                        }
                        else if (hypo.type == 2)
                        {
                            node.Text += Util_All.getFalseMarker();
                        }
                    }

                    #endregion

                    int truthvalue = 2;
                    if (node.ToolTipText.Equals("N"))
                        truthvalue = 0;
                    else if (node.ToolTipText.Equals("-1"))
                        truthvalue = -1;
                    else if (node.ToolTipText.Equals("1"))
                        truthvalue = 1;


                    Util_All.setTreeNodeFont(f, node, truthvalue);



                    updateTreeNodeFont(node.Nodes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void capturingExperienceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //txtGuide.Text = "Select items in any data source, and then press [Space] key.";
                isCapture = 1;
                setCaptureUseVisualbility();
                resetAll();
                treeViewTree.Nodes.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void usingExperienceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //txtGuide.Text = "Click [Experience Search] when you finish observing";
                if (isCapture != -1)
                {
                    treeViewTree.Nodes.Clear();
                }
                isCapture = -1;
                setCaptureUseVisualbility();
                resetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void setCaptureUseVisualbility()
        {
            try
            {
                if (isCapture == 1)
                {
                    //this.menuHypothesis.Visible = true;
                    this.btnFinish.Visible = true;
                    //this.btnObservationBegin.Visible = false;
                    // this.btnObservationEnd.Visible = false;
                    this.menuCaptureEx.BackColor = Color.LightSalmon;
                    this.menuUseEx.BackColor = Control.DefaultBackColor;
                }
                else if (isCapture == -1)
                {
                    //this.menuHypothesis.Visible = false;
                    this.btnFinish.Visible = false;
                    //this.btnObservationBegin.Visible = true;
                    //this.btnObservationEnd.Visible = true;
                    this.menuCaptureEx.BackColor = Control.DefaultBackColor;
                    this.menuUseEx.BackColor = Color.LightSalmon;
                }
                else
                {
                    //this.menuHypothesis.Visible = false;
                    this.btnFinish.Visible = false;
                    //this.btnObservationBegin.Visible = false;
                    // this.btnObservationEnd.Visible = false;
                    this.menuCaptureEx.BackColor = Control.DefaultBackColor;
                    this.menuUseEx.BackColor = Control.DefaultBackColor;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                //txtGuide.Text = "Select [Capturing Experience] or [Using Experience] on the top";

                //[7-23]
                acsc.Add("4/5/2012 10:");
                acsc.Add("Generic Protocol Command Decode");
                acsc.Add("Misc activity");
                acsc.Add("[1:2000355:5] ET POLICY IRC authorization message");

                this.txtToolFilterValue.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
                this.txtToolFilterValue.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
                this.txtToolFilterValue.AutoCompleteCustomSource = acsc;

                //[7-24 ONGOING OBS]
                currentobs = new frmCurrentObservation(this);
                currentobs.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void updateAutoComplete(string str)
        {
            this.txtToolFilterValue.AutoCompleteCustomSource.Add(str);
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {

            //MessageBox.Show("click");
            try
            {
                // txtGuide.Text = "Click [H-Tree] on the top to get the big picture";

                Con_Observation obs = new Con_Observation();
                Con_Actions act = new Con_Actions();
                Con_Experience_Unit exU = null;

                if (confirmedObs.sds.Count > 0 || tempObs.sds.Count > 0)
                {
                    exU = newExperienceUnit(obs, act, lastHypo);
                }
                if (exTree.root != null)
                {
                    //Util_FileWriter.outputExperienceTree(exTree);
                    Util_FileWriter.addExperienceTree(exTree);
                    MessageBox.Show("Experience has been writtern in files!");

                    //begin [TRACE 3-24]
                    if (trace != null && !trace.isEmpty())
                        trace.Output_XML();
                    //end [TRACE 3-24]

                    //begin [4-08 GRAPHVIZ]
                    graph.addETree(exTree);
                    if (trace != null && !trace.isEmpty())
                    {
                        graph.addTrace(trace);
                    }
                    graph.finishGraph();
                    //end [4-08 GRAPHVIZ]

                    if(Util_FileWriter.writeTestTimes(test_time_str))
                    {
                        MessageBox.Show("Test data wrote!");
                    }
                }



                //resetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void sortETNList(LinkedList<Con_Observation> obsList)
        {
            //isOutofTree = true;
            //foreach (Util_ETN_Score etn in ETNList)
            //{
            //    bool outTree = etn.updateScoreTree(obsList);
            //    if (!outTree)
            //        isOutofTree = false;
            //}

            //ETNList.Sort(new Util_ETN_Compare());
            ////updateTreeNodeFont(treeViewTree.Nodes);
            //updateTreeViewFont();

            //List<string> alertStrings = new List<string>();
            //foreach (TreeNode node in lastETreeNodeList)
            //{
            //    if (node.ToolTipText.Equals("-1"))
            //    {
            //        alertStrings.Add(Util_All.trimMarker(node.Text) + "\r\n");
            //    }
            //}
            //if (alertStrings.Count > 0)
            //{
            //    string alert = "ATTENTION PLEASE-- existing Negative hypothesis:\r\n";
            //    foreach (string str in alertStrings)
            //    {
            //        alert += str;
            //    }
            //    MessageBox.Show(alert);
            //}
        }


        public void updateSearchTreeView()
        {
            //treeViewTree.Nodes.Clear();
            //treeViewTree.BeginUpdate();


            //foreach (Util_ETN_Score etn in ETNList)
            //{
            //    //Util_All.setTreeNodeFont(etn.tree);
            //    this.treeViewTree.Nodes.Add(etn.tree);
            //}

            //updateTreeViewFont();
            //treeViewTree.EndUpdate();
            //treeViewTree.Refresh();
        }



        //[6-01 UI]
        #region H-Tree

        public Font f2 = new Font("Segoe UI", 12);
        public TreeNode HTselectedNode = null;
        public string HTcurrentNodeTag = " ■ ";

        //[TRACE 3-24] 
        string lastTruthValue = "Unknown"; //not represented as digital, say 0/1/-1, but True/False/Unknown
        string lastHypoContent = "";


        public void initHTree()
        {
            HTselectedNode = null;

            f2 = Font;

            loadHypoTree();

            BindComboBox();
            cmb_Temp.Visible = false;
            cmb_Temp.SelectedIndexChanged += new EventHandler(cmb_Temp_SelectedIndexChanged);
            this.dataGridViewDetail.Controls.Add(cmb_Temp);

        }

        #region Load form and load HypoTree by a Con_Hypo


        public void loadHypoTree()
        {//Treenode.tag is a Con_hypo instance
            treeHypo.Nodes.Clear();

            if (hypoTree != null && !hypoTree.isNull())
            {
                Con_Hypo root = hypoTree.root;
                TreeNode treeRoot = new TreeNode();
                treeRoot.Text = root.id;

                //[7-25]
                id2ht.Remove(root.id);
                id2ht.Add(root.id, treeRoot);


                if (root.type == 1)
                {
                    treeRoot.Text += Util_All.getMissingMarker();
                }
                else if (root.type == 2)
                {
                    treeRoot.Text += Util_All.getFalseMarker();
                }
                if (root == lastHypo)
                {
                    treeRoot.Text += HTcurrentNodeTag;
                }
                treeRoot.Tag = root;


                Util_All.setTreeNodeFont(f, treeRoot, root.truthValue);
                //Util_All.setTreeNodeFont(treeRoot);
                treeHypo.Nodes.Add(treeRoot);
                fillChildTree(root.childrenHypos, treeRoot);

                treeHypo.ExpandAll();
                //lblLastHypo.Text = main.lastHypo.id;

                updateTreeViewFont();
            }
            //treeHypo.HideSelection = false;
        }

        public void fillChildTree(List<Con_Hypo> list, TreeNode treeParent)
        {
            foreach (Con_Hypo child in list)
            {
                TreeNode treeChild = new TreeNode();
                treeChild.Text = child.id;

                //[7-25]
                id2ht.Remove(child.id);
                id2ht.Add(child.id, treeChild);

                if (child.type == 1)
                {
                    treeChild.Text += Util_All.getMissingMarker();
                }
                else if (child.type == 2)
                {
                    treeChild.Text += Util_All.getFalseMarker();
                }

                if (child == lastHypo)
                {
                    treeChild.Text += HTcurrentNodeTag;
                }

                treeChild.Tag = child;

                Util_All.setTreeNodeFont(f, treeChild, child.truthValue);

                List<Con_Hypo> childList = child.childrenHypos;
                fillChildTree(childList, treeChild);
                //Util_All.setTreeNodeFont(treeChild);
                treeParent.Nodes.Add(treeChild);
            }
        }

        //update current treeview color and * tag
        public void updateTreeView(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                Con_Hypo hypo = (Con_Hypo)node.Tag;
                Util_All.setTreeNodeFont(f, node, hypo.truthValue);



                node.Text = hypo.id;

                //[7-26]
                if (id2et.ContainsKey(hypo.id))
                {
                    TreeNode et_node = id2et[hypo.id];
                    if (et_node != null)
                    {
                        Util_All.setTreeNodeFont(f, et_node, hypo.truthValue);
                    }
                }

                //end [7-26]

                if (hypo.type == 1)
                {
                    node.Text += Util_All.getMissingMarker();
                }
                else if (hypo.type == 2)
                {
                    node.Text += Util_All.getFalseMarker();
                }

                if (node == HTselectedNode)
                {
                    node.Text += HTcurrentNodeTag;
                }

                updateTreeView(node.Nodes);
            }


        }


        private void updateTreeViewAndDataGridView()
        {
            dgvBindData();

            this.cmb_Temp.Visible = false;

            updateTreeView(treeHypo.Nodes);
            // lblLastHypo.Text = selectedNode.Text;
        }

        #endregion



        #region Bind TreeHypo, ComboBox, DataGridView Data



        private void BindComboBox()
        {
            DataTable dtValue = new DataTable();
            dtValue.Columns.Add("Value");
            dtValue.Columns.Add("Name");
            DataRow drValue = dtValue.NewRow();
            drValue[0] = "1";
            drValue[1] = "True";
            dtValue.Rows.Add(drValue);
            drValue = dtValue.NewRow();
            drValue[0] = "-1";
            drValue[1] = "False";
            dtValue.Rows.Add(drValue);
            drValue = dtValue.NewRow();
            drValue[0] = "0";
            drValue[1] = "Unknown";
            dtValue.Rows.Add(drValue);
            cmb_Temp.ValueMember = "Value";
            cmb_Temp.DisplayMember = "Name";
            cmb_Temp.DataSource = dtValue;
            cmb_Temp.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void dgvBindData()
        {
            if (HTselectedNode != null)
            {
                Con_Hypo hypo = (Con_Hypo)HTselectedNode.Tag;
                if (hypo != null)
                {
                    DataTable dt = Util_All.getHypoDetail(hypo);
                    dataGridViewDetail.DataSource = dt;
                    dataGridViewDetail.Columns[0].ReadOnly = true;
                    //color changed in DataBindingComplete
                }
                if (hypo.truthValue == -1)
                {
                    dataGridViewDetail.BackgroundColor = Color.LightGray;
                }

                dataGridViewDetail.Columns[0].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewDetail.Columns[1].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
        }

        #endregion



        #region Visible/Invisible ComboBox

        private void dataGridViewDetail_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.cmb_Temp.Visible = false;
            //treeHypo.Focus();
        }

        private void dataGridViewDetail_Scroll(object sender, ScrollEventArgs e)
        {
            this.cmb_Temp.Visible = false;
            //treeHypo.Focus();
        }

        private void dataGridViewDetail_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            // treeHypo.Focus();
            if (dataGridViewDetail.Rows[0].Cells[1].Value != null)
            {
                dataGridViewDetail.Rows[0].Cells[1].Tag = dataGridViewDetail.Rows[0].Cells[1].Value.ToString();
                if (dataGridViewDetail.Rows[0].Cells[1].Value.ToString() == "1")
                {
                    dataGridViewDetail.Rows[0].Cells[1].Value = "True";
                }
                else if (dataGridViewDetail.Rows[0].Cells[1].Value.ToString() == "-1")
                {
                    dataGridViewDetail.Rows[0].Cells[1].Value = "False";
                    dataGridViewDetail.CurrentRow.DefaultCellStyle.BackColor = Color.LightGray;
                }
                else if (dataGridViewDetail.Rows[0].Cells[1].Value.ToString() == "0")
                {
                    dataGridViewDetail.Rows[0].Cells[1].Value = "Unknown";
                }

            }
        }

        private void dataGridViewDetail_CurrentCellChanged(object sender, EventArgs e)
        {

        }


        private void dataGridViewDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewDetail.CurrentCell.RowIndex == 0 && dataGridViewDetail.CurrentCell.ColumnIndex == 1)
            {
                Rectangle rect = dataGridViewDetail.GetCellDisplayRectangle(dataGridViewDetail.CurrentCell.ColumnIndex, dataGridViewDetail.CurrentCell.RowIndex, false);
                string truthValue = dataGridViewDetail.CurrentCell.Value.ToString();

                if (truthValue.Equals("1") || truthValue.Equals("True"))
                {
                    cmb_Temp.Text = "True";
                }
                else if (truthValue.Equals("-1") || truthValue.Equals("False"))
                {
                    cmb_Temp.Text = "False";
                }
                else if (truthValue.Equals("0") || truthValue.Equals("Unknown"))
                {
                    cmb_Temp.Text = "Unknown";
                }
                cmb_Temp.Left = rect.Left;
                cmb_Temp.Top = rect.Top;
                cmb_Temp.Width = rect.Width;
                cmb_Temp.Height = rect.Height;
                cmb_Temp.Visible = true;

            }
            else
            {
                cmb_Temp.Visible = false;

                if (dataGridViewDetail.CurrentCell.RowIndex == 2 && dataGridViewDetail.CurrentCell.ColumnIndex == 1)
                {
                    //[TRACE 3-24]
                    lastHypoContent = dataGridViewDetail.CurrentCell.Value.ToString();
                }
            }

        }

        #endregion


        private void cmb_Temp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HTselectedNode == null)
                return;

            Con_Hypo nodehypo = (Con_Hypo)HTselectedNode.Tag;

            //[TRACE 3-24]
            lastTruthValue = nodehypo.truthValueString();

            //if (((ComboBox)sender).Text == "True")
            if (cmb_Temp.SelectedIndex == 0)
            {
                dataGridViewDetail.CurrentCell.Value = "True";
                dataGridViewDetail.CurrentCell.Tag = "1";

                if (HTselectedNode != null)
                {
                    //Con_Hypo nodehypo = (Con_Hypo)selectedNode.Tag;
                    nodehypo.truthValue = 1;


                }

            }
            //else if (((ComboBox)sender).Text == "False")
            else if (cmb_Temp.SelectedIndex == 1)
            {
                dataGridViewDetail.CurrentCell.Value = "False";
                dataGridViewDetail.CurrentCell.Tag = "-1";

                //set all decedents false
                if (HTselectedNode != null)
                {
                    //Con_Hypo nodehypo = (Con_Hypo)selectedNode.Tag;
                    nodehypo.setFalse();//[7-27 changed] orginal: not only this one, but also its offsprings
                }

                // dataGridViewDetail.CurrentRow.DefaultCellStyle.BackColor = Color.LightGray;
            }
            else
            {
                dataGridViewDetail.CurrentCell.Value = "Unknown";
                dataGridViewDetail.CurrentCell.Tag = "0";

                if (HTselectedNode != null)
                {
                    //Con_Hypo nodehypo = (Con_Hypo)selectedNode.Tag;
                    nodehypo.truthValue = 0;

                }
            }

            //begin [TRACE 3-24]
            string currentTruthValue = dataGridViewDetail.CurrentCell.Value.ToString();
            if (!currentTruthValue.Equals(lastTruthValue))
            {
                string op = Con_Operation.Hypo_Op_String(Con_Operation.Enum_Hypo_Op.CHANGE_TRUTH_VALUE_3, nodehypo, lastTruthValue, currentTruthValue);
                trace.AddItem(Util_All.currentTime(), op);
            }
            //end

            updateTreeViewAndDataGridView();
        }



        private void treeHypo_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            if (treeHypo.SelectedNode != null)
            {
                HTselectedNode = treeHypo.SelectedNode;
                dgvBindData();
            }
        }


        private void btnAddChild_Click_1(object sender, EventArgs e)
        {
            //try
            //{
            //    if (treeHypo.SelectedNode == null)
            //    {
            //        MessageBox.Show("You need to select a node in the tree first :)");
            //        return;
            //    }
            //    else
            //    {
            //        Con_Hypo hypo = (Con_Hypo)treeHypo.SelectedNode.Tag;
            //        Con_Experience_Unit nextEU = hypo.next_unit;

            //        frmHypothesis fhp = null;
            //        if (nextEU == null)
            //        {
            //            MessageBox.Show("Sorry, you can't make new hypothesis because there is no new observation got after generating " + hypo.id + "\r\n You'd better generate a sibling hypothesis of " + hypo.id);
            //            return;
            //        }
            //        else
            //        {
            //            fhp = new frmHypothesis(this, nextEU.obs, confirmedHypo);

            //        }
            //        fhp.ShowDialog();

            //        if (!isfrmHypothesisDone)
            //            return;

            //        isfrmHypothesisDone = false;

            //        lastHypo = hypo;

            //        newHypo(nextEU, null, confirmedHypo, Con_Operation.Enum_Hypo_Op.ADD_CHILD_2);

            //        //this.treeHypo.Focus();
            //        //updateTreeViewAndDataGridView();   

            //        treeHypo.Nodes.Clear();
            //        loadHypoTree();
            //        dgvBindData();

            //        refreshETree();
            //        updateLastHypoLocation();

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }


        private void dataGridViewDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewDetail.CurrentCell.ColumnIndex == 1)
            {
                Con_Hypo hypo = (Con_Hypo)HTselectedNode.Tag;
                int row = dataGridViewDetail.CurrentCell.RowIndex;
                if (row == 1)
                {
                    MessageBox.Show("Sorry, you can't edit ID.");
                }
                else if (row == 2)
                {
                    //begin [TRACE 3-24]
                    lastHypoContent = hypo.hypo;
                    string currentHypoContent = dataGridViewDetail.CurrentCell.Value.ToString();
                    if (!lastHypoContent.Equals(currentHypoContent))
                    {
                        string op = Con_Operation.Hypo_Op_String(Con_Operation.Enum_Hypo_Op.CHANGE_CONTENT_3, hypo, lastHypoContent, currentHypoContent);
                        trace.AddItem(Util_All.currentTime(), op);
                    }
                    //end

                    hypo.hypo = dataGridViewDetail.CurrentCell.Value.ToString();
                }
            }
        }


        //[7-25]
        private void highlightPath(TreeNode node)
        {
            TreeNode currentnode = node;

            while (currentnode != null)
            {
                currentnode.BackColor = Color.LightBlue;
                currentnode = currentnode.Parent;
            }
        }

        private void treeHypo_DoubleClick(object sender, EventArgs e)
        {

            HTselectedNode = treeHypo.SelectedNode;

            //[7-25] show current EU

            if (HTselectedNode != null)
            {
                Con_Hypo hypo = (Con_Hypo)HTselectedNode.Tag;

                if (hypo != null)
                {
                    //begin [TRACE 3-24]
                    string op = Con_Operation.Hypo_Op_String(Con_Operation.Enum_Hypo_Op.JUMP_FROM_TO_2, lastHypo, hypo);
                    trace.AddItem(Util_All.currentTime(), op);
                    //end

                    if (Util_All.isHTRoot(hypo.id))
                    {
                        lastHypo = hypoTree.root;

                        updateTreeViewFont();

                        updateTreeViewAndDataGridView();

                        btnHTContext.Visible = true;
                        btnHTContext.Location = new Point(HTselectedNode.Bounds.Right + 5, HTselectedNode.Bounds.Bottom - 2);
                        TreeNode currentnode_et = id2et[exTree.root.id];

                        if (currentnode_et != null)
                        {
                            btnETContext.Visible = true;
                            btnETContext.Location = new Point(currentnode_et.Bounds.Right + 5, currentnode_et.Bounds.Bottom - 2);
                        }
                        else
                        {
                            btnETContext.Visible = false;
                        }
                        highlightPath(currentnode_et);
                        highlightPath(HTselectedNode);
                    }
                    else
                    {

                        lastHypo = hypo;
                        LinkedList<Con_Observation> obsList = Con_Hypo.getContextObs(hypo);

                        sortETNList(obsList);
                        updateTreeViewFont();

                        updateTreeViewAndDataGridView();


                        //[7-25]
                        btnHTContext.Visible = true;
                        btnHTContext.Location = new Point(HTselectedNode.Bounds.Right + 5, HTselectedNode.Bounds.Bottom - 2);
                        TreeNode currentnode_et = null;
                        if (!id2et.ContainsKey(lastHypo.id) && id2et.ContainsKey(exTree.root.id))
                        {
                            currentnode_et = id2et[exTree.root.id];
                        }
                        else
                        {
                            currentnode_et = id2et[lastHypo.id];
                        }

                        if (currentnode_et != null)
                        {
                            btnETContext.Visible = true;
                            btnETContext.Location = new Point(currentnode_et.Bounds.Right + 5, currentnode_et.Bounds.Bottom - 2);
                        }
                        else
                        {
                            btnETContext.Visible = false;
                        }

                        highlightPath(currentnode_et);
                        highlightPath(HTselectedNode);
                    }
                }
            }
        }




        #endregion


        //[6-01 SNIP]
        #region Snipping
        private void frmMain_Activated(object sender, EventArgs e)
        {
            MyTest.HotKey.RegisterHotKey(Handle, 102, MyTest.HotKey.KeyModifiers.Alt | MyTest.HotKey.KeyModifiers.Ctrl, Keys.S);
        }

        private void frmMain_Leave(object sender, EventArgs e)
        {
            MyTest.HotKey.UnregisterHotKey(Handle, 102);
        }

        public void clearSnipBox()
        {
            snippingBox.Image = null;
            snippingBox.Visible = false;
        }

        private String CanNotCreateObsMsg()
        {
            return 
@"Can't generate new observation when current observation exists.

Current hypothesis is " + lastHypo.id + @", it's following/child 
Action-Observation unit exists: " + lastHypo.next_unit.id + @".

You can first create new hypothesis/thought in two ways:
(1) Create a sibling hypothesis/thought of the current hypothesis
(2) Create new hypothesis/thought based on the child EU (observation) of the 
current hypothesis
Then, you can create new observation under this new hypothesis/thought.
";
        }

        private void captureAScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int canobs = canCreateObservation();
                if (canobs == 2)
                {
                    MessageBox.Show(
                        CanNotCreateObsMsg());
                    return;
                }
                else if (canobs == 1)
                {
                    Con_Hypo newInitHypo = new Con_Hypo();
                    newInitHypo.id = Util_All.generateID(Util_All.init_h);
                    exTree.root.nextHypoList.Add(newInitHypo);
                    newInitHypo.pre_unit = exTree.root;
                    newInitHypo.parentHypo = hypoTree.root;
                    hypoTree.root.addChildrenHypo(newInitHypo);
                    lastHypo = newInitHypo;
                    canobs = 0;
                }

                if (canobs == 0)
                {
                    CaptureImageTool capture = new CaptureImageTool();
                    //capture.SelectCursor = new Cursor(Properties.Resources.Arrow_M.Handle); 
                    if (capture.ShowDialog() == DialogResult.OK)
                    {
                        Image image = capture.Image;

                        frmSnipObservation newFrmSObs = new frmSnipObservation(this, image);
                        newFrmSObs.ShowDialog();


                        refreshETree();


                    }

                    clearSnipBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void selectRawDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int canobs = canCreateObservation();
            if (canobs == 2)
            {
                MessageBox.Show(
                    CanNotCreateObsMsg());
                return;
            }
            else if (canobs == 1)
            {
                Con_Hypo newInitHypo = new Con_Hypo();
                newInitHypo.id = Util_All.generateID(Util_All.init_h);
                exTree.root.nextHypoList.Add(newInitHypo);
                newInitHypo.pre_unit = exTree.root;
                newInitHypo.parentHypo = hypoTree.root;
                hypoTree.root.addChildrenHypo(newInitHypo);
                lastHypo = newInitHypo;
                canobs = 0;
            }

            if (canobs == 0)
            {
                this.tab.SelectedIndex = 0;
            }

        }


        #endregion


        #region Endorse: ILike
        //[6-15 UI ILIKE]
        private void hTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CaptureImageTool capture = new CaptureImageTool();
            //capture.SelectCursor = new Cursor(Properties.Resources.Arrow_M.Handle); 
            if (capture.ShowDialog() == DialogResult.OK)
            {
                Image image = capture.Image;
                String path = Util_All.storeIlikeImage(image);


                string op = Con_Operation.Sys_Endorse_String(Con_Operation.Enum_Sys_Endorse.LIKE_SCREENSHOT, path);
                trace.AddItem(Util_All.currentTime(), op);
                MessageBox.Show("Endorse Suceed! Thank you ^_^");
            }

            clearSnipBox();

        }

        private void createAnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string op = Con_Operation.Sys_Endorse_String(Con_Operation.Enum_Sys_Endorse.LIKE_HTREE, "");
            trace.AddItem(Util_All.currentTime(), op);
            MessageBox.Show("Endorse Suceed! Thank you ^_^");
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string op = Con_Operation.Sys_Endorse_String(Con_Operation.Enum_Sys_Endorse.LIKE_HTREE_DETAIL, "");
            trace.AddItem(Util_All.currentTime(), op);
            MessageBox.Show("Endorse Suceed! Thank you ^_^");
        }

        private void eTreeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string op = Con_Operation.Sys_Endorse_String(Con_Operation.Enum_Sys_Endorse.LIKE_ETREE, "");
            trace.AddItem(Util_All.currentTime(), op);
            MessageBox.Show("Endorse Suceed! Thank you ^_^");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string op = Con_Operation.Sys_Endorse_String(Con_Operation.Enum_Sys_Endorse.LIKE_ETREE_DETAIL, "");
            trace.AddItem(Util_All.currentTime(), op);
            MessageBox.Show("Endorse Suceed! Thank you ^_^");
        }

        private void screenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string op = Con_Operation.Sys_Endorse_String(Con_Operation.Enum_Sys_Endorse.LIKE_OBSERVATION_SCREENSHOT, "");
            trace.AddItem(Util_All.currentTime(), op);
            MessageBox.Show("Endorse Suceed! Thank you ^_^");
        }

        private void rawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string op = Con_Operation.Sys_Endorse_String(Con_Operation.Enum_Sys_Endorse.LIKE_OBSSERVATION_RAW, "");
            trace.AddItem(Util_All.currentTime(), op);
            MessageBox.Show("Endorse Suceed! Thank you ^_^");
        }

        /// <summary>
        /// Endorse Quick Find
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quickFindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string op = Con_Operation.Sys_Endorse_String(Con_Operation.Enum_Sys_Endorse.LIKE_FIND, "");
            trace.AddItem(Util_All.currentTime(), op);
            MessageBox.Show("Endorse Suceed! Thank you ^_^");
        }

        #endregion


        //[6-24 ADD_LINE_NUM]
        private void dataGridViewAlerts_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
        }

        private void dataGridViewFirewall_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
        }

        private void txtSearchDGV_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchDGV_Click((object)sender, (EventArgs)e);

            }
        }

        #region [6-25 TERM/PORT LOOKUP ]
        private void btnPortLookup_Click(object sender, EventArgs e)
        {
            try
            {
                //[14-5-19 Test Time]
                watch.Start();

                int port = Convert.ToInt32(txtPortLookUp.Text.ToString().Trim());
                string result = Util_All.lookupPortInfo(port);
                if (!result.Trim().Equals(""))
                {
                    //[14-5-19 Test Time]
                    watch.Stop();
                    test_time_add((int)test_index.INQUIRE_Y);

                    MessageBox.Show(result, "Description of Port " + txtPortLookUp.Text);
                }
                else
                {
                    //[14-5-19 Test Time]
                    watch.Stop();
                    test_time_add((int)test_index.INQUIRE_N);

                    MessageBox.Show("Not found.");
                    
                }
                //[6-29 TRACE]
                trace.AddItem(new Con_Observable_Trace_Item(Util_All.currentTime(), Con_Operation.lookup_2_string(Con_Operation.Enum_LookUp.PORT, this.txtPortLookUp.Text.Trim())));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtPortLookUp_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPortLookup_Click((object)sender, (EventArgs)e);
            }
        }

        private void btnTermLookup_Click(object sender, EventArgs e)
        {
            try
            {
                

                string text = txtTermLookup.Text.Trim().ToLower();
                if (!text.Equals(""))
                {
                    //[14-5-19 Test Time]
                    watch.Start();

                    string result = Util_All.lookupTermInfo(text);

                    if (!result.Trim().Equals(""))
                    {
                        //[14-5-19 Test Time]
                        watch.Stop();
                        test_time_add((int)test_index.INQUIRE_Y);

                        MessageBox.Show(result, "Description of Term " + text);
                    }
                    else
                    {
                        //[14-5-19 Test Time]
                        watch.Stop();
                        test_time_add((int)test_index.INQUIRE_N);

                        MessageBox.Show("Not found.");
                    }
                    //[6-29 TRACE]
                    trace.AddItem(new Con_Observable_Trace_Item(Util_All.currentTime(), Con_Operation.lookup_2_string(Con_Operation.Enum_LookUp.TERM, txtTermLookup.Text.Trim())));

                }
                else
                {
                    MessageBox.Show("Empty term");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtTermLookup_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTermLookup_Click((object)sender, (EventArgs)e);
            }
        }

        #endregion

        #region [6-15 UI SEARCH-DGV]



        private void btnSearchDGV_Click(object sender, EventArgs e)
        {

            try
            {
                

                this.Cursor = Cursors.WaitCursor;

                if (txtSearchDGV.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Please input your keywords for search.");
                    return;
                }
                //Current DGV in the selected tab
                int currentTabIndex = this.tabDSOptions.SelectedIndex;

                if (currentTabIndex == 1)
                {
                    //[14-5-19 Test Time]
                    watch.Start();

                    if (ids_searcher == null)
                    {
                        ids_searcher = new Util_DGV_Searcher(this.dataGridViewAlerts);

                    }
                    string keyword = txtSearchDGV.Text.Trim();
                    ids_searcher.LookupStr = keyword;
                    bool is_found = ids_searcher.Lookup();
                    //[6-29 TRACE]
                    trace.AddItem(new Con_Observable_Trace_Item(Util_All.currentTime(), Con_Operation.Search_2_String("IDS_Alerts", keyword)));

                    //[14-5-19 Test Time]
                    watch.Stop();
                    if (is_found)
                    {
                        test_time_add((int)test_index.SEARCH_Y);
                    }
                    else
                    {
                        test_time_add((int)test_index.SEARCH_N);
                    }

                    
                }
                else if (currentTabIndex == 2)
                {
                    if (firewall_searcher == null)
                    {
                        firewall_searcher = new Util_DGV_Searcher(this.dataGridViewFirewall);
                    }

                    //[14-5-19 Test Time]
                    watch.Start();


                    string keyword2 = txtSearchDGV.Text.Trim();
                    firewall_searcher.LookupStr = keyword2;
                    bool is_found = firewall_searcher.Lookup();
                    //DataRow dr = firewall_dt.Rows.Find(keyword2);
                    //int index = firewall_dt.Rows.IndexOf(dr);
                    // dataGridViewFirewall.CurrentCell = dataGridViewFirewall.Rows[index].Cells[0];
                    //[6-29 TRACE]
                    trace.AddItem(new Con_Observable_Trace_Item(Util_All.currentTime(), Con_Operation.Search_2_String("Firewall_Logs", keyword2)));

                    //[14-5-19 Test Time]
                    watch.Stop();
                    if (is_found)
                    {
                        test_time_add((int)test_index.SEARCH_Y);
                    }
                    else
                    {
                        test_time_add((int)test_index.SEARCH_N);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Cannot search in current data.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not Found.");
                throw ex;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        #endregion

        #region Filter Function [6-27 FILTER-DGV]

        DataGridView currentdgv = null;
        //BindingSource bs = new BindingSource();

        private void getCurrentDGV()
        {
            if (tabDSOptions.SelectedIndex == 1)
                currentdgv = this.dataGridViewAlerts;
            else if (tabDSOptions.SelectedIndex == 2)
                currentdgv = this.dataGridViewFirewall;
        }

        private void loadCmbToolField()
        {
            cmbToolField.Items.Clear();
            if (currentdgv != null)
            {
                int colnum = currentdgv.Columns.Count;
                //cmbToolRela.Items.Add("ALL");
                for (int i = 0; i < colnum; i++)
                {
                    cmbToolField.Items.Add(currentdgv.Columns[i].HeaderText);
                }
            }
        }

        public string getCurrentDGVTable()
        {
            if (tabDSOptions.SelectedIndex == 1)
                return Util_ConfigData.getCurrentIDS();
            else
                return Util_ConfigData.getCurrentFirewall();

        }

        public int getCurrentDGVIndex()
        {
            return tabDSOptions.SelectedIndex;
        }

        public string firewall_filter_rule = "";
        public string ids_filter_rule = "";

        private string FilterDataSQL(ref DataTable dt, string filter_rule, string newrule, DataGridView currentdgv)
        {
            try
            {


                if (!newrule.Trim().Equals(""))
                {

                    if (!filter_rule.Contains(newrule))
                    {
                        if (!filter_rule.Equals(""))
                        {
                            filter_rule += " AND ";
                        }

                        filter_rule += newrule;

                        string sql = "SELECT * FROM " + getCurrentDGVTable() + " WHERE " + filter_rule;

                        if (dt != null)
                            dt.Clear();

                        dt = Util_DBA.dbFilterTable(sql);

                        trace.AddItem(new Con_Observable_Trace_Item(Util_All.currentTime(), Con_Operation.Filter_2_String(getCurrentDGVTable(), sql)));

                        updateAutoComplete(txtToolFilterValue.Text);
                    }
                }
                return filter_rule;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[7-17 DB_SQL]
        /// <summary>
        /// Begin filtering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                getCurrentDGV();


                if (cmbToolField.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a field to filter");
                    cmbToolField.Focus();
                    return;
                }
                else if (cmbToolRela.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a relation");
                    cmbToolRela.Focus();
                    return;
                }

                else if (txtToolFilterValue.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Please input a filter value in the text box");
                    txtToolFilterValue.Focus();
                    return;
                }




                //[7-17 DB_SQL]
                string newrule = cmbToolField.SelectedItem.ToString() + " " + cmbToolRela.SelectedItem.ToString() + " \'" + txtToolFilterValue.Text + "\'";


                bool emptydt = false;

                if (currentdgv == dataGridViewAlerts)
                {
                    //[14-5-19 Test Time]
                    watch.Start();

                    ids_filter_rule = FilterDataSQL(ref ids_dt, ids_filter_rule, newrule, dataGridViewAlerts);
                    //update IDS DGV
                    updateIDSDGV(ids_dt);

                    watch.Stop();
                    test_time_add((int)test_index.FILTER);
                   

                    //[7-29]
                    btnFilterRule.Text = ids_filter_rule;

                    //[7-27]
                    if (ids_dt != null && ids_dt.Rows.Count < 1)
                    {
                        emptydt = true;
                    }

                }
                else if (currentdgv == dataGridViewFirewall)
                {
                    //[14-5-19 Test Time]
                    watch.Start();

                    firewall_filter_rule = FilterDataSQL(ref firewall_dt, firewall_filter_rule, newrule, dataGridViewAlerts);

                    updateFirewallDGV(firewall_dt);

                    watch.Stop();
                    test_time_add((int)test_index.FILTER);


                    //[7-29]
                    btnFilterRule.Text = firewall_filter_rule;

                    //[7-27]
                    if (firewall_dt != null && firewall_dt.Rows.Count < 1)
                    {
                        emptydt = true;
                    }
                }
                if (emptydt)
                {
                    MessageBox.Show(
@"No result.

Click the [Clear] button to clear the filter conditions and try again.

To view the existing conditions, click the [Multi-condition] button with a plus icon.");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //private void toolStripButton1_Click(object sender, EventArgs e)
        //{
        //    getCurrentDGV();


        //    if (cmbToolField.SelectedIndex == -1)
        //    {
        //        MessageBox.Show("Please select a field to filter");
        //        txtToolFilterValue.Focus();
        //        return;
        //    }

        //    else if (txtToolFilterValue.Text.Trim().Equals(""))
        //    {
        //        MessageBox.Show("Please input a filter value in the text box");
        //        txtToolFilterValue.Focus();
        //        return;
        //    }


        //    BindingSource bs = new BindingSource(); 
        //    bs.DataSource = currentdgv.DataSource;

        //    try
        //    {
        //       string filterstr = filterString(cmbToolField.SelectedItem.ToString(), cmbToolRela.SelectedItem.ToString(), txtToolFilterValue.Text);

        //       if (!filterstr.Equals(""))
        //       {
        //           bs.Filter = filterstr;
        //           currentdgv.DataSource = bs;

        //           // begin [6-29 TRACE]
        //           string datasource = "";
        //           if (currentdgv == dataGridViewAlerts)
        //           {
        //               datasource = "IDS_Alerts";
        //           }
        //           else if (currentdgv == dataGridViewFirewall)
        //           {
        //               datasource = "Firewall_Logs";
        //           }
        //           trace.AddItem(new Con_Observable_Trace_Item(Util_All.currentTime(), Con_Operation.Filter_2_String(datasource, filterstr)));
        //           // end [6-29 TRACE]
        //       }

        //       (currentdgv.DataSource as DataTable).DefaultView.RowFilter = string.Format("'{0}' = '{1}'", cmbToolField.SelectedItem.ToString(), this.txtToolFilterValue.Text);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private string filterString(string field, string rela, string value)
        {
            if (rela.Equals("="))
            {
                return field + "= \'" + value + "\'";
            }
            else if (rela.Equals(">="))
            {
                return field + ">= \'" + value + "\'";
            }
            else if (rela.Equals("<="))
            {
                return field + "<= \'" + value + "\'";
            }
            else if (rela.Equals("LIKE"))
            {
                return field + " like '%" + value + "%'";
            }
            else
            {
                return "";
            }
        }


        private void tabDSOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabDSOptions.SelectedIndex > 0)
            {
                if (!firstPopTip)
                {
                    string msg =
@"1. You can use [Quick Find] and [Filter] functions 
on the menu bar to explore the data.
2. You can double click a cell of the datatable to copy the text in it";
                    frmTip tip = new frmTip(this, msg, 1);
                    tip.ShowDialog();
                }
            }
            getCurrentDGV();
            loadCmbToolField();


            //[7-29]
            if (tabDSOptions.SelectedIndex == 1)
            {
                btnFilterRule.Text = ids_filter_rule;
            }
            else if (tabDSOptions.SelectedIndex == 2)
            {
                btnFilterRule.Text = firewall_filter_rule;
            }
            else
            {
                btnFilterRule.Text = "";
            }
        }

        /// <summary>
        /// Clear filtering condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //[7-17 DB_SQL]
            //getCurrentDGV();
            //BindingSource bs1 = new BindingSource();
            //bs1.DataSource = currentdgv.DataSource;
            //bs1.RemoveFilter();

            //[7-17 DB_SQL]
            try
            {
                //[7-29]
                btnFilterRule.Text = "";

                this.cmbToolField.SelectedIndex = -1;
                this.txtToolFilterValue.Text = "";
                this.cmbToolRela.SelectedIndex = 0;


                this.Cursor = Cursors.WaitCursor;

                if (tabDSOptions.SelectedIndex == 1)
                {
                    ids_filter_rule = "";
                    resetIDSDGV();
                }
                else if (tabDSOptions.SelectedIndex == 2)
                {
                    firewall_filter_rule = "";
                    resetFirewallDGV();
                }

                trace.AddItem(new Con_Observable_Trace_Item(Util_All.currentTime(), "Clear Filter Condition"));
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }


        /// <summary>
        /// Multi-condition filtering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabDSOptions.SelectedIndex == 1)
                {
                    frmFilter newfilter = new frmFilter(currentdgv, this, ids_filter_rule);
                    DialogResult result = newfilter.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        //[7-29]
                        btnFilterRule.Text = ids_filter_rule;

                        string sql = "Select * from " + getCurrentDGVTable() + " where " + ids_filter_rule;

                        ids_dt = Util_DBA.dbFilterTable(sql);

                        updateIDSDGV(ids_dt);

                        trace.AddItem(new Con_Observable_Trace_Item(Util_All.currentTime(), Con_Operation.Filter_2_String(getCurrentDGVTable(), sql)));
                    }
                }
                else if (tabDSOptions.SelectedIndex == 2)
                {
                    frmFilter newfilter = new frmFilter(currentdgv, this, firewall_filter_rule);
                    DialogResult result = newfilter.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        //[7-29]
                        btnFilterRule.Text = firewall_filter_rule;

                        string sql = "Select * from " + getCurrentDGVTable() + " where " + firewall_filter_rule;

                        firewall_dt = Util_DBA.dbFilterTable(sql);

                        updateFirewallDGV(firewall_dt);

                        trace.AddItem(new Con_Observable_Trace_Item(Util_All.currentTime(), Con_Operation.Filter_2_String(getCurrentDGVTable(), sql)));
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Tip

        private void dataGridViewAlerts_MouseUp(object sender, MouseEventArgs e)
        {
            if (!firstPopTip_selectobs)
            {
                string msg = @"Press the [SPACE] key on your keyboard to select the items";
                frmTip tip = new frmTip(this, msg, 2);
                tip.ShowDialog();
            }
        }

        private void dataGridViewFirewall_MouseUp(object sender, MouseEventArgs e)
        {
            if (!firstPopTip_selectobs)
            {
                string msg = @"Press the [SPACE] key on your keyboard to select the items";
                frmTip tip = new frmTip(this, msg, 2);
                tip.ShowDialog();
            }
        }


        #endregion

        #region Help

        private void toKnowATermsMeaningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Go to the [Mornitoring Data] tab and use the [Term explaination] function on the top strip");
            this.tab.SelectedIndex = 0;
        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Go to the [Mornitoring Data] tab and use [Lookup a port] function on the top strip");
            this.tab.SelectedIndex = 0;
        }

        private void toCopyTheTextInACellOfCurrentDataTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Double click the cell to get a copy of the text");
            tab.SelectedIndex = 0;
        }

        private void dataviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To switch to the Monitoring Data view, click the first Tab on the left.");
            tab.SelectedIndex = 0;
        }

        private void analysisviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To switch to the Analysis view, click the second Tab on the left.");
            tab.SelectedIndex = 1;
        }

        private void czz111psueduToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("czz111@psu.edu");
            MessageBox.Show("This email address has been copied to your clipboard.");
        }
        #endregion


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnFinish_Click(sender, e);
        }


        #region [7-19 VIRTUAL MODE]

        //[VAST-DATA 4-1]
        public void loadVAST_Firewall()
        {
            this.dataGridViewFirewall.VirtualMode = true;
            this.dataGridViewFirewall.ReadOnly = true;
            this.dataGridViewFirewall.AllowUserToAddRows = false;
            this.dataGridViewFirewall.AllowUserToOrderColumns = false;
            this.dataGridViewFirewall.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFirewall.CellValueNeeded += new DataGridViewCellValueEventHandler(dataGridViewFirewall_CellValueNeeded);

            this.dataGridViewFirewall.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;


            firewall_dt = Util_FileReader.dbLoadData(Util_ConfigData.getCurrentFirewall());

            loadColumnFirewallDGV(firewall_dt);

            //[7-19 VIRTUAL MODE]
            //-- this.dataGridViewFirewall.DataSource = dt2;
            this.dataGridViewFirewall.RowCount = firewall_dt.Rows.Count;

            setFirewallDGV_format();
            dataGridViewFirewall.ClearSelection();

        }

        private void resetIDSDGV()
        {

            ids_dt = Util_FileReader.dbLoadData(Util_ConfigData.getCurrentIDS());
            dataGridViewAlerts.DataSource = ids_dt;
            dataGridViewAlerts.Refresh();
        }
        private void updateIDSDGV(DataTable dt)
        {

            this.dataGridViewAlerts.DataSource = dt;
            dataGridViewAlerts.Refresh();
        }

        private void resetFirewallDGV()
        {

            firewall_dt = Util_FileReader.dbLoadData(Util_ConfigData.getCurrentFirewall());
            int rownum = firewall_dt.Rows.Count;
            this.dataGridViewFirewall.Rows.Clear();
            this.dataGridViewFirewall.RowCount = rownum;
            this.dataGridViewFirewall.Refresh();
        }
        private void updateFirewallDGV(DataTable dt)
        {
            int rownum = dt.Rows.Count;
            this.dataGridViewFirewall.Rows.Clear();
            this.dataGridViewFirewall.RowCount = rownum;
            this.dataGridViewFirewall.Refresh();
        }

        private void loadColumnFirewallDGV(DataTable dt)
        {
            dataGridViewFirewall.Columns.Clear();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataGridViewColumn col = new DataGridViewTextBoxColumn();

                col.Name = dt.Columns[i].ColumnName;
                col.HeaderText = dt.Columns[i].ToString();


                dataGridViewFirewall.Columns.Add(col);
            }


        }

        private void dataGridViewFirewall_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            int dtRowNum = firewall_dt.Rows.Count;
            int dgvNum = dataGridViewFirewall.RowCount;

            if (firewall_dt != null && dtRowNum > 0 && dtRowNum == dgvNum)
            {
                // If this is the row for new records, no values are needed.
                if (e.RowIndex >= dataGridViewFirewall.RowCount)
                    return;


                // read data from datatable  
                string colName = dataGridViewFirewall.Columns[e.ColumnIndex].Name;

                e.Value = firewall_dt.Rows[e.RowIndex][colName].ToString();

                // e.Value = firewall_dt.Rows[e.RowIndex].GetType().GetProperties()[e.ColumnIndex].GetValue(firewall_dt.Rows[e.RowIndex], null); ;
            }
        }


        #endregion



        //[7-24 ONGOING OBS]
        private void currentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentobs == null)
                {
                    currentobs = new frmCurrentObservation(this);
                }
                
                currentobs.loadCurrentObs();
                currentobs.Show();
                currentobs.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewAlerts_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewAlerts.CurrentCell != null)
                {
                    Clipboard.SetText(dataGridViewAlerts.CurrentCell.Value.ToString());
                    MessageBox.Show("Text in current cell has been copied to clipboard");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewFirewall_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewFirewall.CurrentCell != null)
                {
                    Clipboard.SetText(dataGridViewFirewall.CurrentCell.Value.ToString());
                    MessageBox.Show("Text in current cell has been copied to clipboard");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void updateLastHypoLocation()
        {
            try
            {
                TreeNode currentnode_ht = id2ht[lastHypo.id];
                if (currentnode_ht != null)
                {
                    btnHTContext.Visible = true;
                    btnHTContext.Location = new Point(currentnode_ht.Bounds.Right + 5, currentnode_ht.Bounds.Bottom - 2);
                }
                else
                {
                    btnHTContext.Visible = false;
                }

                TreeNode currentnode_et = id2et[lastHypo.id];
                if (currentnode_et != null)
                {
                    btnETContext.Visible = true;
                    btnETContext.Location = new Point(currentnode_et.Bounds.Right + 5, currentnode_et.Bounds.Bottom - 2);
                }
                else
                {
                    btnETContext.Visible = false;
                }

                highlightPath(HTselectedNode);
                highlightPath(currentnode_et);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void btnAddSibling_Click(object sender, EventArgs e)
        {
            try
            {

                if (treeHypo.SelectedNode == null)
                {
                    MessageBox.Show("You need to select a node in the tree first :)");
                    return;
                }
                else
                {

                    Con_Hypo hypo = (Con_Hypo)treeHypo.SelectedNode.Tag;
                    if (hypo != null)
                    {

                        Con_Experience_Unit parentEU = hypo.pre_unit;

                        Con_Hypo newhypo = null;

                        frmHypothesis fhp = null;
                        if (parentEU == null)
                        {
                            MessageBox.Show(
@"Hypothesis can't be created since there is no new observation got before generating " 
+ hypo.id +
@"
You can select its child hypothesis a sibling hypothesis instead.");
                            return;
                        }
                        else
                        {
                            //bool isinithypo = Util_All.isINITHypo(hypo.id);

                            fhp = new frmHypothesis(this, parentEU.obs, confirmedHypo);
                            fhp.ShowDialog();

                            if (!isfrmHypothesisDone)
                                return;

                            isfrmHypothesisDone = false;

                            if (Util_All.isINITHypo(hypo.id))
                            {


                                newhypo = newHypo(exTree.root, null, confirmedHypo, Con_Operation.Enum_Hypo_Op.ADD_SIBLING_2);
                                //newhypo.id = "INIT-H-" + newhypo.id;
                                newhypo.id = Util_All.generateID(Util_All.init_h);

                                hypoTree.root.addChildrenHypo(newhypo);
                                newhypo.parentHypo = hypoTree.root;
                                exTree.root.preHypo = hypoTree.root;

                            }
                            else
                            {
                                Con_Hypo parentHypo = parentEU.preHypo;
                                if (parentHypo != null)
                                {
                                    lastHypo = parentHypo;
                                    newhypo = newHypo(parentEU, null, confirmedHypo, Con_Operation.Enum_Hypo_Op.ADD_SIBLING_2);
                                }
                            }

                            string op = Con_Operation.Hypo_Op_String(Con_Operation.Enum_Hypo_Op.ADD_SIBLING_2, hypo, newhypo);
                            trace.AddItem(Util_All.currentTime(), op);

                            treeHypo.Nodes.Clear();
                            loadHypoTree();
                            dgvBindData();

                            refreshETree();

                            updateLastHypoLocation();

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHTContext_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
@"This is the current hypothesis.

It's direct observation is the parent node in the 
[Action-Observation-Thouth Tree (E-Tree)] below.

It's context is highlighted with blue backgroud in both H-Tree and E-Tree");
        }

        private void btnETContext_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
@"This is the current hypothesis.

It's direct observation can be viewed in the parent node.

It's context is highlighted with blue backgroud in both H-Tree and E-Tree");
        }

        private void treeViewTree_DoubleClick(object sender, EventArgs e)
        {
            if (treeViewTree.SelectedNode != null)
            {
                MessageBox.Show(
@"Please double click the node in the [Thought Tree (H-Tree)] to specify 'Current Hypothesis'.
(The node with 'Location icon' is the 'Current Hypothesis')");
            }
        }

        private void btnFilterRule_Click(object sender, EventArgs e)
        {
            try
            {
                toolStripButton3_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //[8-22 CREATE OBS BY DESCR]
        private void writeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int canobs = canCreateObservation();
                if (canobs == 2)
                {
                    MessageBox.Show(
                        CanNotCreateObsMsg());
                    return;
                }
                else if (canobs == 1)
                {
                    Con_Hypo newInitHypo = new Con_Hypo();
                    newInitHypo.id = Util_All.generateID(Util_All.init_h);
                    exTree.root.nextHypoList.Add(newInitHypo);
                    newInitHypo.pre_unit = exTree.root;
                    newInitHypo.parentHypo = hypoTree.root;
                    hypoTree.root.addChildrenHypo(newInitHypo);
                    lastHypo = newInitHypo;
                    canobs = 0;
                }

                if (canobs == 0)
                {
                    frmWriteDownObservation frmWDO = new frmWriteDownObservation(this);
                    frmWDO.ShowDialog();
                    refreshETree();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        //[8-31 NODE OP]
        private void treeHypo_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeHypo_ItemDrag(object sender, ItemDragEventArgs e)
        {

            // 开始进行拖放操作，并将拖放的效果设置成移动。
            this.DoDragDrop(e.Item, DragDropEffects.Move);

        }


        private void treeHypo_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                TreeNode treeNode;

                if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
                {
                    TreeNode targetTreeNode;

                    // 获取当前光标所处的坐标
                    Point point = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                    targetTreeNode = ((TreeView)sender).GetNodeAt(point);

                    // 获取被拖动的节点
                    treeNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

                    if (treeNode.Level == 0)
                        return;




                    string comfirm = String.Format(
                             @"Do you want to move Node [{0}] as the child of Node [{1}]",
                             treeNode.Text,
                             targetTreeNode.Text);

                    DialogResult result1 = MessageBox.Show(comfirm, "Attention", MessageBoxButtons.YesNo);

                    if (result1 == DialogResult.Yes)
                    {
                        if (targetTreeNode.Nodes.Contains(treeNode))
                        {
                            string msg = String.Format(
                             @"No need to move because Node [{0}] is already the child of Node [{1}]",
                             treeNode.Text,
                             targetTreeNode.Text);
                            MessageBox.Show(msg);
                            return;
                        }

                        if (MoveTreeNode(treeNode, targetTreeNode))
                        {
                            refreshETree();
                            loadHypoTree();
                            updateLastHypoLocation();


                        }
                        else
                        {
                            MessageBox.Show("Sorry, the treenode can't be moved here.");
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       
        private bool MoveTreeNode(TreeNode src, TreeNode des)
        {
            try
            {
                if (src.Level == 0)
                    return false;
                
                Con_Hypo srcHypo = (Con_Hypo)src.Tag;
                Con_Hypo desHypo = (Con_Hypo)des.Tag;

                if (srcHypo == null || desHypo == null)
                    return false;

                if (des.Level == 0)
                {
                    //Create a new INIT-HYPO
                    Con_Hypo new_init = new Con_Hypo();
                    //new_init.id = "INIT-H-" + new_init.id;
                    new_init.id = Util_All.generateID(Util_All.init_h);

                    desHypo.childrenHypos.Add(new_init);
                    new_init.parentHypo = desHypo;
                    new_init.pre_unit = exTree.root;
                    exTree.root.nextHypoList.Add(new_init);

                    desHypo = new_init;
                    
                }

                //srchypo != null && deshypo != null
            
                //deshypo is not srchypo or a child of srchypo
                if(srcHypo == desHypo )
                    return false;

                if (desHypo.isOffSpringOf(srcHypo))
                    return false;

                //move hypo in both H-Tree and E-Tree
                Con_Hypo src_parent_hypo = srcHypo.parentHypo;
                Con_Experience_Unit src_parent_eu = srcHypo.pre_unit;

                Con_Experience_Unit des_child_eu = desHypo.next_unit;

                bool moved = false;


                if (des_child_eu != null)
                {

                    if (src_parent_hypo != null)
                    {
                        //only move srchypo, but not its parent_eu
                        srcHypo.parentHypo = desHypo;
                        srcHypo.pre_unit = des_child_eu;

                        desHypo.next_unit = des_child_eu;
                        desHypo.next_unit.nextHypoList.Add(srcHypo);
                        desHypo.childrenHypos.Add(srcHypo);


                        src_parent_hypo.childrenHypos.Remove(srcHypo);

                        if (src_parent_hypo.childrenHypos.Count < 1)
                        {
                            //if srchypo is the only child, then remove its parent eu
                            src_parent_eu.nextHypoList.Remove(srcHypo);
                           // src_parent_hypo.next_unit = null;
                        }
                        else
                        {
                            src_parent_hypo.next_unit.nextHypoList.Remove(srcHypo);
                        }

                        moved = true;
                    }
                }
                else
                {
                    if (src_parent_eu != null)
                    {
                        bool flag_move_only_srchypo = true;
                        bool flag_agree_move_all_sibling = false;

                        if (src_parent_eu.nextHypoList.Count > 1)
                        {
                            flag_move_only_srchypo = false;

                            DialogResult result1 = MessageBox.Show(
@"Since there is no observation gained under the targeted hypotheses,
we need to move the parent observation of the current selected hypothesis here.

If so, ALL ITS CHILD HYPOTHESES (the siblings of the current selected hypothesis) 
WILL ALSO BE MOVED HERE. 

Do you want to continue?", "Attention", MessageBoxButtons.YesNo);


                            if (result1 == DialogResult.Yes)
                            {
                                flag_agree_move_all_sibling = true;
                                
                                //check: target node is the offspring of  a sibling of src
                                foreach (Con_Hypo sibling in src_parent_hypo.childrenHypos)
                                {
                                    if(desHypo.isOffSpringOf(sibling))
                                    {
                                        return false;
                                    }
                                    if (desHypo == sibling)
                                        return false;
                                }
                            }
                        }

                        if(flag_move_only_srchypo || flag_agree_move_all_sibling)
                        {
                            
                                //move srchypo and its parent_eu
                                foreach (Con_Hypo hypo in src_parent_eu.nextHypoList)
                                {
                                    hypo.parentHypo = desHypo;
                                    desHypo.childrenHypos.Add(hypo);
                                    src_parent_hypo.childrenHypos.Remove(hypo);
                                }

                                desHypo.next_unit = src_parent_eu;

                                src_parent_eu.preHypo = desHypo;

                                src_parent_hypo.next_unit = null;

                                moved = true;
                            

                        }
                       

                    }
                }
                if (moved)
                {
                    lastHypo = srcHypo;

                     //addtrace
                    string op1 = Con_Operation.Hypo_Op_String(Con_Operation.Enum_Hypo_Op.MOVE_FROM_TO_3,
                        srcHypo, src_parent_hypo.id, desHypo.id);
                              
                    trace.AddItem(Util_All.currentTime(), op1);
                }

                return moved;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmHelpExplainTreeStructure frmTreeS = new frmHelpExplainTreeStructure();
                frmTreeS.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }













    }
}