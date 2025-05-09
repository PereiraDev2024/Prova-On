using System.Data;
using System.Security.Cryptography.X509Certificates;
using ApiProvaOnline.Model;
using MySql.Data.MySqlClient;

namespace ApiProvaOnline.Service
{
    public class ProfessorService
    {
        public void CadastrarProfessor(Professor professor)
        {
            string sql = $"insert into Professor (Nome, Email, Senha, DataHoraRegistro) VALUES ('{professor.Nome}', '{professor.Email}','{professor.Senha}', '{professor.DataHoraRegistro.ToString("yyyy-MM-dd HH:mm:ss")}')";



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
        public bool RemoverProfessor(int Id)
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

            string sql = $"select * from Professor where Id = '{Id}'";

            MySqlConnection connection = DbConnection.GetConnection();

            try
            {

                MySqlCommand command = new MySqlCommand(sql, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
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
            string sql = $"select * from Professor order by Nome ASC";

            MySqlConnection connection = DbConnection.GetConnection();

            List<Professor> professores = new List<Professor>();

            try
            {

                MySqlCommand command = new MySqlCommand(sql, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Professor professor = new Professor();

                    professor.Id = reader.GetInt32("id");
                    professor.Nome = reader.GetString("Nome");
                    professor.Email = reader.GetString("Email");
                    professor.DataHoraRegistro = reader.GetDateTime("DataHoraRegistro");

                    professores.Add(professor);


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
            return professores;



        }

        public bool AtualizarProfessor(Professor professor)
        {

            string sql = $"update Professor set (Nome, Email, Senha, DataHoraRegistro) VALUES ('{professor.Nome}', '{professor.Email}','{professor.Senha}', '{professor.DataHoraRegistro.ToString("yyyy-MM-dd HH:mm:ss")}') where Id = {professor.Id}";

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
    }
}
