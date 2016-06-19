using CsvSkim.Properties;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CsvSkim
{
    public partial class Form1 : Form
    {
        private readonly string file;
        private readonly char separator;

        public Form1(string file, char separator)
        {
            this.file = file;
            this.separator = separator;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MaximizeWindow();
            LoadFile();
        }

        private void MaximizeWindow()
        {
            WindowState = FormWindowState.Maximized;
        }

        private void LoadFile()
        {
            try
            {
                using (StreamReader csvReader = new StreamReader(file))
                {
                    LoadLines(csvReader);
                }
            }
            catch (IOException)
            {
                ShowFileNotFoundErrorMessage();
            }
        }

        private void LoadLines(StreamReader csvReader)
        {
            string line = csvReader.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                return;
            }

            InitializeColumns(line);

            do
            {
                string[] row = SplitLine(line);
                string[] processedRow = row.Select(CleanColumnValue).ToArray();

                this.dataGridView1.Rows.Add(row);
            } while ((line = csvReader.ReadLine()) != null);
        }

        private void InitializeColumns(string line)
        {
            string[] row = SplitLine(line);

            for (int i = 0; i < row.Length; i++)
            {
                this.dataGridView1.Columns.Add("", "");
            }
        }

        private string[] SplitLine(string line)
        {
            return line.Split(this.separator);
        }

        private string CleanColumnValue(string rawValue)
        {
            return rawValue.Trim().Trim('"');
        }

        private static void ShowFileNotFoundErrorMessage()
        {
            MessageBox.Show(Resources.FileNotFoundErrorMessage, Resources.ErrorText, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}