using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

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
    public partial class Ressource_Assistant : Form
    {
        #region Classe permettant de définir les différentes caractéristiques d'un objet.
        /// <summary>
        /// This class represent a group. It permit to keep his location, caption, and information about
        /// the mouse when it move the object.
        /// It contains an information about the color use as a border for the control.
        /// </summary>
        public class ParamsObject
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
                    sDescription_ =value;
                }
            }


            #region Gestion de la couleur du tour.
            private bool bUseWhite;
            private Color EncircleColor_;
            public Color EncircleColor
            {
                get
                {
                    if(bUseWhite)
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

        #region Classe permettant de définir les différentes flèches présentes dans le schéma.
        public class ArrowParam
        {
            public String Origin;
            public String sOriginDescription;
            public String Goal;
            public String GoalDescription;

            public Double Weight;

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
                Weight = 0;
                Distribution = "Constant";
                Param1 = 0;
                Param2 = 0;
                Param3 = 0;
            }
            public ArrowParam(String Origin_, String sOriginDescription_, String Goal_, String GoalDescription_, Double Weight_, String Distribution_, Double Param1_, Double Param2_, Double Param3_)
            {
                Origin = Origin_;
                sOriginDescription = sOriginDescription_;
                Goal = Goal_;
                GoalDescription = GoalDescription_;
                Weight = Weight_;

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
                Weight = param.Weight;

                Distribution = param.Distribution;
                Param1 = param.Param1;
                Param2 = param.Param2;
                Param3 = param.Param3;
            }
        }
        #endregion

        #region Les différentes variable de la fenêtre
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
        public static int PAS = 10;

        /// <summary>
        /// The class use to represent the airport screen.
        /// </summary>
        Draw_Assistant drAssistant;
        /// <summary>
        /// A boolean which indicate if the form contents had been modified or not.
        /// </summary>
        bool bAEteModifie = false;

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

        Image imgBackground;

        bool bEditable = false;
        #endregion

        #region Les différents constructeurs et fonctions d'initialisation du formulaire.

            #region Fonction d'initialisation du formulaire.
        private void Initialize(ArrayList lesElements_, 
            ArrayList lesDescriptions_, 
            TreeNode AirportStructure, 
            DataTable ItineraryTable_, 
            GestionDonneesHUB2SIM gdh_Datas__,
            EventHandler ehDraw_AssistantClick)
        {
            InitializeComponent();
            changeBackgroundToolStripMenuItem.Visible = PAX2SIM.bJNK;
            imgBackground = null;
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
            ArrowAssistantList.Add(new Arrow_Assistant(gdh_Datas_, "", "", "", "", 0, "Constant", 0, 0, 0));
            ((Arrow_Assistant)ArrowAssistantList[0]).TopLevel = false;
            ((Arrow_Assistant)ArrowAssistantList[0]).Parent = panel1;
            ((Arrow_Assistant)ArrowAssistantList[0]).Location = new Point(0, 0);

            Displayed = 0;
            PAS = 10;
            imgBackground = OverallTools.FonctionUtiles.dessinerFondEcran(panel2.Width, panel2.Height, PAX2SIM.Couleur1, PAX2SIM.Couleur1, 0);
            drAssistant = new Draw_Assistant();
            drAssistant.TopLevel = false;
            drAssistant.Parent = panel2;
            drAssistant.Location = new Point(0, 0);
            drAssistant.Show();
            bShowArrowList = true;
            showArrowListToolStripMenuItem_Click(null, null);
            bool bDescription = lesElements_.Count == lesDescriptions_.Count;
            for(int i=0;i<lesElements_.Count;i++)
                //String GroupName in lesElements_)
            {
                String GroupName = lesElements_[i].ToString();
                String Description = "";
                if(bDescription)
                    Description = " (" + lesDescriptions_[i].ToString() + ")";
                PictureBox picture = CreateNewObject(GroupName, Description);
                if (picture != null)
                {
                    Objects.Add(picture.Name, picture);
                }
            }
            drAssistant.setScenario(gdh_Datas_, ehDraw_AssistantClick);
            this.PerformLayout();
        }
        public void ChangeColor()
        {
            if (drAssistant != null)
            {
                drAssistant.ChangeColor();
            }
        }
        #endregion

            #region Fonction appelée par les boutons du menu lorsque l'on clic dessus.
        void Ressource_Assistant_Click(object sender, EventArgs e)
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
                        ((iType[0] - 1) * GestionDonneesHUB2SIM.NbLevels + (iType[1]-1) ) * 
                        (((PictureBox)Objects[newPicture]).Size.Height + 10 - 
                            ((((PictureBox)Objects[newPicture]).Size.Height%PAS)))
                        );
                }
            }
            while (Intersection(((PictureBox)Objects[newPicture]))!=null)
            {
                if (((PictureBox)Objects[newPicture]).Location.X > hsb_Graph.Maximum)
                {
                    ((PictureBox)Objects[newPicture]).Location = new Point(0, ((PictureBox)Objects[newPicture]).Location.Y + ((PictureBox)Objects[newPicture]).Size.Height + 10-((((PictureBox)Objects[newPicture]).Size.Height%PAS)));
                }
                else
                {
                    ((PictureBox)Objects[newPicture]).Location = new Point(((PictureBox)Objects[newPicture]).Location.X + ((PictureBox)Objects[newPicture]).Size.Width + (10 - (((PictureBox)Objects[newPicture]).Size.Width % PAS)), ((PictureBox)Objects[newPicture]).Location.Y);
                }
            }
            ((ParamsObject)((PictureBox)Objects[newPicture]).Tag).LastLocation = ((PictureBox)Objects[newPicture]).Location;
            entitiesToolStripMenuItem.DropDownItems.Remove((ToolStripItem)sender);
            ((PictureBox)Objects[newPicture]).Parent = drAssistant;
            ((PictureBox)Objects[newPicture]).ContextMenuStrip = drAssistant.getContextMenu();
            ((PictureBox)Objects[newPicture]).BringToFront();
            ((PictureBox)Objects[newPicture]).Visible = true;
            cb_Origin.Items.Add(newPicture);

            if (cb_Origin.Items.Count != 0)
            {
                String sSelected = cb_Origin.Text;
                ArrayList alOrigins = new ArrayList(cb_Origin.Items);
                cb_Origin.Items.Clear();
                alOrigins.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                foreach (String sValue in alOrigins)
                {
                    cb_Origin.Items.Add(sValue);
                }
                alOrigins = null;
                setSelect(sSelected);
            }
        }

        private void addAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bAddAll = true;
            while (entitiesToolStripMenuItem.DropDownItems.Count != 2)
            {
                Ressource_Assistant_Click(entitiesToolStripMenuItem.DropDownItems[2], null);
            }
            bAddAll = false;
        }
            #endregion

            #region Les constructeurs de la classe.
        public Ressource_Assistant(ArrayList lesElements_,
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
            if((ItineraryTable.Columns[2].DataType!=typeof(Double))&&
                (ItineraryTable.Columns[2].DataType != typeof(double)) &&
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
                    Double Weight = (Double)ligne.ItemArray[2];
                    String distribution = ligne.ItemArray[3].ToString();
                    Double param1, param2, param3;
                    if ((!Double.TryParse(ligne.ItemArray[4].ToString(), out param1)) ||
                        (!Double.TryParse(ligne.ItemArray[5].ToString(), out param2)) ||
                        (!Double.TryParse(ligne.ItemArray[6].ToString(), out param3)))
                        continue;

                    Arrows.Add(new ArrowParam(from,"", to,"", Weight, distribution, param1, param2, param3));
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
                                param.Weight = arrow.Weight;
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
                if (Objects.Contains(select))
                ((ParamsObject)((PictureBox)Objects[select]).Tag).EncircleColor = Color.Transparent;
                //((ParamsObject)((PictureBox)Objects[select]).Tag).EncircleColor = Color.FromArgb(255 - tmp.R, 255 - tmp.G, 255 - tmp.B);
                //OverallTools.FonctionUtiles.InvertColors((Bitmap)((PictureBox)Objects[select]).Image);
                //((PictureBox)Objects[select]).BorderStyle = BorderStyle.None;
            }
            select = newSelect;
            if (select != "")
            {
                if(Objects.Contains(select))
                ((ParamsObject)((PictureBox)Objects[select]).Tag).EncircleColor = Color.Transparent;
                //((ParamsObject)((PictureBox)Objects[select]).Tag).EncircleColor = Color.FromArgb(255 - tmp.R, 255 - tmp.G, 255 - tmp.B);
                //OverallTools.FonctionUtiles.InvertColors((Bitmap)((PictureBox)Objects[select]).Image);
                //((PictureBox)Objects[select]).BorderStyle = BorderStyle.FixedSingle;
            }
            cb_Origin.Text = select;
            UpdateList(select);
            drAssistant.Refresh();
            return true;
        }
            #endregion
        #endregion

        #region Les différentes fonctions pour la gestion des déplacements des images
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
            drAssistant.Refresh();
            vsb_Graph.Focus();
            /*if (e.Button == MouseButtons.Right)
            {
                //((PictureBox)sender).ContextMenuStrip.Show(e.Location);
            }*/
        }
        private void PictureMouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left)&&(select != "")&&(bIsOnMove))
            {
                bAEteModifie = true;
                Point ScreenPosition = ((PictureBox)sender).PointToScreen(e.Location);
                Point pt1 = ((ParamsObject)((PictureBox)Objects[select]).Tag).MousePositionInControl;
                Point pt2 = drAssistant.PointToClient(ScreenPosition);
                pt2 = new Point(((pt2.X - pt1.X) / PAS) * PAS, ((pt2.Y - pt1.Y) / PAS) * PAS);
                if (((ParamsObject)((PictureBox)Objects[select]).Tag).LastLocation != pt2)
                {
                    ((PictureBox)Objects[select]).Location = pt2;
                    drAssistant.Refresh();
                }
            }
            else if (e.Button != MouseButtons.Left)
            {
                bIsOnMove = false;
            }
            //On affiche le nom de l'image sélectionnée.
            drAssistant.ShowToolTip(((ParamsObject)((PictureBox)sender).Tag).ToolTip);
        }

        public void PictureMouseLeave(object sender, EventArgs e)
        {
            Point position = panel2.PointToScreen(drAssistant.Location);
            Point SecondPosition = new Point(panel2.Size);
            SecondPosition.X = position.X + hsb_Graph.LargeChange - 1;
            SecondPosition.Y = position.Y + vsb_Graph.LargeChange - 1;
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
                ((ParamsObject)pic1.Tag).LastLocation = pic1.Location;
                drAssistant.Refresh();
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
                    Arrows.Add(new ArrowParam(pic1.Name,poObject.Description, Intersect[i],""));
                }
            }
            this.PerformLayout();
            if (intersect)
            {
                pic1.Location = ((ParamsObject)pic1.Tag).LastLocation;
                UpdateList(null);
                //cb_Origin_SelectedIndexChanged(null, null);
                drAssistant.Refresh();
            }
            else
            {
                ((ParamsObject)pic1.Tag).LastLocation = pic1.Location;
                drAssistant.Refresh();
                return;
            }
        }
        #endregion

        #region Fonction utilisées pour déterminer si deux images se percutent

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
                for (int i = 0; i < listeIntersections.Count;i++ )
                {
                    resultat[i] = listeIntersections[i].ToString();
                }
                return resultat;
            }
            return null;
        }

        public bool Intersection(Point location1, Point size1, Point location2, Point size2)
        {
            //On calcul la position du second coin extrème.
            Point secondPoint1 = new Point(location1.X + size1.X, location1.Y + size1.Y);

            Point secondPoint2 = new Point(location2.X, location2.Y + size2.Y);
            Point ThirdPoint2 = new Point(location2.X + size2.X, location2.Y + size2.Y);
            Point FourthPoint2 = new Point(location2.X + size2.X, location2.Y);

            //Désormais on teste si un des quatres points de la seconde figure est contenue entre les deux
            //points extrèmes.
            return ((Intersection(location2, location1, secondPoint1)) || (Intersection(secondPoint2, location1, secondPoint1))
                || (Intersection(ThirdPoint2, location1, secondPoint1)) || (Intersection(FourthPoint2, location1, secondPoint1)));

        }
        public bool Intersection(Point pointaTeste, Point firstPoint, Point secondPoint)
        {
            if ((pointaTeste.X >= firstPoint.X) && (pointaTeste.X <= secondPoint.X)&&
                (pointaTeste.Y >= firstPoint.Y) && (pointaTeste.Y <= secondPoint.Y))
                return true;
            return false;
        }
        #endregion

        #region Fonction pour mettre à jour l'affichage lors du redimensionnement de la fenêtre
        private void Ressource_Assistant_Resize(object sender, EventArgs e)
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
            if (this.panel2.Height - hsb_Graph.Height <= 0)
                vsb_Graph.LargeChange =1;
            else
                vsb_Graph.LargeChange = this.panel2.Height - hsb_Graph.Height;

            if (this.panel2.Width - vsb_Graph.Width <= 0)
                hsb_Graph.LargeChange = 1;
            else
                hsb_Graph.LargeChange = this.panel2.Width - vsb_Graph.Width;
            menuStrip1.Width = this.ClientRectangle.Width;
        }
        #endregion

        #region Fonctions pour la mise à jour de l'affichage de la liste

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
            if (((sSelected == null) && (cb_Origin.Text != "")) || (sSelected!=""))
            {
                foreach (ArrowParam param in Arrows)
                {
                    if (param.Origin == cb_Origin.Text)
                    {
                        if (ArrowAssistantList.Count <= i)
                        {
                            ArrowAssistantList.Add(new Arrow_Assistant(gdh_Datas_,param.Origin,param.sOriginDescription, param.Goal,param.GoalDescription,  param.Weight,param.Distribution,param.Param1,param.Param2,param.Param3));
                            ((Arrow_Assistant)ArrowAssistantList[i]).TopLevel = false;
                            ((Arrow_Assistant)ArrowAssistantList[i]).Parent = panel1;
                            ((Arrow_Assistant)ArrowAssistantList[i]).Location = new Point(0, i * ElementSize.Height);
                            ((Arrow_Assistant)ArrowAssistantList[i]).Show();
                        }
                        else
                        {
                            ((Arrow_Assistant)ArrowAssistantList[i]).setDatas(param.Origin, param.sOriginDescription, param.Goal,param.GoalDescription, param.Weight, param.Distribution,param.Param1,param.Param2,param.Param3);
                        }
                        ((Arrow_Assistant)ArrowAssistantList[i]).Visible = true;
                        ((Arrow_Assistant)ArrowAssistantList[i]).bEnabled = true;

                        i++;
                    }
                }
            }
            //On masque tous les autres parties de formulaire destinées à afficher les définitions des flèches
            Displayed = i;
            for (; i < ArrowAssistantList.Count; i++)
            {
                ((Arrow_Assistant)ArrowAssistantList[i]).Visible = false;
                ((Arrow_Assistant)ArrowAssistantList[i]).bEnabled = false;
            }
            MAJ_ScrollBar();
        }
        #endregion

        #region Les différentes fonctions utilisées pour les barres de scroll
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
            for (int i=0; i < ArrowAssistantList.Count; i++)
            {
                ((Arrow_Assistant)ArrowAssistantList[i]).Location = new Point(0, i * tailleObjet - vsb_Liste.Value);
            }
        }

        private void vsb_Graph_Scroll(object sender, ScrollEventArgs e)
        {
            foreach (String key in Objects.Keys)
            {
                PictureBox pic = (PictureBox)Objects[key];
                if (pic.Parent != null)
                {
                    pic.Location = new Point(pic.Location.X, pic.Location.Y - e.NewValue + e.OldValue);
                    ((ParamsObject)pic.Tag).LastLocation = new Point(((ParamsObject)pic.Tag).LastLocation.X, ((ParamsObject)pic.Tag).LastLocation.Y - e.NewValue + e.OldValue);
                }
            }
            if (drAssistant != null)
                drAssistant.LocationBackGround = new Point(drAssistant.LocationBackGround.X, -vsb_Graph.Value);
            drAssistant.Refresh();
        }

        private void hsb_Graph_Scroll(object sender, ScrollEventArgs e)
        {
            foreach (String key in Objects.Keys)
            {
                PictureBox pic = (PictureBox)Objects[key];
                if (pic.Parent != null)
                {
                    pic.Location = new Point(pic.Location.X - e.NewValue + e.OldValue, pic.Location.Y);
                    ((ParamsObject)pic.Tag).LastLocation = new Point(((ParamsObject)pic.Tag).LastLocation.X - e.NewValue + e.OldValue, ((ParamsObject)pic.Tag).LastLocation.Y);
                }
            }
            if (drAssistant != null)
                drAssistant.LocationBackGround = new Point(-hsb_Graph.Value, drAssistant.LocationBackGround.Y);
            drAssistant.Refresh();
        }

        #endregion

        #region Les différentes fonctions appelées par l'assistant pour les tracés.
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

        #region Fonctions pour déterminer quels sont les successeurs de l'actuel point.
        
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

        #region Fonctions pour la gestion de la sortie de cette fenêtre.
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
            if ( bAEteModifie)
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

        #region Fonctions pour mettre à jour les tables liées à cet assistant
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
            if (ItineraryTable.Columns.Count != GestionDonneesHUB2SIM.ListeEntete_ItineraryTable.Length)
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
                        newLine[2] = param.Weight;
                        newLine[3] = param.Distribution;
                        newLine[4] = param.Param1;
                        newLine[5] = param.Param2;
                        newLine[6] = param.Param3;
                        ItineraryTable.Rows.Add(newLine);
                    }
                }
            }
        }

        private ArrayList UpdateLocationsInformations(TreeNode AirportRacine)
        {
            int vOldValue, hOldValue;
            vOldValue = vsb_Graph.Value;
            hOldValue = hsb_Graph.Value;
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
                tag.Location = new Point(((PictureBox)Objects[key]).Location.X + hOldValue, ((PictureBox)Objects[key]).Location.Y + vOldValue);
                tag.Visible = (((PictureBox)Objects[key]).Parent != null);
            }
            return absents;
        }
        #endregion

        #region Fonction pour afficher et masquer la liste des flèches.
        private void showArrowListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bShowArrowList = showArrowListToolStripMenuItem.Checked;
            this.panel1.Visible = bShowArrowList;
            this.cb_Origin.Visible = bShowArrowList;
            Ressource_Assistant_Resize(null, null);
        }
        #endregion

        #region Fonctions qui servent à mettre à jour l'affichage.
        /// <summary>
        /// Fonction qui s'occupe de remettre à jour l'affichage dans l'itineraryView 
        /// lors de la modification de l'aéroport, ou des informations sur les vitesses.
        /// </summary>
        /// <param name="lesElements"></param>
        /// <param name="ItineraryTable_"></param>
        /// <param name="AirportStructure"></param>
        internal void UpdateInformations(ArrayList lesElements, ArrayList lesDescriptions, DataTable ItineraryTable_, TreeNode AirportStructure, EventHandler Draw_Assistant_Click)
        {
            String sSelected_ = select;
            setSelect("");

            UpdateList(null);
            ScrollEventArgs arg = new ScrollEventArgs(ScrollEventType.EndScroll, vsb_Graph.Value, 0);
            vsb_Graph.Value = 0;
            vsb_Graph_Scroll(vsb_Graph, arg);
            arg = new ScrollEventArgs(ScrollEventType.EndScroll, hsb_Graph.Value,0);
            hsb_Graph.Value = 0;
            hsb_Graph_Scroll(hsb_Graph, arg);
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
            ArrayList alOrigins = new ArrayList();
            for(i= 0; i<  entete.Count; i++){
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
                        ((ParamsObject)picture.Tag).Description = " ("+sDescription+")";
                        TreeNode child = OverallTools.TreeViewFunctions.getAirportChild(structureAirport, picture.Name);
                        if (child != null)
                        {
                            if (((TreeViewTag)child.Tag).Visible)
                            {
                                picture.Location = ((TreeViewTag)child.Tag).Location;
                                picture.Parent = drAssistant;
                                ((ParamsObject)picture.Tag).LastLocation = picture.Location;
                                picture.BringToFront();
                                picture.Visible = true;
                                picture.ContextMenuStrip = drAssistant.getContextMenu();
                                alOrigins.Add(picture.Name);
                            }
                            else
                            {
                                //Si le mode d'affichage de l'objet a été modifié, c'est que l'objet est déjà placé sur
                                //l'espace de travail. sinon on doit le rendre accessible dans le menu déroulant.
                                OverallTools.FonctionUtiles.AddSortedItem(entitiesToolStripMenuItem, picture.Name, 2).Click += new EventHandler(Ressource_Assistant_Click);
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
            if (alOrigins.Count != 0)
            {
                alOrigins.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                foreach (String sValue in alOrigins)
                {
                    cb_Origin.Items.Add(sValue);
                }
                alOrigins = null;
            }

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
            drAssistant.setScenario(gdh_Datas_, Draw_Assistant_Click);
            setSelect(sSelected_);
            this.Refresh();
        }
        private PictureBox CreateNewObject(String GroupName, String sDescription)
        {
            Int32[] types = OverallTools.DataFunctions.AnalyzeGroupName(GroupName);
            if (types == null)
                return null;
            if (types[2] > 8)
                return null;
            PictureBox Picture = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(Picture)).BeginInit();
            //ici on choisit l'image de ce nouveau control.
            /*2011.02.25 ==> Changement d'image pour utilisé désormais les images utilisées sous S8*/
            
            //Picture.Image = imageList1.Images[types[2]];
            Picture.Image = OverallTools.FonctionUtiles.CreateSimul8Image(imageList1.Images[types[2]], types[0], types[1], types[3]);
            Picture.BackColor = Color.Transparent;
            
            bool bEstPlace = false;
            if (structureAirport != null)
            {
                TreeNode child = OverallTools.TreeViewFunctions.getAirportChild(structureAirport, GroupName);
                if (child != null)
                {
                    if (((TreeViewTag)child.Tag).Visible)
                    {
                        Picture.Location = ((TreeViewTag)child.Tag).Location;
                        Picture.Parent = drAssistant;
                        Picture.ContextMenuStrip = drAssistant.getContextMenu();
                        Picture.BringToFront();
                        bEstPlace = true;
                        cb_Origin.Items.Add(GroupName);
                        if (cb_Origin.Items.Count != 0)
                        {
                            String sSelected = cb_Origin.Text;
                            ArrayList alOrigins = new ArrayList(cb_Origin.Items);
                            cb_Origin.Items.Clear();
                            alOrigins.Sort(new OverallTools.FonctionUtiles.ColumnsComparer());
                            foreach (String sValue in alOrigins)
                            {
                                cb_Origin.Items.Add(sValue);
                            }
                            alOrigins = null;
                            setSelect(sSelected);
                        }
                    }
                }
            }
            if (!bEstPlace)
            {
                //Si le mode d'affichage de l'objet a été modifié, c'est que l'objet est déjà placé sur
                //l'espace de travail. sinon on doit le rendre accessible dans le menu déroulant.
                OverallTools.FonctionUtiles.AddSortedItem(entitiesToolStripMenuItem, GroupName, 2).Click += new EventHandler(Ressource_Assistant_Click);
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

            /*2011.02.25 ==> Changement d'image pour utilisé désormais les images utilisées sous S8 => La couleur d'encerclement est désormais noire.*/
            //Double Position = (((Double)types[1]) / GestionDonneesHUB2SIM.NbLevels) * Math.PI;
            //Color encircleColor = Color.FromArgb((int)Math.Abs(Math.Cos(Position + 2.0 * Math.PI / 3.0) * 255), (int)Math.Abs(Math.Cos(Position +  Math.PI / 3.0) * 255), (int)Math.Abs(Math.Cos(Position) * 255));
            Color encircleColor = Color.Black;
            Picture.Tag = new ParamsObject(new Point(16, 16), Picture.Location, Picture.Name,sDescription, new Point(Picture.Size), encircleColor);
            return Picture;
        }

        void Picture_DoubleClick(object sender, EventArgs e)
        {
            if(sender.GetType() != typeof(PictureBox))
                return;
            DataTable dtProcessTable = gdh_Datas_.getTable("Input", GlobalNames.Times_ProcessTableName);
            String sCurrentGroupName = ((PictureBox)sender).Name;
            if (OverallTools.DataFunctions.indexLigne(dtProcessTable, 0, sCurrentGroupName) != -1)
            {
                DataTable dtOneOfTable = gdh_Datas_.getTable("Input", GlobalNames.OneofSpecificationTableName);
                Assistant.SubForms.Process_SubForm pa = new SIMCORE_TOOL.Assistant.SubForms.Process_SubForm(dtProcessTable, dtOneOfTable, sCurrentGroupName);

                DataTable dtGroupQueues = gdh_Datas_.getTable("Input", GlobalNames.Group_QueuesName);
                DataTable dtStationsQueues = gdh_Datas_.getTable("Input", GlobalNames.Capa_QueuesTableName);
                SubForms.Capacity_SubForm.Node nContent = SubForms.Capacity_SubForm.Node.GetTree(gdh_Datas_.getRacine(), dtGroupQueues, sCurrentGroupName);
                List<Form> lfForm = new List<Form>();
                lfForm.Add(pa);
                SubForms.Capacity_SubForm csf_Capacity = null;
                if (nContent != null)
                {
                    csf_Capacity = new SIMCORE_TOOL.Assistant.SubForms.Capacity_SubForm(nContent, dtGroupQueues, dtStationsQueues, true);
                    lfForm.Add(csf_Capacity);
                }

                Edit_Assistant ea = new Edit_Assistant("Edit " + sCurrentGroupName, lfForm);
                if (ea.ShowDialog() == DialogResult.OK)
                {
                    if (csf_Capacity != null)
                        csf_Capacity.Save();
                    if (pa != null)
                        pa.saveChanges();
                }
                //pa.ShowDialog();

                if (csf_Capacity != null)
                    csf_Capacity.Dispose();
                pa.Dispose();
            }
        }

        void Picture_Paint(object sender, PaintEventArgs e)
        { 
            /*2011.02.25 ==> Changement d'image pour utilisé désormais les images utilisées sous S8 => La couleur d'encerclement est désormais noire.*/
            //Pen p = new Pen(((ParamsObject)((PictureBox)sender).Tag).EncircleColor, 3);
            Pen p = new Pen(((ParamsObject)((PictureBox)sender).Tag).EncircleColor, 1);
            Graphics g = e.Graphics;
            g.DrawLine(p, new Point(0, 0), new Point(0, ((PictureBox)sender).Width-1));
            g.DrawLine(p, new Point(0, 0), new Point(((PictureBox)sender).Height - 1, 0));
            g.DrawLine(p, new Point(0, ((PictureBox)sender).Width - 1), new Point(((PictureBox)sender).Height - 1, ((PictureBox)sender).Width - 1));
            g.DrawLine(p, new Point(((PictureBox)sender).Height - 1, ((PictureBox)sender).Width - 1), new Point(((PictureBox)sender).Height - 1, 0));
        }
        #endregion

        #region Fonction pour supprimer le groupe sélectioné
        public void DeleleSelectedNode()
        {
            
            String TheSelected = select;
            if (select == "")
                return;
            bAEteModifie = true;
            setSelect("");
            OverallTools.FonctionUtiles.AddSortedItem(entitiesToolStripMenuItem, TheSelected, 2).Click += new EventHandler(Ressource_Assistant_Click);
            /*entitiesToolStripMenuItem.DropDownItems.Add(TheSelected);
            entitiesToolStripMenuItem.DropDownItems[entitiesToolStripMenuItem.DropDownItems.Count - 1].Click += new EventHandler(Ressource_Assistant_Click);*/
            PictureBox Picture = (PictureBox)Objects[TheSelected];
            Picture.Location = new System.Drawing.Point(-1, -1);
            Picture.Visible = false;
            Picture.Parent = null;
            DeleteArrowFromSelectedNode(TheSelected);
            drAssistant.HideToolTip();
            cb_Origin.Items.Remove(TheSelected);
            drAssistant.Refresh();
            this.Refresh();
        }
        private void DeleteArrowFromSelectedNode(String sGroup)
        {
            for(int i=0;i<Arrows.Count;i++)
            {
                if ((((ArrowParam)Arrows[i]).Goal == sGroup) || (((ArrowParam)Arrows[i]).Origin == sGroup))
                {
                    Arrows.RemoveAt(i);
                    i--;
                }
            }
        }
        #endregion

        private void changeBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofd_FileDialog.ShowDialog() != DialogResult.OK)
                return;
            String sImagePath = ofd_FileDialog.FileName;
            imgBackground = Image.FromFile(ofd_FileDialog.FileName);
            if (drAssistant != null)
                drAssistant.Background = imgBackground;
        }

        private void modifyItineraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bEditable = true;
            modifyItineraryToolStripMenuItem.Visible = false;
        }
    }
}