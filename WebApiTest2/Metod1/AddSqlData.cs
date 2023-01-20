/*
The first method
Accepts a file of the form .csv, in which on each new line the value of the form:
{ Date and time in the format YYYY - MM - DD_hh - mm - ss};
{ Integer time value in seconds};
{ Indicator in the form of a floating - point number}
Validation:
• The date cannot be later than the current one and earlier than 01.01.2000
• The time cannot be less than 0
• The value of the indicator cannot be less than 0
• The number of rows cannot be less than 1 and more than 10,000
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Threading.Tasks;

namespace WebApiTest2.Metod1
{
    public class AddSqlData
    {
        protected string con_string_sql;
        protected SqlConnection Sqlcon;
        public AddSqlData(string connection_string_sql, SqlConnection sql_connnection) 
        {
            con_string_sql = connection_string_sql;
            Sqlcon = sql_connnection;
            
        }
        public AddSqlData(string connection_string_sql) 
        {
            con_string_sql = connection_string_sql;
            Sqlcon = new SqlConnection(connection_string_sql);            
        }


        ///Functions of the main implementation...........///////////////////////////////////////////////////////////////////////////////////////////////

        public void ValidationSQL()
        {
            try
            {
                int num_str_del = 0;
                string command_string = @"DELETE FROM Values1 WHERE Date > GETDATE();";                
                Sqlcon.Open();
                SqlCommand command = new SqlCommand(command_string, Sqlcon);
                num_str_del += command.ExecuteNonQuery();
                command.CommandText = @"DELETE FROM Values1 WHERE Date < '2000-01-01';";
                num_str_del += command.ExecuteNonQuery();
                command.CommandText = @"DELETE FROM Values1 WHERE Set1 < 0;";
                num_str_del += command.ExecuteNonQuery();
                command.CommandText = @"DELETE FROM Values1 WHERE Set2 < 0;";
                num_str_del += command.ExecuteNonQuery();
                
            }
            catch (SqlException ex)
            {                
                if (ex.Message.ToString() == "Invalid object name 'Values1'.")
                {
                    Sqlcon.Close();
                    _ = CreateTable1SQL();
                }
                Console.WriteLine("ERROR" + ex.Message);
            }
            finally
            {                
                Sqlcon.Close();
                Console.WriteLine("Подключение закрыто!");
            }
        }

        public void AddDataSQL(string file_path)
        {

            try
            {
                int num_str = 0;
                string command_str = "INSERT INTO Values1 (Date, Set1, Set2, FileName) VALUES (@Date1, @Set1, @Set2, @FileName1);";

                Sqlcon.Open();
                SqlCommand command = new SqlCommand(command_str, Sqlcon);

                using (StreamReader cin = new StreamReader(file_path))
                {
                    while (!cin.EndOfStream && num_str < 10000)
                    {
                        num_str++;
                        string[] str3 = cin.ReadLine().ToString().Split(';');
                        str3[0] = str3[0].Split('_')[0] + 'T' + str3[0].Split('_')[1].Replace('-', ':');

                        command.Parameters.Add(new SqlParameter("@Date1", str3[0]));
                        command.Parameters.Add(new SqlParameter("@Set1", int.Parse(str3[1])));
                        command.Parameters.Add(new SqlParameter("@Set2", float.Parse(str3[2])));
                        command.Parameters.Add(new SqlParameter("@FileName1", file_path.Split('\\').Last()));
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                }
                
                Sqlcon.Close();
                GetResultSQL();

            }
            catch (SqlException ex)
            {                
                if (ex.Message.ToString() == "Invalid object name 'Values1'.")
                {
                    Sqlcon.Close();
                    _ = CreateTable1SQL();
                }
                Console.WriteLine("ERROR" + ex.Message);
            }
            finally
            {
                Sqlcon.Close();                
                Console.WriteLine("Подключение закрыто!");
            }
        }

        public void GetResultSQL()
        {
            try
            {
                Sqlcon.Open();
                string command_str = "TRUNCATE TABLE.Results1";
                SqlCommand command = new SqlCommand(command_str, Sqlcon);
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO Results1 (alltime, mindate, avrtime, avrind, maxind, minind, strcnt, medind) SELECT (MAX(Set1) - MIN(Set1)), MIN(Date), AVG(Set1), AVG(Set2), MAX(Set2), MIN(Set2), COUNT(*), 0.0 FROM Values1";
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Message.ToString() == "Invalid object name 'Results1'.")
                {
                    Sqlcon.Close();
                    _ = CreateTable2SQL();
                }
                Console.WriteLine("ERROR" + ex.Message);
            }
            finally
            {
                Sqlcon.Close();
                Console.WriteLine("Подключение закрыто!");
            }
        }        

        protected async Task CreateTable1SQL()
        {
            try
            {
                await Sqlcon.OpenAsync();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "CREATE TABLE Values1 (Date DATETIME, Set1 INT NOT NULL, Set2 FLOAT NOT NULL, FileName VARCHAR(MAX) NOT NULL);",
                    Connection = Sqlcon
                };
                await command.ExecuteNonQueryAsync();
                Sqlcon.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR" + ex.Message);
            }
            finally
            {
                Sqlcon.Close();
                Console.WriteLine("Подключение закрыто!");
            }
        }

        public async Task CreateTable2SQL()
        {
            try
            {

                await Sqlcon.OpenAsync();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "CREATE TABLE Results1 (alltime INT, mindate DATETIME, avrtime INT, avrind FLOAT, medind FLOAT, maxind FLOAT, minind FLOAT, strcnt FLOAT);",
                    Connection = Sqlcon
                };
                await command.ExecuteNonQueryAsync();
                Sqlcon.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR" + ex.Message);
            }
            finally
            {
                Sqlcon.Close();
                Console.WriteLine("Подключение закрыто!");
            }
        }

        protected async Task CreatBaseSQL()
        {
            try
            {
                await Sqlcon.OpenAsync();
                SqlCommand command = new SqlCommand("CREATE DATABASE Database1", Sqlcon);
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR" + ex.Message);
            }
            finally
            {
                Sqlcon.Close();
                Console.WriteLine("Подключение закрыто!");
            }

        }

        ///Side implementation functions...........///////////////////////////////////////////////////////////////////////////////////////////////

        public void DelDataSQL(string file_path)
        {            
            try
            {
                string command_string = "DELETE FROM Values1 WHERE FileName='" + file_path.Split('\\').Last() + "';";                
                Sqlcon.Open();
                SqlCommand command = new SqlCommand(command_string, Sqlcon);
                command.ExecuteNonQuery();
                Sqlcon.Close();
                GetResultSQL();
            }
            catch (SqlException ex)
            {                
                if (ex.Message.ToString() == "Invalid object name 'Values1'.")
                {
                    Sqlcon.Close();
                    _ = CreateTable1SQL();
                }
                Console.WriteLine("ERROR" + ex.Message);
            }
            finally
            {                
                Sqlcon.Close();
                Console.WriteLine("Подключение закрыто!");
            }
        }                      

        

        public void ParseInTable1FromSQL(Table Table1, Table Table2)
        {
            try
            {
                Table1.Rows.Clear();
                Sqlcon.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Values1", Sqlcon);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TableRow r = new TableRow();
                        TableCell c1 = new TableCell(), c2 = new TableCell(), c3 = new TableCell(), c4 = new TableCell();
                        c1.Controls.Add(new LiteralControl(reader.GetDateTime(0).ToString()));
                        c2.Controls.Add(new LiteralControl(Convert.ToString(reader.GetInt32(1))));
                        c3.Controls.Add(new LiteralControl(reader.GetDouble(2).ToString()));
                        c4.Controls.Add(new LiteralControl(reader.GetString(3)));
                        r.Cells.Add(c1);
                        r.Cells.Add(c2);
                        r.Cells.Add(c3);
                        r.Cells.Add(c4);
                        Table1.Rows.Add(r);
                    }
                }                
                Sqlcon.Close();
                ParseInTable2FromSQL(Table2);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR" + ex.Message);
            }
            finally
            {
                Sqlcon.Close();                
                Console.WriteLine("Подключение закрыто!");
            }

        }

        public void ParseInTable2FromSQL(Table Table2)
        {
            Table2.Rows.Clear();
            try
            {
                Sqlcon.Open();                
                SqlCommand command = new SqlCommand("SELECT * FROM Results1", Sqlcon);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    reader.Read();

                    { 
                        TableRow r = new TableRow();
                        TableCell c1 = new TableCell(), c2 = new TableCell();
                        c1.Controls.Add(new LiteralControl("ALL TIME"));
                        if(!reader.IsDBNull(0))
                            c2.Controls.Add(new LiteralControl(reader.GetInt32(0).ToString()));
                        else
                            c2.Controls.Add(new LiteralControl("-"));
                        r.Cells.Add(c1);
                        r.Cells.Add(c2);
                        Table2.Rows.Add(r);
                    }

                    {
                        TableRow r = new TableRow();
                        TableCell c1 = new TableCell(), c2 = new TableCell();
                        c1.Controls.Add(new LiteralControl("MIN DATE"));
                        if (!reader.IsDBNull(1))
                            c2.Controls.Add(new LiteralControl(reader.GetDateTime(1).ToString()));
                        else
                            c2.Controls.Add(new LiteralControl("-"));
                        r.Cells.Add(c1);
                        r.Cells.Add(c2);
                        Table2.Rows.Add(r);
                    }
                    { 
                        TableRow r = new TableRow();
                        TableCell c1 = new TableCell(), c2 = new TableCell();
                        c1.Controls.Add(new LiteralControl("AVG TIME"));
                        if (!reader.IsDBNull(2))
                            c2.Controls.Add(new LiteralControl(reader.GetInt32(2).ToString()));
                        else
                            c2.Controls.Add(new LiteralControl("-"));
                        r.Cells.Add(c1);
                        r.Cells.Add(c2);
                        Table2.Rows.Add(r);
                    }
                    { 
                        TableRow r = new TableRow();
                        TableCell c1 = new TableCell(), c2 = new TableCell();
                        c1.Controls.Add(new LiteralControl("AVG INDEX"));
                        if (!reader.IsDBNull(3))
                            c2.Controls.Add(new LiteralControl(reader.GetDouble(3).ToString("#.#")));
                        else
                            c2.Controls.Add(new LiteralControl("-"));
                        r.Cells.Add(c1);
                        r.Cells.Add(c2);
                        Table2.Rows.Add(r);
                    }
                    { 
                        TableRow r = new TableRow();
                        TableCell c1 = new TableCell(), c2 = new TableCell();
                        c1.Controls.Add(new LiteralControl("MEDIAN INDEX"));
                        if (!reader.IsDBNull(4))
                            c2.Controls.Add(new LiteralControl(reader.GetDouble(4).ToString("#.#")));
                        else
                            c2.Controls.Add(new LiteralControl("-"));
                        r.Cells.Add(c1);
                        r.Cells.Add(c2);
                        Table2.Rows.Add(r);
                    }
                    { 
                        TableRow r = new TableRow();
                        TableCell c1 = new TableCell(), c2 = new TableCell();
                        c1.Controls.Add(new LiteralControl("MAX INDEX"));
                        if (!reader.IsDBNull(5))
                            c2.Controls.Add(new LiteralControl(reader.GetDouble(5).ToString("#.#")));
                        else
                            c2.Controls.Add(new LiteralControl("-"));
                        r.Cells.Add(c1);
                        r.Cells.Add(c2);
                        Table2.Rows.Add(r);
                    }
                    { 
                        TableRow r = new TableRow();
                        TableCell c1 = new TableCell(), c2 = new TableCell();
                        c1.Controls.Add(new LiteralControl("MIN INDEX"));
                        if (!reader.IsDBNull(6))
                            c2.Controls.Add(new LiteralControl(reader.GetDouble(6).ToString("#.#")));
                        else
                            c2.Controls.Add(new LiteralControl("-"));
                        r.Cells.Add(c1);
                        r.Cells.Add(c2);
                        Table2.Rows.Add(r);
                    }
                    { 
                        TableRow r = new TableRow();
                        TableCell c1 = new TableCell(), c2 = new TableCell();
                        c1.Controls.Add(new LiteralControl("STR CNT"));
                        if (!reader.IsDBNull(7))
                            c2.Controls.Add(new LiteralControl(reader.GetDouble(7).ToString("#.#")));
                        else
                            c2.Controls.Add(new LiteralControl("-"));
                        r.Cells.Add(c1);
                        r.Cells.Add(c2);
                        Table2.Rows.Add(r);
                    }

                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("ERROR" + ex.Message);
            }
            finally
            {
                Sqlcon.Close();
                Console.WriteLine("Подключение закрыто!");
            }

        }
    }
}