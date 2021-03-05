namespace SIMCORE_TOOL.Assistant
{
    partial class FP_Assistant
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FP_Assistant));
            this.lbl_Titre = new System.Windows.Forms.Label();
            this.lbl_clef = new System.Windows.Forms.Label();
            this.lbl_value = new System.Windows.Forms.Label();
            this.txt_Clef = new System.Windows.Forms.TextBox();
            this.txt_Value = new System.Windows.Forms.TextBox();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OkAdd = new System.Windows.Forms.Button();
            this.lbl_GroundHandlers = new System.Windows.Forms.Label();
            this.cb_GroundHandlers = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbl_Titre
            // 
            resources.ApplyResources(this.lbl_Titre, "lbl_Titre");
            this.lbl_Titre.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Titre.Name = "lbl_Titre";
            // 
            // lbl_clef
            // 
            resources.ApplyResources(this.lbl_clef, "lbl_clef");
            this.lbl_clef.BackColor = System.Drawing.Color.Transparent;
            this.lbl_clef.Name = "lbl_clef";
            // 
            // lbl_value
            // 
            resources.ApplyResources(this.lbl_value, "lbl_value");
            this.lbl_value.BackColor = System.Drawing.Color.Transparent;
            this.lbl_value.Name = "lbl_value";
            // 
            // txt_Clef
            // 
            this.txt_Clef.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_Clef.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.txt_Clef, "txt_Clef");
            this.txt_Clef.Name = "txt_Clef";
            // 
            // txt_Value
            // 
            this.txt_Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txt_Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.txt_Value, "txt_Value");
            this.txt_Value.Name = "txt_Value";
            // 
            // btn_Ok
            // 
            resources.ApplyResources(this.btn_Ok, "btn_Ok");
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            resources.ApplyResources(this.btn_Cancel, "btn_Cancel");
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_OkAdd
            // 
            resources.ApplyResources(this.btn_OkAdd, "btn_OkAdd");
            this.btn_OkAdd.Name = "btn_OkAdd";
            this.btn_OkAdd.UseVisualStyleBackColor = true;
            this.btn_OkAdd.Click += new System.EventHandler(this.btn_OkAdd_Click);
            // 
            // lbl_GroundHandlers
            // 
            resources.ApplyResources(this.lbl_GroundHandlers, "lbl_GroundHandlers");
            this.lbl_GroundHandlers.BackColor = System.Drawing.Color.Transparent;
            this.lbl_GroundHandlers.Name = "lbl_GroundHandlers";
            this.lbl_GroundHandlers.Click += new System.EventHandler(this.lbl_GroundHandlers_Click);
            // 
            // cb_GroundHandlers
            // 
            this.cb_GroundHandlers.FormattingEnabled = true;
            resources.ApplyResources(this.cb_GroundHandlers, "cb_GroundHandlers");
            this.cb_GroundHandlers.Name = "cb_GroundHandlers";
            // 
            // FP_Assistant
            // 
            this.AcceptButton = this.btn_Ok;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.Controls.Add(this.cb_GroundHandlers);
            this.Controls.Add(this.lbl_GroundHandlers);
            this.Controls.Add(this.btn_OkAdd);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.txt_Value);
            this.Controls.Add(this.txt_Clef);
            this.Controls.Add(this.lbl_value);
            this.Controls.Add(this.lbl_clef);
            this.Controls.Add(this.lbl_Titre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FP_Assistant";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.FP_Assistant_Load);
            this.Shown += new System.EventHandler(this.FP_Assistant_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Titre;
        private System.Windows.Forms.Label lbl_clef;
        private System.Windows.Forms.Label lbl_value;
        private System.Windows.Forms.TextBox txt_Clef;
        private System.Windows.Forms.TextBox txt_Value;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OkAdd;
        private System.Windows.Forms.Label lbl_GroundHandlers;
        private System.Windows.Forms.ComboBox cb_GroundHandlers;
    }
}