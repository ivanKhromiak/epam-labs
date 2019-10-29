using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;

namespace Epam.HomeWork.Lab7Runner
{
    public class FilenameComparsionExcelWriter : IFilenameComparisonWriter
    {
        public FilenameComparsionExcelWriter(string path)
        {
            this.Path = path;
        }

        public string Path { get; }

        public void WriteFilenameData(string headerMessage, IEnumerable<string> filenames, string filepath)
        {
            Application excelApp;
            _Workbook excelWorkBook;
            _Worksheet excelSheet;

            object misvalue = System.Reflection.Missing.Value;

            try
            {
                //Start Excel and get Application object.
                excelApp = new Application
                {
                    Visible = true
                };

                //Get a new workbook.
                excelWorkBook = excelApp.Workbooks.Add("");
                excelSheet = (_Worksheet)excelWorkBook.ActiveSheet;
                

                excelApp.Visible = false;
                excelApp.UserControl = false;
                excelWorkBook.SaveAs(Path, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                excelWorkBook.Close();
                excelApp.Quit();
            }
            catch(Exception e)
            {

            }
        }
    }
}
