using CsvSkim.Properties;
using System;
using System.Windows.Forms;

namespace CsvSkim
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            string file, potentialSeparator;
            char separator;

            ProcessArguments(args, out file, out potentialSeparator);
            ValidateFile(file);
            ValidateSeparator(potentialSeparator);
            separator = char.Parse(potentialSeparator);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(file, separator));
        }

        private static void ProcessArguments(string[] args, out string file, out string potentialSeparator)
        {
            file = string.Empty;
            potentialSeparator = ",";

            if (0 == args.Length)
            {
                return;
            }

            if (1 == args.Length)
            {
                file = args[0];
                return;
            }

            potentialSeparator = args[0];
            file = args[1];
        }

        private static void ValidateFile(string file)
        {
            if (string.IsNullOrWhiteSpace(file))
            {
                ShowUsage();
            }
        }

        private static void ValidateSeparator(string potentialSeparator)
        {
            if (string.IsNullOrWhiteSpace(potentialSeparator))
            {
                ShowInvalidSeparatorMessage();
            }

            if (1 < potentialSeparator.Length)
            {
                ShowInvalidSeparatorMessage();
            }
        }

        private static void ShowUsage()
        {
            MessageBox.Show(Resources.UsageText);
            Environment.Exit(1);
        }

        private static void ShowInvalidSeparatorMessage()
        {
            MessageBox.Show(Resources.InvalidSeparatorErrorMessage);
            Environment.Exit(2);
        }
    }
}