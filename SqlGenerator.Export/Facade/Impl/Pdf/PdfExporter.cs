using iTextSharp.text;
using iTextSharp.text.pdf;
using SqlGenerator.Export.Pdf;
using SqlGenerator.Export.Pdf.Enum;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SqlGenerator.Export.Facade.Impl.Pdf
{
    public class PdfExporter : IExporter
    {
        public string Filter => "Portable Document Format (*.pdf)|*.pdf";

        private readonly PdfDesign _pdfDesign;

        public PdfExporter(PdfDesign pdfDesign)
        {
            _pdfDesign = pdfDesign;
        }

        private BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

        public bool Export(string filePath, DataTable dataTable, params object[] arguments)
        {
            var result = false;

            try
            {
                GeneratePdf(filePath, dataTable);
                result = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }
            return result;

        }

        private void GeneratePdf(string filePath, DataTable dataTable)
        {
            var document = new Document(PageSize.A4);
            if (_pdfDesign.Orientation.Equals(PdfOrientation.Landscape))
            {
                document = new Document(PageSize.A4.Rotate());
            }

            var writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            var titleFont = new Font(baseFont, _pdfDesign.FontSize + 6);

            var titleParagraph = new Paragraph(_pdfDesign.Title, titleFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20,
            };

            document.Add(titleParagraph);

            if (_pdfDesign.Timestamp)
            {
                var font = new Font(baseFont, 8);
                var timestampParagraph = new Paragraph($"Report generated at { DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") }", font)
                {
                    Alignment = Element.ALIGN_RIGHT,
                    SpacingAfter = 20,
                };
                document.Add(timestampParagraph);
            }

            AddDataTable(ref document, dataTable);

            document.Close();
            writer.Close();
        }

        private void AddDataTable(ref Document document, DataTable dataTable)
        {
            var table = new PdfPTable(dataTable.Columns.Count);
            switch (_pdfDesign.Alignment)
            {
                case PdfAlignment.Centered:
                    table.HorizontalAlignment = Element.ALIGN_CENTER;
                    break;
                case PdfAlignment.RightAlignment:
                    table.HorizontalAlignment = Element.ALIGN_RIGHT;
                    break;
                case PdfAlignment.LeftAlignment:
                default:
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    break;
            }

            table.DefaultCell.Padding = 1;
            table.DefaultCell.BorderWidth = 1;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;

            table.TotalWidth = _pdfDesign.DataCollection.Where(d => !d.Hidden).Sum(d => d.Width);

            var headerFont = new Font(baseFont, _pdfDesign.FontSize + 1);
            headerFont.Color = new BaseColor(_pdfDesign.HeaderForegroundColor);
            var font = new Font(baseFont, _pdfDesign.FontSize);

            var widths = new float[dataTable.Columns.Count];
            int order = 0;
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                widths[order] = _pdfDesign.DataCollection.First(d => d.Name.Equals(dataColumn.ColumnName)).Width;
                order++;
            }
            table.SetTotalWidth(widths);
            table.LockedWidth = true;

            // Convert the datatable header to the header of the PDFTable
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                var cell = new PdfPCell(new Phrase(dataColumn.ColumnName.ToString(), headerFont))
                {
                    BackgroundColor = new BaseColor(_pdfDesign.HeaderBackgroundColor),
                };

                table.AddCell(cell);
            }

            // Insert data
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    var cell = new PdfPCell(new Phrase(dataTable.Rows[i][j].ToString(), font));
                    var alignment = _pdfDesign.DataCollection.First(d => d.Name.Equals(dataTable.Columns[j].ColumnName)).Alignment;
                    switch (alignment)
                    {
                        case PdfAlignment.Centered:
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            break;
                        case PdfAlignment.RightAlignment:
                            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            break;
                        case PdfAlignment.LeftAlignment:
                        default:
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            break;
                    }

                    table.AddCell(cell);
                }
            }

            document.Add(table);
        }
    }
}