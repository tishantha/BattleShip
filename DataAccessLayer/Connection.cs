using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;

namespace DataAccessLayer
{
    public static class Connection
    {
        public static SqlXml ConvertStringToSqlXml(string Data)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var xmlWriter = XmlWriter.Create(memoryStream))
                {
                    xmlWriter.WriteString(Data);
                    return new System.Data.SqlTypes.SqlXml(memoryStream);
                }
            }
        }

        public static DataSet SelectDataReturningDataSet(string storedProcedureName, Dictionary<string, object> inputParameter)
        {
            DataSet dataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = storedProcedureName;
            command.Connection = OpenConnection();
            command.CommandTimeout = 0;
            adapter.SelectCommand = command;

            foreach (KeyValuePair<string, object> items in inputParameter)
            {
                SqlParameter parameter = new SqlParameter(items.Key, items.Value == null ? DBNull.Value : items.Value);
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);
            }
            adapter.Fill(dataSet);
            CloseConnection(command.Connection);
            return dataSet;
        }

        public static DataTable SelectDataReturningTable(string storedProcedureName, Dictionary<string, object> inputParameter)
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = storedProcedureName;
            command.Connection = OpenConnection();
            command.CommandTimeout = 0;
            adapter.SelectCommand = command;

            foreach (KeyValuePair<string, object> items in inputParameter)
            {
                SqlParameter parameter = new SqlParameter(items.Key, items.Value == null ? DBNull.Value : items.Value);
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);
            }
            adapter.Fill(data);
            CloseConnection(command.Connection);
            return data;
        }

        public static DataTable SelectDataReturningTable(SqlConnection objCon, string storedProcedureName, Dictionary<string, object> inputParameter)
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = storedProcedureName;
            command.Connection = objCon;
            command.CommandTimeout = 0;
            adapter.SelectCommand = command;

            foreach (KeyValuePair<string, object> items in inputParameter)
            {
                SqlParameter parameter = new SqlParameter(items.Key, items.Value == null ? DBNull.Value : items.Value);
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);
            }
            adapter.Fill(data);
            CloseConnection(command.Connection);
            return data;
        }

        public static string PopulateData(SqlConnection objCon, string storedProcedureName, Dictionary<string, object> inputParameter, Dictionary<string, object> outputParameter, Dictionary<string, object> outputParameterValues)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = objCon;
                command.CommandTimeout = 0;
                string returnMsg = "";
                foreach (KeyValuePair<string, object> items in inputParameter)
                {
                    SqlParameter parameter = new SqlParameter(items.Key, items.Value == null ? DBNull.Value : items.Value);
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);
                }

                foreach (KeyValuePair<string, object> items in outputParameter)
                {
                    if (items.Value.GetType().ToString() == "System.Data.SqlClient.SqlParameter")
                    {
                        SqlParameter par = (SqlParameter)items.Value;
                        SqlParameter parameter = new SqlParameter(items.Key, par.SqlDbType, par.Size);
                        parameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parameter);
                    }
                    else
                    {
                        ArrayList parameterItem = (ArrayList)items.Value;
                        SqlParameter parameter = new SqlParameter(items.Key, (SqlDbType)parameterItem[0], (int)parameterItem[1]);
                        parameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parameter);
                    }
                }

                Object objRet = command.ExecuteScalar();

                if (objRet != null)
                    returnMsg = objRet.ToString();
                else
                    returnMsg = "";

                CloseConnection(command.Connection);

                foreach (SqlParameter parameter in command.Parameters)
                {
                    if (parameter.Direction == ParameterDirection.Output)
                        outputParameterValues.Add(parameter.ParameterName, parameter.Value);
                }

                return returnMsg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static SqlConnection OpenConnection()
        {
            SqlConnection _conn = new SqlConnection();
            _conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            _conn.Open();
            return _conn;
        }

        public static void CloseConnection(SqlConnection _conn)
        {
            if (_conn.State == ConnectionState.Open)
                _conn.Close();
        }

    }
}
