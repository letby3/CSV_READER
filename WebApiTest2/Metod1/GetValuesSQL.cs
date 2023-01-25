/*
    Получить значения из таблицы Values по имени файла
*/

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

    public class GetValuesSQL : GetValuesSQLBase
    {
        public struct get_val
        {
            public string[] Date;
            public int[] Set1;
            public float[] Set2;

            public get_val(string[] Date, int[] Set1, float[] Set2)
            {
                this.Date = Date;
                this.Set1 = Set1;
                this.Set2 = Set2;
            }
        }

        public get_val Values;

        public GetValuesSQL(string con_string_sql)
        {
            string proc = @"CREATE PROCEDURE sp_GetValuesByName
                                @FileName1 varchar(9),
                                @Date Date out,
                                @Set1 int out,
                                @Set2 float out
                            AS
                                SELECT @Date = Date, @Set1 = Set1, @Set2 = Set2 FROM Values1 WHERE FileName LIKE '%' + @FileName1 + '%';";

            Sqlcon = new SqlConnection(con_string_sql);

            try
            {
                
                Sqlcon.Open();
                SqlCommand command = new SqlCommand(proc, Sqlcon);
                command.ExecuteNonQuery();
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

        public override void GetSQL(string file_name_SQL)
        {
            string[] Date;
            int[] Set1;
            float[] Set2;
            try
            {
                Sqlcon.Open();
                SqlCommand command = new SqlCommand("sp_GetValuesByName", Sqlcon);

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Date",
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    Direction = System.Data.ParameterDirection.Output
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Set1",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Set2",
                    SqlDbType = System.Data.SqlDbType.Float,
                    Direction = System.Data.ParameterDirection.Output
                });
                command.Parameters.Add("@FileName1", System.Data.SqlDbType.VarChar).Value = file_name_SQL;

                command.ExecuteNonQuery();
                object timeDate = command.Parameters["@Date"].Value;                
                
                Date = (string[])command.Parameters["@Date"].Value;
                Set1 = (int[])command.Parameters["@Set1"].Value;
                Set2 = (float[])command.Parameters["@Set2"].Value;
                Values = new get_val(Date, Set1, Set2);

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