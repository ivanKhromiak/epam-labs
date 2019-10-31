// <copyright file="FilesStatisticsExcelWriter.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.Lab7Runner
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Syncfusion.XlsIO;

    /// <summary>
    /// Used to write files stats into excel doc 
    /// </summary>
    public class FilesStatisticsExcelWriter : IFilesStatisticWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilesStatisticsExcelWriter" /> class.
        /// </summary>
        /// <param name="path">Output path</param>
        /// <param name="filename">Output filename</param>
        public FilesStatisticsExcelWriter(string path, string filename)
        {
            this.Path = path;
            this.Filename = filename;
        }

        /// <summary>
        /// Gets or sets output path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets output filename
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Writes statistics into xlsx data format
        /// </summary>
        /// <param name="filenames">List of filenames</param>
        /// <param name="statsName">Statistics name</param>
        /// <param name="directories">List of directories involved</param>
        public void WriteFilenameData(IEnumerable<string> filenames, string statsName, IEnumerable<DirectoryInfo> directories)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                int i = 1;

                worksheet.Range[$"A{i}"].Text = "Directories";
                worksheet.Range[$"B{i}"].Text = $"Count: {directories.Count()}";

                ++i;

                foreach (var dir in directories)
                {
                    worksheet.Range[$"A{i}"].Text = "Directory: ";
                    worksheet.Range[$"B{i}"].Text = dir.FullName;
                    ++i;
                }
                ++i;

                worksheet.Range[$"A{i}"].Text = statsName;
                worksheet.Range[$"B{i}"].Text = $"Count: {filenames.Count()}";
                ++i;

                foreach (var filename in filenames)
                {
                    worksheet.Range[$"A{i}"].Text = "Filename: ";
                    worksheet.Range[$"B{i}"].Text = filename;
                    ++i;
                }

                worksheet.AutofitColumn(1);
            
                var namePart = Regex.Replace(statsName, @"\s+", string.Empty);

                // Saving the workbook to disk in XLSX format
                using (MemoryStream ms = new MemoryStream())
                using (FileStream fs = new FileStream($"{Path}{namePart}_{Filename}.xlsx", FileMode.OpenOrCreate))
                {
                    workbook.SaveAs(ms);
                    ms.WriteTo(fs);
                }
            }
        }
    }
}
