using System.Data;
using ApiProvaOnline.Model;
using MySql.Data.MySqlClient;

namespace ApiProvaOnline.Service
{
    public class AlunoService
    {
        public void CadastrarAluno(Aluno aluno)
        {
            string sql = $"insert into Aluno (Nome, DataNascimento, DataHoraRegistro, IdProva) VALUES ('{aluno.Nome}', '{aluno.DataNascimento.ToString("yyyy-MM-dd HH:mm:ss")}', '{aluno.DataHoraRegistro.ToString("yyyy-MM-dd HH:mm:ss")}', '{aluno.IdProva}' )";

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

        public bool RemoverAluno(int Id)
        {
            string sql = $"delete from Aluno where id = {Id}";

            MySqlConnection connection = DbConnection.GetConnection();

            try
            {

                MySqlCommand command = new MySqlCommand(sql, connection);

                int LinhasAfetadas = command.ExecuteNonQuery();


                if (LinhasAfetadas > 0) {

                   
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


        public Aluno BuscarAluno(int Id)
        {

            string sql = $"select * from Aluno where id = {Id}";


            MySqlConnection connection = DbConnection.GetConnection();


            try
            {

                MySqlCommand command = new MySqlCommand(sql, connection);

                MySqlDataReader reader = command.ExecuteReader();

                Aluno aluno = new Aluno();

                while (reader.Read())
                {

                { 
                    aluno.Id = reader.GetInt32("id");
                    aluno.Nome = reader.GetString("Nome");
                    aluno.DataHoraRegistro = reader.GetDateTime("DataHoraRegistro");
                    aluno.DataNascimento = reader.GetDateTime("DataNascimento");
                    aluno.IdProva = reader.GetInt32("idProva");
                }


                }
                return aluno;

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
    
        public List<Aluno> ListarAluno()
        {
            string sql = $"select * from Aluno order by Nome ASC";

            MySqlConnection connection = DbConnection.GetConnection();

            List<Aluno> alunos = new List<Aluno>();

            try
            {

                MySqlCommand command = new MySqlCommand(sql , connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Aluno aluno = new Aluno();

                    aluno.Id = reader.GetInt32("id");
                    aluno.Nome = reader.GetString("Nome");
                    aluno.DataHoraRegistro = reader.GetDateTime("DataHoraRegistro");
                    aluno.DataNascimento = reader.GetDateTime("DataNascimento");
                    aluno.IdProva = reader.GetInt32("idProva");

                    alunos.Add(aluno);
               
                
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { 
                    
                connection.Close();
            
            }

            return alunos;

        }

        public bool AtualizarAluno(Aluno aluno)
        {

            string sql = $"update Aluno set Nome = '{aluno.Nome}' , DataHoraRegistro =  '{aluno.DataHoraRegistro.ToString("yyyy-MM-dd HH:mm:ss")}' , DataNascimento = '{aluno.DataNascimento.ToString("yyyy-MM-dd HH:mm:ss")}', Idprova = '{aluno.IdProva}' where Id = {aluno.Id}";

            MySqlConnection connection = DbConnection.GetConnection();

            try
            {

                MySqlCommand command = new MySqlCommand(sql , connection);

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
