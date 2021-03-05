using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIMCORE_TOOL.com.crispico.general_allocation.constants;
using SIMCORE_TOOL.com.crispico.general_allocation.parameter;
using SIMCORE_TOOL.Prompt.Allocation.General;

namespace SIMCORE_TOOL.com.crispico.general_allocation.assistant
{
    public partial class FlightGroupingRulesAssistant : Form
    {
        private Dictionary<GeneralAllocationConstants.P2S_ATTRIBUTES, List<string>> p2sAttributesAndValues;
        
        private int groupBoxId = 0;
        /// <summary>
        /// K = group box id as string
        /// V = list of values selected by using the multi-selection assistant
        /// </summary>
        private Dictionary<string, List<string>> ruleConditionMultiInputValues = new Dictionary<string, List<string>>();

        internal FlightGroupingRule resultedFlightGroupingRule;

        internal FlightGroupingRulesAssistant(Dictionary<GeneralAllocationConstants.P2S_ATTRIBUTES, List<string>> pP2sAttributesAndValues,
            FlightGroupingRule pInitialFlightGroupingRule)
        {
            InitializeComponent();
            OverallTools.FonctionUtiles.MajBackground(this);

            p2sAttributesAndValues = pP2sAttributesAndValues;
                        
            fillP2sAttributesCombobox();
            if (pInitialFlightGroupingRule != null)
            {
                updateAssistantWithInitialFlightGroupRule(pInitialFlightGroupingRule);
            }
        }

        private void fillP2sAttributesCombobox()
        {
            p2sAttributesComboBox.Items.Clear();
            foreach (GeneralAllocationConstants.P2S_ATTRIBUTES p2sAttribute in Enum.GetValues(typeof(GeneralAllocationConstants.P2S_ATTRIBUTES)))
            {
                if (p2sAttribute == GeneralAllocationConstants.P2S_ATTRIBUTES.DEFAULT)
                {
                    continue;
                }
                p2sAttributesComboBox.Items.Add(p2sAttribute.getDescription());
            }
        }

        private void p2sAttributesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (p2sAttributesComboBox.SelectedItem == null)
            {
                return;
            }
            string p2sAttributeDescription = p2sAttributesComboBox.SelectedItem.ToString();
            GeneralAllocationConstants.P2S_ATTRIBUTES selectedP2sAttribute
                = GeneralAllocationConstants.getValueFromDescription<GeneralAllocationConstants.P2S_ATTRIBUTES>(p2sAttributeDescription);
            if (p2sAttributesAndValues.ContainsKey(selectedP2sAttribute))
            {
                List<string> flightPlanValues = p2sAttributesAndValues[selectedP2sAttribute];
                string fullText = "";
                foreach (string value in flightPlanValues)
                {
                    fullText += value + Environment.NewLine;
                }
                previewTextBox.Text = fullText;
            }
        }

        private void updateAssistantWithInitialFlightGroupRule(FlightGroupingRule initialFlightGroupingRule)
        {
            ruleNameTextBox.Text = initialFlightGroupingRule.name;
            ruleDescriptionTextBox.Text = initialFlightGroupingRule.description;
            createGroupBoxesBasedOnInitialFlightGroupingRuleConditions(initialFlightGroupingRule.ruleConditions);
        }

        private void createGroupBoxesBasedOnInitialFlightGroupingRuleConditions(List<FlightGroupingRuleCondition> initialFlightGroupingRuleConditions)
        {            
            foreach (FlightGroupingRuleCondition condition in initialFlightGroupingRuleConditions)
            {
                GroupBox gb = createGroupBoxAndAddToPanel(ruleConditionsPanel);
                fillGroupBoxByP2sAttribute(gb, condition.p2sAttribute);
                updateGroupBoxeByFlightGroupingCondition(gb, condition);
            }
        }

        private void updateGroupBoxeByFlightGroupingCondition(GroupBox gb, FlightGroupingRuleCondition condition)
        {
            //get the operator cb
            ComboBox operatorCb = getOperatorComboBoxFromPanel(gb);
            if (operatorCb != null && operatorCb.Items.Contains(condition.conditionOperator.getDescription()))
            {
                operatorCb.SelectedItem = condition.conditionOperator.getDescription();
            }
            //get the input control based on the operator type (or on the input control visibility)
            if (condition.conditionOperator == GeneralAllocationConstants.OPERATORS.EQUAL_TO)
            {
                if (ruleConditionMultiInputValues.ContainsKey(gb.Tag.ToString()))
                {
                    ruleConditionMultiInputValues[gb.Tag.ToString()] = condition.inputValues;
                }
                else
                {
                    ruleConditionMultiInputValues.Add(gb.Tag.ToString(), condition.inputValues);
                }
            }
            else
            {
                Control singleInputControl = getSingleInputControlFromPanel(gb);
                if (singleInputControl != null && singleInputControl.GetType() == typeof(TextBox)
                    && condition.inputValues.Count == 1)
                {
                    ((TextBox)singleInputControl).Text = condition.inputValues[0];
                }
            }
        }

        private const int groupBoxLocationX = 3;
        private const int initialGroupBoxLocationY = 3;
        private const int groupBoxHeight = 60;
        private const int groupBoxWidth = 500;
        private const int spaceBetweenGroupBoxes = 2;
        private void addP2sAttributeButton_Click(object sender, EventArgs e)
        {
            if (p2sAttributesComboBox.SelectedItem == null)
            {
                return;
            }
            string p2sAttributeDescription = p2sAttributesComboBox.SelectedItem.ToString();
            GeneralAllocationConstants.P2S_ATTRIBUTES selectedP2sAttribute
                = GeneralAllocationConstants.getValueFromDescription<GeneralAllocationConstants.P2S_ATTRIBUTES>(p2sAttributeDescription);
            GroupBox gb = createGroupBoxAndAddToPanel(ruleConditionsPanel);
            fillGroupBoxByP2sAttribute(gb, selectedP2sAttribute);
        }

        private GroupBox createGroupBoxAndAddToPanel(Panel parentPanel)
        {
            GroupBox gb = new GroupBox();
            gb.Text = "";
            gb.Size = new Size(groupBoxWidth, groupBoxHeight);
            
            gb.Tag = groupBoxId;
            groupBoxId++;
                        
            int nbOfExistingGroupBoxes = getNbOfGroupBoxesInPanel(parentPanel);                        
            int locationY = calculateGroupBoxLocationY(nbOfExistingGroupBoxes, parentPanel);            
            gb.Location = new Point(groupBoxLocationX, locationY);

            parentPanel.Controls.Add(gb);
            return gb;
        }

        private int getNbOfGroupBoxesInPanel(Panel panel)
        {
            int nbGroupBoxes = 0;
            if (panel == null)
            {
                return nbGroupBoxes;
            }
            foreach (Control control in panel.Controls)
            {
                if (control.GetType() == typeof(GroupBox))
                {
                    nbGroupBoxes++;
                }
            }
            return nbGroupBoxes;
        }
        
        private void fillGroupBoxByP2sAttribute(GroupBox parentGroupBox, GeneralAllocationConstants.P2S_ATTRIBUTES p2sAttribute)
        {
            createAndAddLabel(parentGroupBox, p2sAttribute.getDescription());            
            
            //first add the input controls because the operator switches between them depending on the operator type
            createAndAddInputButtonBox(parentGroupBox, p2sAttribute);
            createAndAddInputTextBox(parentGroupBox);
            createAndAddOperatorComboBox(parentGroupBox);

            createAndAddRemoveButton(parentGroupBox);            
        }

        private const int labelLocationX = 17;
        private const int labelLocationY = 26;
        private const int labelHeight = 13;
        private const int labelWidth = 77;
        private const string P2S_ATTRIBUTE_DESCRIPTION_TAG = "P2SAttributeDescription";
        private void createAndAddLabel(GroupBox parentGroupBox, string p2sAttributeDescription)
        {
            Label label = new Label();
            label.Text = p2sAttributeDescription;
            label.Size = new Size(labelWidth, labelHeight);
            label.Location = new Point(labelLocationX, labelLocationY);
            label.Tag = P2S_ATTRIBUTE_DESCRIPTION_TAG;
            parentGroupBox.Controls.Add(label);
        }

        private const int operatorCbLocationX = 109;
        private const int operatorCbLocationY = 23;
        private const int operatorCbHeight = 21;
        private const int operatorCbWidth = 102;
        private const string OPERATOR_TAG = "Operator";
        private void createAndAddOperatorComboBox(GroupBox parentGroupBox)
        {
            ComboBox operatorCb = new ComboBox();
            parentGroupBox.Controls.Add(operatorCb);
            operatorCb.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (GeneralAllocationConstants.OPERATORS conditionOperator in Enum.GetValues(typeof(GeneralAllocationConstants.OPERATORS)))
            {
                if (conditionOperator == GeneralAllocationConstants.OPERATORS.DEFAULT)
                {
                    continue;
                }
                operatorCb.Items.Add(conditionOperator.getDescription());
            }            
            if (operatorCb.Items.Count > 0)
            {
                operatorCb.SelectedIndex = 0;
            }            
            operatorCb.Size = new Size(operatorCbWidth, operatorCbHeight);
            operatorCb.Location = new Point(operatorCbLocationX, operatorCbLocationY);
            operatorCb.SelectedIndexChanged += new EventHandler(operatorCb_SelectedIndexChanged);
            operatorCb.Tag = OPERATOR_TAG;            
        }

        private const int inputControlLocationX = 232;
        private const int inputControlLocationY = 23;
        private const int inputControlHeight = 21;
        private const int inputControlWidth = 169;
        private const string MULTI_INPUT_TAG = "MultiInput";
        private void createAndAddInputButtonBox(GroupBox parentGroupBox, GeneralAllocationConstants.P2S_ATTRIBUTES p2sAttribute)
        {
            Button inputButton = new Button();
            inputButton.Text = "Edit " + p2sAttribute.getDescription() + " values";
            inputButton.Size = new Size(inputControlWidth, inputControlHeight);
            inputButton.Location = new Point(inputControlLocationX, inputControlLocationY);
            inputButton.Tag = MULTI_INPUT_TAG;
            inputButton.Click += new EventHandler(inputButton_Click);
            parentGroupBox.Controls.Add(inputButton);
        }

        void inputButton_Click(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(Button))
            {
                return;
            }
            Button inputButton = (Button)sender;
            if (inputButton.Parent == null || inputButton.Parent.GetType() != typeof(GroupBox))
            {
                return;
            }
            GroupBox parentGroupBox = (GroupBox)inputButton.Parent;
            GeneralAllocationConstants.P2S_ATTRIBUTES p2sAttribute = getP2sAttributeByParentGroupBox(parentGroupBox);
            if (p2sAttributesAndValues.ContainsKey(p2sAttribute))
            {
                List<string> p2sAttributeValues = p2sAttributesAndValues[p2sAttribute];
                List<string> initiallySelectedValues = new List<string>();
                if (parentGroupBox.Tag != null && ruleConditionMultiInputValues.ContainsKey(parentGroupBox.Tag.ToString()))
                {
                    initiallySelectedValues = ruleConditionMultiInputValues[parentGroupBox.Tag.ToString()];
                }

                MultiSelectionPopUp multiSelectionAssistant = new MultiSelectionPopUp(p2sAttributeValues, initiallySelectedValues);
                if (multiSelectionAssistant.ShowDialog() == DialogResult.OK 
                    && parentGroupBox.Tag != null)
                {
                    if (ruleConditionMultiInputValues.ContainsKey(parentGroupBox.Tag.ToString()))
                    {
                        ruleConditionMultiInputValues[parentGroupBox.Tag.ToString()] = multiSelectionAssistant.selectedItems;
                    }
                    else
                    {
                        ruleConditionMultiInputValues.Add(parentGroupBox.Tag.ToString(), multiSelectionAssistant.selectedItems);
                    }
                }
            }
        }

        private const string SINGLE_INPUT_TAG = "SingleInput";
        private void createAndAddInputTextBox(GroupBox parentGroupBox)
        {
            TextBox inputTb = new TextBox();
            inputTb.Size = new Size(inputControlWidth, inputControlHeight);
            inputTb.Location = new Point(inputControlLocationX, inputControlLocationY);
            inputTb.Visible = false;
            inputTb.Tag = SINGLE_INPUT_TAG;
            parentGroupBox.Controls.Add(inputTb);
        }

        private const int removeButtonLocationX = 417;
        private const int removeButtonLocationY = 21;
        private const int removeButtonHeight = 23;
        private const int removeButtonWidth = 75;
        private void createAndAddRemoveButton(GroupBox parentGroupBox)
        {
            Button button = new Button();
            button.Text = "Remove";
            button.Size = new Size(removeButtonWidth, removeButtonHeight);
            button.Location = new Point(removeButtonLocationX, removeButtonLocationY);
            button.Click += new EventHandler(removeButton_Click);
            parentGroupBox.Controls.Add(button);
        }

        void operatorCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(ComboBox))
            {
                return;
            }
            ComboBox operatorCb = (ComboBox)sender;
            if (operatorCb.SelectedItem == null)
            {
                return;
            }
            string selectedOperatorDescription = operatorCb.SelectedItem.ToString();
            GeneralAllocationConstants.OPERATORS selectedOperator
                = GeneralAllocationConstants.getValueFromDescription<GeneralAllocationConstants.OPERATORS>(selectedOperatorDescription);
            Control singleInputControl = getSingleInputControlFromPanel(operatorCb.Parent);
            Control multiInputControl = getMultiInputControlFromPanel(operatorCb.Parent);
            if (singleInputControl == null || multiInputControl == null)
            {
                return;
            }
            multiInputControl.Visible = (selectedOperator == GeneralAllocationConstants.OPERATORS.EQUAL_TO);
            singleInputControl.Visible = (selectedOperator != GeneralAllocationConstants.OPERATORS.EQUAL_TO);
        }

        private Control getSingleInputControlFromPanel(Control parentControl)
        {
            if (parentControl == null)
            {
                return null;
            }
            foreach (Control control in parentControl.Controls)
            {
                if (control.Tag != null && control.Tag.ToString() == SINGLE_INPUT_TAG)
                {
                    return control;
                }
            }
            return null;
        }

        private Control getMultiInputControlFromPanel(Control parentControl)
        {
            if (parentControl == null)
            {
                return null;
            }
            foreach (Control control in parentControl.Controls)
            {
                if (control.Tag != null && control.Tag.ToString() == MULTI_INPUT_TAG)
                {
                    return control;
                }
            }
            return null;
        }

        private TextBox getInputTextBoxFromPanel(Control parentControl)
        {
            if (parentControl == null)
            {
                return null;
            }
            foreach (Control control in parentControl.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    TextBox tb = (TextBox)control;
                    if (tb.Tag != null && tb.Tag.ToString() == SINGLE_INPUT_TAG)
                    {
                        return tb;
                    }
                }
            }
            return null;
        }

        void removeButton_Click(object sender, EventArgs e)
        {
            if (sender == null || sender.GetType() != typeof(Button))
            {
                return;
            }
            Button removeButton = (Button)sender;
            if (removeButton.Parent.GetType() != typeof(GroupBox))
            {
                return;
            }
            GroupBox parentGroupBox = (GroupBox)removeButton.Parent;
            Control parentControl = parentGroupBox.Parent;
            if (parentControl == null || parentControl.GetType() != typeof(Panel))
            {
                return;
            }
            parentControl.Controls.Remove(parentGroupBox);
            //remove the group box id from the rule conditions multi selection dictionary
            if (parentGroupBox.Tag != null && ruleConditionMultiInputValues.ContainsKey(parentGroupBox.Tag.ToString()))
            {
                ruleConditionMultiInputValues.Remove(parentGroupBox.Tag.ToString());
            }
            //recalculate the "Y"s for all the remaining groupboxes (using the formula) starting from  the 1st gb
            Panel parentPanel = (Panel)parentControl;
            for (int i = 0; i < parentControl.Controls.Count; i++)
            {
                if (parentControl.Controls[i].GetType() == typeof(GroupBox))
                {
                    GroupBox childGb = (GroupBox)parentPanel.Controls[i];                    
                    int locationY = calculateGroupBoxLocationY(i, parentPanel);                    
                    childGb.Location = new Point(groupBoxLocationX, locationY);
                }
            }            
        }

        private int calculateGroupBoxLocationY(int nbOfExistingGroupBoxes, Panel parentPanel)
        {
            int verticalScrollPosition = parentPanel.VerticalScroll.Value;
            //Y2 = Y1 + groupBoxHeight + spaceBetweenGroupBoxes // Y3 = Y2 + groupBoxHeight + spaceBetweenGroupBoxes = Y1 + 2 * (groupBoxHeight + spaceBetweenGroupBoxes) ...
            int absoluteLocationY = initialGroupBoxLocationY + nbOfExistingGroupBoxes * (groupBoxHeight + spaceBetweenGroupBoxes);
            return absoluteLocationY - verticalScrollPosition;
        }
                
        private ComboBox getOperatorComboBoxFromPanel(Control parentControl)
        {
            if (parentControl == null)
            {
                return null;
            }
            foreach (Control control in parentControl.Controls)
            {
                if (control.GetType() == typeof(ComboBox))
                {
                    ComboBox cb = (ComboBox)control;
                    if (cb.Tag != null && cb.Tag.ToString() == OPERATOR_TAG)
                    {
                        return cb;
                    }
                }
            }
            return null;
        }

        private GeneralAllocationConstants.P2S_ATTRIBUTES getP2sAttributeByParentGroupBox(GroupBox parentGroupBox)
        {
            foreach (Control control in parentGroupBox.Controls)
            {
                if (control.GetType() != typeof(Label))
                {
                    continue;
                }
                Label label = (Label)control;
                if (label.Tag != null && label.Tag.ToString() != P2S_ATTRIBUTE_DESCRIPTION_TAG)
                {
                    continue;
                }
                return GeneralAllocationConstants.getValueFromDescription<GeneralAllocationConstants.P2S_ATTRIBUTES>(label.Text);
            }
            return GeneralAllocationConstants.P2S_ATTRIBUTES.DEFAULT;
        }

        private GeneralAllocationConstants.OPERATORS getOperatorByParentGroupBox(GroupBox parentGroupBox)
        {
            ComboBox operatorCb = getOperatorComboBoxFromPanel(parentGroupBox);
            if (operatorCb != null && operatorCb.SelectedItem != null)
            {
                return GeneralAllocationConstants.getValueFromDescription<GeneralAllocationConstants.OPERATORS>(operatorCb.SelectedItem.ToString());
            }
            return GeneralAllocationConstants.OPERATORS.DEFAULT;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            List<FlightGroupingRuleCondition> conditions = new List<FlightGroupingRuleCondition>();
            foreach (Control control in ruleConditionsPanel.Controls)
            {
                if (control.GetType() != typeof(GroupBox))
                {
                    continue;
                }
                GroupBox conditionGroupBox = (GroupBox)control;
                GeneralAllocationConstants.P2S_ATTRIBUTES p2sAttribute = getP2sAttributeByParentGroupBox(conditionGroupBox);
                GeneralAllocationConstants.OPERATORS conditionOperator = getOperatorByParentGroupBox(conditionGroupBox);
                List<string> inputValues = new List<string>();
                if (conditionOperator == GeneralAllocationConstants.OPERATORS.EQUAL_TO)
                {
                    if (conditionGroupBox.Tag != null && ruleConditionMultiInputValues.ContainsKey(conditionGroupBox.Tag.ToString()))
                    {
                        inputValues = ruleConditionMultiInputValues[conditionGroupBox.Tag.ToString()];
                    }
                }
                else
                {
                    Control singleInputControl = getSingleInputControlFromPanel(conditionGroupBox);
                    if (singleInputControl != null && singleInputControl.GetType() == typeof(TextBox))
                    {
                        inputValues.Add(((TextBox)singleInputControl).Text);
                    }
                }
                FlightGroupingRuleCondition condition = new FlightGroupingRuleCondition(p2sAttribute, conditionOperator, inputValues);
                conditions.Add(condition);
            }
            resultedFlightGroupingRule = new FlightGroupingRule(ruleNameTextBox.Text, ruleDescriptionTextBox.Text, conditions);
        }
    }
}
