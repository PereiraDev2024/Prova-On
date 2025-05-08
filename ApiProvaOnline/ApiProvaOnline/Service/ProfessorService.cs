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



    }
}
