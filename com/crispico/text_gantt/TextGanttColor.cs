using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using SIMCORE_TOOL.Prompt.Liege;
using SIMCORE_TOOL.com.crispico.adapters;
using System.Data;
using SIMCORE_TOOL.DataManagement;

namespace SIMCORE_TOOL.com.crispico.text_gantt
{
    class TextGanttColor
    {        
        private const string NO_SELECTION_DATA = "NoSelection";
        private static readonly Color NO_SELECTION_COLOR = Color.White;

        private TextGanttColorParameters parameters;
        
        private List<Flight> fpiFlights;

        private GestionDonneesHUB2SIM donnees;
                
        private Dictionary<string, Color> colorsByValues;

        public TextGanttColor(TextGanttColorParameters parameters, List<Flight> fpiFlights, GestionDonneesHUB2SIM donnees)
        {
            this.donnees = donnees;
            this.parameters = parameters;            
            this.fpiFlights = fpiFlights;            

            updateColorsByValues();
        }

        private void updateColorsByValues()
        {
            colorsByValues = new Dictionary<string, Color>();
            if (parameters.getByOverlap())
                updateColorsByValuesUsingOverlapSynchronizedWithAircraftMovements();   //updateColorsByValuesUsingOverlap();
            else if (parameters.getByDefaultColor())
                updateColorsByValuesUsingDefaultSettings();
            else
                updateColorsByValuesUsingFlightAttributes();
        }

        private void updateColorsByValuesUsingOverlap()
        {
            colorsByValues.Add(NO_SELECTION_DATA, NO_SELECTION_COLOR);
            colorsByValues.Add("3", Color.LightGreen);  //Green, LightGreen, Orange, Red. => check the Allocation's text gantt - synchronize the colors and limit values
            colorsByValues.Add("7", Color.LightYellow);
            colorsByValues.Add(int.MaxValue.ToString(), Color.Red);
        }

        private void updateColorsByValuesUsingOverlapSynchronizedWithAircraftMovements()
        {
            colorsByValues.Add(NO_SELECTION_DATA, NO_SELECTION_COLOR);
            colorsByValues.Add("1", Color.FromArgb(0, 255, 0));
            colorsByValues.Add("2", Color.FromArgb(192, 255, 0));
            colorsByValues.Add("3", Color.FromArgb(255, 255, 0));
            colorsByValues.Add("4", Color.FromArgb(255, 128, 0));
            colorsByValues.Add(int.MaxValue.ToString(), Color.FromArgb(255, 0, 0));
        }

        private void updateColorsByValuesUsingDefaultSettings()
        {
            colorsByValues.Add(NO_SELECTION_DATA, NO_SELECTION_COLOR);
        }

        private void updateColorsByValuesUsingFlightAttributes()
        {
            string baseInputTableName = string.Empty;
            string baseInputColumnName = string.Empty;
            List<string> baseInputValues = new List<string>();
            if (parameters.getByAirline())
            {
                baseInputTableName = GlobalNames.FP_AirlineCodesTableName;
                baseInputColumnName = GlobalNames.sFPAirline_AirlineCode;
            }
            else if (parameters.getByFlightCategory())
            {
                baseInputTableName = GlobalNames.FP_FlightCategoriesTableName;
                baseInputColumnName = GlobalNames.sFPFlightCategory_FC;
            }
            else if (parameters.getByGroundHandler())
            {
                baseInputTableName = GlobalNames.FP_AirlineCodesTableName;
                baseInputColumnName = GlobalNames.sFPAirline_GroundHandlers;
            }
            if (baseInputTableName == string.Empty || baseInputColumnName == string.Empty)
                return;
            DataTable baseInputTable = donnees.getTable("Input", baseInputTableName);
            if (baseInputTable == null)
                return;
            int valueColumnIndex = baseInputTable.Columns.IndexOf(baseInputColumnName);
            if (valueColumnIndex == -1)
                return;
            List<string> inputValues = new List<string>();
            foreach (DataRow row in baseInputTable.Rows)
            {
                if (row[valueColumnIndex] != null && !inputValues.Contains(row[valueColumnIndex].ToString()))
                    inputValues.Add(row[valueColumnIndex].ToString());
            }
            inputValues.Sort();
            for (int i = 0; i < inputValues.Count; i++)            
                colorsByValues.Add(inputValues[i], OverallTools.FonctionUtiles.getColor(i + 1));
        }

        public void colorDataGrid(DataGridView datagrid)
        {
            if (datagrid == null || datagrid.Rows == null || datagrid.Rows.Count == 0)
                return;
            datagrid.ClearSelection();
            
            Dictionary<string, Color> colorsByValuesInUse = new Dictionary<string, Color>();
            colorsByValuesInUse.Add(NO_SELECTION_DATA, NO_SELECTION_COLOR);

            for (int rowIndex = 0; rowIndex < datagrid.Rows.Count; rowIndex++)
            {
                for (int cellIndex = 0; cellIndex < datagrid.Rows[rowIndex].Cells.Count; cellIndex++)
                {
                    if (datagrid.Rows[rowIndex].Cells[cellIndex].Value == null)
                        continue;
                    string cellContent = datagrid.Rows[rowIndex].Cells[cellIndex].Value.ToString();
                    Color cellBackgroundColor = getColorByDatagridCellContent(cellContent);
                    datagrid.Rows[rowIndex].Cells[cellIndex].Style.BackColor = cellBackgroundColor;
                    
                    //rewrite this!!!
                    if (cellBackgroundColor != NO_SELECTION_COLOR && colorsByValues.ContainsKey(getColorKeyByCellContent(cellContent)) && !colorsByValuesInUse.ContainsKey(getColorKeyByCellContent(cellContent)))
                        colorsByValuesInUse.Add(getColorKeyByCellContent(cellContent), colorsByValues[getColorKeyByCellContent(cellContent)]);                    
                }
            }
            updateSourceTableLegend(datagrid, colorsByValuesInUse);
            datagrid.Refresh();            
        }

        private Color getColorByDatagridCellContent(string cellContent)
        {
            string colorKey = getColorKeyByCellContent(cellContent);
            if (!colorsByValues.ContainsKey(colorKey))
                return NO_SELECTION_COLOR;
            return colorsByValues[colorKey];
        }

        private string getColorKeyByCellContent(string cellContent)
        {
            string colorKey = NO_SELECTION_DATA;
            if (parameters.getByOverlap())
                colorKey = getColorKeyFromDatagridCellContentForOverlap(cellContent);
            else if (parameters.getByDefaultColor())
                colorKey = getColorKeyForDefaultColor();
            else
                colorKey = getColorKeyFromDatagridCellContentForFlightAttribute(cellContent);
            return colorKey;
        }

        private string getColorKeyFromDatagridCellContentForOverlap(string cellContent)
        {
            int flightId = FlightPlanInformationToTextGantt.getFlightIdFromFlightTextInGantt(cellContent);
            string flightNb = FlightPlanInformationToTextGantt.getFlightNbFromFlightTextInGantt(cellContent);
            if (flightId == FlightPlanInformationToTextGantt.NO_ID || flightNb == string.Empty)
                return NO_SELECTION_DATA;
            string[] contents = cellContent.Split(',');
            int nbFlights = contents.Length;
            List<string> colorKeys = colorsByValues.Keys.ToList();
            List<int> colorsKeysAsIntegers = new List<int>();
            foreach (string key in colorKeys)
            {
                int keyAsInteger = -1;
                if (Int32.TryParse(key, out keyAsInteger))
                    colorsKeysAsIntegers.Add(keyAsInteger);
            }
            colorsKeysAsIntegers.Sort();
            for (int i = 0; i < colorsKeysAsIntegers.Count; i++)
            {
                if (nbFlights <= colorsKeysAsIntegers[i])
                    return colorsKeysAsIntegers[i].ToString();
            }
            return NO_SELECTION_DATA;
        }

        private string getColorKeyForDefaultColor()
        {
            return NO_SELECTION_DATA;
        }

        private string getColorKeyFromDatagridCellContentForFlightAttribute(string cellContent)
        {
            int flightId = FlightPlanInformationToTextGantt.getFlightIdFromFlightTextInGantt(cellContent);
            string flightNb = FlightPlanInformationToTextGantt.getFlightNbFromFlightTextInGantt(cellContent);
            if (flightId == FlightPlanInformationToTextGantt.NO_ID || flightNb == string.Empty)
                return NO_SELECTION_DATA;
            Flight flight = getFlightFromFpiFlightsByIdAndFlightNb(flightId, flightNb);
            if (flight == null)
                return NO_SELECTION_DATA;
            if (parameters.getByAirline() && flight.airlineCode != null)
                return flight.airlineCode;
            if (parameters.getByFlightCategory() && flight.flightCategory != null)
                return flight.flightCategory;
            if (parameters.getByGroundHandler() && flight.specificGroundHandler != null)
                return flight.specificGroundHandler;
            return NO_SELECTION_DATA;
        }

        private Flight getFlightFromFpiFlightsByIdAndFlightNb(int id, string flightNb)
        {
            Flight flight = null;
            if (fpiFlights == null)
                return flight;
            foreach (Flight f in fpiFlights)
            {
                if (f.id == id && f.flightNumber == flightNb)
                {
                    flight = f;
                    break;
                }
            }
            return flight;
        }

        private void updateSourceTableLegend(DataGridView datagrid, Dictionary<string, Color> colorsByValuesInUse)
        {
            if (datagrid == null || datagrid.DataSource == null || datagrid.DataSource.GetType() != typeof(DataTable))
                return;
            DataTable sourceTextGanttTable = (DataTable)datagrid.DataSource;            
            int legendColumnIndex = sourceTextGanttTable.Columns.IndexOf(FlightPlanInformationToTextGantt.TEXT_GANTT_COLUMN_NAME_LEGEND);
            if (legendColumnIndex == -1)
                return;
            
            for (int i = 0; i < datagrid.Rows.Count; i++)
                datagrid.Rows[i].Cells[legendColumnIndex].Value = string.Empty;

            if (sourceTextGanttTable.Rows.Count < colorsByValuesInUse.Count)
                return;

            for (int i = 0; i < colorsByValuesInUse.Count; i++)
            {
                string colorKey = colorsByValuesInUse.Keys.ToList()[i];
                datagrid.Rows[i].Cells[legendColumnIndex].Value = getColorKeyDisplayValue(colorKey);

                Color color = colorsByValuesInUse[colorKey];                
                datagrid.Rows[i].Cells[legendColumnIndex].Style.BackColor = color;
            }
        }

        private string getColorKeyDisplayValue(string colorKey)
        {
            string displayValue = colorKey;
            if (parameters.getByOverlap() && !colorKey.Equals(NO_SELECTION_DATA))
            {
                int colorKeyAsInteger = -1;
                if (Int32.TryParse(colorKey, out colorKeyAsInteger) && Int32.MaxValue == colorKeyAsInteger)
                    displayValue = "Higher values";
                else
                    displayValue = "<= " + colorKey;                
            }
            return displayValue;
        }

    }
}
