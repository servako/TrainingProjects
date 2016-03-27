using System;
using System.Linq;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace WindowsFormsApplicationExcelWorkbook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var document = SpreadsheetDocument.Open(openFileDialog1.FileName, true))
                    {
                        var workbookPart = document.WorkbookPart;
                        var workbook = workbookPart.Workbook;

                        var sheets = workbook.Descendants<Sheet>();
                        foreach (var sheet in sheets)
                        {
                            var worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                            var sharedStringPart = workbookPart.SharedStringTablePart;
                            var values = sharedStringPart.SharedStringTable.Elements<SharedStringItem>().ToArray();

                            var cells = worksheetPart.Worksheet.Descendants<Cell>();
                            foreach (var cell in cells)
                            {
                                string value = String.Empty;
                                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                                {
                                    var index = int.Parse(cell.CellValue.Text);
                                    value = values[index].InnerText;
                                }
                                else
                                {
                                    value = cell.CellValue.Text;
                                }

                                if (value.ToLower() == "red")
                                {
                                    CellFormat cellFormat = cell.StyleIndex != null ? GetCellFormat(workbookPart, cell.StyleIndex).CloneNode(true) as CellFormat : new CellFormat();
                                    cellFormat.FillId = InsertFill(workbookPart, GenerateFill());
                                    cell.StyleIndex = InsertCellFormat(workbookPart, cellFormat);
                                }
                            }
                        }

                        workbook.Save();

                        MessageBox.Show("Done!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

        }

        public CellFormat GetCellFormat(WorkbookPart workbookPart, uint styleIndex)
        {
            return workbookPart.WorkbookStylesPart.Stylesheet.Elements<CellFormats>().First().Elements<CellFormat>().ElementAt((int)styleIndex);
        }

        public uint InsertFill(WorkbookPart workbookPart, Fill fill)
        {
            Fills fills = workbookPart.WorkbookStylesPart.Stylesheet.Elements<Fills>().First();
            fills.Append(fill);
            return (uint)fills.Count++;
        }

        public Fill GenerateFill()
        {
            Fill fill = new Fill();

            PatternFill patternFill = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor1 = new ForegroundColor() { Rgb = "FFF00000" };

            patternFill.Append(foregroundColor1);

            fill.Append(patternFill);

            return fill;
        }

        public uint InsertCellFormat(WorkbookPart workbookPart, CellFormat cellFormat)
        {
            CellFormats cellFormats = workbookPart.WorkbookStylesPart.Stylesheet.Elements<CellFormats>().First();
            cellFormats.Append(cellFormat);
            return (uint)cellFormats.Count++;
        }
    }
}
