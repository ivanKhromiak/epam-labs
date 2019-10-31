// <copyright file="SyncfusionExcelColumnReader.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.LabRunners.Common
{
    using System.Collections.Generic;
    using System.IO;
    using Syncfusion.XlsIO;

    /// <summary>
    /// Used to read a range from concrete column
    /// </summary>
    public class SyncfusionExcelColumnReader : IExcelColumnReader
    {
        /// <summary>
        /// Gets a range data from specific column
        /// </summary>
        /// <param name="columnName">Name of the column</param>
        /// <param name="startRange">Start index</param>
        /// <param name="inputFileName">File to read from</param>
        /// <returns>List of data</returns>
        public IEnumerable<string> GetData(string columnName, int startRange, string inputFileName)
        {
            List<string> elements = null;

            using (FileStream inputStream = new FileStream(inputFileName, FileMode.Open))
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IWorkbook workbook = excelEngine.Excel.Workbooks.Open(inputStream);
                IWorksheet worksheet = workbook.Worksheets[0];

                elements = new List<string>();
                int i = startRange;
                string data = null;

                do
                {
                    data = worksheet.Range[$"{columnName}{i}"].Value;

                    if (!string.IsNullOrWhiteSpace(data))
                    {
                        elements.Add(data);
                    }
                    ++i;
                }
                while (!string.IsNullOrWhiteSpace(data));
            }

            return elements;
        }
    }
}