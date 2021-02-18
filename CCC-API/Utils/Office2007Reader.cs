using CCC_Infrastructure.API.Utils;
using CCC_Infrastructure.Utils;
using DocumentFormat.OpenXml.Packaging;
using Excel;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace CCC_API.Utils
{
    public class Office2007Reader
    {

        /// <summary>
        /// Very basic docx reader. This code is very basic. Check original method here: 
        /// https://stackoverflow.com/questions/39992870/how-to-access-openxml-content-by-page-number
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<string> ParseDocx(string filePath)
        {
            using (var wordDocument = WordprocessingDocument.Open(filePath, false))
            {
                if (!File.Exists(filePath))
                    throw new ArgumentException(Err.Msg($"{filePath} not found"));

                var body = wordDocument.MainDocumentPart.Document.Body;
                var pageviseContent = new List<string>();

                int pageCount = 0;
                pageCount = Convert.ToInt32(wordDocument.ExtendedFilePropertiesPart.Properties.Pages.Text);

                var pageContentBuilder = new StringBuilder();
                foreach (var element in body.ChildElements)
                {
                    if (element.InnerXml.IndexOf("<w:br w:type=\"page\" />", StringComparison.OrdinalIgnoreCase) < 0)
                        pageContentBuilder.Append(element.InnerText);
                    else
                    {
                        pageviseContent.Add(pageContentBuilder.ToString());
                        pageContentBuilder = new StringBuilder();
                    }

                    if (element == body.LastChild && pageContentBuilder.Length > 0)
                        pageviseContent.Add(pageContentBuilder.ToString());
                }
                return pageviseContent;
            }
        }

        /// <summary>
        /// Safely reads the xlsx file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="checkFunc">Provide function to operate on dataset from the file</param>
        public static void ProcessXlsx(string filePath, Action<DataSet> checkFunc)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException(Err.Msg($"{filePath} not found"));

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                {
                    var result = reader.AsDataSet();
                    checkFunc(result);
                }
            }
        }

        /// <summary>
        /// Downloads xlxs file (with retry logic) by given link.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="checkFunc"></param>
        public static void DownloadAssertXlsxFile(string url, Action<DataSet> checkFunc)
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                new Poller(TimeSpan.FromMinutes(2), TimeSpan.FromSeconds(10))
                    .TryUntil(() =>
                    {
                        var client = new RestClient(url);
                        var downloadData = client.DownloadData(new RestRequest());
                        // if (downloadData.Length < 150) return null; // Junk file, just pointers
                        downloadData.SaveAs(tempFile);
                        var length = new FileInfo(tempFile).Length;
                        if (length > 500) // If not junk file
                            return tempFile;
                        return null; // continue waiting
                    });
                ProcessXlsx(tempFile, checkFunc);
            }
            finally // lets delete junk file
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
}
