namespace Amaryllis.Database {
    using MySQL = MySql.Data.MySqlClient;
    class Database {
        private static MySQL.MySqlConnection Connection;
        public static bool Init(string HostAddress, int HostPort, string[] AuthenticationDetails, string Database = "angw") {
            try {
                Connection = new MySQL.MySqlConnection();
                Connection.ConnectionString = "server=" + HostAddress + ";port=" + HostPort.ToString() + ";database=" + Database + ";uid=" + AuthenticationDetails[0] + ";password=" + AuthenticationDetails[1] + ";pooling=false;";
                Connection.Open();
            }

            catch (MySQL.MySqlException Exception) {
                if (Program.Debug) Utils.Log.Error("Database", Exception.Message);
                return false;
            }
            return true;
        }

        public static string GetObjectProperty(string Field, string Identifier, bool Primary = true, string Table = "Accounts") {
            lock (Connection) {
                try {
                    using (MySQL.MySqlCommand MySqlCommand = new MySQL.MySqlCommand("SELECT " + MySQL.MySqlHelper.EscapeString(Field) + " FROM " + Table + " WHERE " + (Primary ? "ID" : "Name") + " = '" + MySQL.MySqlHelper.EscapeString(Identifier) + "';", Connection)) {
                        using (MySQL.MySqlDataReader MySqlDataReader = MySqlCommand.ExecuteReader()) {
                            if (MySqlDataReader.Read()) return MySqlDataReader[0].ToString();
                            else return string.Empty;
                        }
                    }
                }

                catch (System.Exception Exception) {
                    if (Program.Debug) Utils.Log.Error("Database", Exception.Message);
                    return null;
                }
            }
        }

        public static string[] GetObjectProperties(string[] Fields, string Identifier, bool Primary = true) {
            lock (Connection) {
                string[] FieldValue = new string[Fields.Length];
                try {
                    using (MySQL.MySqlCommand MySqlCommand = new MySQL.MySqlCommand("SELECT * FROM Accounts WHERE " + (Primary ? "ID" : "Name") + "='" + MySQL.MySqlHelper.EscapeString(Identifier) + "'", Connection)) {
                        using (MySQL.MySqlDataReader MySqlDataReader = MySqlCommand.ExecuteReader()) {
                            int RowNumber = 0;
                            if (MySqlDataReader.Read()) {
                                foreach (string Field in Fields) {
                                    FieldValue[RowNumber] = MySqlDataReader[Field].ToString();
                                    RowNumber++;
                                }
                            }
                        }
                    }
                }

                catch (System.Exception Exception) {
                    if (Program.Debug) Utils.Log.Error("Database", Exception.Message);
                    return new string[] { };
                }
                return FieldValue;
            }
        }

        public static bool CheckForObjectExistence(string Identifier, bool Primary = true, string Table = "Accounts") {
            lock (Connection) {
                try {
                    using (MySQL.MySqlCommand MySqlCommand = new MySQL.MySqlCommand("SELECT COUNT(*) FROM " + Table + " WHERE " + (Primary ? "ID" : "Name") + " = '" + MySQL.MySqlHelper.EscapeString(Identifier) + "';", Connection)) {
                        return int.Parse(MySqlCommand.ExecuteScalar().ToString()) == 0 ? false : true;
                    }
                }

                catch (System.Exception Exception) {
                    if (Program.Debug) Utils.Log.Error("Database", Exception.Message);
                    return false;
                }
            }
        }

        public static void UpdateObjectProperty(string Field, string Value, string Identifier, bool Primary = true, string Table = "Accounts") {
            lock (Connection) {
                try {
                    using (MySQL.MySqlCommand MySqlCommand = new MySQL.MySqlCommand("UPDATE " + Table + " SET " + MySQL.MySqlHelper.EscapeString(Field) + "='" + MySQL.MySqlHelper.EscapeString(Value) + "'" + (Primary ? " WHERE ID='" : " WHERE Name='") + MySQL.MySqlHelper.EscapeString(Identifier) + "'", Connection)) {
                        MySqlCommand.ExecuteNonQuery();
                    }
                }

                catch (System.Exception Exception) {
                    if (Program.Debug) Utils.Log.Error("Database", Exception.Message);
                }
            }
        }

        public static void InsertObjectProperty(string Field, string[] Values, string Table = "Accounts") {
            lock (Connection) {
                try {
                    using (MySQL.MySqlCommand MySqlCommand = new MySQL.MySqlCommand("INSERT INTO " + Table + " VALUES('" + MySQL.MySqlHelper.EscapeString(Values[0]) + "','" + MySQL.MySqlHelper.EscapeString(Values[1]) + "','" + MySQL.MySqlHelper.EscapeString(Values[2]) + "'" + ")", Connection)) {
                        MySqlCommand.ExecuteNonQuery();
                    }
                }

                catch (System.Exception Exception) {
                    if (Program.Debug) Utils.Log.Error("Database", Exception.Message);
                }
            }
        }
    }
}
