using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using SqlGenerator.Export.Pdf;
using SqlGenerator.Export.Pdf.Enum;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace SqlGenerator.Export.Facade.Impl.Pdf
{
    public class PdfExporter : IExporter
    {
        public string Filter => "Portable Document Format (*.pdf)|*.pdf";

        private readonly PdfDesign _pdfDesign;
        private PdfWriter _pdfWriter;

        public PdfExporter(PdfDesign pdfDesign)
        {
            _pdfDesign = pdfDesign;
        }

        private PdfFont _baseFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

        public bool Export(string filePath, DataTable dataTable, params object[] arguments)
        {
            bool result;
            try
            {
                _pdfWriter = new PdfWriter(new FileStream(filePath, FileMode.Create));
                GeneratePdf(dataTable);
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating Pdf document", ex);
            }
            finally
            {
                _pdfWriter.Close();
            }
            return result;

        }

        private void GeneratePdf(DataTable dataTable)
        {
            var pdfDocument = new PdfDocument(_pdfWriter); 

            var document = new Document(pdfDocument, PageSize.A4, false);
            if (_pdfDesign.Orientation.Equals(PdfOrientation.Landscape))
            {
                document = new Document(pdfDocument, PageSize.A4.Rotate(), false);
            }

            // Add title
            document.Add(new Paragraph(_pdfDesign.Title)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(_baseFont)
                .SetFontSize(_pdfDesign.FontSize + 8));


            // Add legend
            foreach(var designData in _pdfDesign.DataCollection)
            {
                if(!string.IsNullOrEmpty(designData.Legend))
                {
                    // Add title
                    document.Add(new Paragraph(designData.Legend)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFont(_baseFont)
                        .SetFontSize(_pdfDesign.FontSize));
                }
            }

            // Add table
            AddDataTable(ref document, dataTable);


            AddHeaders(document);

            document.Close();
            pdfDocument.Close();
            _pdfWriter.Close();
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

            // Insert Totals
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                object totalObject = "";
                var designData = _pdfDesign.DataCollection.First(d => d.Name.Equals(dataColumn.ColumnName));
                if (designData.Total)
                {
                    try
                    {
                        totalObject = dataTable.Compute($"SUM({dataColumn.ColumnName})", string.Empty);
                    }
                    catch(Exception)
                    {
                        totalObject = "#NaN#";
                    }
                }
                var cell = new Cell().Add(
                    new Paragraph(totalObject.ToString()))
                    .SetFontSize(_pdfDesign.FontSize + 2);

                switch (designData.Alignment)
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


            document.Add(table);
        }

        private void AddHeaders(Document document)
        {
            var totalPages = document.GetPdfDocument().GetNumberOfPages();

            for (int i = 1; i <= totalPages; i++)
            {

                var pageSize = document.GetPdfDocument().GetPage(i).GetPageSize();
                
                if (_pdfDesign.ShowTimestamp)
                {
                    // Add timestamp
                    document.ShowTextAligned(new Paragraph($"Report generated at: { DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") }"),
                       pageSize.GetWidth() - 10, pageSize.GetHeight() - 20, i, TextAlignment.RIGHT, VerticalAlignment.BOTTOM, 0)
                      .SetFont(_baseFont)
                      .SetFontSize(8);
                }

                if (_pdfDesign.ShowPages)
                {
                    // Add pagination
                    document.ShowTextAligned(new Paragraph(string.Format("Page {0} of {1}", i, totalPages)),
                         20, pageSize.GetHeight() - 20, i, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0)
                        .SetFont(_baseFont)
                        .SetFontSize(8);
                }
            }
        }
    }
}