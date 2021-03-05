using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIMCORE_TOOL.com.crispico.generalUse
{
    class FormUtils
    {
        private const char DOT_CHAR = (char)46;
        private const char BACKSPACE_CHAR = (char)8;

        public static void restrictNumbersWithDecimalsOnTextBox(object sender, KeyPressEventArgs e)
        {
            if (sender.GetType() != typeof(TextBox))
                return;

            TextBox senderTextBox = (TextBox)sender;
            char ch = e.KeyChar;

            if (ch == DOT_CHAR && (senderTextBox.Text.IndexOf('.') != -1 || senderTextBox.Text.Length == 0))
            {
                e.Handled = true;
                return;
            }
            if (!Char.IsDigit(ch) && ch != BACKSPACE_CHAR && ch != DOT_CHAR)
            {
                e.Handled = true;
                return;
            }
        }
    }
}
