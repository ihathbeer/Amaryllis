using System;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Amaryllis
{
    class Database
    {
        private static MySqlConnection Connection;
        public static Boolean Init()
        {
            try
            {
                Connection = new MySqlConnection();
                Connection.ConnectionString = "server=localhost;port=3307;database=angw;uid=root;password=un&*qN3^et3n;";
                Connection.Open();
            }

            catch (MySqlException Exception)
            {
                Console.WriteLine(Exception.Message);
                return false;
            }

            return true;
        }

        public static String GetObjectProperty(String Field, String Identifier, Boolean Primary = true, String Table = "Accounts")
        {
            lock (Connection)
            {
                try
                {
                    using (MySqlCommand MySqlCommand = new MySqlCommand("SELECT " + MySqlHelper.EscapeString(Field) + " FROM " + Table + " WHERE " + (Primary ? "ID" : "Name") + " = '" + MySqlHelper.EscapeString(Identifier) + "';", Connection))
                    {
                        using (MySqlDataReader MySqlDataReader = MySqlCommand.ExecuteReader())
                        {
                            if (MySqlDataReader.Read()) return MySqlDataReader[0].ToString();
                            else return String.Empty;
                        }
                    }
                }

                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static String[] GetObjectProperties(String[] Fields, String Identifier, Boolean Primary = true)
        {
            lock (Connection)
            {
                String[] FieldValue = new String[Fields.Count()];
                try
                {
                    using (MySqlCommand MySqlCommand = new MySqlCommand("SELECT * FROM Accounts WHERE " + (Primary ? "ID" : "Name") + "='" + MySqlHelper.EscapeString(Identifier) + "'", Connection))
                    {
                        using (MySqlDataReader MySqlDataReader = MySqlCommand.ExecuteReader())
                        {
                            Int32 RowNumber = 0;
                            if (MySqlDataReader.Read())
                            {
                                foreach (String Field in Fields)
                                {
                                    FieldValue[RowNumber] = MySqlDataReader[Field].ToString();
                                    RowNumber++;
                                }
                            }
                        }
                    }
                }

                catch (Exception Exception)
                {
                    Console.WriteLine(Exception.ToString());
                    return new string[] { };
                }
                return FieldValue;
            }
        }

        public static Boolean CheckForObjectExistence(String Identifier, Boolean Primary = true, String Table = "Accounts")
        {
            lock (Connection)
            {
                try
                {
                    using (MySqlCommand MySqlCommand = new MySqlCommand("SELECT COUNT(*) FROM " + Table + " WHERE " + (Primary ? "ID" : "Name") + " = '" + MySqlHelper.EscapeString(Identifier) + "';", Connection))
                    {
                        return Int32.Parse(MySqlCommand.ExecuteScalar().ToString()) == 0 ? false : true;
                    }
                }

                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static void UpdateObjectProperty(String Field, String Value, String Identifier, Boolean Primary = true, String Table = "Accounts")
        {
            lock (Connection)
            {
                try
                {
                    using (MySqlCommand MySqlCommand = new MySqlCommand("UPDATE " + Table + " SET " + MySqlHelper.EscapeString(Field) + "='" + MySqlHelper.EscapeString(Value) + "'" + (Primary ? " WHERE ID='" : " WHERE Name='") + MySqlHelper.EscapeString(Identifier) + "'", Connection))
                    {
                        MySqlCommand.ExecuteNonQuery();
                    }
                }

                catch (Exception)
                {

                }
            }
        }

        public static void InsertObjectProperty(String Field, String[] Values, String Table = "Accounts")
        {
            lock (Connection)
            {
                try
                {
                    using (MySqlCommand MySqlCommand = new MySqlCommand("INSERT INTO " + Table + " VALUES('" + MySqlHelper.EscapeString(Values[0]) + "','" + MySqlHelper.EscapeString(Values[1]) + "','" + MySqlHelper.EscapeString(Values[2]) + "'" + ")", Connection))
                    {
                        MySqlCommand.ExecuteNonQuery();
                    }
                }

                catch (Exception)
                {

                }
            }
        }
    }
}
