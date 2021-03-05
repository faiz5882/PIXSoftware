using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SIMCORE_TOOL.Prompt
{
    public interface SIM_LoadingForm
    {
        void Reset();
        void Reset(int iValue);
        void ChargementFichier(String Fichier);
        void KillWindow();
        void setFileNumber(int iNombre);
        int getFileNumber();
        Boolean setChenillard { set; }
    }
    public partial class SIM_Chargement : Form, SIM_LoadingForm
    {
        delegate void ChargementCallBack(String Fichier);
        delegate void KillWindowCallBack();
        delegate void SetNumberFileCallBack(int Nombre);
        delegate void ResetCallBack(int iValue);
        delegate void setChenillardCallBack(Boolean bValue);
        System.Windows.Forms.Timer t_Timer;
        String sNextValue = "";

        public SIM_Chargement(Point Location_, Size Size_)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            Int32 Width_new = (Size_.Width - Size.Width) /2;
            Int32 Height_new = (Size_.Height - Size.Height)/2;
            Point Location_new = Location_;
            Location_new.X += Width_new;
            Location_new.Y += Height_new;
            Location = Location_new;
            pb_Chargement.Value = 0;
            lbl_Chargement.Text = "";
            t_Timer = null;
        }

        private void setChenillardCB(Boolean bValue)
        {
            if (bValue)
            {
                pb_Chargement.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                pb_Chargement.Style = ProgressBarStyle.Blocks;
            }
        }
        public Boolean setChenillard
        {
            set
            {
                if (this.InvokeRequired)
                {
                    setChenillardCallBack chgCB = new setChenillardCallBack(setChenillardCB);
                    this.Invoke(chgCB, value);
                }
                else
                {
                    setChenillardCB(value);
                }
            }
        }

        public void Reset()
        {
            Reset(0);
        }
        public void Reset(int iValue)
        {
            if (this.InvokeRequired)
            {
                ResetCallBack chgCB = new ResetCallBack(Reset);
                this.Invoke(chgCB, iValue);
            }
            else
            {
                if (pb_Chargement.Maximum < iValue)
                {
                    pb_Chargement.Value = pb_Chargement.Maximum;
                }
                else
                {
                    pb_Chargement.Value = iValue;
                }
            }
        }
        public void ChargementFichier(String Fichier)
        {
            if (this.InvokeRequired)
            {
                ChargementCallBack chgCB = new ChargementCallBack(ChargementFichier);
                this.Invoke(chgCB, Fichier);
            }
            else
            {
                if (pb_Chargement.Value < pb_Chargement.Maximum)
                    pb_Chargement.Value += 1;
                sNextValue = Fichier;
                if (t_Timer == null)
                {
                    t_Timer = new System.Windows.Forms.Timer();
                    t_Timer.Interval = 250;
                    t_Timer.Tick += new System.EventHandler(this.tRefresh_Tick);
                    t_Timer.Start();
                }
                //lbl_Chargement.Text = Fichier;// +"___" + pb_Chargement.Value.ToString();
            }
        }
        public void KillWindow()
        {
            if (this.InvokeRequired)
            {
                KillWindowCallBack kwCB = new KillWindowCallBack(KillWindow);
                this.Invoke(kwCB, null);
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }
        public void setFileNumber(int iNombre)
        {
            if (this.InvokeRequired)
            {
                SetNumberFileCallBack snfCB = new SetNumberFileCallBack(setFileNumber);
                this.Invoke(snfCB, iNombre);
            }
            else
            {
                pb_Chargement.Maximum = iNombre;
            }
        }
        public int getFileNumber()
        {
            return pb_Chargement.Maximum;
        }


        private void tRefresh_Tick(object sender, EventArgs e)
        {
            lbl_Chargement.Text = sNextValue;
        }

    }
}