using System;
using System.Windows.Forms;

namespace SimpleCipher
{
    public partial class Calc : Form
    {
        private char[] plainText;
        private char[] encryptedText;
        private string decrypedText = String.Empty;
        private bool mode = false;

        public Calc()
        {
            InitializeComponent();
            label7.Text = plainTextBox.Text.Length.ToString() + " Characters";
            label8.Text = encryptedTextBox.Text.Length.ToString() + " Characters";
            strToArray();
            calculation();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label7.Text = plainTextBox.Text.Length.ToString() + " Characters";
            strToArray();
            calculation();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label8.Text = encryptedTextBox.Text.Length.ToString() + " Characters";
            strToArray();
            calculation();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            calculation();
        }

        private void modeButton_Click(object sender, EventArgs e)
        {
            if (!mode)
            {
                label5.Text = "Text";
                label6.Text = "Encrypted";
                modeButton.Text = "Decryption Mode";
                label9.Text = "Encryption Mode";
                mode = true;
            }
            else
            {
                label5.Text = "Cipher";
                label6.Text = "Decrypted";
                modeButton.Text = "Encryption Mode";
                label9.Text = "Decryption Mode";
                mode = false;
            }
            char[] arrayTemp = plainText;
            string strTemp = cipherRichTextBox.Text;
            plainText = encryptedText;
            encryptedText = arrayTemp;
            cipherRichTextBox.Text = decryptedRichTextBox.Text;
            decryptedRichTextBox.Text = strTemp;
            cryptChar(cipherRichTextBox.Text);
            decryptedRichTextBox.Text = decrypedText;
            calculation();
        }

        private void strToArray()
        {
            if (plainTextBox.Text.Length == encryptedTextBox.Text.Length)
            {
                toolStripStatusLabel1.Text = "Calculation ready!";
                cipherRichTextBox.Enabled = true;
                modeButton.Enabled = true;
                plainText = plainTextBox.Text.ToString().ToCharArray();
                encryptedText = encryptedTextBox.Text.ToString().ToCharArray();
            }
            else
            {
                toolStripStatusLabel1.Text = "Calculation not ready. Characters lengths don't match.";
                cipherRichTextBox.Enabled = false;
                modeButton.Enabled = false;
            }
        }

        private void calculation()
        {
            cryptChar(cipherRichTextBox.Text);
            decryptedRichTextBox.Text = decrypedText;
        }

        private void cryptChar(string text)
        {
            decrypedText = String.Empty;
            foreach (char cipherChar in text)
            {
                if (cipherChar.ToString() == "\n")
                {
                    decrypedText += "\n";
                }
                else
                {
                    int cipherIndex = Array.IndexOf(encryptedText, cipherChar);
                    if (cipherIndex >= 0)
                    {
                        try
                        {
                            decrypedText += plainText.GetValue(cipherIndex);
                        }
                        catch
                        {
                            MessageBox.Show("An unavoidable exception occurred.", "SimpleCipherCalc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }
                    }
                    else
                    {
                        decrypedText += " ";
                    }
                }
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version: 1.1.3.0\nMIT License\nCopyright (C) Tsubasa FUJII", "SimpleCipherCalc", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
