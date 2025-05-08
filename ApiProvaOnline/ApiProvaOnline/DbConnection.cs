using MySql.Data.MySqlClient;

namespace ApiProvaOnline
{
    public static class DbConnection
    {

        public static string connectionstring = "Server=localhost;Database=ProvaOn;Uid=root";


        public static MySqlConnection GetConnection()
        {

            MySqlConnection connection = new MySqlConnection(connectionstring);

            connection.Open();

            return connection;

        }

        public static void CloseConnection(MySqlConnection connection)
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }

    }
}
