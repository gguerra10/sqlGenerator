using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace SqlGenerator.Export.Pdf
{
    public static class PdfGenerator
    {

        public static void GenerateFromDataTable(string title, DataTable dataTable, string file)
        {
            var titleFontSize = 18;
            var headerFontSize = 10;
            var fontSize = 10;

            // Set the font
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font titleFont = new Font(bf, titleFontSize);
            Font headerFont = new Font(bf, headerFontSize);
            Font font = new Font(bf, fontSize);


            Document document = new Document(PageSize.A4.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(file, FileMode.Create));
            document.Open();

            Paragraph titleParagraph = new Paragraph(title, titleFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20,
            };
            document.Add(titleParagraph);


            PdfPTable table = new PdfPTable(dataTable.Columns.Count);
            table.DefaultCell.Padding = 1;
            table.DefaultCell.BorderWidth = 1;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;


            // Get average width for each column
            var maxWidths = new List<string>();
            foreach (DataColumn dc in dataTable.Columns)
            {
                maxWidths.Add(dc.ColumnName.ToString());
            }
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    if (maxWidths[j].Length < dataTable.Rows[i][j].ToString().Length)
                    {
                        maxWidths[j] = dataTable.Rows[i][j].ToString();
                    }
                }
            }
            // Set widths according max widths
            table.SetWidths(GetHeaderWidths(headerFont, maxWidths.ToArray()));

            // Convert the datatable header to the header of the PDFTable
            foreach (DataColumn dc in dataTable.Columns)
            {
                var cell = new PdfPCell(new Phrase(dc.ColumnName.ToString(), headerFont))
                {
                    BackgroundColor = new BaseColor(0x03, 0x9b, 0xe5),
                };
                table.AddCell(cell);
            }

            // Insert data
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    table.AddCell(new PdfPCell(new Phrase(dataTable.Rows[i][j].ToString(), font)));
                }
            }

            document.Add(table);
            document.Close();
            writer.Close();
        }

        private static float[] GetHeaderWidths(Font font, params string[] headers)
        {
            var total = 0;
            var columns = headers.Length;
            var widths = new int[columns];

            // Get absolute widht in pixels according to font
            for (var i = 0; i < columns; ++i)
            {
                var w = font.GetCalculatedBaseFont(true).GetWidth(headers[i]);
                total += w;
                widths[i] = w;
            }

            // Fix: If any widht is 5 times smaller then duplicate
            for (var i = 0; i < columns; i++)
            {
                if (widths[i] < (widths.Max() / 5)) widths[i] = widths[i] * 2;
            }

            // Get percentage width
            var result = new float[columns];
            for (var i = 0; i < columns; ++i)
            {
                result[i] = (float)widths[i] / total * 100;
            }
            return result;
        }
    }
}
