using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.Assistant
{
    public partial class ParkingAllocation : Form
    {
        public ParkingAllocation(DataTable ParkingTable)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            foreach (DataRow ligne in ParkingTable.Rows)
            {
                cb_Parking.Items.Add(ligne[0]);
            }
        }
    }
}