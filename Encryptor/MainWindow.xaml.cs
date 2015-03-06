using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using CipherLib;

namespace Lab1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    
	public partial class Window1 : Window
	{
        string PlainText, CipherText, CipherKey; // clear and chipered text strings

		public Window1()
		{
			this.InitializeComponent();
			// Insert code required on object creation below this point.
		}

        #region Lab1
        private void ButtonAction_Click(object sender, RoutedEventArgs e)
        {
            CipherKey = textBox2.Text.Trim();
            char[,] CodeTable;
            if (radioButton1.IsChecked == true) //encrypt
            {
                PlainText = textBox1.Text.ToLower();                
                ColumnarTransposition ct = new ColumnarTransposition();
                textBox3.Text = ct.Encrypt(PlainText, CipherKey, out CodeTable);
                DataGridFill(CodeTable);
            }
            else // decrypt
            {
                CipherText = textBox1.Text.ToLower();
                ColumnarTransposition ct = new ColumnarTransposition();
                textBox3.Text = ct.Decrypt(CipherText, CipherKey, out CodeTable);
                DataGridFill(CodeTable);
            }
        }

        private void DataGridFill(char[,] CodeTable)
        {
            int RowCount, ColCount;
            DataGridView1.RowCount = RowCount = CodeTable.GetLength(0);
            DataGridView1.ColumnCount = ColCount = CodeTable.GetLength(1);
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
                {
                    DataGridView1.Rows[i].Cells[j].Value = CodeTable[i, j];
                    DataGridView1.Columns[j].HeaderText = CipherKey[j].ToString();
                }
            }
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {            
            label1.Content = "Ciphertext";
            ButtonAction.Content = "Decrypt";
            label3.Content = "Plaintext";            
        }

        private void radioButton2_Unchecked(object sender, RoutedEventArgs e)
        {
            label1.Content = "Plaintext";
            ButtonAction.Content = "Encrypt";
            label3.Content = "Ciphertext";
        }
        #endregion

        #region Lab2
        private void radioButton4_Checked(object sender, RoutedEventArgs e)
        {
            butVigCipher.Content = "Decrypt";
        }

        private void radioButton4_Unchecked(object sender, RoutedEventArgs e)
        {
            butVigCipher.Content = "Encrypt";
        }

        private void butVigCipher_Click(object sender, RoutedEventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        Caesar caesar = new Caesar(Alphabet.All);
                        if (radioButton3.IsChecked == true)
                        {
                            textBox6.Text = caesar.Encrypt(textBox4.Text, Int32.Parse(textBox5.Text));
                        }
                        else
                        {
                            textBox6.Text = caesar.Decrypt(textBox4.Text, Int32.Parse(textBox5.Text));
                        }
                        break;
                    }
                case 1:
                    {
                        Vigenere vigenere = new Vigenere(Alphabet.RusU+Alphabet.EngU);
                        if (radioButton3.IsChecked == true)
                        {
                            textBox6.Text = vigenere.Encrypt(textBox4.Text.ToUpper().Replace(" ", ""), textBox5.Text.ToUpper()).Replace(" ", "");
                        }
                        else
                        {
                            textBox6.Text = vigenere.Decrypt(textBox4.Text.ToUpper().Replace(" ", ""), textBox5.Text.ToUpper().Replace(" ", ""));
                        }
                        break;
                    }
            }
        }
        #endregion

        #region Lab3
        private void L3RadioDecrypt_Checked(object sender, RoutedEventArgs e)
        {
            L3ButtonAction.Content = "Decrypt";
        }

        private void L3RadioDecrypt_Unchecked(object sender, RoutedEventArgs e)
        {
            L3ButtonAction.Content = "Encrypt";
        }

        private void L3ButtonAction_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OpenDlg = CreateOpenDialog("Select codetable", ".txt", "Text documents (.txt)|*.txt");
            if (!OpenDlg.ShowDialog().Value) 
                return;
            switch (L3RadioEncrypt.IsChecked)
            {
                case true:
                    {
                        Homophonic homo = new Homophonic(Alphabet.EngU + " ", OpenDlg.FileName);
                        L3TextBoxOutput.Text = homo.Encrypt(L3TextBoxInput.Text.ToUpper());
                        break;
                    }
                case false:
                    {
                        Homophonic homo = new Homophonic(Alphabet.EngU + " ", OpenDlg.FileName);
                        L3TextBoxOutput.Text = homo.Decrypt(L3TextBoxInput.Text.ToUpper());
                        break;
                    }
            }
        }

        #endregion

        #region Lab4
        private void L4RadioDecrypt_Unchecked(object sender, RoutedEventArgs e)
        {
            L4ButtonAction.Content = "Encrypt";
        }

        private void L4RadioDecrypt_Checked(object sender, RoutedEventArgs e)
        {
            L4ButtonAction.Content = "Decrypt";
        }

        private void L4ButtonAction_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OpenDlg = CreateOpenDialog("Select codetable", ".txt", "Text documents (.txt)|*.txt");
            if (!OpenDlg.ShowDialog().Value)
                return;
            switch (L4RadioEncrypt.IsChecked)
            {
                case true:
                    {
                        Analytic a = new Analytic(Alphabet.EngU + " ", OpenDlg.FileName);
                        L4TextBoxOutput.Text = a.Encrypt(L4TextBoxInput.Text.ToUpper());
                        break;
                    }
                case false:
                    {
                        Analytic a = new Analytic(Alphabet.EngU + " ", OpenDlg.FileName);
                        L4TextBoxOutput.Text = a.Decrypt(L4TextBoxInput.Text);
                        break;
                    }
            }
        }

        #endregion

        #region OtherMethods
        private static OpenFileDialog CreateOpenDialog(string title, string DefaultExt, string Filter)
        {
            OpenFileDialog OpenDlg = new Microsoft.Win32.OpenFileDialog();
            OpenDlg.Title = title;
            OpenDlg.CheckFileExists = false;
            OpenDlg.DefaultExt = DefaultExt;
            OpenDlg.Filter = Filter;
            return OpenDlg;
        }     
        

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            TextBox tb = e.Source as TextBox;
            ReadWriteTxt.Write(tb.Text);
        }

        private void OpenCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            TextBox tb = e.Source as TextBox;
            tb.Text = ReadWriteTxt.Read();
        }

        private void CloseExpanders(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i <= 5; i++)
            {
                object exp = this.FindName(String.Format("Expander{0}", i));
                if (exp != sender) ((Expander)exp).IsExpanded = false;
            }
        }

        #endregion


        private void L5Button_Click(object sender, RoutedEventArgs e)
        {
            switch (L5CBoxAlg.SelectedIndex) //Encryption Algorythm
            { 
                case 0: // Triple DES
                    {
                        switch (L5CBoxAction.SelectedIndex)
                        {
                            case 0: //Encrypt
                                {
                                    switch (L5CBoxSource.SelectedIndex)
                                    {
                                        case 0: //File ".txt", "Text documents (.txt)|*.txt"
                                            {
                                                OpenFileDialog FileToEncrypt = CreateOpenDialog("Select File to encrypt", "*", "All File (.*)|*.*");
                                                if (!FileToEncrypt.ShowDialog().Value)
                                                    return;
                                                OpenFileDialog FileToSaveKey = CreateOpenDialog("Select where to save key", ".des3key", "Triple DES key (.des3key)|*.des3key");
                                                if (!FileToSaveKey.ShowDialog().Value)
                                                    return;
                                                OpenFileDialog CryptedFile = CreateOpenDialog("Select where to save encrypted file", ".des3", "Triple DES (.des3)|*.des3");
                                                if (!CryptedFile.ShowDialog().Value)
                                                    return;
                                                FeistelCipher.Encrypt(FileToEncrypt.FileName, FileToSaveKey.FileName, CryptedFile.FileName);
                                                break;
                                            }
                                        case 1: //Text string
                                            {
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case 1: //Decrypt
                                {
                                    switch (L5CBoxSource.SelectedIndex)
                                    {
                                        case 0: //File
                                            {
                                                OpenFileDialog FileToDecrypt = CreateOpenDialog("Select File to decrypt", ".des3", "Triple DES (.des3)|*.des3");
                                                if (!FileToDecrypt.ShowDialog().Value)
                                                    return;
                                                OpenFileDialog FileToSaveKey = CreateOpenDialog("Select key file", ".des3key", "Triple DES key (.des3key)|*.des3key");
                                                if (!FileToSaveKey.ShowDialog().Value)
                                                    return;
                                                OpenFileDialog DecryptedFile = CreateOpenDialog("Select where to save decrypted file", "*", "All File (.*)|*.*");
                                                if (!DecryptedFile.ShowDialog().Value)
                                                    return;
                                                FeistelCipher.Decrypt(FileToDecrypt.FileName, FileToSaveKey.FileName, DecryptedFile.FileName);
                                                break;
                                            }
                                        case 1: //Text string
                                            {
                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case 1: //DES
                    {
                        break;
                    }
                case 2: //AES
                    {
                        break;
                    }
            }
        }
    }
}