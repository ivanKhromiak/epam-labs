using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;

namespace Epam.HomeWork.Lab7Runner
{
    public class FilenameComparsionExcelWriter : IFilenameComparisonWriter
    {
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

                //Add table headers going cell by cell.
                excelSheet.Cells[1, 1] = "First Name";
                excelSheet.Cells[1, 2] = "Last Name";
                excelSheet.Cells[1, 3] = "Full Name";
                excelSheet.Cells[1, 4] = "Salary";

                //Format A1:D1 as bold, vertical alignment = center.
                excelSheet.get_Range("A1", "D1").Font.Bold = true;
                excelSheet.get_Range("A1", "D1").VerticalAlignment =
                    XlVAlign.xlVAlignCenter;

                // Create an array to multiple values at once.
                string[,] saNames = new string[5, 2];

                saNames[0, 0] = "John";
                saNames[0, 1] = "Smith";
                saNames[1, 0] = "Tom";

                saNames[4, 1] = "Johnson";
                

                excelApp.Visible = false;
                excelApp.UserControl = false;
                excelWorkBook.SaveAs("c:\\test\\test505.xls", XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
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
