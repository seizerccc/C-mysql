using System.Data;
using MySql.Data.MySqlClient;

//you have to reference Mysql.Data.dll firstly 
//this method is only used on local(if you want to change, pay attention to your property of service of Mysql)

namespace WindowsFormsApp1
{
    public class MysqlConnector
    {
        string server, userid, password, database, port, charset = null;
        static MysqlConnector connector;

        public MysqlConnector() { }
        public MysqlConnector SetServer(string server)
        {
            this.server = server;
            return this;
        }
        public MysqlConnector SetUserID(string userid)
        {
            this.userid = userid;
            return this;
        }
        public MysqlConnector SetDataBase(string database)
        {
            this.database = database;
            return this;
        }
        public MysqlConnector SetPassword(string password)
        {
            this.password = password;
            return this;
        }
        public MysqlConnector SetPort(string port)
        {
            this.port = port;
            return this;
        }
        public MysqlConnector SetCharset(string charset)
        {
            this.charset = charset;
            return this;
        }

        private MySqlConnection GetMysqlConnection()
        {
            string M_str_sqlcon = string.Format("server={0};user id={1};password={2};database={3};port={4};Charset={5}", server, userid, password, database, port, charset);
            MySqlConnection myCon = new MySqlConnection(M_str_sqlcon);
            return myCon;
        }

        public void ExeUpdate(string M_str_sqlstr)
        {
            MySqlConnection mysqlcon = this.GetMysqlConnection();
            mysqlcon.Open();
            MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
            mysqlcom.ExecuteNonQuery();
            mysqlcom.Dispose();
            mysqlcon.Close();
            mysqlcon.Dispose();
        }

        public MySqlDataReader ExeQuery(string M_str_sqlstr)
        {
            MySqlConnection mysqlcon = this.GetMysqlConnection();
            MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
            mysqlcon.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            return mysqlread;
        }

        public static MysqlConnector GetConnector()
        {
            connector = new MysqlConnector();
            connector.SetServer("localhost")
                     .SetDataBase("mydb")
                     .SetUserID("root")
                     .SetPassword("******")// your password
                     .SetPort("3306")
                     .SetCharset("utf8");
            return connector;
        }
    }
}
