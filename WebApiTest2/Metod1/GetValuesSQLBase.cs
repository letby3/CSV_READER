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
    public abstract class GetValuesSQLBase
    {        
        protected string con_string_sql;
        protected SqlConnection Sqlcon;

        public abstract void GetSQL(string file_name_SQL);
    }
}