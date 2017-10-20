using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Excel = Microsoft.Office.Interop.Excel;
using System.ComponentModel;
using System.Data;
using Microsoft.Win32;
using System.Reflection;
using NewApp.BusinessTier.Models;


namespace ExcelInterOp
{
    public class ExcelInterOp
    {
        public ExcelInterOp()
        {
        }
    }

    public class ExcelData
    {
        string m_option = null;

        public ExcelData()
        {
        }


        public DataSet Data
        {
            get
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook;
                Excel.Worksheet worksheet;
                Excel.Range range;
                string filename;
                DataSet Ds;
                DataTable dt;

                workbook = null;
                filename = null;
                Ds = null;
                dt = null;


                try
                {
                    OpenFileDialog dlg = new OpenFileDialog();

                    dlg.DefaultExt = ".txt";
                    dlg.Filter = "EXCEL Files (*.xls)|*.xlsx";

                    Nullable<bool> result = dlg.ShowDialog();


                    // Get the selected file name and display in a TextBox 
                    if (result == true)
                    {
                        filename = dlg.FileName;
                        workbook = excelApp.Workbooks.Open(filename, 2, ReadOnly: true);

                        int column = 0;
                        int row = 0;

                        Ds = new DataSet();
                                
                        #region Customer
                        if ((Excel.Worksheet)workbook.Sheets["Customer"] != null)
                        {
                            worksheet = (Excel.Worksheet)workbook.Sheets["Customer"];

                            dt = new DataTable();
                            dt.TableName = "Customer";
                            range = worksheet.UsedRange;
                            dt.Columns.Add("Id");
                            dt.Columns.Add("Name");
                            dt.Columns.Add("Status");
                            dt.Columns.Add("DateofBirth");
                            dt.Columns.Add("Gender");
                            dt.Columns.Add("PresentAddress");
                            dt.Columns.Add("PresentCity");
                            dt.Columns.Add("PresentPinCode");
                            dt.Columns.Add("PresentPhone");
                            dt.Columns.Add("Mobile");
                            dt.Columns.Add("EMailId");
                            dt.Columns.Add("IsPresentPermAddressSame");
                            dt.Columns.Add("PermanentAddress");
                            dt.Columns.Add("PermanentCity");
                            dt.Columns.Add("PermanentPinCode");
                            dt.Columns.Add("PermanentPhone");
                            dt.Columns.Add("IDProofTypeId");
                            dt.Columns.Add("IDProofValue");
                            dt.Columns.Add("RelationshipStartDate");
                            dt.Columns.Add("RelationshipEndDate");
                            dt.Columns.Add("AmtTobePaid");
                            dt.Columns.Add("AmtPaidYTD");
                            for (row = 2; row < range.Rows.Count; row++)
                            {
                                DataRow dr = dt.NewRow();
                                for (column = 1; column <= range.Columns.Count; column++)
                                {
                                    dr[column - 1] = (range.Cells[row, column] as Excel.Range).Value2.ToString();

                                    if ((column == 4) || (column == 19) || (column == 20))
                                        if (dr[column - 1] != null)
                                            if (dr[column - 1].ToString() != "")
                                                dr[column - 1] = DateTime.FromOADate(Convert.ToDouble(dr[column - 1].ToString()));
                                }
                                dt.Rows.Add(dr);
                                dt.AcceptChanges();
                            }

                            Ds.Tables.Add(dt);
                        }
                        #endregion

                        #region StoreItem
                        if ((Excel.Worksheet)workbook.Sheets["ItemBasic"] != null)
                        {
                            worksheet = (Excel.Worksheet)workbook.Sheets["ItemBasic"];

                            dt = new DataTable();
                            dt.TableName = "ItemBasic";
                            range = worksheet.UsedRange;
                            dt.Columns.Add("ItemId");
                            dt.Columns.Add("ItemName");
                            dt.Columns.Add("ItemDescription");
                            for (row = 2; row < range.Rows.Count; row++)
                            {
                                DataRow dr = dt.NewRow();
                                for (column = 1; column <= range.Columns.Count; column++)
                                {
                                    dr[column - 1] = (range.Cells[row, column] as Excel.Range).Value2.ToString();
                                }
                                dt.Rows.Add(dr);
                                dt.AcceptChanges();
                            }

                            Ds.Tables.Add(dt);

                            if ((Excel.Worksheet)workbook.Sheets["ItemDetail"] != null)
                            {
                                worksheet = (Excel.Worksheet)workbook.Sheets["ItemDetail"];

                                DataTable dt1 = new DataTable();
                                dt1.TableName = "ItemDetail";
                                range = worksheet.UsedRange;
                                dt1.Columns.Add("IBItemId");
                                dt1.Columns.Add("IBBatchId");
                                dt1.Columns.Add("IBBarCode");
                                dt1.Columns.Add("IBMRP");
                                dt1.Columns.Add("IBQtyProcured");
                                dt1.Columns.Add("IBPriceProcured");
                                dt1.Columns.Add("IBDateProcured");
                                dt1.Columns.Add("IBWeightProcured");
                                dt1.Columns.Add("IBQtyAvailable");
                                dt1.Columns.Add("IBWeightAvailable");
                                dt1.Columns.Add("IBDateManuf");
                                dt1.Columns.Add("IBDateExp");
                                dt1.Columns.Add("IBPriceSell");
                                dt1.Columns.Add("IBManufId");
                                dt1.Columns.Add("IBVendorId");
                                for (row = 2; row < range.Rows.Count; row++)
                                {
                                    DataRow dr1 = dt1.NewRow();
                                    for (column = 1; column <= range.Columns.Count; column++)
                                    {
                                        dr1[column - 1] = (range.Cells[row, column] as Excel.Range).Value2.ToString();

                                        if ((column == 7) || (column == 11) || (column == 12))
                                            if (dr1[column - 1] != null)
                                                if (dr1[column - 1].ToString() != "")
                                                    dr1[column - 1] = DateTime.FromOADate(Convert.ToDouble(dr1[column - 1].ToString()));
                                    }
                                    dt1.Rows.Add(dr1);
                                    dt1.AcceptChanges();
                                }

                                Ds.Tables.Add(dt1);
                            }
                        }
                        #endregion

                        #region Manufacturer
                        if ((Excel.Worksheet)workbook.Sheets["Manufacturer"] != null)
                        {
                            worksheet = (Excel.Worksheet)workbook.Sheets["Manufacturer"];

                            dt = new DataTable();
                            dt.TableName = "Manufacturer";
                            range = worksheet.UsedRange;
                            dt.Columns.Add("Id");
                            dt.Columns.Add("Name");
                            dt.Columns.Add("Status");
                            dt.Columns.Add("DateofBirth");
                            dt.Columns.Add("Gender");
                            dt.Columns.Add("PresentAddress");
                            dt.Columns.Add("PresentCity");
                            dt.Columns.Add("PresentPinCode");
                            dt.Columns.Add("PresentPhone");
                            dt.Columns.Add("Mobile");
                            dt.Columns.Add("EMailId");
                            dt.Columns.Add("IsPresentPermAddressSame");
                            dt.Columns.Add("PermanentAddress");
                            dt.Columns.Add("PermanentCity");
                            dt.Columns.Add("PermanentPinCode");
                            dt.Columns.Add("PermanentPhone");
                            dt.Columns.Add("IDProofTypeId");
                            dt.Columns.Add("IDProofValue");
                            dt.Columns.Add("RelationshipStartDate");
                            dt.Columns.Add("RelationshipEndDate");
                            dt.Columns.Add("AmtTobePaid");
                            dt.Columns.Add("AmtPaidYTD");
                            for (row = 2; row < range.Rows.Count; row++)
                            {
                                DataRow dr = dt.NewRow();
                                for (column = 1; column <= range.Columns.Count; column++)
                                {
                                    dr[column - 1] = (range.Cells[row, column] as Excel.Range).Value2.ToString();

                                    if ((column == 4) || (column == 19) || (column == 20))
                                        if (dr[column - 1] != null)
                                            if (dr[column - 1].ToString() != "")
                                                dr[column - 1] = DateTime.FromOADate(Convert.ToDouble(dr[column - 1].ToString()));
                                }
                                dt.Rows.Add(dr);
                                dt.AcceptChanges();
                            }

                            Ds.Tables.Add(dt);
                        }
                        #endregion

                        #region Vendor
                        if ((Excel.Worksheet)workbook.Sheets["Vendor"] != null)
                        {        
                            worksheet = (Excel.Worksheet)workbook.Sheets["Vendor"];

                            dt = new DataTable();
                            dt.TableName = "Vendor";
                            range = worksheet.UsedRange;
                            dt.Columns.Add("Id");
                            dt.Columns.Add("Name");
                            dt.Columns.Add("Status");
                            dt.Columns.Add("DateofBirth");
                            dt.Columns.Add("Gender");
                            dt.Columns.Add("PresentAddress");
                            dt.Columns.Add("PresentCity");
                            dt.Columns.Add("PresentPinCode");
                            dt.Columns.Add("PresentPhone");
                            dt.Columns.Add("Mobile");
                            dt.Columns.Add("EMailId");
                            dt.Columns.Add("IsPresentPermAddressSame");
                            dt.Columns.Add("PermanentAddress");
                            dt.Columns.Add("PermanentCity");
                            dt.Columns.Add("PermanentPinCode");
                            dt.Columns.Add("PermanentPhone");
                            dt.Columns.Add("IDProofTypeId");
                            dt.Columns.Add("IDProofValue");
                            dt.Columns.Add("RelationshipStartDate");
                            dt.Columns.Add("RelationshipEndDate");
                            dt.Columns.Add("AmtTobePaid");
                            dt.Columns.Add("AmtPaidYTD");
                            for (row = 2; row < range.Rows.Count; row++)
                            {
                                DataRow dr = dt.NewRow();
                                for (column = 1; column <= range.Columns.Count; column++)
                                {
                                    dr[column - 1] = (range.Cells[row, column] as Excel.Range).Value2.ToString();

                                    if ((column == 4) || (column == 19) || (column == 20))
                                        if (dr[column - 1] != null)
                                            if (dr[column - 1].ToString() != "")
                                                dr[column - 1] = DateTime.FromOADate(Convert.ToDouble(dr[column - 1].ToString()));
                                }
                                dt.Rows.Add(dr);
                                dt.AcceptChanges();
                            }

                            Ds.Tables.Add(dt);
                        }
                        #endregion

                        #region User
                        if ((Excel.Worksheet)workbook.Sheets["User"] != null)
                        {
                            worksheet = (Excel.Worksheet)workbook.Sheets["User"];

                            dt = new DataTable();
                            dt.TableName = "User";
                            range = worksheet.UsedRange;
                            dt.Columns.Add("Id");
                            dt.Columns.Add("Name");
                            dt.Columns.Add("Status");
                            dt.Columns.Add("DateofBirth");
                            dt.Columns.Add("Gender");
                            dt.Columns.Add("PresentAddress");
                            dt.Columns.Add("PresentCity");
                            dt.Columns.Add("PresentPinCode");
                            dt.Columns.Add("PresentPhone");
                            dt.Columns.Add("Mobile");
                            dt.Columns.Add("EMailId");
                            dt.Columns.Add("IsPresentPermAddressSame");
                            dt.Columns.Add("PermanentAddress");
                            dt.Columns.Add("PermanentCity");
                            dt.Columns.Add("PermanentPinCode");
                            dt.Columns.Add("PermanentPhone");
                            dt.Columns.Add("IDProofTypeId");
                            dt.Columns.Add("IDProofValue");
                            dt.Columns.Add("RelationshipStartDate");
                            dt.Columns.Add("RelationshipEndDate");
                            dt.Columns.Add("AmtTobePaid");
                            dt.Columns.Add("AmtPaidYTD");
                            for (row = 2; row < range.Rows.Count; row++)
                            {
                                DataRow dr = dt.NewRow();
                                for (column = 1; column <= range.Columns.Count; column++)
                                {
                                    dr[column - 1] = (range.Cells[row, column] as Excel.Range).Value2.ToString();

                                    if ((column == 4) || (column == 19) || (column == 20))
                                        if (dr[column - 1] != null)
                                            if (dr[column - 1].ToString() != "")
                                                dr[column - 1] = DateTime.FromOADate(Convert.ToDouble(dr[column - 1].ToString()));
                                }
                                dt.Rows.Add(dr);
                                dt.AcceptChanges();
                            }

                            Ds.Tables.Add(dt);
                        }
                        #endregion

                        workbook.Close(true, Type.Missing, Type.Missing);
                        excelApp.Quit();
                    }
                    return Ds;
                }
                catch (Exception ex1)
                {
                    workbook.Close(true, Type.Missing, Type.Missing);
                    excelApp.Quit();
                    return Ds;
                }
            }
        }
    }

    /// <summary>
    /// Class for generator of Excel file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class ExportToExcel<T, U>
        where T : class
        where U : List<T>
    {
        public List<T> dataToPrint;
        // Excel object references.
        private Excel.Application _excelApp = null;
        private Excel.Workbooks _books = null;
        private Excel._Workbook _book = null;
        private Excel.Sheets _sheets = null;
        private Excel._Worksheet _sheet = null;
        private Excel.Range _range = null;
        private Excel.Font _font = null;
        // Optional argument variable
        private object _optionalValue = Missing.Value;

        /// <summary>
        /// Generate report and sub functions
        /// </summary>
        public void GenerateReport()
        {
            try
            {
                if (dataToPrint != null)
                {
                    if (dataToPrint.Count != 0)
                    {
                        Mouse.SetCursor(Cursors.Wait);
                        CreateExcelRef();
                        FillSheet();
                        OpenReport();
                        Mouse.SetCursor(Cursors.Arrow);
                    }
                }
            }
            catch (Exception ex1)
            {
                //MessageBox.Show("Error while generating Excel report");
            }
            finally
            {
                ReleaseObject(_sheet);
                ReleaseObject(_sheets);
                ReleaseObject(_book);
                ReleaseObject(_books);
                ReleaseObject(_excelApp);
            }
        }
        /// <summary>
        /// Make MS Excel application visible
        /// </summary>
        private void OpenReport()
        {
            _excelApp.Visible = true;
        }
        /// <summary>
        /// Populate the Excel sheet
        /// </summary>
        private void FillSheet()
        {
            object[] header = CreateHeader();
            WriteData(header);
        }
        /// <summary>
        /// Write data into the Excel sheet
        /// </summary>
        /// <param name="header"></param>
        private void WriteData(object[] header)
        {
            object[,] objData = new object[dataToPrint.Count, header.Length];

            for (int j = 0; j < dataToPrint.Count; j++)
            {
                var item = dataToPrint[j];
                for (int i = 0; i < header.Length; i++)
                {
                    var y = typeof(T).InvokeMember(header[i].ToString(), BindingFlags.GetProperty, null, item, null);

                    ExportItem MyExportItem = new ExportItem();

                    objData[j, i] = (y == null) ? "" : MyExportItem.GetItemData(y, y.GetType());
                }
            }
            AddExcelRows("A2", dataToPrint.Count, header.Length, objData);
            AutoFitColumns("A1", dataToPrint.Count + 1, header.Length);
        }
        /// <summary>
        /// Method to make columns auto fit according to data
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        private void AutoFitColumns(string startRange, int rowCount, int colCount)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.Columns.AutoFit();
        }
        /// <summary>
        /// Create header from the properties
        /// </summary>
        /// <returns></returns>
        private object[] CreateHeader()
        {
            PropertyInfo[] headerInfo = typeof(T).GetProperties();

            // Create an array for the headers and add it to the
            // worksheet starting at cell A1.
            List<object> objHeaders = new List<object>();
            for (int n = 0; n < headerInfo.Length; n++)
            {
                objHeaders.Add(headerInfo[n].Name);
            }

            var headerToAdd = objHeaders.ToArray();
            AddExcelRows("A1", 1, headerToAdd.Length, headerToAdd);
            SetHeaderStyle();

            return headerToAdd;
        }
        /// <summary>
        /// Set Header style as bold
        /// </summary>
        private void SetHeaderStyle()
        {
            _font = _range.Font;
            _font.Bold = true;
        }
        /// <summary>
        /// Method to add an excel rows
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        /// <param name="values"></param>
        private void AddExcelRows(string startRange, int rowCount, int colCount, object values)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.set_Value(_optionalValue, values);
        }
        /// <summary>
        /// Create Excel applicaiton parameters instances
        /// </summary>
        private void CreateExcelRef()
        {
            _excelApp = new Excel.Application();
            _books = (Excel.Workbooks)_excelApp.Workbooks;
            _book = (Excel._Workbook)(_books.Add(_optionalValue));
            _sheets = (Excel.Sheets)_book.Worksheets;
            _sheet = (Excel._Worksheet)(_sheets.get_Item(1));
        }
        /// <summary>
        /// Release unused COM objects
        /// </summary>
        /// <param name="obj"></param>
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                //MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }

    public class ExportItem
    {
        public ExportItem()
        {
        }

        public string GetItemData(object Item, Type DataType)
        {
            string retval;

            retval = null;

            switch (DataType.ToString())
            {
                case "NewApp.BusinessTier.Models.CustomerStatus":
                    retval = ((CustomerStatus)Item).CustomerStatusId.ToString();
                    break;

                case "NewApp.BusinessTier.Models.GenderType":
                    retval = ((GenderType)Item).GenderTypeId.ToString();
                    break;

                case "NewApp.BusinessTier.Models.IDProofType":
                    retval = ((IDProofType)Item).IDProofTypeId.ToString();
                    break;

                default:
                    retval = Item.ToString();
                    break;
            }

            return retval;
        }
    }
}
