using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PDFjet.NET;
using System.Windows.Forms;
using System.Collections;
using SIMCORE_TOOL.ParseHTML;

namespace SIMCORE_TOOL
{
    class PDFGenerator
    {
        #region Attributes

        private BufferedStream bStream;
        private PDF pdf;
        private Double[] pageSize = A4.PORTRAIT; // horizontal size ; vertical size
        private Double[] margin = new Double[4] { 40.0, 40.0, 40.0, 30.0 }; // top ; bottom ; left ; right
        private int pageNumber = 0;

        private Double[] writtingPosition = new Double[2];

        #region Attributes for Chapters list
        private Hashtable chapters = new Hashtable();
        private int chapterLevel = 0;
        //private float SummaryTabulation = 15;
        private double dotSpace = 0;
        private double pageNumberSpace = 30;
        private List<int> chapterNumerotation;
        #endregion

        #region Attributes for Images list
        private Hashtable ImagesTable = new Hashtable();
        public enum HorizontalPosition { LEFT, CENTER, RIGHT, CURSOR };
        public enum VerticalPosition { TOP, CENTER, BOTTOM, CURSOR };
        public enum ScaleMethod { HEIGHT, WIDTH };
        #endregion

        #region Attributes for page number
        private Double[] pageNumberPossitionOffSet = new Double[2] { 20.0, 20.0 };
        private int PageNumberPossition = 1;
        public enum numberPosition { RIGHTTOP, RIGTHBOTTOM, LEFTTOP, LEFTBOTTOM };
        #endregion

        #region Attributes for header and footer
        private bool hasHeader = true;
        private bool hasFooter = true;
        private String[] headerText = new String[3] { "", "", "" };
        private String[] footerText = new String[3] { "", "", "" };
        private double lineSpaceRatio = 0.2;
        private String headerFont = "Helvetica";
        private double headerFontSize = 9.0;
        #endregion

        private Page currentPage;
        private Image[] puces = new Image[4];

        #region Attributes for Fonts
        private Dictionary<String, Font> dfonts;
        private String sCurrentFont;
        private String defaultFont = "Helvetica";
        //private FontList currentFont;
        //public enum FontList { NORMAL, TITLE, SUPERSIZE };
        //private Font superSizeFont;
        //private Font titleFont;
        //private Font textFont;
        #endregion

        #region Attributes for list
        private String listFont = "Helvatica";
        private double listFontSize = 9.0;
        private int listLevel = 0;
        private float listTabulation = 30;
        #endregion

        #region Attributes for content table
        private String contentTableFont = "Helvatica";
        private double contentTableFontSize = 9.0;
        #endregion

        #region Attributes for html
        ParseHTML.ParseHTML parse = null;
        Dictionary<String, String> dCharDef = new Dictionary<String, String>();
        //List<String> htmlFonts; // pile des font a utiliser pour le html
        //Dictionary<String, Font> fonts; // liste detoutes les font charger au debut
        const string defaultHtmlFont = "Times-Roman";
        const int defaultHtmlSize = 2;
        int[] blueLink = new int[3] { 0, 0, 255 };
        #endregion

        #endregion Attributes

        public PDFGenerator(String filePath)
        {
            FileStream fos = new FileStream(filePath, FileMode.Create);
            bStream = new BufferedStream(fos);
            pdf = new PDF(bStream);
            //pdf.setCompressor(Compressor.ORIGINAL_ZLIB);

            dfonts = new Dictionary<String, Font>();
            foreach (String fontName in new String[] {
                "Courier",
                "Courier-Bold",
                "Courier-BoldOblique",
                "Courier-Oblique",
                "Helvetica",
                "Helvetica-Bold",
                "Helvetica-BoldOblique",
                "Helvetica-Oblique",
                "Times-Bold",
                "Times-BoldItalic",
                "Times-Italic",
                "Times-Roman"})
            {
                Font f = new Font(pdf, fontName);
                f.SetSize(15);
                dfonts.Add(fontName, f);
            }
            sCurrentFont = "Helvetica";

            // set the list of special html char
            dCharDef.Add("amp","&");
            dCharDef.Add("lt","<");
            dCharDef.Add("gt",">");
            dCharDef.Add("nbsp", " ");
        }

        /// <summary>
        /// Close the stream.
        /// </summary>
        public void Close()
        {
            pdf.Flush();
            bStream.Close();
        }

        #region Page de garde
        /// <summary>
        /// Fonction pour créer la page de garde.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="version"></param>
        /// <param name="date"></param>
        /// <param name="templateRef"></param>
        /// <param name="logoP2SRef"></param>
        /// <param name="userLogoRef"></param>
        public void MakeFirstPage(String title, String author, String version, String date, String templateRef, String logoP2SRef, String userLogoRef)
        {
            double pos2X = 0.0;
            double pos2Y = 0.0;

            if (currentPage == null)
            {
                pageNumber++;
                currentPage = new Page(pdf, pageSize);
            }
            else // cas étrange
                AddPage();

            // Image 1, Template en haut
            if (templateRef != null && !templateRef.Equals("") && ImagesTable[templateRef] != null)
            {
                Image image1 = (Image)ImagesTable[templateRef];
                double img1Width = image1.GetWidth();
                double img1ScaleBy = pageSize[0] / img1Width;
                image1.SetPosition(0.0, 0.0);
                image1.ScaleBy(img1ScaleBy);
                image1.DrawOn(currentPage);
            }

            // Image 2, logo P2S en bas à gauche
            if (logoP2SRef != null && !logoP2SRef.Equals("") && ImagesTable[logoP2SRef] != null)
            {
                Image image2 = (Image)ImagesTable[logoP2SRef];
                double img2Width = image2.GetWidth();
                double img2Height = image2.GetHeight();
                pos2X = pageSize[0] / 10;
                double img2ScaleBy = (pageSize[0] / 3.5 - pos2X) / img2Width;
                pos2Y = pageSize[1] - (pageSize[1] / 16) - (img2Height * img2ScaleBy);
                image2.SetPosition(pos2X, pos2Y);
                image2.ScaleBy(img2ScaleBy);
                image2.DrawOn(currentPage);
            }

            // Image 3, logo perso en bas à droite
            if (userLogoRef != null && !userLogoRef.Equals("") && ImagesTable[userLogoRef] != null)
            {
                Image image3 = (Image)ImagesTable[userLogoRef];
                double img3Width = image3.GetWidth();
                double img3Height = image3.GetHeight();
                double pos3X = pageSize[0] * 2 / 3;
                double img3ScaleBy = (pageSize[0] / 3.2) / img3Width;
                double pos3Y = pageSize[1] - (pageSize[1] / 16) - (img3Height * img3ScaleBy);
                image3.SetPosition(pos3X, pos3Y);
                image3.ScaleBy(img3ScaleBy);
                image3.DrawOn(currentPage);
            }

            // title
            Font = "Helvetica"; FontSize = 25.0;
            String[] textLines = GetSplitedText(title, GetCurrentFont(), PageFormat[0] * 0.90); // text limté a 90% de la largueur de la page
            double posY = pageSize[1] / 2;
            foreach (String text in textLines)
            {
                TextLine titleTXT = new TextLine(GetCurrentFont(), text);
                titleTXT.SetPosition((pageSize[0] - GetCurrentFont().StringWidth(text)) / 2, posY);
                titleTXT.DrawOn(currentPage);
                posY += GetCurrentFont().getSize() * (1 + lineSpaceRatio);
            }

            ///// Information part
            FontSize = 9.0;
            double nextLine = GetCurrentFont().getSize() * (1 + lineSpaceRatio);
            pos2Y -= 2;
            // soft version
            String versionString = OverallTools.AssemblyActions.AssemblyTitle + " ";
            versionString += OverallTools.AssemblyActions.AssemblyVersion;
            TextLine softVersionTXT = new TextLine(GetCurrentFont(), "Soft: " + versionString);
            softVersionTXT.SetPosition(pos2X, pos2Y);
            softVersionTXT.DrawOn(currentPage);
            // doc version
            if (!(version == null || version.Equals("")))
            {
                TextLine docVersionTXT = new TextLine(GetCurrentFont(), "Version: " + version);
                pos2Y -= nextLine;
                docVersionTXT.SetPosition(pos2X, pos2Y);
                docVersionTXT.DrawOn(currentPage);
            }
            // doc date
            if (!(date == null || date.Equals("")))
            {
                TextLine dateTXT = new TextLine(GetCurrentFont(), "Created: " + date);
                pos2Y -= nextLine;
                dateTXT.SetPosition(pos2X, pos2Y);
                dateTXT.DrawOn(currentPage);
            }
            // author name
            if (!(author == null || author.Equals("")))
            {
                TextLine autorTXT = new TextLine(GetCurrentFont(), "Author: " + author);
                pos2Y -= nextLine;
                autorTXT.SetPosition(pos2X, pos2Y);
                autorTXT.DrawOn(currentPage);
            }

            AddPage();

            // test
            /*TextLine test = new TextLine(dfonts["Helvetica"], "Helvetica abcdefgh ABCDFGH 12345");
            test.SetPosition(10, 100);
            test.DrawOn(currentPage);
            test = new TextLine(dfonts["Times-Roman"], "Times-Roman abcdefgh ABCDFGH 12345");
            test.SetPosition(10, 120);
            test.DrawOn(currentPage);
            test = new TextLine(dfonts["Courier"], "Courier abcdefgh ABCDFGH 12345");
            test.SetPosition(10, 140);
            test.DrawOn(currentPage);
            */
        }
        #endregion Page de garde

        #region Image
        /// <summary>
        /// Load image.
        /// (Image should be loaded before the creation of the fisrt page).
        /// </summary>
        /// <param name="imgRef">Image reference (used to insert the image)</param>
        /// <param name="path">Image location</param>
        public void LaodImage(String imgRef, String path)
        {
            if (path == null || path.Equals("") || imgRef == null || imgRef.Equals(""))
                return;
            // >> Report generate/preview error - Illegal char in path
            foreach (string illegalChar in GlobalNames.ILLEGAL_CHARACTERS_IN_FILE_PATH)
            {
                if (path.Contains(illegalChar))
                    path = path.Replace(illegalChar, GlobalNames.SPECIAL_CHAR_UNDERSCORE);
            }
            // Report generate/preview error - Illegal char in path
            if (!File.Exists(path))
                return;
            if (ImagesTable.ContainsKey(imgRef))
                return;
                        
            // finding the image type
            String[] str = path.Split('.');
            String ext = str[str.Length - 1];
            int imageType = -1;
            if (ext.Equals("jpg") || ext.Equals("JPG") || ext.Equals("jpeg") || ext.Equals("JPEG"))
                imageType = ImageType.JPEG;
            else if (ext.Equals("png") || ext.Equals("PNG"))
                imageType = ImageType.PNG;
            else if (ext.Equals("bmp") || ext.Equals("BMP"))
                imageType = ImageType.BMP;
            else
                return;

            FileStream img = new FileStream(path, FileMode.Open);
            LaodImage(imgRef, img, imageType);
        }

        /// <summary>
        /// Load image.
        /// (Image should be loaded before the creation of the fisrt page).
        /// </summary>
        /// <param name="imgRef">Image reference (used to insert the image)</param>
        /// <param name="path">Image location</param>
        /// <param name="imageType">Type de l'image</param>
        public void LaodImage(String imgRef, Stream img, int imageType)
        {
            if (ImagesTable.ContainsKey(imgRef))
                return;
            Image image = new Image(pdf, img, imageType);
            ImagesTable.Add(imgRef, image);
        }
        /// <summary>
        /// Get size of image referenced by "imageRef".
        /// </summary>
        /// <param name="imageRef">Image reference</param>
        /// <returns>Image size</returns>
        public double[] GetImageSize(String imageRef)
        {
            double[] size = new double[2] { 0, 0 };
            if (!ImagesTable.ContainsKey(imageRef))
                return size;
            Image img = ImagesTable[imageRef] as Image;
            size[0] = img.GetWidth();
            size[1] = img.GetHeight();
            return size;
        }
        /// <summary>
        /// Draw image at real size at the writting position.
        /// </summary>
        /// <param name="imgRef"></param>
        public void DrawImage(String imgRef)
        {
            DrawImage(imgRef, 1, writtingPosition);
        }
        
        /// <summary>
        /// Draw image
        /// </summary>
        /// <param name="imgRef"></param>
        /// <param name="scale"></param>
        /// <param name="horizontalPosition"></param>
        public void DrawImage(String imgRef, ScaleMethod scaleMeth, Double scaleRatio, HorizontalPosition posH, VerticalPosition posV)
        {
            if (!ImagesTable.ContainsKey(imgRef)) return;

            double scale = 1;
            Image img = ImagesTable[imgRef] as Image;
            Double[] position = new Double[2] { writtingPosition[0], writtingPosition[1] + 5 };

            // scale
            switch (scaleMeth)
            {
                case ScaleMethod.HEIGHT :
                    scale = (pageSize[1] * scaleRatio) / img.GetHeight();
                    break;
                case ScaleMethod.WIDTH :
                    scale = (pageSize[0] * scaleRatio) / img.GetWidth();
                    break;
            }
            // Horizontal Position
            switch (posH)
            {
                case HorizontalPosition.LEFT:
                    position[0] = 0.0;
                    break;
                case HorizontalPosition.CENTER:
                    position[0] = (pageSize[0] - img.GetWidth() * scale) / 2;
                    break;
                case HorizontalPosition.RIGHT:
                    position[0] = pageSize[0] - img.GetWidth() * scale;
                    break;
                case HorizontalPosition.CURSOR:
                    position[0] = writtingPosition[0];
                    break;
            }
            // Vertical Position
            switch (posV)
            {
                case VerticalPosition.TOP :
                    position[1] = 0.0;
                    break;
                case VerticalPosition.CENTER :
                    position[1] = (pageSize[1] - img.GetHeight() * scale) / 2;
                    break;
                case VerticalPosition.BOTTOM :
                    position[1] = pageSize[1] - img.GetHeight() * scale;
                    break;
                case VerticalPosition.CURSOR :
                    position[1] = writtingPosition[1];
                    break;
            }
            DrawImage(imgRef, scale, position);
        }
        /// <summary>
        /// Draw the image referenced by "imgRef". 
        /// </summary>
        /// <param name="imgRef"></param>
        /// <param name="scale"></param>
        /// <param name="horizontalPosition"></param>
        public void DrawImage(String imgRef, Double scale, HorizontalPosition posi)
        {
            if (!ImagesTable.ContainsKey(imgRef)) return;

            Image img = ImagesTable[imgRef] as Image;
            Double[] position = new Double[2] { writtingPosition[0], writtingPosition[1] + 5 };
            switch (posi)
            {
                case HorizontalPosition.LEFT:
                    position[0] = margin[2];
                    break;
                case HorizontalPosition.CENTER:
                    position[0] = (pageSize[0] - img.GetWidth() * scale) / 2;
                    break;
                case HorizontalPosition.RIGHT:
                    position[0] = pageSize[0] - img.GetWidth() * scale;
                    break;
            }
            DrawImage(imgRef, scale, position);
        }
        /// <summary>
        /// Draw image scaled by "scale", with the top left corner at position "position".
        /// </summary>
        /// <param name="imgRef"></param>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        public void DrawImage(String imgRef, Double scale, Double[] position)
        {
            if (!ImagesTable.ContainsKey(imgRef)) return;

            Image image = ImagesTable[imgRef] as Image;
            image.SetPosition(position[0], position[1]);
            image.ScaleBy(scale);
            image.DrawOn(currentPage);
            //if (writtingPosition[1] + image.GetHeight() * scale > pageSize[1] - margin[1])
            //    AddPage();
            if (!(writtingPosition[1] + image.GetHeight() * scale > pageSize[1] - margin[1]))
            //else
            {
                writtingPosition[0] = margin[2];
                writtingPosition[1] = writtingPosition[1] + image.GetHeight(); 
            }
        }
        #endregion Image

        #region sommaire et titres
        /// <summary>
        /// Création d'un Sommaire 
        /// </summary>
        public void StartContentTable(String fontName, double fontSize)
        {
            if (dfonts.ContainsKey(fontName))
                contentTableFont = fontName;
            contentTableFontSize = fontSize;
            if (!HasPage() | IsEndPage(fontSize)) AddPage();
            chapterLevel = 0;
            CRLF();
        }
        /// <summary>
        /// Used before writting all the table of content elements.
        /// </summary>
        /// <param name="fontName">Font use in the table of content</param>
        /// <param name="fontSize">Font size in the table of content</param>
        /// <param name="tableName">Title wrote before the table of content</param>
        /// <param name="NameFontName">Font use for the title</param>
        /// <param name="NameFontSize">Font size for the title</param>
        public void StartContentTable(String fontName, double fontSize, String tableName, String NameFontName, double NameFontSize)
        {
            chapterNumerotation = new List<int>();
            chapterNumerotation.Add(0);
            CRLF();
            CRLF();
            Font aFont = GetCurrentFont();
            if (dfonts.ContainsKey(NameFontName))
                aFont = dfonts[NameFontName];
            double prevSize = aFont.getSize();
            aFont.SetSize(NameFontSize);
            TextLine title = new TextLine(aFont, tableName);
            title.SetPosition(writtingPosition[0], writtingPosition[1]);
            title.DrawOn(currentPage);
            aFont.SetSize(prevSize);
            CRLF(aFont.getSize(), 1, false);
            StartContentTable(fontName, fontSize);
        }
        /// <summary>
        /// Ajout d'un élément dans le sommaire. 
        /// Cette methode nécessite de tenir à jour le niveau d'imbrication des chapitres
        /// à l'aide des methodes ContentTableLevelUp() et ContentTableLevelDown().
        /// </summary>
        /// <param name="key">Clé d'association de l'élément.</param>
        /// <param name="title">Text de l'élément.</param>
        public void AddContentElement(String key, String title)
        { 
            if (!HasPage() | IsEndPage(contentTableFontSize)) AddPage();

            Font aFont = dfonts[contentTableFont];
            double prevSize = aFont.getSize();
            aFont.SetSize(contentTableFontSize);

            // define dot space
            if (dotSpace == 0)
                dotSpace = aFont.StringWidth(" .");
            String dotLine = "";

            // draw chapter name in contents table
            CRLF();
            IncrChapterNumber();
            //writtingPosition[0] += chapterLevel * SummaryTabulation;
            TextLine titre = new TextLine(aFont, GetChapterNumber(false) + "  " + title);
            titre.SetPosition(writtingPosition[0] + 5.0, writtingPosition[1]);
            titre.DrawOn(currentPage);

            // draw dot line
            int nbDot = (int)((pageSize[0] - GetCurrentFont().StringWidth(titre.GetText()) - margin[3] - pageNumberSpace - writtingPosition[0] - 5.0) / dotSpace);
            if (nbDot < 0) // if there aren't enougth space for the page number, write it on next line without dot line
                CRLF();
            else
                for (int i = 0; i < nbDot; i++) dotLine += " .";
            TextLine line = new TextLine(aFont, dotLine);
            line.SetPosition(pageSize[0] - margin[3] - 30.0 - nbDot * dotSpace, writtingPosition[1]);
            line.DrawOn(currentPage);

            // save number field position
            TextLine number = new TextLine(aFont);
            double[] posi = new double[] { pageSize[0] - margin[3] - pageNumberSpace + 5, writtingPosition[1] }; 
            number.SetPosition(pageSize[0] - margin[3] - pageNumberSpace + 5, writtingPosition[1]);
            titre.SetText(GetChapterNumber(true) + "  " + title);
            SummaryElement chap = new SummaryElement(titre, posi, currentPage, aFont.getSize());
            chapters.Add(key, chap);

            aFont.SetSize(prevSize);
        }
        
        /// <summary>
        /// Ajout le numero de page de l'element 'key' dans le sommaire.
        /// </summary>
        /// <param name="key">Clé d'association de l'élément.</param>
        public void SetContentElementPage(String key)
        {
            if (!chapters.ContainsKey(key)) return;
            if (!HasPage() | IsEndPage(contentTableFontSize)) AddPage();

            SummaryElement chap = chapters[key] as SummaryElement;
            chap.DrawPageNumber(pageNumber, this);
        }

        /// <summary>
        /// Ajoute le numero de page de l'element 'key' dans le sommaire.
        /// Puis ajoute le titre sur la page actuelle.
        /// </summary>
        /// <param name="key">Clé d'association de l'élément.</param>
        public void PutContentElement(String key, String fontName, double fontSize)
        {
            if (!chapters.ContainsKey(key)) return;
            if (!HasPage() | IsEndPage(FontSize)) AddPage();

            Font aFont = GetCurrentFont();
            Font prevFont = aFont;
            if (dfonts.ContainsKey(fontName))
                aFont = dfonts[fontName];
            double prevSize = aFont.getSize();

            // put the page number in the table of contents
            SummaryElement chap = chapters[key] as SummaryElement;
            chap.DrawPageNumber(pageNumber, this);

            // draw chapter title
            if (!HasPage() | IsEndPage(fontSize)) AddPage();
            CRLF(fontSize, 1, false);
            TextLine txt = chap.Title;
            txt.SetFont(aFont);
            aFont.SetSize(fontSize);
            txt.SetPosition(writtingPosition[0], writtingPosition[1]);
            txt.DrawOn(currentPage);

            aFont.SetSize(prevSize);
        }

        /// <summary>
        /// Incremente le niveau d'imbrication dans le sommaire. Affectera la prochaine entrée
        /// dans le sommaire.
        /// </summary>
        /// <returns>Niveau mis à jour</returns>
        public int ContentTableLevelUp()
        {
            return ContenTableLevelUp(1);
        }
        /// <summary>
        /// Incremente de "shiftNumber" le niveau d'imbrication dans le sommaire.
        /// Affectera la prochaine entrée dans le sommaire.
        /// </summary>
        /// <param name="shiftNumber"></param>
        /// <returns>Niveau mis à jour</returns>
        public int ContenTableLevelUp(int shiftNumber)
        {
            chapterNumerotation.Add(0);
            return chapterLevel += shiftNumber;
        }
        /// <summary>
        /// Décremente le niveau d'imbrication dans le sommaire. Affectera la prochaine entrée
        /// dans le sommaire.
        /// </summary>
        /// <returns>Niveau mis à jour</returns>
        public int ContentTableLevelDown()
        {
            return ContentTableLevelDown(1);
        }
        /// <summary>
        /// Décremente de "shiftNumber" le niveau d'imbrication dans le sommaire.
        /// Affecte la prochaine entrée dans le sommaire.
        /// </summary>
        /// <param name="shiftNumber"></param>
        /// <returns>Niveau mis à jour</returns>
        public int ContentTableLevelDown(int shiftNumber)
        {
            chapterNumerotation.RemoveAt(chapterNumerotation.Count - 1);
            return chapterLevel -= shiftNumber;
        }
        /// <summary>
        /// Function used to get the chapter number. 
        /// </summary>
        /// <param name="allNumber"></param>
        /// <returns></returns>
        private String GetChapterNumber(bool allNumber)
        {
            String cn = "";
            int i;
            for (i = 0; i < chapterNumerotation.Count-1; i++)
                if (allNumber)
                    cn += chapterNumerotation[i].ToString() + ".";
                else
                    cn += "   ";
            cn += chapterNumerotation[i].ToString();
            cn += ".";
            return cn;
        }
        /// <summary>
        /// Function used to set the numbering of the chapters. It increases the number of the
        /// lowest level number. It will affect the next chapter entry.
        /// </summary>
        private void IncrChapterNumber ()
        {
            chapterNumerotation[chapterNumerotation.Count - 1]++;
        }
        /// <summary>
        /// Cette methode permet de reaffiché un element du sommaire. Ce qui offre la possibilité 
        /// de faire un sommaire partiel.
        /// </summary>
        /// <param name="key">Clé d'association de l'élément.</param>
        public void ReDrawContentElement(String key)
        {
            SummaryElement chap;
            if (chapters.ContainsKey(key))
                chap = chapters[key] as SummaryElement;
            else
                return;
            if (!HasPage() | IsEndPage(contentTableFontSize)) AddPage(true);
            Font aFont = dfonts[contentTableFont];
            double prevSize = aFont.getSize();
            aFont.SetSize(contentTableFontSize);

            // define dot space
            if (dotSpace == 0)
                dotSpace = aFont.StringWidth(" .");
            String dotLine = "";

            // draw chapter name in contents table
            CRLF(contentTableFontSize, 1, true);
            TextLine titre = new TextLine(aFont, "   " + chap.Title.GetText());
            titre.SetPosition(writtingPosition[0] + 5.0, writtingPosition[1]);
            titre.DrawOn(currentPage);

            // draw dot line
            int nbDot = (int)((pageSize[0] - aFont.StringWidth(titre.GetText()) - margin[3] - pageNumberSpace - writtingPosition[0] - 5.0) / dotSpace);
            if (nbDot < 0) // if there aren't enougth space for the page number, write it on next line without dot line
                CRLF(contentTableFontSize, 1, true);
            else
                for (int i = 0; i < nbDot; i++) dotLine += " .";
            TextLine line = new TextLine(aFont, dotLine);
            line.SetPosition(pageSize[0] - margin[3] - 30.0 - nbDot * dotSpace, writtingPosition[1]);
            line.DrawOn(currentPage);

            // save number field position (to draw it later)
            double[] posi = new double[] { pageSize[0] - margin[3] - pageNumberSpace + 5, writtingPosition[1] };
            chap.AddPrintPosition(currentPage, posi);

            aFont.SetSize(prevSize);
        }
        #endregion sommaire et titres

        #region Gestion de liste à puce
        /// <summary>
        /// Start a ticked list.
        /// </summary>
        /// <param name="fontName">Font to use</param>
        /// <param name="fontSize">Size of font</param>
        /// <param name="firstEntry">first list element</param>
        public void StartList(String fontName, double fontSize, String firstEntry)
        {
            StartList(fontName, fontSize);
            AddToList(firstEntry);
        }
        /// <summary>
        /// Start a ticked list. 
        /// </summary>
        /// <param name="fontName">font to use</param>
        /// <param name="fontSize">Size of font</param>
        public void StartList(String fontName, double fontSize)
        {
            if (dfonts.ContainsKey(fontName))
                listFont = fontName;
            listFontSize = fontSize;
            if (!HasPage() | IsEndPage(GetCurrentFont().getSize())) AddPage();
            listLevel = 0;
            CRLF();
        }
        /// <summary>
        /// Ajout d'un élément dans une liste à puce.
        /// </summary>
        /// <param name="name">Text de l'élément.</param>
        /// <param name="level">Niveau dans l'imbrication des listes</param>
        public void AddToList(String name, int level)
        {
            Font realFont = dfonts[listFont];
            double prevSize = realFont.getSize();
            realFont.SetSize(listFontSize);
            if (!HasPage() | IsEndPage(listFontSize)) AddPage();
            CRLF();
            writtingPosition[0] += level * listTabulation;
            TextLine txt = new TextLine(realFont, ((char)149).ToString() + " " + name);
            txt.SetPosition(writtingPosition[0], writtingPosition[1]);
            txt.DrawOn(currentPage);
            realFont.SetSize(prevSize); // restore the previous set size
        }

        /// <summary>
        /// Ajout d'un élément dans une liste à puce. 
        /// Cette methode nécessite de tenir à jour le niveau d'imbrication dans la liste
        /// à l'aide des methodes ListLevelUp() et ListLevelDown()
        /// </summary>
        /// <param name="name">Text de l'élément.</param>
        public void AddToList(String name)
        {
            AddToList(name, listLevel);
        }
        /// <summary>
        /// Incremente le niveau d'imbrication de la liste à puce. Affecte la prochaine entrée
        /// dans la liste.
        /// </summary>
        /// <returns>Niveau mis à jour</returns>
        public int ListLevelUp()
        {
            return ++listLevel;
        }
        /// <summary>
        /// Incremente de "shiftNumber" le niveau d'imbrication de la liste à puce.
        /// Affecte la prochaine entrée dans la liste.
        /// </summary>
        /// <param name="shiftNumber"></param>
        /// <returns>Niveau mis à jour</returns>
        public int ListLevelUp(int shiftNumber)
        {
            return listLevel += shiftNumber;
        }
        /// <summary>
        /// Décremente le niveau d'imbrication de la liste à puce. Affecte la prochaine entrée
        /// dans la liste.
        /// </summary>
        /// <returns>Niveau mis à jour</returns>
        public int ListLevelDown()
        {
            return --listLevel;
        }
        /// <summary>
        /// Décremente de "shiftNumber" le niveau d'imbrication de la liste à puce.
        /// Affecte la prochaine entrée dans la liste.
        /// </summary>
        /// <param name="shiftNumber"></param>
        /// <returns>Niveau mis à jour</returns>
        public int ListLevelDown(int shiftNumber)
        {
            return listLevel -= shiftNumber;
        }
        #endregion

        #region Gestion du pied et de l'entete de page
        /// <summary>
        /// Header is drawn in the margin. So change the margin size if you need more space.
        /// </summary>
        private void PrintHeader()
        {
            ArrayList texts = new ArrayList();
            Font aFont = dfonts[headerFont];
            double prevSize = aFont.getSize();
            aFont.SetSize(headerFontSize);

            // print left element
            if (!headerText[0].Equals(""))
            {
                TextLine txt = new TextLine(aFont);
                txt.SetText(GetElement(headerText[0]));
                txt.SetPosition(
                    margin[2],
                    margin[1] - 10); //textFont.getSize());
                texts.Add(txt);
            }

            // print center element
            if (!headerText[1].Equals(""))
            {
                TextLine txt = new TextLine(aFont);
                txt.SetText(GetElement(headerText[1]));
                txt.SetPosition(
                    (pageSize[0] - aFont.StringWidth(txt.GetText())) / 2, // to center
                    margin[1] - 10);
                texts.Add(txt);
            }

            // print right element
            if (!headerText[2].Equals(""))
            {
                TextLine txt = new TextLine(aFont);
                txt.SetText(GetElement(headerText[2]));
                txt.SetPosition(
                    pageSize[0] - aFont.StringWidth(txt.GetText()) - margin[3],
                    margin[1] - 10);
                texts.Add(txt);
            }

            // draw texts
            foreach (TextLine txt in texts)
                txt.DrawOn(currentPage);

            // draw the lines
            Line ligne = new Line(margin[2], margin[1] - 5, pageSize[0] - margin[3], margin[1] - 5);
            ligne.DrawOn(currentPage);

            aFont.SetSize(prevSize);
        }
        /// <summary>
        /// Footer is drawn in the margin. So change the margin size if you need more space.
        /// </summary>
        private void PrintFooter()
        {
            ArrayList texts = new ArrayList();
            Font aFont = dfonts[headerFont];
            double prevSize = aFont.getSize();
            aFont.SetSize(headerFontSize);

            // print left element
            if (!footerText[0].Equals(""))
            {
                TextLine txt = new TextLine(aFont);
                txt.SetText(GetElement(footerText[0]));
                txt.SetPosition(
                    margin[2],
                    pageSize[1] - margin[1] + 10 + aFont.getSize());
                texts.Add(txt);
            }

            // print center element
            if (!footerText[1].Equals(""))
            {
                TextLine txt = new TextLine(aFont);
                txt.SetText(GetElement(footerText[1]));
                txt.SetPosition(
                    (pageSize[0] - aFont.StringWidth(txt.GetText())) / 2, // to center
                    pageSize[1] - margin[1] + 10 + aFont.getSize());
                texts.Add(txt);
            }

            // print right element
            if (!footerText[2].Equals(""))
            {
                TextLine txt = new TextLine(aFont);
                txt.SetText(GetElement(footerText[2]));
                txt.SetPosition(
                    pageSize[0] - aFont.StringWidth(txt.GetText()) - margin[3],
                    pageSize[1] - margin[1] + 10 + aFont.getSize());
                texts.Add(txt);
            }

            // draw texts
            foreach (TextLine txt in texts)
                txt.DrawOn(currentPage);

            // draw the lines
            Line ligne = new Line(margin[2], pageSize[1] - margin[1] + 5, pageSize[0] - margin[3], pageSize[1] - margin[1] + 5);
            ligne.DrawOn(currentPage);

            aFont.SetSize(prevSize);
        }
        /// <summary>
        /// Return the text according to a tag. (ex: %PAGE%, return the current page number)
        /// </summary>
        /// <param name="text">Tag</param>
        /// <returns>Related text</returns>
        private String GetElement(String text)
        {
            if (text.Equals("%PAGE%"))
                return pageNumber.ToString();
            //if (text.Equals("%AUTOR%"))
            //return ;
            return text;
        }
        /// <summary>
        /// Set the text of the header
        /// </summary>
        /// <param name="t1">header left part</param>
        /// <param name="t2">header center part</param>
        /// <param name="t3">header left part</param>
        public void SetHeaderText(String t1, String t2, String t3)
        {
            headerText = new String[3] { t1, t2, t3 };
        }
        /// <summary>
        /// Set page footer
        /// </summary>
        /// <param name="t1">footer left part</param>
        /// <param name="t2">footer center part</param>
        /// <param name="t3">footer left part</param>
        public void SetFooterText(String t1, String t2, String t3)
        {
            footerText = new String[3] { t1, t2, t3 };
        }
        #endregion /* gestion du pied et de l'entete de page */

        #region Gestion des tableaux
        /// <summary>
        /// Add a table. 
        /// </summary>
        /// <param name="table">Table cells</param>
        /// <param name="columnWidth">Table column width</param>
        /// <param name="bMultiline">Allow multiline cell</param>
        public void AddTable(List<List<Cell>> table, int[] columnWidth, bool bMultiline)
        {
            /// add row and remove line for multiline cell
                /// methode :
                /// . on parcourt toutes les cellules du tableau en verifiant que le text des cellules ne soit pas trop long
                ///    . si le text d'une cellule est trop long
                ///        . on crée un tableau de String de ce text decoupé
                ///        . puis on crée (si il n'existe pas) un tableau de ligne a ajouter
                ///        . puis on y insert la colonne de texte découpé
                ///    . à la fin du parcourt d'une ligne on insert les nouvelles ligne créent
            bool HasNewRows = false;
            List<int[]> linesToRemove = new List<int[]>(); // [0]<- line 1 , [1]<- line 2
            for (int rowIndex=0 ; rowIndex<table.Count ; rowIndex++) //(List<Cell> row in table)
            {
                List<List<Cell>> newTextLines = null;
                List<Cell> row = table[rowIndex]; //int rowIndex = table.IndexOf(row);
                // check all cell text size
                foreach (Cell cell in row)
                {
                    int cellIndex = row.IndexOf(cell);
                    String[] cutText = GetSplitedText(cell.GetText(), GetCurrentFont(), columnWidth[cellIndex]);
                    if (cutText.Length > 1) // if cell text is too large
                    {
                        HasNewRows = true;
                        if (newTextLines == null)
                            newTextLines = new List<List<Cell>>();
                        // on insert le text dans la colonne correspondante (donc parcourt de la colonne)
                        for (int i=0 ; i<cutText.Length ; i++) 
                        {
                            // test if the line already exists
                            if (newTextLines.Count <= i) 
                            {
                                List<Cell> cellsLine = new List<Cell>();
                                for (int j = 0; j < row.Count; j++)
                                {
                                    Cell newCell = new Cell(row[cellIndex].GetFont(), "");
                                    // copy properties
                                        newCell.SetBgColor(row[cellIndex].GetBgColor());
                                        newCell.SetWidth(row[cellIndex].GetWidth());
                                        newCell.SetHeight(row[cellIndex].GetHeight());
                                    cellsLine.Add(newCell);
                                }
                                newTextLines.Add(cellsLine);
                            }
                            // set the cells
                            newTextLines[i][cellIndex].SetText(cutText[i]);
                        }
                    }
                }

                // Add the new rows
                if (HasNewRows)
                {
                    // update first row content
                    for(int i=0 ; i<table[rowIndex].Count ; i++)
                        if (!newTextLines[0][i].GetText().Equals(""))
                            table[rowIndex][i].SetText(newTextLines[0][i].GetText());
                    newTextLines.RemoveAt(0);
                    //table[rowIndex]
                    for (int i=rowIndex ; i<rowIndex + newTextLines.Count ; i++)
                        linesToRemove.Add(new int[2] { i, i+1 });
                    //linesToRemove.Add(new int[2] { rowIndex, rowIndex + newTextLines.Count }); // suprime qu'une seul ligne
                    table.InsertRange(rowIndex + 1, newTextLines);    //.Insert(rowIndex+1, //la nouvelle ligne
                    rowIndex += newTextLines.Count;
                    HasNewRows = false;
                }
            }

            // create the pdf table instance
            Table pdfTable = new Table(GetCurrentFont(), GetCurrentFont());
            pdfTable.SetData(table);
            pdfTable.setBottomMargin(margin[1]);
            //writtingPosition[1] += GetCurrentFont().getSize() + 5;
            pdfTable.SetPosition(margin[2], writtingPosition[1]);

            // remove the lines between the addes rows
            foreach (int[] lines in linesToRemove)
                pdfTable.RemoveLineBetweenRows(lines[0], lines[1]);

            // set the column width
            if (columnWidth != null)
                for (int i = 0; i < columnWidth.Length; i++)
                    pdfTable.SetColumnWidth(i, columnWidth[i]);
            else 
                if (bMultiline)
                    return; // if column width is not defined multiline can't be possible
                else
                    pdfTable.AutoAdjustColumnWidths();

            // delete column out of page
            if (pdfTable.GetWidth() > pageSize[0])
            {
                List<Cell> row = pdfTable.GetRow(0);
                int lastIn;
                double size = 0.0;
                for (lastIn = 0; lastIn < row.Count; lastIn++)
                {
                    size += row[lastIn].GetWidth();
                    if (size > pageSize[0]) break;
                }
                lastIn++;
                for (int i = 0; i < table.Count; i++)
                {
                    List<Cell> tmp_row = table[i];
                    tmp_row.RemoveRange(lastIn, tmp_row.Count - lastIn);
                }
            }

            // start writting it on the page
            //pdfTable.RightAlignNumbers(); // coute tres cher en temps (10x) -> voir Table.cs ligne 173
            double writtedLength;
            bool multiPage = false;
            while (true)
            {
                writtedLength = pdfTable.GetHeight();
                pdfTable.DrawOn(currentPage);
                if (!pdfTable.HasMoreData())
                    break;
                else
                    multiPage = true;
                AddPage();
                pdfTable.SetPosition(margin[2], writtingPosition[1] + GetCurrentFont().getSize() + 5);
            }
            double lastLength = pdfTable.GetHeight() - writtedLength; // longueur du dernier bloc du tableau ecri sur la derniere page
            if (multiPage) // if it's a new page, start writing at the beginning
                writtingPosition[1] = lastLength + margin[0];
            else
                writtingPosition[1] += lastLength;
            CRLF();
        }
        /// <summary>
        /// Add a table. Note that column width could be null.
        /// </summary>
        /// <param name="table">Cells of the table</param>
        /// <param name="columnWidth">Table column width</param>
        public void AddTable(List<List<Cell>> table, double[] columnWidth)
        {
            Table pdfTable = new Table(GetCurrentFont(), GetCurrentFont());
            pdfTable.SetData(table);
            pdfTable.setBottomMargin(margin[1]);
            CRLF();
            pdfTable.SetPosition(margin[2], writtingPosition[1]);
            if (columnWidth != null)
                for (int i = 0; i < columnWidth.Length; i++)
                    pdfTable.SetColumnWidth(i, columnWidth[i]);
            else
                pdfTable.AutoAdjustColumnWidths();
            // delete column out of page
            if (pdfTable.GetWidth() > pageSize[0])
            {
                List<Cell> row = pdfTable.GetRow(0);
                int lastIn;
                double size = 0.0;
                for (lastIn = 0; lastIn < row.Count; lastIn++)
                {
                    size += row[lastIn].GetWidth();
                    if (size > pageSize[0]) break;
                }
                lastIn += 1;
                for (int i = 0; i < table.Count; i++)
                {
                    List<Cell> tmp_row = table[i];
                    if (tmp_row.Count - lastIn > 0) // >> Task #10156 Pax2Sim - Statistic dev - Target
                        tmp_row.RemoveRange(lastIn, tmp_row.Count - lastIn);
                }
            }
            //pdfTable.RightAlignNumbers(); // coute tres cher en temps (10x) -> voir Table.cs ligne 173
            double writtedLength;
            bool multiPage = false;
            while (true)
            {
                writtedLength = pdfTable.GetHeight();
                pdfTable.DrawOn(currentPage);
                if (!pdfTable.HasMoreData())
                    break;
                else
                    multiPage = true;
                AddPage();
                pdfTable.SetPosition(margin[2], writtingPosition[1]);// + FontSize + 5);
            }
            double lastLength = pdfTable.GetHeight() - writtedLength; // longueur du dernier bloc du tableau ecri sur la derniere page
            if (multiPage)
            {
                writtingPosition[1] = lastLength + margin[0];
                CRLF();
            }
            else
                writtingPosition[1] += lastLength;
        }
        /// <summary>
        /// Function to obtain the columns width
        /// </summary>
        /// <param name="table"></param>
        /// <returns>Table of the columns width</returns>
        public double[] GetTableColumnWidth(List<List<Cell>> table)
        {
            List<double> width = new List<double>();
            
            // init pdf table
            Table pdfTable = new Table(GetCurrentFont(), GetCurrentFont());
            pdfTable.SetData(table);
            pdfTable.AutoAdjustColumnWidths();

            // read column width
            List < Cell > row = pdfTable.GetRow(0);
            for (int i = 0; i < row.Count; i++)
                width.Add(row[i].GetWidth());

            return width.ToArray();
        }
        #endregion /* Gestion des tableaux */

        #region Accesseur et Définition des parametres du document
        /// <summary>
        /// Ajoute un titre en debut du pdf. à faire avant d'écrire autre chose dans le document.
        /// </summary>
        /// <param name="title">le titre à ajouter.</param>
        public void SetHeadTitle(String title, String fontName, double fontSize)
        {
            if (!HasPage()) AddPage();
            Font aFont = dfonts[defaultFont];
            if (dfonts.ContainsKey(fontName))
                aFont = dfonts[fontName];
            double prevSize = aFont.getSize();
            aFont.SetSize(fontSize);

            writtingPosition[0] = margin[2] + 20.0;
            writtingPosition[1] = margin[0] + 10.0;
            TextLine txt = new TextLine(aFont, title);
            txt.SetPosition(writtingPosition[0], writtingPosition[1]);
            txt.DrawOn(currentPage);
            writtingPosition[0] = margin[2];
            writtingPosition[1] += 30.0;

            aFont.SetSize(prevSize);
        }

        /// <summary>
        /// Définition de la taille de la page.
        /// ex: SetPageFormat(PDFjet.NET.A4.PORTRAIT);
        /// </summary>
        /// <param name="pageSize"></param>
        public void SetPageFormat(Double[] pageSize)
        {
            this.pageSize = pageSize;
        }
        /// <summary>
        /// Définition de la taille de la page.
        /// ex: PageFormat = PDFjet.NET.A4.PORTRAIT;
        /// </summary>
        public Double[] PageFormat
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public void SetMargin(Double top, Double bottom, Double left, Double right)
        {
            this.margin = new Double[4] { top, bottom, left, right };
        }
        public Double[] Margin
        {
            get { return margin; }
        }

        /// <summary>
        /// Gets or sets the cursor position.
        /// </summary>
        public double[] CursorPosition
        {
            get { return writtingPosition; }
            set { writtingPosition = value; }
        }

        /// <summary>
        /// Gets or sets if the page number will be written.
        /// </summary>
        private bool hasPageNumber = true;
        public bool DisplayPageNumber
        {
            get {  return hasPageNumber; }
            set { hasPageNumber = value; }
        }

        /// <summary>
        /// Function to set if page number will be printed on the next page and 
        /// set its position.
        /// ex : SetPageNumber(true, PDFGenerator.RIGTHBOTTOM)
        /// </summary>
        /// <param name="printed"></param>
        /// <param name="position"></param>
        public void SetPageNumber(bool printed, int position)
        {
            hasPageNumber = printed;
            PageNumberPossition = position;
        }

        /// <summary>
        /// Function to set if page number will be printed on the next page, 
        /// set its position and an offset.
        /// ex : SetPageNumber(true, PDFGenerator.RIGTHBOTTOM, new Double[2] { 20.0 , 20.0 })
        /// </summary>
        /// <param name="printed"></param>
        /// <param name="position"></param>
        /// <param name="offset"></param>
        public void SetPageNumber(bool printed, int position, Double[] offset)
        {
            hasPageNumber = printed;
            pageNumberPossitionOffSet = offset;
            PageNumberPossition = position;
        }

        /// <summary>
        /// Function to set if the header will be printed on the next page.
        /// </summary>
        /// <param name="header"></param>
        public void SetHeader(bool header)
        {
            hasHeader = header;
        }

        /// <summary>
        /// Function to set if the footer will be printed on the next page.
        /// </summary>
        /// <param name="footer"></param>
        public void SetFooter(bool footer)
        {
            hasFooter = footer;
        }

        /// <summary>
        /// Percentage of the font that will be used to set the line space.
        /// </summary>
        public int LineSpacePercentage
        {
            get { return (int)lineSpaceRatio * 100; }
            set { lineSpaceRatio = value / 100; }
        }

        /// <summary>
        /// Access or change the current font.
        /// </summary>
        //public FontList CurrentFont
        //{
        //    set { currentFont = value; }
        //    get { return currentFont; }
        //}
        #endregion

        #region font
        /// <summary>
        /// Get the current used font.
        /// </summary>
        /// <returns>the font</returns>
        public Font GetCurrentFont()
        {
            return dfonts[sCurrentFont];
        }
        /// <summary>
        /// Define the font to use.
        /// </summary>
        /// <param name="name">Font name</param>
        /// <param name="size">Font size</param>
        public void SetFont(String name, double size)
        {
            if (!dfonts.ContainsKey(name))
                return;
            sCurrentFont = name;
            GetCurrentFont().SetSize(size);
        }
        /// <summary>
        /// Get or define the current used font.
        /// </summary>
        public String Font
        {
            set
            {
                if (!dfonts.ContainsKey(value))
                    return;
                double lastFont = GetCurrentFont().getSize();
                sCurrentFont = value;
                GetCurrentFont().SetSize(lastFont);
            }
            get { return sCurrentFont; }
        }
        /// <summary>
        /// Get or define the current used font size.
        /// </summary>
        public double FontSize
        {
            set { GetCurrentFont().SetSize(value); }
            get { return GetCurrentFont().getSize(); }
        }
        /// <summary>
        /// Get the length of a string, according to the current font.
        /// </summary>
        /// <param name="text">String to measure</param>
        /// <returns>String width</returns>
        public double GetTextLength(String text)
        {
            return GetTextLength(text, GetCurrentFont());
        }
        /// <summary>
        /// Get the length of a string, according to the given font name.
        /// </summary>
        /// <param name="text">String to measure</param>
        /// <param name="fontName">Name of the font to use</param>
        /// <param name="size">size of the font to use</param>
        /// <returns>String width</returns>
        public double GetTextLength(String text, String fontName, double size)
        {
            double width = 0.0;
            if (dfonts.ContainsKey(fontName))
            {
                Font f = dfonts[fontName];
                double prevSize = f.getSize();
                f.SetSize(size);
                width = f.StringWidth(text);
                f.SetSize(prevSize);
            }
            return width;
        }
        /// <summary>
        /// Get the length of a string, according to the given font.
        /// </summary>
        /// <param name="text">String to measure</param>
        /// <param name="font">Font to use</param>
        /// <returns>String width</returns>
        private double GetTextLength(String text, Font font)
        {
            return font.StringWidth(text);
        }
        /// <summary>
        /// Obtain the font according to font name and 'bold' 'italic' setting.
        /// </summary>
        /// <param name="FontName">Font name</param>
        /// <param name="bold">returned font in bold</param>
        /// <param name="italic">returned font in italic</param>
        /// <returns></returns>
        private Font GetModifiedFont(String FontName, bool bold, bool italic)
        {
            return GetModifiedFont(FontName, 0, bold, italic);
        }
        /// <summary>
        /// Obtain the font according to font name and 'bold' 'italic' setting.
        /// </summary>
        /// <param name="FontName">Font name</param>
        /// <param name="bold">returned font in bold</param>
        /// <param name="italic">returned font in italic</param>
        /// <returns></returns>
        private Font GetModifiedFont(String FontName, int fontSize, bool bold, bool italic)
        {
            Font aFont = dfonts["Helvetica"];
            if (FontName == "Times-Roman")
            {
                if (bold && italic)
                    aFont = dfonts["Times-BoldItalic"];
                else if (bold)
                    aFont = dfonts["Times-Bold"];
                else if (italic)
                    aFont = dfonts["Times-Italic"];
                else
                    aFont = dfonts[FontName];
            }
            else if (FontName == "Courier" || FontName == "Helvetica")
            {
                if (bold && italic)
                    aFont = dfonts[FontName + "-BoldOblique"];
                else if (bold)
                    aFont = dfonts[FontName + "-Bold"];
                else if (italic)
                    aFont = dfonts[FontName + "-Oblique"];
                else
                    aFont = dfonts[FontName];
            }
            if (fontSize == 0)
                return aFont;
            else
            {
                switch (fontSize)
                {
                    case 1: aFont.SetSize(7.0); break;
                    case 2: aFont.SetSize(9.0); break;
                    case 3: aFont.SetSize(11.0); break;
                    case 4: aFont.SetSize(13.0); break;
                    case 5: aFont.SetSize(15.0); break;
                    case 6: aFont.SetSize(17.0); break;
                    case 7: aFont.SetSize(19.0); break;
                    case 8: aFont.SetSize(21.0); break;
                    case 9: aFont.SetSize(23.0); break;
                    case 10: aFont.SetSize(25.0); break;
                    default: aFont.SetSize(27.0); break;
                }
            }
            return aFont;
        }
        /// <summary>
        /// Obtain the font size according to html font size.
        /// </summary>
        /// <param name="fontSize">Font size</param>
        /// <returns></returns>
        private double GetModifiedFont(int fontSize)
        {
            switch (fontSize)
            {
                case 1: return 7.0;
                case 2: return 9.0;
                case 3: return 11.0;
                case 4: return 13.0;
                case 5: return 15.0;
                case 6: return 17.0;
                case 7: return 19.0;
                case 8: return 21.0;
                case 9: return 23.0;
                case 10: return 25.0;
                default: return 27.0;
            }
        }
        /*
        public Font GetFontFromFontList(FontList font)
        {
            switch (font)
            {
                case FontList.NORMAL: return textFont;
                case FontList.TITLE: return titleFont;
                case FontList.SUPERSIZE: return superSizeFont;
                default: return textFont;
            }
        }
        public Font GetCurrentFont()
        {
            switch (currentFont)
            {
                case FontList.NORMAL: return textFont;
                case FontList.TITLE: return titleFont;
                case FontList.SUPERSIZE: return superSizeFont;
                default: return textFont;
            }
        }
        /// <summary>
        /// Function used to get the length of a string with the current font.
        /// </summary>
        /// <param name="text">Concerned string</param>
        /// <returns>The length</returns>
        public double GetTextLength(String text)
        {
            return GetTextLength(text, GetCurrentFont());
        }
        /// <summary>
        /// Function used to get the length of a string.
        /// </summary>
        /// <param name="text">Concerned string</param>
        /// <param name="font">Font to use</param>
        /// <returns>The length</returns>
        public double GetTextLength(String text, FontList font)
        {
            return GetTextLength(text, GetFontFromFontList(font));
        }
        /// <summary>
        /// Function used to get the length of a string.
        /// </summary>
        /// <param name="text">Concerned string</param>
        /// <param name="font">Font to use</param>
        /// <returns>The length</returns>
        private double GetTextLength(String text, Font font)
        {
            return font.StringWidth(text);
        }
        */
        #endregion font

        #region space
        /// <summary>
        /// function to go to the beginning of the next page.
        /// </summary>
        public void PageBreak()
        {
            AddPage();
        }
        /// <summary>
        /// Move the cursor to the beginning of the next line, 
        /// using the current font size.
        /// Add a page in cas.
        /// </summary>
        public void CRLF()
        {
            CRLF(FontSize, 1, false);
        }
        /// <summary>
        /// Move the cursor to the beginning of the 'nb' next line,
        /// using the current font size. Add a page in cas.
        /// </summary>
        /// <param name="nb">number of line to jump</param>
        public void CRLF(int nb)
        {
            CRLF(FontSize, nb, false);
        }
        /// <summary>
        /// Move the cursor to the beginning of the 'nb' next line,
        /// using the specified size. Add a page in cas.
        /// </summary>
        /// <param name="fontSize">reference font size</param>
        /// <param name="nb">number of line to jump</param>
        public void CRLF(double fontSize, int nb, bool bBlancPage)
        {
            double toAddSpace = fontSize * (1 + lineSpaceRatio);
            if (!HasPage() | IsEndPage(toAddSpace)) AddPage(bBlancPage);
            writtingPosition[0] = margin[2];
            writtingPosition[1] = writtingPosition[1] + toAddSpace * nb;
        }
        public double GetPageLeftSpace()
        {
            return (pageSize[1] - margin[1]) - writtingPosition[1];
        }
        #endregion space

        #region page
        /// <summary>
        /// Function to check if this is the end of the current page.
        /// </summary>
        /// <returns></returns>
        private bool IsEndPage(Double offset)
        {
            return writtingPosition[1] > pageSize[1] - margin[1] - offset;
        }

        /// <summary>
        /// Function to check if there are a current page.
        /// </summary>
        private bool HasPage()
        {
            return currentPage != null;
        }

        /// <summary>
        /// Function to add a new page.
        /// </summary>
        /// <param name="bBlank">To create a page without header and footer.</param>
        private void AddPage(bool bBlank)
        {
            pageNumber++;
            currentPage = new Page(pdf, pageSize);
            writtingPosition[0] = margin[2];
            writtingPosition[1] = margin[0] + GetCurrentFont().getSize();
            //if (hasPageNumber) PrintPageNumber();
            if (hasHeader && !bBlank) PrintHeader();
            if (hasFooter && !bBlank) PrintFooter();
        }
        /// <summary>
        /// Add a page to the pdf document with header and footer
        /// </summary>
        private void AddPage()
        {
            AddPage(false);
        }

        /// <summary>
        /// Add a page without footer and header
        /// </summary>
        public void AddBlankPage()
        {
            AddPage(true);
        }

        /// <summary>
        /// Check if the page is newly created.
        /// </summary>
        /// <returns></returns>
        public bool IsNewPage()
        {
            return writtingPosition[0] == margin[2] &&
                writtingPosition[1] == margin[0] + GetCurrentFont().getSize();
        }
        #endregion page

        #region Paragraphe
        /// <summary>
        /// Function to write a paragraph between the margin.
        /// </summary>
        /// <param name="paragraphText"></param>
        public void PutParagraph (String paragraphText)
        {
            double largueur = pageSize[0] - margin[2] - margin[3];
            bool finish = paragraphText.Equals("");
            String[] words = paragraphText.Split(' ');
            int position = 0;
            int nbWords = words.Length;
            while (!finish)
            {
                String tmp = "";
                if (position == 0) tmp = "    ";
                while ((GetCurrentFont().StringWidth(tmp) + GetCurrentFont().StringWidth(words[position])) < largueur)
                {
                    tmp += " " + words[position++];
                    if (position >= nbWords)
                    {
                        finish = true;
                        break;
                    }
                }

                // affichage
                CRLF();
                TextLine txt = new TextLine(GetCurrentFont(), tmp);
                txt.SetPosition(writtingPosition[0], writtingPosition[1]);
                txt.DrawOn(currentPage);
            }
             
        }

        /// <summary>
        /// Determine la longueur que fera un paragraphe
        /// </summary>
        /// <param name="paragraphText"></param>
        public double GetParagraphLenth(String paragraphText)
        {
            double parLenth = 0.0;
            double largueur = pageSize[0] - margin[2] - margin[3];
            bool finish = paragraphText.Equals("");
            String[] words = paragraphText.Split(' ');
            int position = 0;
            int nbWords = words.Length;
            while (!finish)
            {
                String tmp = "";
                if (position == 0) tmp = "    ";
                while ((GetCurrentFont().StringWidth(tmp) + GetCurrentFont().StringWidth(words[position])) < largueur)
                {
                    tmp += " " + words[position++];
                    if (position >= nbWords)
                    {
                        finish = true;
                        break;
                    }
                }
                // comptage
                parLenth += GetCurrentFont().getSize() * (1 + lineSpaceRatio);
            }
            return parLenth;
        }
        #endregion Paragraphe

        #region text
        /// <summary>
        /// Split text in part smaller than 'limit'.
        /// </summary>
        /// <param name="text">Text to split</param>
        /// <param name="font">Reference font</param>
        /// <param name="maxSize">max size of the txt part.</param>
        /// <returns>Table of the text part.</returns>
        private String[] GetSplitedText(String text, Font font, double maxSize)
        {
            List<String> textLines = new List<String>();
            bool finish = text.Equals("");
            String[] words = text.Split(' ');
            int wordsIndex = 0;
            int nbWords = words.Length;
            while (!finish)
            {
                String tmp = "";
                while (true)
                {
                    if (wordsIndex >= nbWords) // end check
                    {
                        finish = true;
                        break;
                    }
                    String tmp2 = tmp + (tmp == "" ? "" : " ") + words[wordsIndex];
                    if (font.StringWidth(tmp2) < maxSize)
                    {
                        tmp = tmp2;
                        wordsIndex++;
                    }
                    else
                    {
                        if (tmp.Equals("")) // cas ou un mot seul ne passe pas
                        {
                            textLines.AddRange(
                                GetSplitedWord(words[wordsIndex], font, maxSize));
                            wordsIndex++;
                        }
                        break;
                    }
                }
                if (!tmp.Equals(""))
                    textLines.Add(tmp);
            }
            return textLines.ToArray();
        }
        /// <summary>
        /// Split word in part smaller than 'maxSize'.
        /// </summary>
        /// <param name="word">Text to split</param>
        /// <param name="font">Reference font</param>
        /// <param name="maxSize">max size of the txt part.</param>
        /// <returns>Table of the text part.</returns>
        private String[] GetSplitedWord(String word, Font font, double maxSize)
        {
            List<String> textLines = new List<String>();
            String wordPart = "";
            int charIndex = 0;
            bool finish = false;
            while (!finish)
            {
                wordPart = "";
                while (true)
                {
                    if (charIndex >= word.Length) // end check, no more word
                    {
                        finish = true;
                        break;
                    }
                    String tmp2 = wordPart + word[charIndex];
                    if (font.StringWidth(tmp2) < maxSize)
                    {
                        wordPart = tmp2;
                        charIndex++;
                    }
                    else
                    {
                        if (wordPart.Equals("")) // cas ou une seule lettre ne passe pas
                            charIndex++; // y a rien a faire
                        break;
                    }
                }
                textLines.Add(wordPart);
            }
            return textLines.ToArray();
        }
        /// <summary>
        /// Cut a part in the text smaller than 'limit'.
        /// </summary>
        /// <param name="text">Text to split</param>
        /// <param name="font">Reference font</param>
        /// <param name="maxSize">max size of the txt part.</param>
        /// <returns>Table containing the cut text and the rest of the text.</returns>
        private String[] GetFirstSplitedText(String text, Font font, double maxSize)
        {
            int spaceIndex = 0;

            // puis on va d'espace en espace pour trouver la bonne taille
            int i = 0;
            while (i < text.Length)
            {
                if (text[i] == ' ')
                {
                    if (font.StringWidth(text.Substring(0, i)) < maxSize)
                        spaceIndex = i + 1; // keep the space char at the end
                    else
                        break;
                }
                i++;
            }
            String str = text.Substring(0, spaceIndex);
            String remaining = "";
            if (spaceIndex < text.Length)
                remaining = text.Substring(spaceIndex, text.Length - spaceIndex);
            return new String[2] { str, remaining };
        }
        /// <summary>
        /// Add a text line and go to the next line.
        /// </summary>
        /// <param name="line"></param>
        public void PutTextLine(String line)
        {
            CRLF();
            TextLine txt = new TextLine(GetCurrentFont(), line);
            txt.SetPosition(margin[2], writtingPosition[1]);
            txt.DrawOn(currentPage);
        }
        /// <summary>
        /// Put a title at the current cursor position, whith the current font and font size,
        /// without ading it in the table of content.
        /// </summary>
        /// <param name="line"></param>
        public void PutTitle(String line)
        {
            CRLF();
            TextLine txt = new TextLine(GetCurrentFont(), line);
            txt.SetPosition(margin[2], writtingPosition[1]);
            txt.DrawOn(currentPage);
        }
        #endregion text

        #region HTML
        /// <summary>
        /// Insert simple html into pdf.
        /// </summary>
        public void AddSimpleHTML(String html)
        {
            //String htmlContent = OpenHTML(htmlPath);
            String htmlContent = html;
            if (htmlContent == null) return;
            String prevFont = this.Font;
            double prevSize = this.FontSize;
            
            List<TextPart> TextParts = new List<TextPart>();
            Stack<AttributeList> st_fonts = new Stack<AttributeList>();
            String lastFont = defaultHtmlFont;
            int lastSize = defaultHtmlSize;
            int[] lastColor = null;
            Stack<String> align = new Stack<string>();
            String link = null;
            bool bold = false;
            bool italic = false;
            bool underline = false;
            bool strikeThrough = false;
            StringBuilder foundText = new StringBuilder();
            char prevCh = '\0';

            parse = new ParseHTML.ParseHTML();
            parse.Source = htmlContent;
            //parse.Source = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\"><HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=utf-8\"><META content=\"MSHTML 6.00.6000.17093\" name=GENERATOR></HEAD><BODY><P><FONT size=7><IMG height=133 alt=\"\" hspace=0 src=\"C:\\Documents and Settings\\Contrat Pro\\Mes documents\\Mes images\\Commentaire.jpg\" width=160 align=baseline border=0>&nbsp;</FONT><FONT size=3>Voici Une belle BUlle Je peux aussi vous montrer une autre bulle<IMG style=\"WIDTH: 154px; HEIGHT: 127px\" height=236 alt=\"\" hspace=0 src=\"C:\\Documents and Settings\\Contrat Pro\\Mes documents\\Mes images\\commentaire.GIF\" width=335 align=baseline border=0></FONT></P></BODY></HTML>";
            
            // set default stack state
            align.Push("left");

            while (!parse.Eof())
            {
                char ch = parse.Parse();
                if (ch == 0)
                {
                    AttributeList tag = parse.GetTag();
                    if (tag.Name == "TABLE")
                    {
                        if (foundText.Length != 0)
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                        ParseHtmlTable();
                    }
                    if (tag.Name == "FONT")
                    {
                        if (foundText.Length != 0)
                        {
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                            foundText.Remove(0, foundText.Length);
                        }
                        // update the used value
                        st_fonts.Push(tag);
                        lastFont = SearchLastFont (st_fonts);
                        lastSize = SearchLastSize(st_fonts);
                        lastColor = SearchLastColor(st_fonts);
                    } 
                    if (tag.Name == "/FONT")
                    {
                        if (foundText.Length != 0)
                        {
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                            foundText.Remove(0, foundText.Length);
                        }
                        // update the used value
                        st_fonts.Pop();
                        lastFont = SearchLastFont(st_fonts);
                        lastSize = SearchLastSize(st_fonts);
                        lastColor = SearchLastColor(st_fonts);
                    }
                    if (tag.Name == "A")
                    {
                        if (foundText.Length != 0)
                        {
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                            foundText.Remove(0, foundText.Length);
                        }
                        if (tag["href"] != null)
                            link = tag["href"].Value;
                    }
                    if (tag.Name == "/A")
                    {
                        if (foundText.Length != 0)
                        {
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                            foundText.Remove(0, foundText.Length);
                        }
                        link = null;
                    }
                    if (tag.Name == "P")
                    {
                        if (foundText.Length != 0)
                        {
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                            foundText.Remove(0, foundText.Length);
                        }
                        WriteHtmlText(TextParts, align.Peek()); // write the text that is not in the new paragraphe
                        //CRLF(GetModifiedFont(lastSize), 1, false); // to separate paragraphs
                        TextParts = new List<TextPart>();
                        if (tag["align"] != null)
                            align.Push(tag["align"].Value);
                        else
                            align.Push("left");
                    }
                    if (tag.Name == "/P")
                    {
                        if (foundText.Length != 0)
                        {
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                            CRLF(GetModifiedFont(lastSize), 1, false);
                            foundText.Remove(0, foundText.Length);;
                        }
                        WriteHtmlText(TextParts, align.Peek()); // write this paragraph
                        //CRLF(GetModifiedFont(lastSize), 1, false); // to separate paragrphs
                        TextParts = new List<TextPart>();
                        align.Pop();
                    }
                    if (tag.Name.ToUpper() == "IMG")
                    {
                        if (foundText.Length != 0)
                        {
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                            foundText.Remove(0, foundText.Length);
                        }
                        WriteHtmlText(TextParts, align.Peek());
                        TextParts = new List<TextPart>();
                    /*    else
                        {*/
                            if (tag["src"] == null) 
                                continue;
                            String source = tag["src"].Value;
                            if (source.Substring(0, 4).Equals("file"))
                                source = source.Substring(8); // remove the "file:///" at the beginning of html link
                            source = source.Replace("%20", " ");
                            if (!ImagesTable.ContainsKey(source)) 
                                continue;
                            Image img = ImagesTable[source] as Image;
                            double[] pos = new double[2] {margin[2], writtingPosition[1]};
                            if (align.Peek() == "center")
                                pos[0] = (img.GetWidth() + PageFormat[0]) / 2;
                            else if (align.Peek() == "right")
                                pos[0] = PageFormat[0] - img.GetWidth() - margin[3];

                            DrawImage(source, 1, pos);
                            writtingPosition[1] += img.GetHeight();
                       // }
                    }
                    if (tag.Name == "EM" || tag.Name == "I" || tag.Name == "ITALIC")
                    {
                        if (foundText.Length != 0)
                        {
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                            foundText.Remove(0, foundText.Length);
                        }
                        italic = true;
                    }
                    if (tag.Name == "/EM" || tag.Name == "/I" || tag.Name == "/ITALIC")
                    {
                        if (foundText.Length != 0)
                        {
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                            foundText.Remove(0, foundText.Length);
                        }
                        italic = false;
                    }
                    if (tag.Name == "B" || tag.Name == "STRONG") // Bold
                    {
                        if (foundText.Length != 0)
                        {
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                            foundText.Remove(0, foundText.Length);
                        }
                        bold = true;
                    }
                    if (tag.Name == "/B" || tag.Name == "/STRONG") // Bold
                    {
                        if (foundText.Length != 0)
                        {
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                            foundText.Remove(0, foundText.Length);
                        }
                        bold = false;
                    }
                    if (tag.Name == "U") // underline
                    {
                        if (foundText.Length != 0)
                        {
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                            foundText.Remove(0, foundText.Length);
                        }
                        underline = true;
                    }
                    if (tag.Name == "/U") // underline
                    {
                        if (foundText.Length != 0)
                        {
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                            foundText.Remove(0, foundText.Length);
                        }
                        underline = false;
                    }
                    if (tag.Name == "S" || tag.Name == "STRIKE") // Barré
                    {
                        if (foundText.Length != 0)
                        {
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                            foundText.Remove(0, foundText.Length);
                        }
                        strikeThrough = true;
                    }
                    if (tag.Name == "/S" || tag.Name == "/STRIKE") // Barré
                    {
                        if (foundText.Length != 0)
                        {
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                            foundText.Remove(0, foundText.Length);
                        }
                        strikeThrough = false;
                    }
                    if (tag.Name == "BR" || tag.Name == "BR/" || tag.Name == "BR /") 
                    {
                        if (foundText.Length != 0)
                        {
                            TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
                            foundText.Remove(0, foundText.Length);
                        } 
                        TextParts.Add(new TextPart(lastSize));
                    }
                }
                else
                {
                    if (ch == '&') // traitement des caracteres speciaux (&nbsp; &amp; ...)
                    {
                        String specChar = "";
                        while (!parse.Eof())
                        {
                            ch = parse.Parse();
                            if (ch == ';')
                                break;
                            specChar += ch.ToString();
                        }
                        if (dCharDef.ContainsKey(specChar))
                            foundText.Append(dCharDef[specChar]);
                    }
                    else if (ch != '\t' && ch != '\n' && ch !='\r') // le html ne prend pas en compte les tabulations et les retour chariots  
                    {
                        if (!(prevCh == ' ' && ch == ' ')) // le html ne prend pas en compte les espaces consecutifs
                        {
                            foundText.Append(ch);
                            prevCh = ch;
                        }
                    }
                }
            }
            if (foundText.Length > 0)
                TextParts.Add(new TextPart(foundText.ToString(), lastFont, lastSize, lastColor, link, bold, italic, underline, strikeThrough));
            if (TextParts.Count > 0)
                WriteHtmlText(TextParts, align.Peek());
            this.Font = prevFont;
            this.FontSize = prevSize;
        }
        /// <summary>
        /// Search the last parameters in the font stack
        /// </summary>
        /// <param name="st_fonts">Font Stack</param>
        /// <param name="param">parameter name</param>
        /// <returns>Parameters value</returns>
        private String SearchLastParam (Stack<AttributeList> st_fonts, String param)
        {
            Stack<AttributeList> elements = new Stack<AttributeList>();
            String result = null;
            while (st_fonts.Count != 0)
            {
                AttributeList attr = st_fonts.Pop();
                elements.Push(attr);
                if (attr[param] != null)
                {
                    result = attr[param].Value;
                    break;
                }
            }
            while (elements.Count != 0)
                st_fonts.Push(elements.Pop());
            return result;
        }
        /// <summary>
        /// Search the last font in the font Stack.
        /// </summary>
        /// <param name="st_fonts">Font Stack</param>
        /// <returns>Font face name</returns>
        private String SearchLastFont(Stack<AttributeList> st_fonts)
        {
            String res = SearchLastParam(st_fonts, "face");
            if (res == null)
                return defaultHtmlFont;
            return res;
        }
        /// <summary>
        /// Search the last font size in the font Stack.
        /// </summary>
        /// <param name="st_fonts">Font Stack</param>
        /// <returns>Font size</returns>
        private int SearchLastSize(Stack<AttributeList> st_fonts)
        {
            String str = SearchLastParam(st_fonts, "size");
            int res = defaultHtmlSize;
            int.TryParse(str, out res);
            return res;
        }
        /// <summary>
        /// Search the last font color in the font Stack.
        /// </summary>
        /// <param name="st_fonts">Font Stack</param>
        /// <returns>Font face name</returns>
        private int[] SearchLastColor(Stack<AttributeList> st_fonts)
        {
            String str = SearchLastParam(st_fonts, "color");
            if (str == null) return null;
            int[] res = null;
            if (str[0] == '#')
                res = OverallTools.FonctionUtiles.fromHexa(str.Substring(1));
            else
            {
                System.Drawing.Color clr = System.Drawing.Color.FromName(str);
                res = new int[3];
                res[0] = clr.R;
                res[1] = clr.G;
                res[2] = clr.B;
            }
            return res;
        }
        /// <summary>
        /// Write the text found in the html according to the setting of the TextPart.
        /// </summary>
        /// <param name="paragraph">List of text part.</param>
        /// <param name="align">Alignement of the paragraph.</param>
        private void WriteHtmlText(List<TextPart> paragraph, String align)
        {
            if (paragraph.Count == 0) return;
            double textSpace = pageSize[0] - margin[2] - margin[3];
            String[] cutText;
            bool finish = false;
            double leftSpace = 0.0;
            char[] trimChar = new char[1] { ' ' };
            List<TextLine> textLine = new List<TextLine>();
            List<double> textSizes = new List<double>();
            bool endLine = false;
            TextLine textForNextLine = null;
            double sizeForNextLine = 0.0;

            /// Foreach text part:
            foreach (TextPart textPart in paragraph)
            {
                String fontName = htmlFont2pdfFont(textPart.Font);
                int fontSize = textPart.Size;
                if (textPart.IsBRTag)
                {
                    // finalise writting of the previous line
                    if (textLine.Count > 0)
                        writeLine(textLine, textSizes, align); 
                    else
                        CRLF(GetModifiedFont(fontSize), 1, false);
                    // raz and load the text saved for next line
                    textLine = new List<TextLine>();
                    textSizes = new List<double>(); if (textForNextLine != null)
                    {
                        textLine.Add(textForNextLine);
                        textSizes.Add(sizeForNextLine);
                        textForNextLine = null;
                    }
                    
                    continue;
                }
                int[] color = textPart.Color;
                Font aFont = GetModifiedFont(fontName, fontSize, textPart.Bold, textPart.Italic);
                cutText = new String[2] { "", textPart.Text };
                ///    - try to build text lines untill the end of the text part: <br />
                while (!finish) 
                {
                    leftSpace = pageSize[0] - margin[3] - writtingPosition[0];
                    double text_Width = aFont.StringWidth(cutText[1]);//.TrimEnd(trimChar));
                    if (text_Width < leftSpace) 
                    {
                        // if there is enougth space for the rest of the text then save it
                        TextLine txt = new TextLine(aFont, cutText[1]);
                        txt.SetStrikeLine(textPart.StrikeThrough);
                        txt.SetUnderline(textPart.Underline);
                        if (textPart.Link != null)
                        {
                            txt.SetURIAction(textPart.Link);
                            txt.SetColor(blueLink);
                            txt.SetUnderline(true);
                        }
                        if (color != null)
                            txt.SetColor(color);
                        txt.SetPosition(writtingPosition[0], writtingPosition[1]);
                        //txt.DrawOn(currentPage);
                        textLine.Add(txt);
                        textSizes.Add(aFont.getSize());
                        writtingPosition[0] += text_Width;
                        if (paragraph[paragraph.Count-1] == textPart) // if it's the last part
                            endLine = true; // to write the saved line
                        finish = true; // to process the next textPart
                    }
                    else 
                    {
                        // if not, cut the text
                        cutText = GetFirstSplitedText(cutText[1], aFont, leftSpace);
                        // and try write the first part
                        text_Width = aFont.StringWidth(cutText[0]);//.TrimEnd(trimChar));
                        if (text_Width > 0)
                        {
                            // if there is enougth space then write it
                            TextLine txt = new TextLine(aFont, cutText[0]);
                            txt.SetStrikeLine(textPart.StrikeThrough);
                            txt.SetUnderline(textPart.Underline); 
                            if (textPart.Link != null)
                            {
                                txt.SetURIAction(textPart.Link);
                                txt.SetColor(blueLink);
                                txt.SetUnderline(true);
                            }
                            if (color != null)
                                txt.SetColor(color);
                            txt.SetPosition(writtingPosition[0], writtingPosition[1]);
                            textLine.Add(txt);
                            textSizes.Add(aFont.getSize());
                            writtingPosition[0] += text_Width;
                        }
                        else
                        {
                            // else save it for the next line
                            endLine = true;
                            String trimedText = cutText[0].TrimStart(trimChar);
                            textForNextLine = new TextLine(aFont, trimedText);
                            textForNextLine.SetStrikeLine(textPart.StrikeThrough);
                            textForNextLine.SetUnderline(textPart.Underline);
                            if (textPart.Link != null)
                            {
                                textForNextLine.SetURIAction(textPart.Link);
                                textForNextLine.SetColor(blueLink);
                                textForNextLine.SetUnderline(true);
                            }
                            if (color != null)
                                textForNextLine.SetColor(color);
                            textForNextLine.SetPosition(writtingPosition[0], writtingPosition[1]);
                            sizeForNextLine = aFont.getSize();
                            writtingPosition[0] += aFont.StringWidth(trimedText);
                        }
                    }
                    if (endLine) // write the saved TextLine
                    {
                        endLine = false;
                        writeLine(textLine, textSizes, align); // finalise the writting of the previous line
                        // raz and load the text saved for next line
                        textLine = new List<TextLine>();
                        textSizes = new List<double>();
                        if (textForNextLine != null)
                        {
                            textLine.Add(textForNextLine);
                            textSizes.Add(sizeForNextLine);
                            textForNextLine = null;
                        }
                    }
                }
                finish = false;
            }

        }

        /// <summary>
        /// Write a text line with the right line space.
        /// </summary>
        /// <param name="line">list of the text to write</param>
        /// <param name="textSize">list of the text size</param>
        /// <param name="align">Alignment of the text (left right or justify)</param>
        private void writeLine(List<TextLine> line, List<double> textSize, String align)
        {
            double maxSize = 0.0;
            bool center = align == "center";
            bool right = align == "right";
            double shift = 0.0;
            foreach (double d in textSize)
                maxSize = d > maxSize ? d : maxSize;
            CRLF(maxSize, 1, false);
            if (center || right)
            { 
                // calculate text shifting
                int index = line.Count-1;
                TextLine txt = line[index];
                Font f = txt.GetFont();
                f.SetSize(textSize[index]);
                double lastTextwidth = f.StringWidth(txt.GetText());
                double lastTextPos = txt.GetPosition()[0];
                if (center)
                    shift = (pageSize[0] - (lastTextPos + lastTextwidth + margin[3])) / 2;
                if (right)
                    shift = (pageSize[0] - (lastTextPos + lastTextwidth + margin[3]));
            }
            for (int i =0; i<line.Count ; i++)
            {
                TextLine txt = line[i];
                Font aFont = txt.GetFont();
                aFont.SetSize(textSize[i]);
                double[] pos = txt.GetPosition();
                if (center || right) // set the x position
                    pos[0] += shift;
                // set the y position
                txt.SetPosition(pos[0], writtingPosition[1]);
                txt.DrawOn(currentPage);
            }
        }
        /// <summary>
        /// Return the pdf equivalant for an html font.
        /// </summary>
        /// <param name="htmlFontName">html font name</param>
        /// <returns></returns>
        private String htmlFont2pdfFont(String htmlFontName)
        {
            String pdfFontName = "Helvetica"; // default font
            if (htmlFontName == "Courier")
                return htmlFontName;
            if (htmlFontName == "Times New Roman")
                return "Times-Roman";
            return pdfFontName;
        }
        /// <summary>
        /// Read all the information contained in the html table
        /// and launch the function to draw it(DrawHtmlTable).
        /// </summary>
        private void ParseHtmlTable()
        {
            String captiontText = ""; // table title
            bool isCaptionText = false;
            String text = "";
            //bool isCellText = false;
            Dictionary<String, AttributeList> images = null; // key: cell position; value: image tag
            List<int[]> headerCell = new List<int[]>(); // remenber wich cell is between th tag
            int columnIndex = 0;
            int rowIndex = 0;
            List<List<Cell>> table = new List<List<Cell>>();
            List<Cell> row = null;

            while (!parse.Eof())
            {
                char ch = parse.Parse();
                if (ch == 0)
                {
                    AttributeList tag = parse.GetTag();
                    if (tag.Name == "CAPTION") // table title
                    {
                        isCaptionText = true;
                    }
                    if (tag.Name == "/CAPTION") // end table title
                    {
                        isCaptionText = false;
                        captiontText = "";
                    }
                    if (tag.Name == "TR") // table row
                    {
                        row = new List<Cell>();
                    }
                    if (tag.Name == "/TR") // end table row
                    {
                        if (row != null && row.Count > 0)
                        {
                            table.Add(row);
                            rowIndex++;
                        }
                        columnIndex = 0;
                    }
                    if (tag.Name == "TH") // table head cell
                    {
                        //isCellText = true;
                    }
                    if (tag.Name == "/TH") // end table head cell
                    {
                        Cell cell = new Cell(GetCurrentFont(), text);//new Cell(fonts[htmlFonts[htmlFonts.Count -1]], text);
                        row.Add(cell);
                        //isCellText = false;
                        text = "";
                        headerCell.Add(new int[2] {columnIndex, rowIndex});
                        columnIndex++;
                    }
                    if (tag.Name == "TD") // table data cell
                    {
                        //isCellText = true;
                    }
                    if (tag.Name == "/TD") // end table data cell
                    {
                        Cell cell = new Cell(GetCurrentFont(), text);
                        if (tag["colspan"] != null)
                        {
                            int colspan = 0;
                            if (int.TryParse(tag["colspan"].Value, out colspan))
                                cell.setColspan(colspan);
                        }
                        row.Add(cell);
                        //isCellText = false;
                        text = "";
                        columnIndex++;
                    }
                    if (tag.Name == "/TABLE") // end table
                    {
                        DrawHtmlTable(table, images);
                        table = new List<List<Cell>>();
                    }
                    if (tag.Name == "FONT") // change selected font
                    {
                        double nbr = 12.0;
                        if (tag["size"] != null)
                            double.TryParse(tag["size"].Value, out nbr);
                        String fontId = tag["face"] + nbr.ToString();
                        //if (fonts.ContainsKey(fontId))
                        //    htmlFonts += fontId;
                    }
                    if (tag.Name == "IMG") // change selected font
                    {
                        if (images == null)
                            images = new Dictionary<String, AttributeList>();
                        images.Add(
                            columnIndex + "_" + rowIndex,
                            tag );
                    }
                }
                else
                {
                    if (ch != '\t' && ch != '\n')
                    {
                        if (isCaptionText)
                        {
                            captiontText += ch.ToString();
                            continue;
                        }
                        text += ch.ToString();
                    }
                }
            }
        }
        private void DrawHtmlTable(List<List<Cell>> table, Dictionary<String, AttributeList> images)
        {
            AppendMissingCells(table);
            Table pdfTable = new Table(GetCurrentFont(), GetCurrentFont());
            pdfTable.SetData(table);
            pdfTable.AutoAdjustColumnWidths();
            pdfTable.SetPosition(writtingPosition[0], writtingPosition[1]);
            if (images != null && images.Count > 0)
            {
                AttributeList img = images[0.ToString() + "_" + 0.ToString()];
                Image imgPDF = ImagesTable[img["src"].Value] as Image;

                // get the final image scale
                double width = 0.0;
                double height = 0.0 ;
                if (img["width"] == null)
                    width = imgPDF.GetWidth();
                else
                    double.TryParse(img["width"].Value, out width);
                if (img["height"] == null)
                    height = imgPDF.GetHeight();
                else
                    double.TryParse(img["height"].Value, out height);
                
                // set the cell size
                pdfTable.SetColumnWidth(0, width);
                //pdfTable.
                foreach (Cell cell in pdfTable.GetRow(0))
                    cell.SetHeight(height);
            }
            while (true)
            {
                pdfTable.DrawOn(currentPage);
                if (!pdfTable.HasMoreData()) break;
                AddPage();
            }
        }
        /// <summary>
        /// Open a text file and return the inner text.
        /// </summary>
        /// <param name="path">Path of the file to open.</param>
        /// <returns>Content of the file.</returns>
        private String OpenHTML(string path)
        {
            if (!System.IO.File.Exists(path))
                return null;
            StreamReader sr_html = new StreamReader(path);
            String line;
            StringBuilder text = new StringBuilder();
            while ((line = sr_html.ReadLine()) != null)
            {
                text.Append(line);
            }
            sr_html.Close();
            return text.ToString();
        }
        public void LoadhtmlIMG(String htmlPath)
        {
            //String htmlContent = OpenHTML(htmlPath);
            String htmlContent = htmlPath;
            if (htmlContent == null) return;
            ParseHTML.ParseHTML parse = new ParseHTML.ParseHTML();
            parse.Source = htmlContent;
            //parse.Source = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\"><HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=utf-8\"><META content=\"MSHTML 6.00.6000.17093\" name=GENERATOR></HEAD><BODY><P><FONT size=7><IMG height=133 alt=\"\" hspace=0 src=\"C:\\Documents and Settings\\Contrat Pro\\Mes documents\\Mes images\\Commentaire.jpg\" width=160 align=baseline border=0>&nbsp;</FONT><FONT size=3>Voici Une belle BUlle Je peux aussi vous montrer une autre bulle<IMG style=\"WIDTH: 154px; HEIGHT: 127px\" height=236 alt=\"\" hspace=0 src=\"C:\\Documents and Settings\\Contrat Pro\\Mes documents\\Mes images\\commentaire.GIF\" width=335 align=baseline border=0></FONT></P></BODY></HTML>";
            while (!parse.Eof())
            {
                char ch = parse.Parse();
                if (ch == 0)
                {
                    AttributeList tag = parse.GetTag();
                    if (tag.Name.ToUpper() == "IMG")
                    {
                        String path = tag["src"].Value;
                        if (path.Substring(0, 4).Equals("file"))
                            path = path.Substring(8); // remove the "file:///" at the beginning of html link
                        path = path.Replace("%20", " ");
                        LaodImage(path, path);
                    }
                }
            }
        }
        /// <summary>
        /// Add cells when a line cell number doesn't match the column number.
        /// </summary>
        /// <param name="tableData"></param>
        private void AppendMissingCells(List<List<Cell>> tableData)
        {
            List<Cell> firstRow = tableData[0];
            int numOfColumns = firstRow.Count;
            for (int i = 1; i < tableData.Count; i++)
                numOfColumns = tableData[i].Count > numOfColumns ? tableData[i].Count : numOfColumns;
            for (int i = 0; i < tableData.Count; i++)
            {
                List<Cell> dataRow = tableData[i];
                int dataRowColumns = dataRow.Count;
                if (dataRowColumns < numOfColumns)
                {
                    for (int j = 0; j < (numOfColumns - dataRowColumns); j++)
                    {
                        dataRow.Add(new Cell(GetCurrentFont()));
                    }
                    dataRow[dataRowColumns - 1].setColspan(
                            (numOfColumns - dataRowColumns) + 1);
                }
            }
        }
        #endregion HTML


    }

    /// <summary>
    /// Class used to save the table of content element, in order to print the page number later.
    /// </summary>
    class SummaryElement
    {
        private TextLine pageNumberText;
        private Page summaryPage;
        private TextLine title;
        private List<double[]> printPositions = new List<double[]>();
        private List<Page> printPages = new List<Page>();
        private double FontSize;

        public SummaryElement(TextLine title, double[] pageNumberPosition, Page summaryPage, double fontSize)
        {
            Title = title;
            PageNumberText = pageNumberText;
            SummaryPage = summaryPage;
            FontSize = fontSize;

            printPositions.Add(pageNumberPosition);
            printPages.Add(summaryPage);
        }

        /// <summary>
        /// Funtion to draw the page number in the table of contents.
        /// </summary>
        public void DrawPageNumber(int pageNumber, PDFGenerator pdf)
        {
            for (int i=0 ; i<printPositions.Count ; i++)
            {
                TextLine numb = new TextLine(pdf.GetCurrentFont(), pageNumber.ToString());
                numb.SetPosition(printPositions[i][0], printPositions[i][1]);
                numb.DrawOn(printPages[i]);
            }
        }

        /// <summary>
        /// Function to add a place where the page number will be print.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="position"></param>
        public void AddPrintPosition(Page page, double[] position)
        {
            printPositions.Add(position);
            printPages.Add(page);
        }
        #region accesseur
        public TextLine PageNumberText
        {
            get { return pageNumberText; }
            set { pageNumberText = value; }
        }

        public Page SummaryPage
        {
            get { return summaryPage; }
            set { summaryPage = value; }
        }

        public TextLine Title
        {
            get { return title; }
            set { title = value; }
        }
        #endregion accesseur
    }

    /// <summary>
    /// Save the text and his format to be written in a paragraph.
    /// </summary>
    class TextPart
    {
        public TextPart(String text, String font, int size, int[] color, String link, bool bold, bool italic, bool underline, bool strikeThrough)
        {
            if (text.Equals(""))
                return;
            this.text = text;
            this.font = font;
            this.size = size;
            this.color = color;
            this.link = link;
            this.bold = bold;
            this.italic = italic;
            this.underline = underline;
            this.strikeThrough = strikeThrough;
            this.brTag = false;
        }

        /// <summary>
        /// Used to add CRLF when found br tag
        /// </summary>
        /// <param name="size">Reference font size</param>
        public TextPart(int FontSize)
        {
            this.text = "";
            this.size = FontSize;
            this.bold = false;
            this.italic = false;
            this.underline = false;
            this.strikeThrough = false;
            this.brTag = true;
        }

        private String text;
        public String Text
        {
            get { return text; }
        }
        private String font;
        public String Font
        {
            get { return font; }
        }
        private int size;
        public int Size
        {
            get { return size; }
        }
        private int[] color;
        public int[] Color
        {
            get { return color; }
        }
        private String link;
        public String Link
        {
            get { return link; }
        }
        private bool bold; 
        public bool Bold
        {
            get { return bold; }
        }
        private bool italic;
        public bool Italic
        {
            get { return italic; }
        }
        private bool underline;
        public bool Underline
        {
            get { return underline; }
        }
        private bool strikeThrough;
        public bool StrikeThrough
        {
            get { return strikeThrough; }
        }
        private bool brTag;
        public bool IsBRTag
        {
            get { return brTag; }
        }
    }
}
