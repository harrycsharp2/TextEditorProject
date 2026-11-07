using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TextEditor.HandleReading
{
    public class Reading
    {
        public static string FilePath = string.Empty;
        public static string FileContents = string.Empty;
        public static List<string> FilePathHistory = new List<string>();
        public static void ReadFile()
        {
            if (File.Exists(FilePath))
            {
                FileContents = File.ReadAllText(FilePath);
                if (!FilePathHistory.Contains(FilePath))
                {
                    FilePathHistory.Add(FilePath);
                }
                return;
            }
            else
            {
                MessageBox.Show("couldnt find file in this file path");
            }
        }
    }
}
