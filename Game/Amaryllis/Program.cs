namespace Amaryllis {
    using Xml = System.Xml;
    class Program {
        public static bool Debug;
        private static int ServerID;
        private static int ServerPort;
        private static string ServerAddress;
        private static int MySQLPort;
        private static string MySQLHost;
        private static string MySQLUser;
        private static string MySQLPassword;
        
        static void Main() {
            WriteHeader();
            Utils.Log.Debug("XmlReader", "Reading configuration file");
            Xml.XmlReader ConfigurationFileReader = new Xml.XmlTextReader("configuration.xml");
            string ParentTab = null;
            while (ConfigurationFileReader.Read()) {
                switch (ConfigurationFileReader.NodeType) {
                    case Xml.XmlNodeType.Element:
                        ParentTab = ConfigurationFileReader.Name;
                        break;
                    case Xml.XmlNodeType.Text:
                        switch (ParentTab) {
                            case "server_id":
                                ServerID = int.Parse(ConfigurationFileReader.Value);
                                break;
                            case "server_address":
                                ServerAddress = ConfigurationFileReader.Value;
                                break;
                            case "server_port":
                                ServerPort = int.Parse(ConfigurationFileReader.Value);
                                break;
                            case "mysql_host":
                                MySQLHost = ConfigurationFileReader.Value;
                                break;
                            case "mysql_port":
                                MySQLPort = int.Parse(ConfigurationFileReader.Value);
                                break;
                            case "mysql_user":
                                MySQLUser = ConfigurationFileReader.Value;
                                break;
                            case "mysql_password":
                                MySQLPassword = ConfigurationFileReader.Value;
                                break;
                            case "debug":
                                Debug = ConfigurationFileReader.Value.ToLower() == "true" ? true : false;
                                break;
                        }
                        break;
                }
            }
            ConfigurationFileReader.Close();
            IServer Server = new IServer(ServerID, ServerAddress, ServerPort, MySQLHost, MySQLPort, new string[] { MySQLUser, MySQLPassword });
            Server.Run();
            System.Console.Read();
        }

        static void WriteHeader() {
            Utils.Log.Trace("|", "    -== -- ==-   ");
            Utils.Log.Trace("|", "   -== ----- ==- ");
            Utils.Log.Trace("|", "  -== ------- ==- ");
            Utils.Log.Trace("|", " -== Amaryllis ==- ");
            Utils.Log.Trace("|", "  -== ------- ==- ");
            Utils.Log.Trace("|", "   -== -2.5- ==- ");
            Utils.Log.Trace("|", "    -== -- ==-   ");
            System.Console.WriteLine();
        }
    }
}
