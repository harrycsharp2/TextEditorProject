using ICSharpCode.AvalonEdit.Indentation.CSharp;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TextEditor.Data;
using TextEditor.FilePath;
using TextEditor.HandleReading;

namespace TextEditor
{
    /// <summary>
    /// todo
    /// Manage Saved Files   +Click
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

            using var db = new AppDbContext();
            db.Database.EnsureCreated();

            CompositionTarget.Rendering += Update;
            
            CodeEditor.TextArea.IndentationStrategy = new CSharpIndentationStrategy(CodeEditor.Options);
            CodeEditor.Options.IndentationSize = 4;
            CodeEditor.Options.ConvertTabsToSpaces = true;
        }
        public void Update(object? sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.F4) && !string.IsNullOrEmpty(Reading.FilePath)) 
            {
                File.WriteAllText(Reading.FilePath, CodeEditor.Text);
                Reading.FileContents = CodeEditor.Text;

                MessageBox.Show($"saved, File: {Reading.FilePath}");
            }
        }
        private void OpenTextEditorClick(object sender, RoutedEventArgs e)
        {
            TextEditorTab.Visibility = Visibility.Visible;
        }
        private void OpenManageSavedFilesClick(object sender, RoutedEventArgs e)
        {
            //TODO
        }
        private void OpenSavedFileClick(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();
            foreach (var fp in db.SavedFilePaths.ToList())
            {
                if (fp.FileName == OpenSavedFileTextBox.Text)
                {
                    Reading.FilePath = fp.FilePath;
                    Reading.ReadFile();
                    CodeEditor.Text = Reading.FileContents;
                }
            }
        }
        private void OpenCsFileClick(object sender, RoutedEventArgs e)
        {
            Reading.FilePath = FilePathTextBox.Text;
            Reading.ReadFile();

            CodeEditor.Text = Reading.FileContents;
        }
        private void SaveFilePath(object sender, RoutedEventArgs e)
        {
            SavedFilePath fp = new SavedFilePath();
            fp.FilePath = FilePathTextBox.Text;
            fp.FileName = FileNameTextBox.Text;

            using var db = new AppDbContext();
            db.SavedFilePaths.Add(fp);
            db.SaveChanges();

            MessageBox.Show("File path saved");
        }
    }
}