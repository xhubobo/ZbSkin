using System.Drawing;
using System.Windows.Forms;

namespace ZbSkin.Tools
{
    public static class DataGridViewHelper
    {
        public static void SetCommonStyle(DataGridView dataGridView, int columnHeadersHeight = 25)
        {
            if (dataGridView == null)
            {
                return;
            }

            dataGridView.BackgroundColor = Color.White;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AutoGenerateColumns = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.ColumnHeadersHeight = columnHeadersHeight;

            dataGridView.ColumnHeadersHeight = 30;
            dataGridView.RowTemplate.Height = 30;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 16);
            dataGridView.DefaultCellStyle.Font = new Font("宋体", 16);

            dataGridView.DataBindingComplete += dataGridView_DataBindingComplete;
        }

        public static void SetDatetimeStyle(DataGridView dataGridView, string datetimeColumn)
        {
            var dateColumn = dataGridView.Columns[datetimeColumn];
            if (dateColumn != null)
            {
                dateColumn.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            }
        }

        private static void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SetCrossColor(sender as DataGridView);
        }

        public static void SetCrossColor(DataGridView dataGridView)
        {
            if (dataGridView == null)
            {
                return;
            }

            if (dataGridView.Rows.Count == 0)
            {
                return;
            }

            for (var i = 0; i < dataGridView.Rows.Count; i++)
            {
                var color = ((i + 1) % 2 == 1) ? Color.LightBlue : Color.LightGray;
                dataGridView.Rows[i].DefaultCellStyle.BackColor = color;
                dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
            }
        }
    }
}
