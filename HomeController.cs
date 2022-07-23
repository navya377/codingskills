using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ToString());
            string data = string.Empty;
       // string html=

        public ActionResult Index()
        {
            return View();
        }
        public FileResult DownloadExcel()
        {
            string path = "/Doc/Users.xlsx";
            return File(path, "application/vnd.ms-excel", "Users.xlsx");
        }

        [HttpPost]
        public JsonResult UploadBarcodeAExcel(HttpPostedFileBase FileUpload)
        {
            string pathToExcelFile = string.Empty;
            string data = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3]{
                                new DataColumn("SupplierID", typeof(int)),
                                new DataColumn("SKU",typeof(string)),
            new DataColumn("Barcode",typeof(string)) });

            if (FileUpload != null)
            {

                string filename = FileUpload.FileName;
                string targetpath = Server.MapPath("~/");
                FileUpload.SaveAs(targetpath + filename);
                pathToExcelFile = targetpath + filename;

            }
            con.Close();
            con.Open();
            string csvData = System.IO.File.ReadAllText(pathToExcelFile);
            try
            {
                foreach (string row in csvData.Split('\n'))
                {

                    if (!string.IsNullOrEmpty(row))
                    {
                        dt.Rows.Add();
                        int i = 0;
                        if (dt.Rows.Count > 0)
                        {
                            //Execute a loop over the columns.
                            foreach (string cell in row.Split(','))
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell;
                                i++;

                            }
                        }
                    }


                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured while getting csvData", e.StackTrace);
            }

            try
            {
               // using (var conn =
               //new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ToString()))
               // {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "dbo.barcodeA ";

                        sqlBulkCopy.ColumnMappings.Add("SupplierID", "SupplierID");
                        sqlBulkCopy.ColumnMappings.Add("SKU", "SKU");
                        sqlBulkCopy.ColumnMappings.Add("Barcode", "Barcode");

                      //  conn.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        data = "Excel uploaded successfully";
                        //conn.Close();

                    } 

                //}
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured while inserting data to database", e.StackTrace);
                data = "Fail to Upload Excel";

            }
            finally
            {
                con.Close();
            }
           
            
            return Json(data);

        }
        [HttpPost]
        public JsonResult UploadBarcodeBExcel(HttpPostedFileBase FileUpload)
        {
            string pathToExcelFile = string.Empty;
            string data = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3]{
                                new DataColumn("SupplierID", typeof(int)),
                                new DataColumn("SKU",typeof(string)),
            new DataColumn("Barcode",typeof(string))});
            
            if (FileUpload != null)
            {

                string filename = FileUpload.FileName;
                string targetpath = Server.MapPath("~/");
                FileUpload.SaveAs(targetpath + filename);
                pathToExcelFile = targetpath + filename;

            }
            con.Close();
            con.Open();
            string csvData = System.IO.File.ReadAllText(pathToExcelFile);
            try
            {
                foreach (string row in csvData.Split('\n'))
                {

                    if (!string.IsNullOrEmpty(row))
                    {
                        dt.Rows.Add();
                        int i = 0;
                        if (dt.Rows.Count > 0)
                        {
                            //Execute a loop over the columns.
                            foreach (string cell in row.Split(','))
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell;
                                i++;

                            }
                        }
                    }
                }
            } 
            catch (Exception e)
            {
                Console.WriteLine("Error occured while getting csvData",e.StackTrace);
            }
            try
            {
               
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "dbo.barcodeB";

                        sqlBulkCopy.ColumnMappings.Add("SupplierID", "SupplierID");
                        sqlBulkCopy.ColumnMappings.Add("SKU", "SKU");
                        sqlBulkCopy.ColumnMappings.Add("Barcode", "Barcode");

                        sqlBulkCopy.WriteToServer(dt);
                        data = "Excel uploaded successfully";

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured while inserting data to database", e.StackTrace);
                data = "Fail to upload Excel";

            }
            finally
            {
                con.Close();
            }



            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult UploadCatalogAExcel(HttpPostedFileBase FileUpload)
        {
            string pathToExcelFile = string.Empty;

            string data = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[2]{
                                new DataColumn("SKU", typeof(string)),
                                new DataColumn("Description",typeof(string)) });

            if (FileUpload != null)
            {

                string filename = FileUpload.FileName;
                string targetpath = Server.MapPath("~/");
                FileUpload.SaveAs(targetpath + filename);
                pathToExcelFile = targetpath + filename;

            }
            con.Close();
            con.Open();
          
            string csvData = System.IO.File.ReadAllText(pathToExcelFile);
            try
            {
                foreach (string row in csvData.Split('\n'))
                {

                    if (!string.IsNullOrEmpty(row))
                    {
                        dt.Rows.Add();
                        int i = 0;
                        if (dt.Rows.Count > 0)
                        {
                            //Execute a loop over the columns.
                            foreach (string cell in row.Split(','))
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell;
                                i++;

                            }
                        }
                    }
                }

            }catch(Exception e)
            {
                Console.WriteLine("Error occured while getting csvData", e.StackTrace);

            }
            try
            {
               
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "dbo.CatalogA ";

                        sqlBulkCopy.ColumnMappings.Add("SKU", "SKU");
                     //   if(sqlBulkCopy.ColumnMappings.Add.Contains(string.IsNullOrEmpty))
                        sqlBulkCopy.ColumnMappings.Add("Description", "Description");
                        
                        sqlBulkCopy.WriteToServer(dt);
                        data = "Excel uploaded successfully";

                        con.Close();
                }
            }
             
            catch (Exception e)
            {
                Console.WriteLine("Error occured while inserting data to database", e.StackTrace);
                data = "Fail to upload Excel";
            }
            finally
            {
                con.Close();
            }
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult UploadCatalogBExcel(HttpPostedFileBase FileUpload)
        {
            // string qry = "select * from Products";
            string pathToExcelFile = string.Empty;


            //  Console.ReadLine();
            string data = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[2]{
                                new DataColumn("SKU",typeof(string)),
            new DataColumn("Description",typeof(string))});

            if (FileUpload != null)
            {

                string filename = FileUpload.FileName;
                string targetpath = Server.MapPath("~/");
                FileUpload.SaveAs(targetpath + filename);
                pathToExcelFile = targetpath + filename;

            }
            con.Close();
            con.Open();
            string csvData = System.IO.File.ReadAllText(pathToExcelFile);
            try {
                foreach (string row in csvData.Split('\n'))
                {

                    if (!string.IsNullOrEmpty(row))
                    {
                        dt.Rows.Add();
                        int i = 0;
                        if (dt.Rows.Count > 0)
                        {
                            //Execute a loop over the columns.
                            foreach (string cell in row.Split(','))
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell;
                                i++;

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured while getting csvData", e.StackTrace);
            }
            try
            {
              
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "dbo.CatalogB ";
                        sqlBulkCopy.ColumnMappings.Add("SKU", "SKU");
                        sqlBulkCopy.ColumnMappings.Add("Description", "Description");
                        sqlBulkCopy.WriteToServer(dt);
                        data = "Excel uploaded successfully";

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured while inserting data to database", e.StackTrace);
                data = "Fail to upload Excel";
            }
            finally
            {
                con.Close();
            }

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult UploadSupplierAExcel(HttpPostedFileBase FileUpload)
        {
            string pathToExcelFile = string.Empty;
            DataTable dt = new DataTable();
            string data = string.Empty;
            dt.Columns.AddRange(new DataColumn[2]{
                                new DataColumn("ID", typeof(int)),
                                new DataColumn("Name",typeof(string)) });

            if (FileUpload != null)
            {

                string filename = FileUpload.FileName;
                string targetpath = Server.MapPath("~/");
                FileUpload.SaveAs(targetpath + filename);
                pathToExcelFile = targetpath + filename;

            }
            con.Close();
            con.Open();

            string csvData = System.IO.File.ReadAllText(pathToExcelFile);
            try
            {
                foreach (string row in csvData.Split('\n'))
                {

                    if (!string.IsNullOrEmpty(row))
                    {
                        dt.Rows.Add();
                        int i = 0;
                        if (dt.Rows.Count > 0)
                        {
                            //Execute a loop over the columns.
                            foreach (string cell in row.Split(','))
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell;
                                i++;

                            }
                        }
                    }
                }

            }
            catch(Exception e)
            {
                Console.WriteLine("Error occured while getting csvData", e.StackTrace);

            }
            try
            {
               
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "dbo.SupplierA ";

                        sqlBulkCopy.ColumnMappings.Add("ID", "ID");
                        sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                        sqlBulkCopy.WriteToServer(dt);
                        data = "Excel uploaded successfully";

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured while inserting data to database", e.StackTrace);
                data = "Fail to upload Excel";
            }
            finally
            {
                con.Close();
            }
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult UploadSupplierBExcel(HttpPostedFileBase FileUpload)
        {
            string pathToExcelFile = string.Empty;
            string data = string.Empty;

            DataTable dt = new DataTable();

            dt.Columns.AddRange(new DataColumn[2]{
                                new DataColumn("ID", typeof(int)),
                                new DataColumn("Name",typeof(string)) });

            if (FileUpload != null)
            {

                string filename = FileUpload.FileName;
                string targetpath = Server.MapPath("~/");
                FileUpload.SaveAs(targetpath + filename);
                pathToExcelFile = targetpath + filename;

            }

            con.Close();
            con.Open();

            string csvData = System.IO.File.ReadAllText(pathToExcelFile);
            try
            {
                foreach (string row in csvData.Split('\n'))
                {

                    if (!string.IsNullOrEmpty(row))
                    {
                        dt.Rows.Add();
                        int i = 0;
                        if (dt.Rows.Count > 0)
                        {
                            //Execute a loop over the columns.
                            foreach (string cell in row.Split(','))
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell;
                                i++;

                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error occured while getting csvData", e.StackTrace);

            }
            try
            {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "dbo.SupplierB ";

                        sqlBulkCopy.ColumnMappings.Add("ID", "ID");
                        sqlBulkCopy.ColumnMappings.Add("Name", "Name");

                        sqlBulkCopy.WriteToServer(dt);
                        data = "Excel uploaded successfully";

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured while inserting data to database", e.StackTrace);
                data = "Fail to upload Excel";
            }
            finally
            {
                con.Close();
            }
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult GetMergeCatalog()
        {
            string query = "EXEC " + "merge_catalog";
         SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ToString());
            SqlCommand cmd = new SqlCommand(query, con);
            string test = "EXEC " + "Upd_companyDetails";
            SqlCommand cmd1 = new SqlCommand(test, con);
            con.Close();

            con.Open();
            DataTable d_table1 = new DataTable();
            d_table1.Load(cmd1.ExecuteReader());

            string data = string.Empty;

            
            DataTable d_table = new DataTable();
            d_table.Load(cmd.ExecuteReader());
           // string DestinationFolder = @"C:\Users\User\source\repos\ConsoleApp1\ConsoleApp1\bin\Debug";
            string datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string FileFullPath =  "\\" + "_" + datetime + ".csv";
            string FileDelimiter = ",";
            StreamWriter sw = new StreamWriter(FileFullPath, false);

            // Write the Header Row to File

            int ColumnCount = d_table.Columns.Count;

            string csv = string.Empty;
            try
            {
                foreach (DataColumn column in d_table.Columns)
                {
                    csv += column.ColumnName + ',';
                }

                csv += "\r\n";

                foreach (DataRow row in d_table.Rows)
                {
                    foreach (DataColumn column in d_table.Columns)
                    {
                        string value = row[column].ToString().Replace('\r', ' ');
                        csv += value.Replace(",", ";") + ',';
                    }
                    csv += "\r\n";
                }


                sw.Write(csv);
                data = "Please check the file in your drive";

                sw.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to write data to excel", e);
                con.Close();
                data = "Failed to write data to excel";
            }

            return Json(data, JsonRequestBehavior.AllowGet);

        }



    }
}