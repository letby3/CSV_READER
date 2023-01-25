using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Text;

namespace WebApiTest2.Metod1
{
    public class GetJsonSQL : GetJsonSQLBase
    {
        public GetJsonSQL(string connection_string_sql, string file_name, SqlConnection sql_connnection)
        {
            con_string_sql = connection_string_sql;
            Sqlcon = sql_connnection;
        }

        public GetJsonSQL(string connection_string_sql, string file_name)
        {
            con_string_sql = connection_string_sql;
            Sqlcon = new SqlConnection(connection_string_sql);
        }

        public override string Save(string FileName, string save_path) 
        {            
            string command1 = @"INSERT INTO Results1 (alltime, mindate, avrtime, avrind, maxind, minind, strcnt, medind, ID) SELECT (MAX(Set1) - MIN(Set1)), MIN(Date), AVG(Set1), AVG(Set2), MAX(Set2), MIN(Set2), COUNT(*), (SELECT TOP(1) PERCENTILE_DISC(0.5) WITHIN GROUP (ORDER BY Set2) OVER() FROM Values1 WHERE FileName='" + FileName + "'), 2 FROM Values1 WHERE FileName='" + FileName + "'";                            
            string command2 = "SELECT * FROM Results1 WHERE ID = 2 FOR JSON PATH";
            string del_command = "DELETE Results1 WHERE ID=2";

            return Save_Prototype(command1, command2, del_command, save_path);
        }

        public override string Save(string beg_date, string end_date, string save_path)
        {            
            string command1 = @"INSERT INTO Results1 (alltime, mindate, medind, avrtime, avrind, maxind, minind, strcnt, ID) SELECT(MAX(Set1) - MIN(Set1)), MIN(Date), (SELECT TOP(1) PERCENTILE_DISC(0.5) WITHIN GROUP(ORDER BY Set2) OVER() FROM Values1 WHERE Date BETWEEN '" + beg_date + "' AND '" + end_date + "'), AVG(Set1), AVG(Set2), MAX(Set2), MIN(Set2), COUNT(*), 3 FROM Values1 WHERE Date BETWEEN '" + beg_date + "' AND '" + end_date + "'";
            string command2 = "SELECT * FROM Results1 WHERE ID = 3 FOR JSON PATH";
            string del_command = "DELETE Results1 WHERE ID=3";

            return Save_Prototype(command1, command2, del_command, save_path);
        }

        public override string Save(int beg_set1, int end_set1, string save_path)
        {            
            string command1 = @"INSERT INTO Results1 (alltime, mindate, medind, avrtime, avrind, maxind, minind, strcnt, ID) SELECT (MAX(Set1) - MIN(Set1)), MIN(Date), (SELECT TOP(1) PERCENTILE_DISC(0.5) WITHIN GROUP (ORDER BY Set2) OVER() FROM Values1 WHERE Set1 BETWEEN " + beg_set1.ToString() + " AND " + end_set1.ToString() + "), AVG(Set1), AVG(Set2), MAX(Set2), MIN(Set2), COUNT(*), 4 FROM Values1 WHERE Set1 BETWEEN " + beg_set1.ToString() + " AND " + end_set1.ToString() + "";
            string command2 = "SELECT * FROM Results1 WHERE ID = 4 FOR JSON PATH";
            string del_command = "DELETE Results1 WHERE ID=4";

            return Save_Prototype(command1, command2, del_command, save_path);
        }

        public override string Save(float beg_set2, float end_set2, string save_path)
        {                        
            string command1 = @"INSERT INTO Results1 (alltime, mindate, medind, avrtime, avrind, maxind, minind, strcnt, ID) SELECT (MAX(Set1) - MIN(Set1)), MIN(Date), (SELECT TOP(1) PERCENTILE_DISC(0.5) WITHIN GROUP (ORDER BY Set2) OVER() FROM Values1 WHERE Set2 BETWEEN " + beg_set2.ToString().Replace(',', '.') + " AND " + end_set2.ToString().Replace(',', '.') + "), AVG(Set1), AVG(Set2), MAX(Set2), MIN(Set2), COUNT(*), 5 FROM Values1 WHERE Set2 BETWEEN " + beg_set2.ToString().Replace(',', '.') + " AND " + end_set2.ToString().Replace(',', '.') + "";                
            string command2 = "SELECT * FROM Results1 WHERE ID = 5 FOR JSON PATH";
            string del_command = "DELETE Results1 WHERE ID=5";

            return Save_Prototype(command1, command2, del_command, save_path);            
        }

        protected override string Save_Prototype(string command1, string command2, string del_command, string save_path) 
        {
            string json_file_name = "Error.json";
            try
            {
                Sqlcon.Open();
                SqlCommand command = new SqlCommand(del_command, Sqlcon);
                
                command.ExecuteNonQuery();

                command.CommandText = command1;
                command.ExecuteNonQuery();

                string data_json = null;
                
                command.CommandText = command2;


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        json_file_name = reader.GetName(0);
                        data_json = reader.GetString(0);
                    }
                }

                json_file_name = json_file_name + DateTime.Now.ToString().Replace(" ", "").Replace('.', '_').Replace(':', '_') + ".json";

                using (StreamWriter WriteFile = new StreamWriter((save_path + @"\" + json_file_name), true, Encoding.ASCII))
                {
                    WriteFile.Write(data_json + '\n');
                    WriteFile.Close();
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
            return json_file_name;


        }
    }
}