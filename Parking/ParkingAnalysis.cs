using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace SIMCORE_TOOL.Parking
{
    public partial class ParkingAnalysis : Form
    {
        ParkingCalcs pcTmp;
        List<ParkingCalcs> pcColumns;
        DataTable dtGeneral_;
        DataTable dtOccupationTime_;
        DataTable dtModaleRepartition_;
        DataTable dtDistributRT_;

        public ParkingAnalysis(DataTable dtGeneral, 
            DataTable dtOccupationTime, 
            DataTable dtModaleRepartition, 
            DataTable dtDistributRT)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
                if (dtDistributRT == null)
                return;
            pcColumns = new List<ParkingCalcs>();
            pcTmp = new ParkingCalcs(dtDistributRT,dtOccupationTime);
            pcColumns.Add(pcTmp);
            panel1.Controls.Add(pcTmp.getPanel());
            pcTmp.getPanel().Parent = panel1;
            pcTmp.getPanel().Location = new Point(225, 0);
            pcTmp.getPanel().Show();
            panel1.PerformLayout();
            pcTmp.FillControl(dtGeneral, dtModaleRepartition, dtOccupationTime,dtDistributRT);
            dtGeneral_ = dtGeneral;
            dtOccupationTime_=dtOccupationTime;
            dtModaleRepartition_=dtModaleRepartition;
            dtDistributRT_=dtDistributRT;


            int i = 0;
            for(int j=2;j<dtDistributRT.Columns.Count;j++)
            {
                Label lblTmp = new Label();
                groupBox2.Controls.Add(lblTmp);
                lblTmp.Location = new System.Drawing.Point(6, i * 22 + 20);
                lblTmp.Name = "lbl_PendantPointeFR" + i.ToString();
                lblTmp.Text = "Complementary traffic for" + dtDistributRT.Columns[j].ColumnName;
                //lblTmp.Text = line[0].ToString();
                lblTmp.AutoSize = true;
                i++;
                lblTmp = new Label();
                groupBox2.Controls.Add(lblTmp);
                lblTmp.Location = new System.Drawing.Point(6, i * 22 + 20);
                lblTmp.Name = "lbl_PendantPointeFR" + i.ToString();
                lblTmp.Text = "Distribution during PH "+dtDistributRT.Columns[j].ColumnName;
                lblTmp.AutoSize = true;
                i++;
            }
            if (i == 0)
            {
                groupBox2.Visible = false;
                groupBox3.Location = new Point(9,groupBox1.Location.Y + groupBox1.Size.Height+ 6);
            }
            else
            {
                groupBox2.Size = new System.Drawing.Size(groupBox2.Width, 20 + 22 * i);
                groupBox3.Location = new Point(9,groupBox2.Location.Y + groupBox2.Size.Height+ 6);
            }
            i=0;
            for (; i < dtOccupationTime.Rows.Count; i++)
            {
                Label lblTmp = new Label();
                groupBox5.Controls.Add(lblTmp);
                lblTmp.Location = new System.Drawing.Point(6, (i+1) * 22);
                lblTmp.Name = "lbl_Occupation_" + i.ToString();
                lblTmp.Text = "]"  +dtOccupationTime.Rows[i][0].ToString() + ";"+dtOccupationTime.Rows[i][1].ToString() + "]";
                lblTmp.AutoSize = true;
            }
            groupBox5.Visible = (i!=0);
            groupBox5.Size = new System.Drawing.Size(groupBox5.Size.Width, 22 * (i + 1));
            groupBox4.Location = new Point(9, groupBox3.Location.Y + groupBox3.Size.Height + 6);
            groupBox5.Location = new Point(9, groupBox4.Location.Y + groupBox4.Size.Height + 6);
            if (i != 0)
                groupBox6.Location = new Point(9, groupBox5.Location.Y + groupBox5.Size.Height + 6);
            else
                groupBox6.Location = new Point(9, groupBox4.Location.Y + groupBox4.Size.Height + 6);
            panel1.Size = new Size(panel1.Width, groupBox6.Location.Y + groupBox6.Height);
        }


        internal DataTable Save()
        {
            pcTmp.SaveControl(dtGeneral_, dtModaleRepartition_, dtOccupationTime_, dtDistributRT_);
            return null;
        }

        private void ParkingAnalysis_FormClosing(object sender, FormClosingEventArgs e)
        {
            //OverallTools.FormFunctions.SaveImage(panel1).Save("D://Test.bmp",ImageFormat.Bmp);
            //SaveImage();
        }
    }
}
