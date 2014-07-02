using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Experience_Based_Cyber_SA
{
    /// <summary>
    /// 在 DataGridView 中实现类似 Ctrl + F 的查找功能。
    /// </summary>
    /// 
    public class Util_DGV_Searcher
    {
        
    
        private DataGridView dgv = null;        // 要在其中进行查找的 DataGridView。     
        private string lookupStr = string.Empty;// 要查找的字符串。

        private int maxColIndex = 0; // DataGridView 总列数。
        private int maxRowIndex = 0; // DataGridView 总行数。

        private int startColIndex = 0;  // 查找的起始列序号。
        private int startRowIndex = 0;  // 查找的其实航序号。

        private int currentColIndex = 0;// 当前列序号。
        private int currentRowIndex = 0;// 当前行序号。

        private bool isFound = false;   // 是否找到一个匹配。

        public Util_DGV_Searcher(DataGridView dgv) {
            this.dgv = dgv;

            this.maxColIndex = dgv.Columns.Count - 1;
            this.maxRowIndex = dgv.Rows.Count - 1;

            if (dgv.CurrentCell == null)
            {
                this.startColIndex = 0;
                this.startRowIndex = 0;

                this.currentColIndex = 0;
                this.currentRowIndex = 0;
            }
            else
            {
                this.startColIndex = dgv.CurrentCell.ColumnIndex;
                this.startRowIndex = dgv.CurrentCell.RowIndex;

                this.currentColIndex = dgv.CurrentCell.ColumnIndex;
                this.currentRowIndex = dgv.CurrentCell.RowIndex;
            }
            this.dgv.CellClick += new DataGridViewCellEventHandler(dgv_CellClick);
        }

        // 如果用户点击了某个单元格，搜索起始点也随之改变。
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (dgv != null && dgv.CurrentCell != null)
            {
                this.startColIndex = dgv.CurrentCell.ColumnIndex;
                this.startRowIndex = dgv.CurrentCell.RowIndex;

                this.currentColIndex = dgv.CurrentCell.ColumnIndex;
                this.currentRowIndex = dgv.CurrentCell.RowIndex;
            }
        }

        /// <summary>
        /// 设置要查找的字符串（只写）。
        /// </summary>
        public string LookupStr {
            set {
                if (this.lookupStr != value) {
                    this.lookupStr = value;
                    this.isFound = false;
                }
            }
        }

        /// <summary>
        /// 在 DataGridView 中查找，将匹配的单元格设置为当前单元格。
        /// </summary>
        public bool Lookup() {
            int colIndex = this.currentColIndex;
            int rowIndex = this.currentRowIndex;
            bool is_found = false;

            Next(ref colIndex, ref rowIndex);

            is_found = Lookup(colIndex, rowIndex);

            this.dgv.CurrentCell = this.dgv[currentColIndex, currentRowIndex];

            return is_found;
        }


        // 在 DataGridView 中进行查找，直到找到匹配的单元格，或者达到搜索的起始点。
        private bool Lookup(int colIndex, int rowIndex)
        {
            while (colIndex != this.startColIndex || rowIndex != this.startRowIndex)
            {
                if (this.dgv.Columns[colIndex].Visible == true && // 仅查找可见列。
                this.Match(this.dgv[colIndex, rowIndex].Value.ToString(), this.lookupStr))
                {

                    this.isFound = true;

                    this.currentColIndex = colIndex;
                    this.currentRowIndex = rowIndex;
                    return this.isFound;
                }

                Next(ref colIndex, ref rowIndex);

                if (colIndex == this.startColIndex && rowIndex == this.startRowIndex)
                {
                    if (this.isFound == true ||
                        Match(this.dgv[colIndex, rowIndex].Value.ToString(), this.lookupStr))
                    {
                        MessageBox.Show("Reached to the end.", "Attention",
                            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Cannot find the text：\n\n" + this.lookupStr, "Attention",
                            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    this.currentColIndex = colIndex;
                    this.currentRowIndex = rowIndex;
                    return this.isFound;
                }
            }
            return this.isFound;

        }

        // 在 DataGridView 中进行查找，直到找到匹配的单元格，或者达到搜索的起始点。
        //private void Lookup(int colIndex, int rowIndex) {
        //    if (colIndex == this.startColIndex && rowIndex == this.startRowIndex) {
        //        if (this.isFound == true || 
        //            Match(this.dgv[colIndex, rowIndex].Value.ToString(), this.lookupStr)) {
        //            MessageBox.Show("Reached to the end.", "Attention", 
        //                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        //        } else {
        //            MessageBox.Show("Cannot find the text：\n\n" + this.lookupStr, "Attention", 
        //                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        //        }
        //        this.currentColIndex = colIndex;
        //        this.currentRowIndex = rowIndex;
        //        return;
        //    }

        //    if (this.dgv.Columns[colIndex].Visible == true && // 仅查找可见列。
        //        this.Match(this.dgv[colIndex, rowIndex].Value.ToString(), this.lookupStr)) {

        //        this.isFound = true;

        //        this.currentColIndex = colIndex;
        //        this.currentRowIndex = rowIndex;
        //        return;
        //    }

        //    Next(ref colIndex, ref rowIndex);

        //    Lookup(colIndex, rowIndex);
        //}

        // 下一个单元格序号。
        private void Next(ref int colIndex, ref int rowIndex) {
            colIndex = colIndex + 1;

            if (colIndex > this.maxColIndex) {
                colIndex = 0;
                rowIndex++;
                if (rowIndex > maxRowIndex) {
                    rowIndex = 0;
                }
            }
        }

        // matchStr 是否是 sourceStr 的子串。
        private bool Match(string sourceStr, string matchStr) {
            string[] matcharray = matchStr.Split(' ');

            for (int i = 0; i < matcharray.Length; i++ )
            {
                string str = matcharray[i];
                if (!str.Trim().Equals(""))
                {
                    if (sourceStr.ToLower().Contains(str.ToLower()))
                        return true;
                }
            }
            return false;
        }
    }
}
    

