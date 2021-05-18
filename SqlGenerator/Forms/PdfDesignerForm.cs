using SqlGenerator.Enum;
using SqlGenerator.Export.Facade;
using SqlGenerator.Export.Facade.Impl.Pdf;
using SqlGenerator.Export.Pdf;
using SqlGenerator.Export.Pdf.Enum;
using SqlGenerator.Extension;
using SqlGenerator.File;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SqlGenerator.Forms
{
    public partial class PdfDesignerForm : Form
    {
        public QueryGeneratorFile QueryGeneratorFile { get; }

        private PdfDesign PdfDesign
        {
            get => QueryGeneratorFile.Content.PdfDesign;
            set => QueryGeneratorFile.Content.PdfDesign = value;
        }

        private IExporter _pdfExporter;
        private DataTable DataTable { get; set; }

        public PdfDesignerForm(DataTable dataTable, ref QueryGeneratorFile queryGeneratorFile)
        {
            InitializeComponent();

            var sizeArray = Enumerable.Range(6, 19).ToArray();
            foreach (var size in sizeArray)
            {
                fontsizeComboBox.Items.Add(size);
            }

            QueryGeneratorFile = queryGeneratorFile;

            saveButton.Enabled = !string.IsNullOrEmpty(QueryGeneratorFile.FilePath);

            if (PdfDesign == null)
            {
                PdfDesign = new PdfDesign();
            }

            DataTable = dataTable.Copy();


            if (PdfDesign.DataCollection.Count == 0)
            {
                // Add every column to PdfDesign
                foreach (DataColumn column in DataTable.Columns)
                {
                    PdfDesign.DataCollection.Add(new PdfDesignData()
                    {
                        Name = column.ColumnName,
                        Hidden = false,
                        Width = 100,
                    });
                }
            }
            else
            {
                // Remove inexistent columns from PdfDesign
                var designDataCollection = new List<PdfDesignData>();
                foreach (var designData in PdfDesign.DataCollection)
                {
                    if (null != DataTable.Columns[designData.Name])
                    {
                        designDataCollection.Add(designData);
                    }
                }
                PdfDesign.DataCollection = designDataCollection;
                
            }

            // Add default FontSize in  PdfDesign
            if (PdfDesign.FontSize == 0)
            {
                PdfDesign.FontSize = 8;
            }

            titleTextBox.Text = PdfDesign.Title;
            horizontalRadioButton.Checked = PdfDesign.Orientation.Equals(PdfOrientation.Landscape);
            timestampCheckBox.Checked = PdfDesign.Timestamp;
            fontsizeComboBox.SelectedItem = PdfDesign.FontSize;

            previewDataGridView.DataSource = DataTable;
            DesignerDataGridViewCreateColumns();
            foreach (var designData in PdfDesign.DataCollection)
            {
                DesignerDataGridViewAddRow(designData);
            }

            UpdatePreview();
        }


        private void DesignerDataGridViewCreateColumns()
        {
            // Grid column (text type): data name
            DataGridViewTextBoxColumn dataName = new DataGridViewTextBoxColumn()
            {
                Name = PdfDesignerGridViewColumns.Data.GetName(),
                HeaderText = PdfDesignerGridViewColumns.Data.GetDescription(),
                ReadOnly = true,
            };
            designerDataGridView.Columns.Add(dataName);

            // Grid column (check type): data hidden
            DataGridViewCheckBoxColumn dataHidden = new DataGridViewCheckBoxColumn()
            {
                Name = PdfDesignerGridViewColumns.Hide.GetName(),
                HeaderText = PdfDesignerGridViewColumns.Hide.GetDescription(),
                ReadOnly = false,
            };
            designerDataGridView.Columns.Add(dataHidden);

            // Grid column (check type): data hidden
            DataGridViewTextBoxColumn dataWidth = new DataGridViewTextBoxColumn()
            {
                Name = PdfDesignerGridViewColumns.Width.GetName(),
                HeaderText = PdfDesignerGridViewColumns.Width.GetDescription(),
                ReadOnly = false,
            };
            designerDataGridView.Columns.Add(dataWidth);

            // Grid column (combo type): database table column agreggation (MAX(), MIN(), etc)
            DataGridViewComboBoxColumn dataAlignment = new DataGridViewComboBoxColumn()
            {
                Name = ColumnsGridViewColumns.Group.GetName(),
                HeaderText = ColumnsGridViewColumns.Group.GetDescription(),
                ValueType = typeof(PdfAlignment),
                DisplayMember = "Display",
                ValueMember = "Value",
                DataSource = System.Enum.GetValues(typeof(PdfAlignment)).OfType<PdfAlignment>().ToList().Select(value => new { Value = value, Display = value.ToString() }).ToList(),
            };
            designerDataGridView.Columns.Add(dataAlignment);

        }

        private void DesignerDataGridViewAddRow(PdfDesignData designData)
        {
            var designGridViewRow = new DataGridViewRow();

            designGridViewRow.CreateCells(designerDataGridView);

            designGridViewRow.Cells[PdfDesignerGridViewColumns.Data.GetPosition()].Value = designData.Name;
            designGridViewRow.Cells[PdfDesignerGridViewColumns.Hide.GetPosition()].Value = designData.Hidden;
            //designGridViewRow.Cells[PdfDesignerGridViewColumns.Group.GetPosition()].Value = designData.Grouped;
            designGridViewRow.Cells[PdfDesignerGridViewColumns.Width.GetPosition()].Value = designData.Width;
            designGridViewRow.Cells[PdfDesignerGridViewColumns.Alignment.GetPosition()].Value = designData.Alignment;

            designerDataGridView.Rows.Add(designGridViewRow);
        }

        /// <summary>
        /// Cell clicked in design grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignerDataGridViewCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Debug.WriteLine("Column: " + e.ColumnIndex + ", row: " + e.RowIndex + " cell content clicked");
            var senderGrid = (DataGridView)sender;

            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex.Equals(PdfDesignerGridViewColumns.Hide.GetPosition()) &&
                    senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
                {
                    UpdatePdfDesign();
                    UpdatePreview();
                }
            }
        }


        private void DesignerDataGridViewCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex.Equals(PdfDesignerGridViewColumns.Hide.GetPosition()) &&
                    senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
                {
                    UpdatePdfDesign();
                    UpdatePreview();
                }
            }
        }

        private void DesignerDataGridViewCurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if(designerDataGridView.IsCurrentCellDirty)
            {
                designerDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DesignerDataGridViewCellValidated(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex.Equals(PdfDesignerGridViewColumns.Width.GetPosition()))
                {
                    senderGrid.EndEdit();

                    UpdatePdfDesign();
                    UpdatePreview();
                }
            }
        }

        private void UpdatePdfDesign()
        {
            PdfDesign.DataCollection.Clear();

            foreach (DataGridViewRow designData in designerDataGridView.Rows)
            {
                var name = designData.Cells[PdfDesignerGridViewColumns.Data.GetPosition()].Value.ToString();
                var checkBoxCell = designData.Cells[PdfDesignerGridViewColumns.Hide.GetPosition()] as DataGridViewCheckBoxCell;
                var hidden = Convert.ToBoolean(checkBoxCell.EditingCellValueChanged ? checkBoxCell.EditingCellFormattedValue : checkBoxCell.Value);
                var width = Convert.ToInt32(designData.Cells[PdfDesignerGridViewColumns.Width.GetPosition()].Value);
                var alignment = (PdfAlignment)System.Enum.Parse(typeof(PdfAlignment), ((DataGridViewComboBoxCell)designData.Cells[PdfDesignerGridViewColumns.Alignment.GetPosition()]).EditedFormattedValue.ToString());

                var pdfDesignData = new PdfDesignData()
                {
                    Name = name,
                    Hidden = hidden,
                    Width = width,
                    Alignment = alignment,
                };

                PdfDesign.DataCollection.Add(pdfDesignData);
            }
        }

        private void UpdatePreview()
        {
            previewDataGridView.DataSource = null;
            previewDataGridView.DefaultCellStyle.Font = new Font(previewDataGridView.DefaultCellStyle.Font.FontFamily, PdfDesign.FontSize);
            previewDataGridView.ColumnHeadersDefaultCellStyle.BackColor = PdfDesign.HeaderBackgroundColor;
            previewDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = PdfDesign.HeaderForegroundColor;
            previewDataGridView.EnableHeadersVisualStyles = false;
            previewDataGridView.DataSource = DataTable;

            previewDataGridView.ColumnWidthChanged -= PreviewDataGridViewColumnWidthChanged;
            foreach (var designData in PdfDesign.DataCollection)
            {
                previewDataGridView.Columns[designData.Name].HeaderCell.Style.Font = new Font(previewDataGridView.DefaultCellStyle.Font.FontFamily, PdfDesign.FontSize + 2);
                previewDataGridView.Columns[designData.Name].Visible = !designData.Hidden;
                previewDataGridView.Columns[designData.Name].Width = Convert.ToInt32(designData.Width);
                switch (designData.Alignment)
                {
                    case PdfAlignment.Centered:
                        previewDataGridView.Columns[designData.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case PdfAlignment.RightAlignment:
                        previewDataGridView.Columns[designData.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case PdfAlignment.LeftAlignment:
                    default:
                        previewDataGridView.Columns[designData.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                }
                
            }
            previewDataGridView.ColumnWidthChanged += PreviewDataGridViewColumnWidthChanged;

            ColumnsWidthsCheck();
        }

        private void ColumnsWidthsCheck()
        {
            var width = 0;
            bool outOfPaperLimit = false;

            // A4 paper are 210mm × 297mm or 8.27in × 11.69in
            uint MaxWidth;
            if (PdfDesign.Orientation.Equals(PdfOrientation.Landscape))
            {
                MaxWidth = (uint)11.69 * 100; // 100 dpi
            }
            else
            {
                MaxWidth = (uint)8.27 * 100; // 100 dpi
            }

            foreach (DataGridViewColumn column in previewDataGridView.Columns)
            {
                if (column.Visible)
                {
                    if (!outOfPaperLimit)
                    {
                        width += column.Width;
                        column.DefaultCellStyle.BackColor = Color.White;
                        if (width > MaxWidth)
                        {
                            outOfPaperLimit = true;
                            column.DefaultCellStyle.BackColor = Color.Gray;
                        }
                    }
                    else
                    {
                        column.DefaultCellStyle.BackColor = Color.Gray;
                    }
                }
            }
        }

        private void VerticalRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            if (verticalRadioButton.Checked)
            {
                PdfDesign.Orientation = PdfOrientation.Vertical;
            }
            else
            {
                PdfDesign.Orientation = PdfOrientation.Landscape;
            }
            UpdatePreview();
        }

        private void TitleTextBoxTextChanged(object sender, EventArgs e)
        {
            PdfDesign.Title = titleTextBox.Text;
        }


        private void PreviewDataGridViewColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            var width = (sender as DataGridView).Columns[e.Column.Index].Width;
            var columnName = (sender as DataGridView).Columns[e.Column.Index].Name;
            foreach (var designData in PdfDesign.DataCollection)
            {
                if (designData.Name.Equals(columnName))
                {
                    designData.Width = width;
                }
            }
            foreach (DataGridViewRow row in designerDataGridView.Rows)
            {
                if (row.Cells[PdfDesignerGridViewColumns.Data.GetPosition()].Value.Equals(columnName))
                {
                    row.Cells[PdfDesignerGridViewColumns.Width.GetPosition()].Value = width;
                }
            }
            UpdatePreview();
        }

        private void PreviewDataGridViewColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (e.Column.Index != e.Column.DisplayIndex)
            {
                previewDataGridView.BeginInvoke(new MethodInvoker(() =>
                {
                    previewDataGridView.ColumnDisplayIndexChanged -= PreviewDataGridViewColumnDisplayIndexChanged;
                    (previewDataGridView.DataSource as DataTable).Columns[e.Column.Index].SetOrdinal(e.Column.DisplayIndex);
                    previewDataGridView.ColumnDisplayIndexChanged += PreviewDataGridViewColumnDisplayIndexChanged;

                    UpdatePdfDesign();
                    ColumnsWidthsCheck();
                }));
            }
        }


        private void ExportButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Export();
            Cursor.Current = Cursors.Arrow;
        }

        private void Export()
        {
            // Remove hidden columns
            var pdfDataTable = DataTable.Copy();
            pdfDataTable.PrimaryKey = null;
            foreach (var designData in PdfDesign.DataCollection)
            {
                if (designData.Hidden)
                {
                    pdfDataTable.Columns.Remove(designData.Name);
                }
            }


            _pdfExporter = new PdfExporter(PdfDesign);
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = _pdfExporter.Filter,
                InitialDirectory = "./",
            };
            var result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {

                // Export to Pdf
                if (_pdfExporter.Export(saveFileDialog.FileName, pdfDataTable))
                {
                    new Process
                    {
                        // Open file using shell
                        StartInfo = new ProcessStartInfo(saveFileDialog.FileName)
                        {
                            UseShellExecute = true
                        }
                    }.Start();
                }
            }
        }

        private void FontButton_Click(object sender, EventArgs e)
        {
            /*
            var fontDialog = new FontDialog
            {
                Font = PdfDesign.Font
            };
            fontDialog.ShowDialog();
            PdfDesign.Font = fontDialog.Font;
            */
            UpdatePreview();

        }

        private void TimestampCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            PdfDesign.Timestamp = timestampCheckBox.Checked;
        }

        private void FontsizeComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(fontsizeComboBox.SelectedItem.ToString(), out int fontSize);
            PdfDesign.FontSize = fontSize;
            UpdatePreview();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            QueryGeneratorFile.Save();
        }

        private void BackgroundColorButton_Click(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog
            {
                Color = PdfDesign.HeaderBackgroundColor
            };
            colorDialog.ShowDialog();
            PdfDesign.HeaderBackgroundColor = colorDialog.Color;
            UpdatePreview();
        }

        private void ForegroundColorButton_Click(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog
            {
                Color = PdfDesign.HeaderForegroundColor
            };
            colorDialog.ShowDialog();
            PdfDesign.HeaderForegroundColor = colorDialog.Color;
            UpdatePreview();
        }


    }
}
