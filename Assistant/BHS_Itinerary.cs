using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SIMCORE_TOOL.Classes;

namespace SIMCORE_TOOL.Assistant
{
    /// <summary>
    /// Explications about this class.
    /// 
    /// This class is use to define the itinerary table. This table is for define all the links between the 
    /// airport group. The particularity of this class is that it's a form which integrate 2 other form 
    /// which permit to add and move groups on the view. 
    /// 
    /// The form can be integrated on other form. Some function can be use on the class to save the modifications
    /// on the itinerary table.
    /// </summary>
    public partial class BHS_Itinerary : Form
    {
        #region Classe permettant de d�finir les diff�rentes caract�ristiques d'un objet.
        /// <summary>
        /// This class represent a group. It permit to keep his location, caption, and information about
        /// the mouse when it move the object.
        /// It contains an information about the color use as a border for the control.
        /// </summary>
        private class ParamsObject
        {
            public Point MousePositionInControl;
            private Point LastLocation_;
            public Point center;
            public String Caption;
            private String sDescription_;
            public String ToolTip
            {
                get
                {
                    return Caption + sDescription_;
                }
            }
            public String Description
            {
                get
                {
                    return sDescription_;
                }
                set
                {
                    sDescription_ = value;
                }
            }


            #region Gestion de la couleur du tour.
            private bool bUseWhite;
            private Color EncircleColor_;
            public Color EncircleColor
            {
                get
                {
                    if (bUseWhite)
                        return Color.White;
                    return EncircleColor_;
                }
                set
                {
                    if (value.A == 0)
                    {
                        bUseWhite = !bUseWhite;
                    }
                    else
                    {
                        EncircleColor_ = value;
                    }
                }
            }
            #endregion
            #region Gestion de la localisation du controle.
            public Point LastLocation
            {
                get
                {
                    return LastLocation_;
                }
                set
                {
                    LastLocation_ = value;
                }
            }
            #endregion
            public ParamsObject(Point MousePositionInControl_, Point location_, String caption_, String sDescription, Point size, Color EncircleColor__)
            {
                MousePositionInControl = MousePositionInControl_;
                LastLocation = location_;
                Caption = caption_;
                center = new Point(size.X / 2, size.Y / 2);
                EncircleColor_ = EncircleColor__;
                bUseWhite = false;
                sDescription_ = sDescription;
            }
        }
        #endregion

        #region Classe permettant de d�finir les diff�rentes fl�ches pr�sentes dans le sch�ma.
        private class ArrowParam
        {
            public String Origin;
            public String sOriginDescription;
            public String Goal;
            public String GoalDescription;

            public Boolean bSucceed;

            public String Distribution;
            public Double Param1;
            public Double Param2;
            public Double Param3;

            public ArrowParam(String Origin_, String sOriginDescription_, String Goal_, String GoalDescription_)
            {
                Origin = Origin_;
                sOriginDescription = sOriginDescription_;
                Goal = Goal_;
                GoalDescription = GoalDescription_;
                bSucceed = true;
                Distribution = "Constant";
                Param1 = 0;
                Param2 = 0;
                Param3 = 0;
            }
            public ArrowParam(String Origin_, String sOriginDescription_, String Goal_, String GoalDescription_, Boolean bSucceed_, String Distribution_, Double Param1_, Double Param2_, Double Param3_)
            {
                Origin = Origin_;
                sOriginDescription = sOriginDescription_;
                Goal = Goal_;
                GoalDescription = GoalDescription_;
                bSucceed = bSucceed_;

                Distribution = Distribution_;
                Param1 = Param1_;
                Param2 = Param2_;
                Param3 = Param3_;
            }
            public ArrowParam(ArrowParam param)
            {
                Origin = param.Origin;
                Goal = param.Goal;
                sOriginDescription = param.sOriginDescription;
                GoalDescription = param.GoalDescription;
                bSucceed = param.bSucceed;

                Distribution = param.Distribution;
                Param1 = param.Param1;
                Param2 = param.Param2;
                Param3 = param.Param3;
            }
        }
        #endregion

        #region Les diff�rentes variable de la fen�tre
        /// <summary>
        /// All the object (group) that exist on the Airport. The one who aren't place on the desktop are
        /// only invisible.
        /// </summary>
        Hashtable Objects;
        /// <summary>
        /// The arrow list. It contains all the arrow defined on the project.
        /// </summary>
        ArrayList Arrows;
        /// <summary>
        /// The List of the form for the arrows. It contains a list of classform showed on the 
        /// left side of the window.
        /// </summary>
        ArrayList ArrowAssistantList;

        /// <summary>
        /// The number of arrow to display for the selected group.
        /// </summary>
        int Displayed;
        /// <summary>
        /// Boolean which indicate if the current group had to move or not.
        /// </summary>
        bool bIsOnMove;
        /// <summary>
        /// The name of the selected group.
        /// </summary>
        String select;
        /// <summary>
        /// A boolean which is use to indicate that the user had clic on the Add all button or not.
        /// It serve to determine the emplacement where to add the new group.
        /// </summary>
        bool bAddAll = false;

        /// <summary>
        /// The pixel step use for move the elements.
        /// </summary>
        private static int PAS = 10;

        /// <summary>
        /// A boolean which indicate if the form contents had been modified or not.
        /// </summary>
        bool bAEteModifie = false;

        bool bEditable = false;

        /// <summary>
        /// The general class for the project.
        /// </summary>
        GestionDonneesHUB2SIM gdh_Datas_;
        /// <summary>
        /// A boolean to know if we must show the Arrow list or not.
        /// </summary>
        bool bShowArrowList;

        /// <summary>
        /// The airport structure defined on the project.
        /// </summary>
        TreeNode structureAirport;
        /// <summary>
        /// The datatable contained on the project.
        /// </summary>
        DataTable ItineraryTable;

        #endregion

        #region Les diff�rents constructeurs et fonctions d'initialisation du formulaire.

        #region Fonction d'initialisation du formulaire.
        private void Initialize(ArrayList lesElements_,
            ArrayList lesDescriptions_,
            TreeNode AirportStructure,
            DataTable ItineraryTable_,
            GestionDonneesHUB2SIM gdh_Datas__,
            EventHandler ehDraw_AssistantClick)
        {
            InitializeComponent();
            gdh_Datas_ = gdh_Datas__;
            Objects = new Hashtable();
            structureAirport = AirportStructure;
            ItineraryTable = ItineraryTable_;
            Arrows = new ArrayList();
            select = "";
            cb_Origin.Text = "";
            panel1.Tag = this;
            panel2.Tag = this;
            this.SuspendLayout();
            ArrowAssistantList = new ArrayList();
            ArrowAssistantList.Add(new Arrow_Assistant(gdh_Datas_, "", "","", "", true, "Constant", 0, 0, 0));
            ((Arrow_Assistant)ArrowAssistantList[0]).TopLevel = false;
            ((Arrow_Assistant)ArrowAssistantList[0]).Parent = panel1;
            ((Arrow_Assistant)ArrowAssistantList[0]).Location = new Point(0, 0);

            Displayed = 0;
            PAS = 10;
            bShowArrowList = true;
            showArrowListToolStripMenuItem_Click(null, null);
            bool bDescription = lesElements_.Count == lesDescriptions_.Count;
            for (int i = 0; i < lesElements_.Count; i++)
            {
                String GroupName = lesElements_[i].ToString();
                String Description = "";
                if (bDescription)
                    Description = " (" + lesDescriptions_[i].ToString() + ")";
                PictureBox picture = CreateNewObject(GroupName, Description);
                if (picture != null)
                {
                    Objects.Add(picture.Name, picture);
                }
            }
            this.PerformLayout();
            t_CheckUpdate.Start();
        }
        public void ChangeColor()
        {
            panel2.BackColor = PAX2SIM.Couleur1;
        }
        #endregion

        #region Fonction appel�e par les boutons du menu lorsque l'on clic dessus.
        void BHS_Assistant_Click(object sender, EventArgs e)
        {
            bAEteModifie = true;
            String newPicture = ((ToolStripItem)sender).Text;
            setSelect("");
            bIsOnMove = false;
            ((PictureBox)Objects[newPicture]).Location = new Point(0, 0);
            if (bAddAll)
            {
                int[] iType = OverallTools.DataFunctions.AnalyzeGroupName(((ParamsObject)((PictureBox)Objects[newPicture]).Tag).Caption);
                if (iType != null)
                {
                    ((PictureBox)Objects[newPicture]).Location = new Point(0,
                        ((iType[0] - 1) * GestionDonneesHUB2SIM.NbLevels + (iType[1] - 1)) *
                        (((PictureBox)Objects[newPicture]).Size.Height + 10 -
                            ((((PictureBox)Objects[newPicture]).Size.Height % PAS)))
                        );
                }
            }
            while (Intersection(((PictureBox)Objects[newPicture])) != null)
            {
                ((PictureBox)Objects[newPicture]).Location = new Point(((PictureBox)Objects[newPicture]).Location.X + ((PictureBox)Objects[newPicture]).Size.Width + (10 - (((PictureBox)Objects[newPicture]).Size.Width % PAS)), ((PictureBox)Objects[newPicture]).Location.Y);
            }
            ((ParamsObject)((PictureBox)Objects[newPicture]).Tag).LastLocation = ((PictureBox)Objects[newPicture]).Location;
            entitiesToolStripMenuItem.DropDownItems.Remove((ToolStripItem)sender);
            ((PictureBox)Objects[newPicture]).Parent = panel2;
            ((PictureBox)Objects[newPicture]).ContextMenuStrip = cmsPicture;
            ((PictureBox)Objects[newPicture]).BringToFront();
            ((PictureBox)Objects[newPicture]).Visible = true;
            cb_Origin.Items.Add(newPicture);
            OverallTools.FormFunctions.SortComboBox(cb_Origin);
        }

        private void addAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bAddAll = true;
            while (entitiesToolStripMenuItem.DropDownItems.Count != 2)
            {
                BHS_Assistant_Click(entitiesToolStripMenuItem.DropDownItems[2], null);
            }
            bAddAll = false;
        }
        #endregion

        #region Les constructeurs de la classe.
        public BHS_Itinerary(ArrayList lesElements_,
                                   ArrayList lesDescriptions_,
                                   TreeNode AirportStructure,
                                   DataTable ItineraryTable_,
                                   GestionDonneesHUB2SIM gdh_Datas__,
                                   EventHandler ehDraw_AssistantClick)
        {
            Initialize(lesElements_, lesDescriptions_, AirportStructure, ItineraryTable_, gdh_Datas__, ehDraw_AssistantClick);
            LoadArrows();
            entitiesToolStripMenuItem.Visible = !PAX2SIM.bRuntime;
        }
        #endregion

        #region Fonctions pour charger les fleches
        private void LoadArrows()
        {
            if (ItineraryTable == null)
                return;
            if ((ItineraryTable.Columns[2].DataType != typeof(Boolean)) &&
                (ItineraryTable.Columns[2].DataType != typeof(bool)) &&
                (ItineraryTable.Columns[4].DataType != typeof(double)) &&
                (ItineraryTable.Columns[4].DataType != typeof(Double)) &&
                (ItineraryTable.Columns[5].DataType != typeof(double)) &&
                (ItineraryTable.Columns[5].DataType != typeof(Double)) &&
                (ItineraryTable.Columns[6].DataType != typeof(double)) &&
                (ItineraryTable.Columns[6].DataType != typeof(Double)))
                return;
            Arrows.Clear();
            foreach (DataRow ligne in ItineraryTable.Rows)
            {
                String from = ligne.ItemArray[0].ToString();
                String to = ligne.ItemArray[1].ToString();
                if (Objects.ContainsKey(from) && Objects.ContainsKey(to))
                {
                    bool bSucceed = FonctionsType.getBoolean( ligne.ItemArray[2]);
                    String distribution = ligne.ItemArray[3].ToString();
                    Double param1, param2, param3;
                    if ((!Double.TryParse(ligne.ItemArray[4].ToString(), out param1)) ||
                        (!Double.TryParse(ligne.ItemArray[5].ToString(), out param2)) ||
                        (!Double.TryParse(ligne.ItemArray[6].ToString(), out param3)))
                        continue;

                    Arrows.Add(new ArrowParam(from,"", to,"", bSucceed, distribution, param1, param2, param3));
                }
            }
        }
        private bool setSelect(String newSelect)
        {
            bool bSaveChange = true;
            foreach (Arrow_Assistant aaTmp in ArrowAssistantList)
            {
                if (!aaTmp.bEnabled)
                    break;
                if (!aaTmp.estValide())
                {
                    if (MessageBox.Show("Some of the information about the current node are not valid. \nWould you like to discard yours changes ?.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return false;
                    }
                    bSaveChange = false;
                    break;
                }
            }
            if (bSaveChange)
            {
                //Ici sauvegarde des informations concernant les fleches.
                foreach (Arrow_Assistant arrow in ArrowAssistantList)
                {
                    if (arrow.bEnabled)
                    {
                        foreach (ArrowParam param in Arrows)
                        {
                            if ((param.Origin == select) && (param.Goal == arrow.Goal))
                            {
                                param.bSucceed = arrow.Succeed;
                                param.Distribution = arrow.Distribution;
                                param.Param1 = arrow.Param1;
                                param.Param2 = arrow.Param2;
                                param.Param3 = arrow.Param3;
                                break;
                            }
                        }
                    }
                }
            }
            if (select == newSelect)
                return true;
            if (select != "")
            {
                ((ParamsObject)((PictureBox)Objects[select]).Tag).EncircleColor = Color.Transparent;
                //((ParamsObject)((PictureBox)Objects[select]).Tag).EncircleColor = Color.FromArgb(255 - tmp.R, 255 - tmp.G, 255 - tmp.B);
                //OverallTools.FonctionUtiles.InvertColors((Bitmap)((PictureBox)Objects[select]).Image);
                //((PictureBox)Objects[select]).BorderStyle = BorderStyle.None;
            }
            select = newSelect;
            if (select != "")
            {

                ((ParamsObject)((PictureBox)Objects[select]).Tag).EncircleColor = Color.Transparent;
                //((ParamsObject)((PictureBox)Objects[select]).Tag).EncircleColor = Color.FromArgb(255 - tmp.R, 255 - tmp.G, 255 - tmp.B);
                //OverallTools.FonctionUtiles.InvertColors((Bitmap)((PictureBox)Objects[select]).Image);
                //((PictureBox)Objects[select]).BorderStyle = BorderStyle.FixedSingle;
            }
            cb_Origin.Text = select;
            UpdateList(select);
            panel2.Refresh();
            return true;
        }
        #endregion
        #endregion

        #region Les diff�rentes fonctions pour la gestion des d�placements des images
        private void PictureMouseDown(object sender, MouseEventArgs e)
        {
            setSelect(((PictureBox)sender).Name);
            if (PAX2SIM.bRuntime)
                return;
            if (!bEditable)
                return;
            if (e.Button == MouseButtons.Left)
            {
                bIsOnMove = true;
                ((ParamsObject)((PictureBox)sender).Tag).MousePositionInControl = e.Location;
                ((PictureBox)sender).BringToFront();
            }
            panel2.Refresh();
        }
        private void PictureMouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (select != "") && (bIsOnMove))
            {
                bAEteModifie = true;
                Point ScreenPosition = ((PictureBox)sender).PointToScreen(e.Location);
                Point pt1 = ((ParamsObject)((PictureBox)Objects[select]).Tag).MousePositionInControl;
                Point pt2 = panel2.PointToClient(ScreenPosition);
                Point newPosition = new Point(((pt2.X - pt1.X) / PAS) * PAS, ((pt2.Y - pt1.Y) / PAS) * PAS);
                if (((PictureBox)Objects[select]).Location != newPosition)
                {
                    ((PictureBox)Objects[select]).Location = newPosition;
                    panel2.Refresh();
                }
            }
            else if (e.Button != MouseButtons.Left)
            {
                bIsOnMove = false;
            }
        }

        public void PictureMouseLeave(object sender, EventArgs e)
        {
            if (!bIsOnMove)
                return;
            Point position = panel2.PointToScreen(new Point(0, 0));
            Point SecondPosition = new Point(panel2.Size);
            SecondPosition.X = position.X;// +hsb_Graph.LargeChange - 1;
            SecondPosition.Y = position.Y;// +vsb_Graph.LargeChange - 1;
            Thread.Sleep(1);
            if (!Intersection(MousePosition, position, SecondPosition))
            {
                PictureMouseUp(null, null);
            }
        }
        public void PictureMouseUp(object sender, MouseEventArgs e)
        {
            if (select == "")
                return;
            if (!bEditable)
                return;
            bIsOnMove = false;
            PictureBox pic1 = (PictureBox)Objects[select];
            String[] Intersect = Intersection(pic1);
            if (Intersect == null)
            {
                Point pLocation = new Point(pic1.Location.X + panel2.HorizontalScroll.Value, pic1.Location.Y + panel2.VerticalScroll.Value);
                if ((pLocation.X < 0) || (pLocation.Y < 0))
                {

                    Point ptDecalage = new Point(0, 0);
                    if (pLocation.X < 0)
                        ptDecalage.X = -pLocation.X;
                    if (pLocation.Y < 0)
                        ptDecalage.Y = -pLocation.Y;
                    foreach (PictureBox ptTmp in Objects.Values)
                    {
                        ptTmp.Top += ptDecalage.Y;
                        ptTmp.Left += ptDecalage.X;
                        ((ParamsObject)ptTmp.Tag).LastLocation = ptTmp.Location;
                    }
                }
                ((ParamsObject)pic1.Tag).LastLocation = pLocation;
                panel2.Refresh();
                return;
            }

            bool intersect = false;
            bool[] present = new bool[Intersect.Length];
            int i;
            for (i = 0; i < present.Length; i++)
            {
                present[i] = false;
            }
            foreach (ArrowParam param in Arrows)
            {
                if (param.Origin == pic1.Name)
                {
                    for (i = 0; i < Intersect.Length; i++)
                    {
                        if (param.Goal == Intersect[i])
                        {
                            present[i] = true;
                        }
                    }
                }
            }
            this.SuspendLayout();
            ParamsObject poObject = null;
            for (i = 0; i < present.Length; i++)
            {
                if (!present[i])
                {
                    poObject = (ParamsObject)pic1.Tag;
                    intersect = true;
                    Arrows.Add(new ArrowParam(pic1.Name, poObject.Description, Intersect[i], ""));

                }
            }
            this.PerformLayout();
            if (intersect)
            {
                Point pLocation = ((ParamsObject)pic1.Tag).LastLocation;
                pLocation.X -= panel2.HorizontalScroll.Value;
                pLocation.Y -= panel2.VerticalScroll.Value;
                pic1.Location = pLocation;
                UpdateList(null);
                panel2.Refresh();
            }
            else
            {
                ((ParamsObject)pic1.Tag).LastLocation = pic1.Location;
                panel2.Refresh();
                return;
            }
        }
        #endregion

        #region Fonction utilis�es pour d�terminer si deux images se percutent

        public String[] Intersection(PictureBox picture)
        {
            ArrayList listeIntersections = new ArrayList();
            foreach (String key in Objects.Keys)
            {
                if (picture.Name == key)
                    continue;
                PictureBox pic2 = (PictureBox)Objects[key];
                if (pic2.Parent == null)
                    continue;
                if (Intersection(picture.Location, new Point(picture.Size), pic2.Location, new Point(pic2.Size)))
                {
                    listeIntersections.Add(key);
                }
            }
            if (listeIntersections.Count > 0)
            {
                String[] resultat = new String[listeIntersections.Count];
                for (int i = 0; i < listeIntersections.Count; i++)
                {
                    resultat[i] = listeIntersections[i].ToString();
                }
                return resultat;
            }
            return null;
        }

        public bool Intersection(Point location1, Point size1, Point location2, Point size2)
        {
            //On calcul la position du second coin extr�me.
            Point secondPoint1 = new Point(location1.X + size1.X, location1.Y + size1.Y);

            Point secondPoint2 = new Point(location2.X, location2.Y + size2.Y);
            Point ThirdPoint2 = new Point(location2.X + size2.X, location2.Y + size2.Y);
            Point FourthPoint2 = new Point(location2.X + size2.X, location2.Y);

            //D�sormais on teste si un des quatres points de la seconde figure est contenue entre les deux
            //points extr�mes.
            return ((Intersection(location2, location1, secondPoint1)) || (Intersection(secondPoint2, location1, secondPoint1))
                || (Intersection(ThirdPoint2, location1, secondPoint1)) || (Intersection(FourthPoint2, location1, secondPoint1)));

        }
        public bool Intersection(Point pointaTeste, Point firstPoint, Point secondPoint)
        {
            if ((pointaTeste.X >= firstPoint.X) && (pointaTeste.X <= secondPoint.X) &&
                (pointaTeste.Y >= firstPoint.Y) && (pointaTeste.Y <= secondPoint.Y))
                return true;
            return false;
        }
        #endregion

        #region Fonction pour mettre � jour l'affichage lors du redimensionnement de la fen�tre
        private void BHS_Itinerary_Resize(object sender, EventArgs e)
        {
            if (bShowArrowList)
            {
                this.panel1.Height = this.ClientRectangle.Height - panel1.Location.Y;
                this.panel1.Width = ((Arrow_Assistant)ArrowAssistantList[0]).Width + vsb_Liste.Width;
                cb_Origin.Width = this.panel1.Size.Width;
                this.panel2.Location = new Point(this.panel1.Location.X + this.panel1.Size.Width, this.panel2.Location.Y);
                MAJ_ScrollBar();
            }
            else
            {
                this.panel2.Location = new Point(this.panel1.Location.X, this.panel2.Location.Y);
            }
            this.panel2.Height = this.ClientRectangle.Height - panel2.Location.Y;
            if (this.panel2.Height < 0)
                return;
            this.panel2.Width = this.ClientRectangle.Width - panel2.Location.X;
            menuStrip1.Width = this.ClientRectangle.Width;
        }
        #endregion

        #region Fonctions pour la mise � jour de l'affichage de la liste

        private void cb_Origin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ArrowAssistantList == null)
                return;
            if (select == cb_Origin.Text)
                return;

            if (!setSelect(cb_Origin.Text))
            {
                cb_Origin.Text = select;
            }
        }
        private void UpdateList(String sSelected)
        {
            Size ElementSize = ((Arrow_Assistant)ArrowAssistantList[0]).Size;
            vsb_Liste.Enabled = false;
            int i = 0;
            if (((sSelected == null) && (cb_Origin.Text != "")) || (sSelected != ""))
            {
                foreach (ArrowParam param in Arrows)
                {
                    if (param.Origin == cb_Origin.Text)
                    {
                        if (ArrowAssistantList.Count <= i)
                        {
                            ArrowAssistantList.Add(new Arrow_Assistant(gdh_Datas_, param.Origin,param.sOriginDescription, param.Goal,param.GoalDescription, param.bSucceed, param.Distribution, param.Param1, param.Param2, param.Param3));
                            ((Arrow_Assistant)ArrowAssistantList[i]).TopLevel = false;
                            ((Arrow_Assistant)ArrowAssistantList[i]).Parent = panel1;
                            ((Arrow_Assistant)ArrowAssistantList[i]).Location = new Point(0, i * ElementSize.Height);
                            ((Arrow_Assistant)ArrowAssistantList[i]).Show();
                        }
                        else
                        {
                            ((Arrow_Assistant)ArrowAssistantList[i]).setDatas(param.Origin, param.sOriginDescription, param.Goal, param.GoalDescription, param.bSucceed, param.Distribution, param.Param1, param.Param2, param.Param3);
                        }
                        ((Arrow_Assistant)ArrowAssistantList[i]).Visible = true;
                        ((Arrow_Assistant)ArrowAssistantList[i]).bEnabled = true;

                        i++;
                    }
                }
            }
            //On masque tous les autres parties de formulaire destin�es � afficher les d�finitions des fl�ches
            Displayed = i;
            for (; i < ArrowAssistantList.Count; i++)
            {
                ((Arrow_Assistant)ArrowAssistantList[i]).Visible = false;
                ((Arrow_Assistant)ArrowAssistantList[i]).bEnabled = false;
            }
            MAJ_ScrollBar();
        }
        #endregion

        #region Les diff�rentes fonctions utilis�es pour les barres de scroll
        private void MAJ_ScrollBar()
        {
            if (panel1.Height < 0)
                return;
            int tailleAffichage = panel1.Height;

            Size ElementSize = ((Arrow_Assistant)ArrowAssistantList[0]).Size;
            int tailleObjet = ElementSize.Height;
            vsb_Liste.Value = 0;
            if (((Displayed) * tailleObjet) > tailleAffichage)
            {
                vsb_Liste.Enabled = true;

                vsb_Liste.Maximum = (Displayed) * tailleObjet;
                vsb_Liste.LargeChange = tailleAffichage;
                vsb_Liste.Minimum = 0;
            }
            else
            {
                vsb_Liste.Enabled = false;
                vsb_Liste.Maximum = 10;
                vsb_Liste.LargeChange = 10;
                vsb_Liste.Minimum = 0;
            }
            vsb_Liste_Scroll(null, null);
        }

        private void vsb_Liste_Scroll(object sender, ScrollEventArgs e)
        {
            Size ElementSize = ((Arrow_Assistant)ArrowAssistantList[0]).Size;
            int tailleObjet = ElementSize.Height;
            for (int i = 0; i < ArrowAssistantList.Count; i++)
            {
                ((Arrow_Assistant)ArrowAssistantList[i]).Location = new Point(0, i * tailleObjet - vsb_Liste.Value);
            }
        }

        #endregion

        #region Les diff�rentes fonctions appel�es par l'assistant pour les trac�s.
        internal void DeleteGoal(string p)
        {
            int iIndex = -1;
            int i = 0;
            foreach (ArrowParam param in Arrows)
            {
                if ((param.Origin == cb_Origin.Text) && (param.Goal == p))
                {
                    iIndex = i;
                    break;
                }
                i++;
            }
            if (iIndex == -1)
                return;
            bAEteModifie = true;
            Arrows.RemoveAt(iIndex);
            UpdateList(null);
            //cb_Origin_SelectedIndexChanged(null, null);
            MAJ_ScrollBar();
            vsb_Liste_Scroll(null, null);
            this.Refresh();
        }

        internal String getSelect()
        {
            return select;
        }

        internal Hashtable getPictures()
        {
            return Objects;
        }
        internal bool isOnMove()
        {
            return bIsOnMove;
        }

        internal ArrayList getArrows()
        {
            return Arrows;
        }
        #endregion

        #region Fonctions pour d�terminer quels sont les successeurs de l'actuel point.

        private ArrayList getSuccesseurs(String OriginGroup)
        {
            ArrayList Resultats = new ArrayList();
            foreach (ArrowParam param in Arrows)
            {
                if (param.Origin == OriginGroup)
                {
                    Resultats.Add(new ArrowParam(param));
                }
            }
            return Resultats;
        }
        #endregion

        #region Fonctions pour la gestion de la sortie de cette fen�tre.
        private void discardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }


        private void saveChangesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveChanges();
        }


        private void Ressource_Assistant_FormClosing(object sender, FormClosingEventArgs e)
        {
            panel2.Focus();
            if (bAEteModifie)
            {
                switch (MessageBox.Show("Do you want to save changes ?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                {
                    case DialogResult.Cancel:
                        this.DialogResult = DialogResult.None;
                        break;
                    case DialogResult.Yes:
                        saveChanges();
                        DialogResult = DialogResult.OK;
                        break;
                    case DialogResult.No:
                        this.DialogResult = DialogResult.Cancel;
                        break;
                }
            }
        }
        #endregion

        #region Fonctions pour mettre � jour les tables li�es � cet assistant
        public bool getAEteModifie()
        {
            foreach (Arrow_Assistant arrow in ArrowAssistantList)
            {
                if ((arrow.bEnabled) && (arrow.AEteModifie))
                    return true;
            }
            return bAEteModifie;
        }

        public void saveChanges()
        {
            bEditable = false;
            modifyItineraryToolStripMenuItem.Visible = true;
            bAEteModifie = false;
            gdh_Datas_.setAEteEnregistre = false;
            setSelect(cb_Origin.Text);
            if (ItineraryTable.Columns.Count != GestionDonneesHUB2SIM.ListeEntete_BHS_Itinerary.Length)
                return;

            ArrayList absents = UpdateLocationsInformations(structureAirport);
            ItineraryTable.Rows.Clear();
            ArrayList liste;
            foreach (String key in Objects.Keys)
            {
                if (absents.Contains(key))
                    continue;
                liste = getSuccesseurs(key);
                if (liste.Count != 0)
                {
                    foreach (ArrowParam param in liste)
                    {
                        if (absents.Contains(param.Goal))
                            continue;
                        DataRow newLine = ItineraryTable.NewRow();
                        newLine.BeginEdit();
                        newLine[0] = key;
                        newLine[1] = param.Goal;
                        newLine[2] = param.bSucceed;
                        newLine[3] = param.Distribution;
                        newLine[4] = param.Param1;
                        newLine[5] = param.Param2;
                        newLine[6] = param.Param3;
                        ItineraryTable.Rows.Add(newLine);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AirportRacine"></param>
        /// <returns></returns>
        private ArrayList UpdateLocationsInformations(TreeNode AirportRacine)
        {
            ArrayList absents = new ArrayList();
            foreach (String key in Objects.Keys)
            {
                TreeNode node = OverallTools.TreeViewFunctions.getAirportChild(AirportRacine, key);
                if (node == null)
                {
                    absents.Add(key);
                    continue;
                }
                TreeViewTag tag = (TreeViewTag)node.Tag;
                tag.BHSLocation = new Point(((PictureBox)Objects[key]).Location.X, ((PictureBox)Objects[key]).Location.Y);
                tag.BHSVisible = (((PictureBox)Objects[key]).Parent != null);
            }
            return absents;
        }
        #endregion

        #region Fonction pour afficher et masquer la liste des fl�ches.
        private void showArrowListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bShowArrowList = showArrowListToolStripMenuItem.Checked;
            this.panel1.Visible = bShowArrowList;
            this.cb_Origin.Visible = bShowArrowList;
            BHS_Itinerary_Resize(null, null);
        }
        #endregion

        #region Fonctions qui servent � mettre � jour l'affichage.
        /// <summary>
        /// Fonction qui s'occupe de remettre � jour l'affichage dans l'itineraryView 
        /// lors de la modification de l'a�roport, ou des informations sur les vitesses.
        /// </summary>
        /// <param name="lesElements"></param>
        /// <param name="ItineraryTable_"></param>
        /// <param name="AirportStructure"></param>
        internal void UpdateInformations(ArrayList lesElements, ArrayList lesDescriptions, DataTable ItineraryTable_, TreeNode AirportStructure, EventHandler Draw_Assistant_Click)
        {
            setSelect("");

            UpdateList(null);
            structureAirport = AirportStructure;
            ItineraryTable = ItineraryTable_;
            if (lesElements == null)
                return;

            while (entitiesToolStripMenuItem.DropDownItems.Count > 2)
                entitiesToolStripMenuItem.DropDownItems.RemoveAt(2);
            cb_Origin.Items.Clear();
            if (lesElements.Count == 0)
            {
                Arrows.Clear();
                foreach (PictureBox pic in Objects.Values)
                {
                    pic.Dispose();
                }
                Objects.Clear();
                bAEteModifie = false;
                return;
            }
            if ((ItineraryTable.Columns[2].DataType != typeof(Double)) &&
                (ItineraryTable.Columns[2].DataType != typeof(double)) &&
                (ItineraryTable.Columns[3].DataType != typeof(Double)) &&
                (ItineraryTable.Columns[3].DataType != typeof(double)) &&
                (ItineraryTable.Columns[4].DataType != typeof(Double)) &&
                (ItineraryTable.Columns[4].DataType != typeof(double)))
                return;
            bAEteModifie = false;
            int i;
            ArrayList entete = new ArrayList(Objects.Keys);

            setSelect("");
            bIsOnMove = false;
            bool bDescription = lesElements.Count == lesDescriptions.Count;
            for (i = 0; i < entete.Count; i++)
            {
                PictureBox picture = (PictureBox)Objects[entete[i]];
                if (lesElements.Contains(picture.Name))
                {
                    String sDescription = "";
                    if (bDescription)
                    {
                        sDescription = (String)lesDescriptions[lesElements.IndexOf(picture.Name)];
                        lesDescriptions.RemoveAt(lesElements.IndexOf(picture.Name));
                    }
                    lesElements.RemoveAt(lesElements.IndexOf(picture.Name));

                    if (structureAirport != null)
                    {
                        ((ParamsObject)picture.Tag).Description = " (" + sDescription + ")";
                        TreeNode child = OverallTools.TreeViewFunctions.getAirportChild(structureAirport, picture.Name);
                        if (child != null)
                        {
                            if (((TreeViewTag)child.Tag).BHSVisible)
                            {
                                picture.Location = ((TreeViewTag)child.Tag).BHSLocation;
                                picture.Parent = panel2;
                                ((ParamsObject)picture.Tag).LastLocation = picture.Location;
                                picture.BringToFront();
                                picture.Visible = true;
                                picture.ContextMenuStrip = cmsPicture;
                                cb_Origin.Items.Add(picture.Name);
                            }
                            else
                            {
                                //Si le mode d'affichage de l'objet a �t� modifi�, c'est que l'objet est d�j� plac� sur
                                //l'espace de travail. sinon on doit le rendre accessible dans le menu d�roulant.
                                OverallTools.FonctionUtiles.AddSortedItem(entitiesToolStripMenuItem, picture.Name, 2).Click += new EventHandler(BHS_Assistant_Click);
                                picture.Location = new System.Drawing.Point(-1, -1);
                                picture.Visible = false;
                                picture.Parent = null;
                            }
                        }
                    }
                    continue;
                }
                else
                {
                    Objects.Remove(picture.Name);
                    entete.RemoveAt(i);
                    i--;
                    picture.Dispose();
                }
            }

            bDescription = lesElements.Count == lesDescriptions.Count;
            for (i = 0; i < lesElements.Count; i++)
            //String GroupName in lesElements_)
            {
                String GroupName = lesElements[i].ToString();
                String Description = "";
                if (bDescription)
                    Description = " (" + lesDescriptions[i].ToString() + ")";
                PictureBox picture = CreateNewObject(GroupName, Description);
                if (picture != null)
                {
                    Objects.Add(picture.Name, picture);
                }
            }
            /*foreach (String GroupName in lesElements)
            {
                PictureBox picture = CreateNewObject(GroupName);
                if (picture != null)
                {
                    Objects.Add(picture.Name, picture);
                }
            }*/
            LoadArrows();
            setScenario(gdh_Datas_, Draw_Assistant_Click);
            this.Refresh();
        }
        private void setScenario(GestionDonneesHUB2SIM gdh_Data, EventHandler Draw_Assistant_Click)
        {
            //Suppression des anciens sc�narios pr�sents.
            while (cmsPicture.Items.Count > 2)
            {
                cmsPicture.Items.RemoveAt(2);
            }
            List<String> ScenarioNames = gdh_Data.getScenarioPAXBHS();
            if(ScenarioNames != null)
            foreach (String Name in ScenarioNames)
            {
                cmsPicture.Items.Add(Name);
                cmsPicture.Items[cmsPicture.Items.Count - 1].Click += Draw_Assistant_Click;
            }
        }


        private int getIndexImage(String sGroupName)
        {
            if(sGroupName.Contains(PAX2SIM.sCheckInGroup))
                return 0;
            if(sGroupName.Contains("Baggage Claim Group"))
                return 1;
            if (sGroupName.Contains("Check In Collector Group"))
                return 2;
            if (sGroupName.Contains(GestionDonneesHUB2SIM.BHS_String_TransferInfeedGroup))
                return 3;
            if (sGroupName.Contains("HBS Lev1 Group"))
                return 4;
            if (sGroupName.Contains("HBS Lev3 Group"))
                return 5;
            if (sGroupName.Contains("HBS Lev5 Group"))
                return 6;
            if (sGroupName.Contains("MES Group"))
                return 7;
            if (sGroupName.Contains("EBS Group"))
                return 8;
            if (sGroupName.Contains("Make-Up Group"))
                return 9;
            if (sGroupName.Contains("HBS Custom Group"))
                return 10;
            if (sGroupName.Contains(GestionDonneesHUB2SIM.BHS_String_ArrivalInfeedGroup))
                return 11;
            return -1;
        }
        private PictureBox CreateNewObject(String GroupName, String sDescription)
        {
            PictureBox Picture = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(Picture)).BeginInit();
            #region //ici on choisit l'image de ce nouveau control.
            //PAX2SIM.ListeNomsObjetsGroup

            int iIndexPicture = getIndexImage(GroupName);
            if (iIndexPicture == -1)
                iIndexPicture = 0;
            Picture.Image = imageList1.Images[iIndexPicture];
            Picture.BackColor = Color.Transparent;
            #endregion

            bool bEstPlace = false;
            if (structureAirport != null)
            {
                TreeNode child = OverallTools.TreeViewFunctions.getAirportChild(structureAirport, GroupName);
                if (child != null)
                {
                    if (((TreeViewTag)child.Tag).BHSVisible)
                    {
                        Picture.Location = ((TreeViewTag)child.Tag).BHSLocation;
                        Picture.Parent = panel2;
                        Picture.ContextMenuStrip = cmsPicture;
                        Picture.BringToFront();
                        bEstPlace = true;
                        cb_Origin.Items.Add(GroupName);
                    }
                }
            }
            if (!bEstPlace)
            {
                //Si le mode d'affichage de l'objet a �t� modifi�, c'est que l'objet est d�j� plac� sur
                //l'espace de travail. sinon on doit le rendre accessible dans le menu d�roulant.
                OverallTools.FonctionUtiles.AddSortedItem(entitiesToolStripMenuItem, GroupName, 2).Click += new EventHandler(BHS_Assistant_Click);
                /*entitiesToolStripMenuItem.DropDownItems.Add(GroupName);
                entitiesToolStripMenuItem.DropDownItems[entitiesToolStripMenuItem.DropDownItems.Count - 1].Click += new EventHandler(Ressource_Assistant_Click);*/
                Picture.Location = new System.Drawing.Point(-1, -1);
                Picture.Visible = false;
                Picture.Parent = null;
            }

            Picture.Name = GroupName;
            Picture.Size = Picture.Image.Size;
            Picture.Paint += new PaintEventHandler(Picture_Paint);
            Picture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureMouseDown);
            Picture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureMouseMove);
            Picture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureMouseUp);
            Picture.MouseLeave += new EventHandler(this.PictureMouseLeave);
            Picture.DoubleClick += new EventHandler(Picture_DoubleClick);
            Double Position = (((Double)1) / GestionDonneesHUB2SIM.NbLevels) * Math.PI;
            Color encircleColor = Color.FromArgb((int)Math.Abs(Math.Cos(Position + 2.0 * Math.PI / 3.0) * 255), (int)Math.Abs(Math.Cos(Position + Math.PI / 3.0) * 255), (int)Math.Abs(Math.Cos(Position) * 255));

            Picture.Tag = new ParamsObject(new Point(16, 16), Picture.Location, Picture.Name, sDescription, new Point(Picture.Size), encircleColor);

            tt_ToolTip.SetToolTip(Picture, ((ParamsObject)Picture.Tag).ToolTip);
            return Picture;
        }

        // << Task #8731 Pax2Sim - Adapt the ProcessAssistant to include the new time distribution        
        /// <summary>
        /// The method is not used anymore - it was used on the old Itinerary when 2xClick-ing on a Picture representing a Group.
        /// Now id doesn't do anything (the code inside is commented).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Picture_DoubleClick(object sender, EventArgs e)
        {            
            /* 
            if (OverallTools.DataFunctions.indexLigne(gdh_Datas_.getTable("Input", "Times_ProcessTable"), 0, ((PictureBox)sender).Name) != -1)
            {
                Assistant.Process_Assistant pa = new SIMCORE_TOOL.Assistant.Process_Assistant(gdh_Datas_.getTable("Input", "Times_ProcessTable"), gdh_Datas_.getTable("Input", "OneofSpecificationTable"), ((PictureBox)sender).Name);
                pa.ShowDialog();
                pa.Dispose();
            }
             */             
        }
        // >> Task #8731 Pax2Sim - Adapt the ProcessAssistant to include the new time distribution

        void Picture_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(((ParamsObject)((PictureBox)sender).Tag).EncircleColor, 3);
            Graphics g = e.Graphics;
            g.DrawLine(p, new Point(0, 0), new Point(0, ((PictureBox)sender).Width - 1));
            g.DrawLine(p, new Point(0, 0), new Point(((PictureBox)sender).Height - 1, 0));
            g.DrawLine(p, new Point(0, ((PictureBox)sender).Width - 1), new Point(((PictureBox)sender).Height - 1, ((PictureBox)sender).Width - 1));
            g.DrawLine(p, new Point(((PictureBox)sender).Height - 1, ((PictureBox)sender).Width - 1), new Point(((PictureBox)sender).Height - 1, 0));
        }
        #endregion

        #region Fonction pour supprimer le groupe s�lection�
        public void DeleleSelectedNode()
        {

            String TheSelected = select;
            if (select == "")
                return;
            bAEteModifie = true;
            setSelect("");
            OverallTools.FonctionUtiles.AddSortedItem(entitiesToolStripMenuItem, TheSelected, 2).Click += new EventHandler(BHS_Assistant_Click);
            /*entitiesToolStripMenuItem.DropDownItems.Add(TheSelected);
            entitiesToolStripMenuItem.DropDownItems[entitiesToolStripMenuItem.DropDownItems.Count - 1].Click += new EventHandler(Ressource_Assistant_Click);*/
            PictureBox Picture = (PictureBox)Objects[TheSelected];
            Picture.Location = new System.Drawing.Point(-1, -1);
            Picture.Visible = false;
            Picture.Parent = null;
            DeleteArrowFromSelectedNode(TheSelected);
            cb_Origin.Items.Remove(TheSelected);
            panel2.Refresh();
            this.Refresh();
        }
        private void DeleteArrowFromSelectedNode(String sGroup)
        {
            for (int i = 0; i < Arrows.Count; i++)
            {
                if ((((ArrowParam)Arrows[i]).Goal == sGroup) || (((ArrowParam)Arrows[i]).Origin == sGroup))
                {
                    Arrows.RemoveAt(i);
                    i--;
                }
            }
        }
        #endregion

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Hide element will delete any connection with other elements. Do you really want to remove it ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                DeleleSelectedNode();
        }

        #region La fonction qui s'occupe de redessiner les fl�ches apr�s chaque modification de l'utilisateur.
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

            //if (((Ressource_Assistant)Parent.Tag).isOnMove())
            //    return;
            Point pt1, pt2;
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.FromArgb(90, 90, 90), 0.5f);
            Pen pRed = new Pen(Color.FromArgb(255, 0, 0), 0.5f);
            p.EndCap = System.Drawing.Drawing2D.LineCap.Custom;
            p.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(4, 4);
            pRed.EndCap = System.Drawing.Drawing2D.LineCap.Custom;
            pRed.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(4, 4);
            for (int i = 0; i < Arrows.Count; i++)
            {
                ArrowParam param = (ArrowParam)Arrows[i];
                PictureBox pic1 = (PictureBox)Objects[param.Origin];
                PictureBox pic2 = (PictureBox)Objects[param.Goal];
                OverallTools.ImageFunctions.PointToPoint(pic1, pic2, out pt1, out pt2);
                if (param.bSucceed)
                    g.DrawLine(p, pt1, pt2);
                else
                    g.DrawLine(pRed, pt1, pt2);
            }
            p.Dispose();
            g.Dispose();
            int iXmax = 0;
            int iYmax = 0;
            foreach (PictureBox pbTmp in Objects.Values)
            {
                if (pbTmp.Location.X > iXmax)
                    iXmax = pbTmp.Location.X;
                if (pbTmp.Location.Y > iYmax)
                    iYmax = pbTmp.Location.Y;
            }
            if (lbl_position.Location.X > iXmax)
                iXmax = lbl_position.Location.X;

            if (lbl_position.Location.Y > iYmax)
                iYmax = lbl_position.Location.Y;
            lbl_position.Location = new Point(iXmax, iYmax);
        }
        #endregion

        private void t_CheckUpdate_Tick(object sender, EventArgs e)
        {
            if (!this.getAEteModifie())
                return;
            //To be sure that when the checkbox for the succeed value is changed, that the visualisation is changed too.
            foreach (Arrow_Assistant aaTmp in ArrowAssistantList)
            {
                if (!aaTmp.bEnabled)
                    break;
                foreach (ArrowParam param in Arrows)
                {
                    if ((param.Origin == aaTmp.Origin) && (param.Goal == aaTmp.Goal))
                    {
                        param.bSucceed = aaTmp.Succeed;
                        break;
                    }
                }
            }
            panel2.Invalidate();
        }

        private void modifyItineraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bEditable = true;
            modifyItineraryToolStripMenuItem.Visible= false;
        }
    }
        
}