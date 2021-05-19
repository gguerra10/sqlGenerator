using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Font;
using iText.Layout.Properties;
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

        private PdfFont _baseFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

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
            var pdfWriter = new PdfWriter(new FileStream(filePath, FileMode.Create));
            var pdfDocument = new PdfDocument(pdfWriter);
            var document = new Document(pdfDocument, PageSize.A4);
            if (_pdfDesign.Orientation.Equals(PdfOrientation.Landscape))
            {
                document = new Document(pdfDocument, PageSize.A4.Rotate());
            }

            // Add title
            document.Add(new Paragraph(_pdfDesign.Title)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(_baseFont)
                .SetFontSize(_pdfDesign.FontSize + 8));

            // Add filters

            if(_pdfDesign.Filters)
            {

            }

            // Add timestamp
            if (_pdfDesign.Timestamp)
            {
                document.Add(new Paragraph($"Report generated at { DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") }")
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetFont(_baseFont)
                    .SetFontSize(8));
            }

            AddDataTable(ref document, dataTable);

            document.Close();
            pdfDocument.Close();
            pdfWriter.Close();
        }


        private void AddDataTable(ref Document document, DataTable dataTable)
        {
            var widths = new float[dataTable.Columns.Count];
            int order = 0;
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                widths[order] = _pdfDesign.DataCollection.First(d => d.Name.Equals(dataColumn.ColumnName)).Width;
                order++;
            }
            var table = new Table(widths);
            switch (_pdfDesign.Alignment)
            {
                case PdfAlignment.Centered:
                    table.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                    break;
                case PdfAlignment.RightAlignment:
                    table.SetHorizontalAlignment(HorizontalAlignment.RIGHT);
                    break;
                case PdfAlignment.LeftAlignment:
                default:
                    table.SetHorizontalAlignment(HorizontalAlignment.LEFT);
                    break;
            }
            table.SetPaddings(1, 1, 1, 1);

            // Insert headers
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                var cell = new Cell().Add(
                    new Paragraph(dataColumn.ColumnName.ToString()))
                    .SetBackgroundColor(new DeviceRgb(_pdfDesign.HeaderBackgroundColor))
                    .SetFontColor(new DeviceRgb(_pdfDesign.HeaderForegroundColor))
                    .SetFontSize(_pdfDesign.FontSize + 2);

                table.AddHeaderCell(cell);
            }

            // Insert data
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    var cell = new Cell().Add(
                        new Paragraph(dataTable.Rows[i][j].ToString()))
                        .SetFontSize(_pdfDesign.FontSize);
                    var alignment = _pdfDesign.DataCollection.First(d => d.Name.Equals(dataTable.Columns[j].ColumnName)).Alignment;
                    switch (alignment)
                    {
                        case PdfAlignment.Centered:
                            cell.SetTextAlignment(TextAlignment.CENTER);
                            break;
                        case PdfAlignment.RightAlignment:
                            cell.SetTextAlignment(TextAlignment.RIGHT);
                            break;
                        case PdfAlignment.LeftAlignment:
                        default:
                            cell.SetTextAlignment(TextAlignment.LEFT);
                            break;
                    }

                    table.AddCell(cell);
                }
            }

            // Insert totals


            document.Add(table);
        }
    }
}