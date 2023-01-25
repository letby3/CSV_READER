/*
Returns Results in JSON format. 
Filters can be applied:
    • By file name
    • By the start time of the first operation (from, to)
    • By average (in the range)
    • By average time (in the range)
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
    public abstract class GetJsonSQLBase
    {
        protected string con_string_sql;
        protected string file_name;
        protected SqlConnection Sqlcon;

        /// <summary>
        /// По имени файла
        /// </summary>
        /// <param name="FileName">Имя файла</param>
        /// <param name="save_path">Путь сохранения</param>
        /// <returns></returns>
        public abstract string Save(string FileName, string save_path);
        /// <summary>
        /// По времени запуска первой операции (от, до)
        /// </summary>
        /// <param name="beg_date"></param>
        /// <param name="end_date"></param>
        /// <param name="save_path"></param>
        /// <returns></returns>
        public abstract string Save(string beg_date, string end_date, string save_path);
        /// <summary>
        /// По среднему показателю (в диапазоне)
        /// </summary>
        /// <param name="beg_set1"></param>
        /// <param name="end_set1"></param>
        /// <param name="save_path"></param>
        /// <returns></returns>
        public abstract string Save(int beg_set1, int end_set1, string save_path);
        /// <summary>
        /// По среднему времени (в диапазоне)
        /// </summary>
        /// <param name="beg_set2"></param>
        /// <param name="end_set2"></param>
        /// <param name="save_path"></param>
        /// <returns></returns>
        public abstract string Save(float beg_set2, float end_set2, string save_path);
        /// <summary>
        /// Абстракный метод
        /// </summary>
        /// <param name="command1"></param>
        /// <param name="command2"></param>
        /// <param name="del_command"></param>
        /// <param name="save_path"></param>
        /// <returns></returns>
        protected abstract string Save_Prototype(string command1, string command2, string del_command, string save_path);
    }
}