using System;
using System.IO;

namespace Lab1
{
    class ReadWriteTxt
    {
        public static string Read()
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".txt";
                dlg.Filter = "Text documents (.txt)|*.txt";
                if (dlg.ShowDialog().Value == true)
                {
                    StreamReader f = new StreamReader(dlg.FileName);
                    return f.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Can't read from selected file");
                return e.Message;
            }
            return ""; 
        }

        public static void Write(string Text)
        {
            try
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.DefaultExt = ".txt";
                dlg.Filter = "Text documents (.txt)|*.txt";
                if (dlg.ShowDialog().Value == true)
                {
                    StreamWriter f = new StreamWriter(dlg.FileName);
                    f.Write(Text);
                    f.Close();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Can't write to selected file");
            }             
        }
    }
}
