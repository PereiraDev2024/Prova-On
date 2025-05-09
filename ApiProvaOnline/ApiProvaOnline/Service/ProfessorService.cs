using System.Data;
using ApiProvaOnline.Model;
using MySql.Data.MySqlClient;

namespace ApiProvaOnline.Service
{
    public class ProfessorService
    {
        public void CadastrarProfessor(Professor professor)
        {
            string sql = $"insert to into Professor (Nome, Email, Senha, DataHoraRegistro) VALUES ('{professor.Nome}', '{professor.Email}','{professor.Senha}', '{professor.DataHoraRegistro}')";

            MySqlConnection connection = DbConnection.GetConnection();

         

            try
            {
                MySqlCommand command = new MySqlCommand(sql, connection);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;

            }
            finally
            {
                connection.Close();
            }
        }
        public bool RemoverProfessor (int Id)
        {
            string sql = $"delete from Professor where Id = '{Id}'";

            MySqlConnection connection = DbConnection.GetConnection();


            try
            {

                MySqlCommand command = new MySqlCommand(sql, connection);

                int LinhasAfetadas = command.ExecuteNonQuery();

                if (LinhasAfetadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            { 
                connection.Close();
            
            }

        }
        public Professor BuscarProfessor(string Id)
        {

            string sql = $"select into Professor where Id = '{Id}'";

            MySqlConnection connection =  DbConnection.GetConnection();

            try
            {

                MySqlCommand command = new MySqlCommand(sql, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    Professor professor = new Professor();

                    {
                        professor.Id = reader.GetInt32("id");
                        professor.Nome = reader.GetString("Nome");
                        professor.Email = reader.GetString("Email");
                        professor.DataHoraRegistro = reader.GetDateTime("DataHoraRegistro");


                    }

                    return professor;

                }


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();

            }
            return null;

        }

        public List<Professor> ListarProfessores()
        {
            string sql = $"select * from Professor orderby Nome ASC";

            MySqlConnection connection = DbConnection.GetConnection();


            try
            {
                List<Professor> professores = new List<Professor>();
                
                MySqlCommand command = new MySqlCommand (sql, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {



                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();

            }
        

    }
}
