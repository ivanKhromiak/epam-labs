// <copyright file="FilesStatisticsExcelWriter.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.LabRunners.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Syncfusion.XlsIO;

    /// <summary>
    /// Used to write files stats into excel doc 
    /// </summary>
    public class FilesStatisticsExcelWriter : IStatisticWriter<string>
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
        /// <param name="statistics">List of statistics</param>
        /// <param name="statsName">Statistics name</param>
        public void WriteData(IEnumerable<StatisticInfo<string>> statistics, string statsName)
        {
            if (statistics == null)
            {
                throw new ArgumentNullException(nameof(statistics));
            }

            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                int i = 1;

                foreach (var stat in statistics)
                {
                    if (stat == null || stat.Data == null || stat.Name == null)
                    {
                        throw new ArgumentException("Data in statistics is null!");
                    }

                    worksheet.Range[$"A{i}"].Text = stat.Name;
                    worksheet.Range[$"B{i}"].Text = $"Count: {stat.Data.Count()}";

                    ++i;
                    
                    foreach (var item in stat.Data)
                    {
                        worksheet.Range[$"A{i}"].Text = $"{stat.DataItemName}: ";
                        worksheet.Range[$"B{i}"].Text = item;
                        ++i;
                    }
                    ++i;
                }

                worksheet.AutofitColumn(1);

                var namePart = Regex.Replace(statsName, @"\s+", string.Empty);

                // Saving the workbook to disk in XLSX format
                using (MemoryStream ms = new MemoryStream())
                using (FileStream fs = new FileStream($"{this.Path}{namePart}_{this.Filename}.xlsx", FileMode.OpenOrCreate))
                {
                    workbook.SaveAs(ms);
                    ms.WriteTo(fs);
                }
            }
        }
    }
}
