using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace DomainLayer
{
    public static class Helper
    {

        public static XDocument ToXmlDocument(this DataTable dataTable, string rootName)
        {
            var XmlDocument = new XDocument
            {
                Declaration = new XDeclaration("1.0", "utf-8", "")
            };
            XmlDocument.Add(new XElement(rootName));
            foreach (DataRow row in dataTable.Rows)
            {
                XElement element = null;
                if (dataTable.TableName != null)
                {
                    element = new XElement(dataTable.TableName);
                }
                foreach (DataColumn column in dataTable.Columns)
                {
                    element.Add(new
                XElement(column.ColumnName, row[column].ToString().Trim(' ')));
                }
                if (XmlDocument.Root != null) XmlDocument.Root.Add(element);
            }

            return XmlDocument;
        }

        private static SqlXml CreateTxnXML(DataTable table)
        {
            table.TableName = "Table";
            MemoryStream ms = new MemoryStream();
            table.WriteXml(ms);
            ms.Position = 0;
            XmlTextReader xr = new XmlTextReader(ms, XmlNodeType.Document, null);
            SqlXml xmlRecDetails = new SqlXml(xr);
            return xmlRecDetails;
        }

        public static SqlXml CreateXML(DataTable dt)
        {
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int h = 0; h < dt.Columns.Count; h++)
                    {
                        if (dt.Rows[i][h].ToString() == string.Empty)
                        {
                            dt.Rows[i][h] = DBNull.Value;
                        }
                    }
                }
                return CreateTxnXML(dt);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Convert XML string to T type object and return as T object.
        /// </summary>
        /// <typeparam name="T">Object type to convert.</typeparam>
        /// <param name="xml">XML as string.</param>
        /// <returns>Converted T object.</returns>
        public static T XmlToObject<T>(String xml)
        {
            XmlDocument subProductWisePackagesXml = new XmlDocument();
            subProductWisePackagesXml.LoadXml(xml);
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeXmlNode(subProductWisePackagesXml));
        }


        /// <summary>
        /// Convert XML document to Json and return as string.
        /// </summary>
        /// <param name="xml">XML Document.</param>
        /// <returns>Json string</returns>
        public static String XmlWithoutRootToJson(String xml)
        {
            XmlDocument subProductWisePackagesXml = new XmlDocument();
            subProductWisePackagesXml.LoadXml(xml);
            return JsonConvert.SerializeXmlNode(subProductWisePackagesXml, Newtonsoft.Json.Formatting.None, true);
        }

        /// <summary>
        /// Convert XML string to Json and return Json as string.
        /// </summary>
        /// <param name="xml">Xml Document to convert.</param>
        /// <returns>Converted Json as string.</returns>
        public static String XmlToJson(String xml)
        {
            XmlDocument subProductWisePackagesXml = new XmlDocument();
            subProductWisePackagesXml.LoadXml(xml);
            return JsonConvert.SerializeXmlNode(subProductWisePackagesXml);
        }


        internal static DataTable ListToTable<T>(this List<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

    }
}
