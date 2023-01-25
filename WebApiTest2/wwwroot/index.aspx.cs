using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebApiTest2.Metod1;

namespace WebApiTest2.wwwroot
{

    public partial class index : System.Web.UI.Page
    {
        
        protected static string ConString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Пользователь\source\repos\WebApiTest2\WebApiTest2\App_Data\Database1.mdf;Integrated Security=True";
        protected AddSqlData add_sql_method = new AddSqlData(ConString);
        GetJsonSQL fileSave = new GetJsonSQL(ConString, "");

        protected void Page_Load(object sender, EventArgs e)
        {                        
            add_sql_method.ParseInTable1FromSQL(Table1, Table2);            
        }
        
        public void ListBoxCSV()
        {
            ArrayList sr = new ArrayList();
            string path = Request.PhysicalApplicationPath + @"uploads\";
            string[] dirFilesStr = Directory.GetFiles(path);            
            for (int i = 0; i < dirFilesStr.Length; i++)
                if (dirFilesStr[i].Contains(".csv"))
                    sr.Add(dirFilesStr[i]);
            ListBox1.DataSource = sr;
            ListBox1.DataBind();
        }      

        protected void UploadDataButton_Click(object sender, EventArgs e)
        {
            //fileSave.Save("Test2.csv", Request.PhysicalApplicationPath + @"\SaveJson", LabelTest);
            string saveDir = @"uploads\";
            string appPath = Request.PhysicalApplicationPath;

            if (FileUpload1.HasFile)
                FileUpload1.SaveAs(appPath + saveDir + FileUpload1.FileName);                
            
            ListBoxCSV();
                                    
        }        

        protected void ButtonListboxAdd_Click(object sender, EventArgs e)
        {
            if(ListBox1.SelectedIndex > -1)
            { 
                add_sql_method.DelDataSQL(ListBox1.SelectedItem.Text);
                add_sql_method.AddDataSQL(ListBox1.SelectedItem.Text);
            }
            add_sql_method.ValidationSQL();
            add_sql_method.GetResultSQL();
            add_sql_method.ParseInTable1FromSQL(Table1, Table2);
           
        }

        protected void ButtonListboxDel_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedIndex > -1)
                add_sql_method.DelDataSQL(ListBox1.SelectedItem.Text);            
            add_sql_method.ParseInTable1FromSQL(Table1, Table2);          
        }

        protected void ExportJsonButton_Click(object sender, EventArgs e)
        {
            xportinfo.Text = DropDownList1.SelectedIndex.ToString();
            if (DropDownList1.SelectedIndex == 0) 
            {
                xportinfo.Text = fileSave.Save(FileNameSort.Text, Request.PhysicalApplicationPath + @"\SaveJson");
            }
            else if (DropDownList1.SelectedIndex == 1) 
            {
                string[] date_drop_list = FileNameSort.Text.Split(';');
                if (date_drop_list.Length == 2)
                    xportinfo.Text = fileSave.Save(date_drop_list[0], date_drop_list[1], Request.PhysicalApplicationPath + @"\SaveJson");
                else
                    xportinfo.Text = "INCORRECT DATE";
            }
            else if (DropDownList1.SelectedIndex == 2)
            {
                string[] set1_drop_list = FileNameSort.Text.Split(';');
                if (set1_drop_list.Length == 2)
                    xportinfo.Text = fileSave.Save(Int32.Parse(set1_drop_list[0]), Int32.Parse(set1_drop_list[1]), Request.PhysicalApplicationPath + @"\SaveJson");
                else
                    xportinfo.Text = "INCORRECT DATE";
            }
            else if (DropDownList1.SelectedIndex == 3)
            {
                string[] set2_drop_list = FileNameSort.Text.Split(';');
                float[] set2_float = { float.Parse(set2_drop_list[0]), float.Parse(set2_drop_list[1]) };                
                if (set2_drop_list.Length == 2)
                    xportinfo.Text = fileSave.Save(set2_float[0], set2_float[1], Request.PhysicalApplicationPath + @"\SaveJson");
                else
                    xportinfo.Text = "INCORRECT DATE";
            }

        }
    }
}