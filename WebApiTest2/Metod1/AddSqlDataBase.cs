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
    public abstract class AddSqlDataBase
    {
        protected string con_string_sql;
        protected SqlConnection Sqlcon;

        /// <summary>
        /// Добавляет в БД данные по имени загруженного файла
        /// </summary>
        /// <param name="file_path">путь к файлу + имя файла</param>
        public abstract void AddDataSQL(string file_path);

        /// <summary>
        /// Создаёт Таблицу Results1 в случае, если она отсутствует
        /// </summary>
        public abstract void CreateTable2SQL();

        /// <summary>
        /// Удаляет данные из БД по имени файла
        /// </summary>
        /// <param name="file_path"></param>
        public abstract void DelDataSQL(string file_path);

        /// <summary>
        /// Добавляет в таблицу Results1 по данным таблицы Values1
        /// </summary>
        public abstract void GetResultSQL();

        /// <summary>
        /// Добавляет данные в таблицу Values в пользовательском интерфейсе
        /// </summary>
        /// <param name="Table1">Ссылка на таблицу 1 Values1</param>
        /// <param name="Table2">Ссылка на таблицу 2 Results1</param>
        public abstract void ParseInTable1FromSQL(Table Table1, Table Table2);
        public abstract void ParseInTable2FromSQL(Table Table2);

        /// <summary>
        /// Проверяет поступаемые в БД данные по заданным параметрам
        /// </summary>
        public abstract void ValidationSQL();

        /// <summary>
        /// Создает БД в случае её отсутсвия
        /// </summary>
        protected abstract void CreatBaseSQL();

        /// <summary>
        /// Создаёт Таблицу Values1 в случае, если она отсутствует
        /// </summary>
        protected abstract void CreateTable1SQL();
    }
}