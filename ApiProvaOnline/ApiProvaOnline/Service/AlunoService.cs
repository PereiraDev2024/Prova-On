using System.Data;
using ApiProvaOnline.Model;
using MySql.Data.MySqlClient;

namespace ApiProvaOnline.Service
{
    public class AlunoService
    {
        public void CadastrarAluno(Aluno aluno)
        {
            string sql = $"insert into Aluno (Nome, DataNascimento, DataHoraRegistro, IdProva) VALUES ('{aluno.Nome}', '{aluno.DataNascimento}', '{aluno.DataHoraRegistro}', '{aluno.IdProva}' )";

            MySqlConnection connection = DbConnection.GetConnection();

            try
            {

                MySqlCommand command = new MySqlCommand(sql, connection);


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
