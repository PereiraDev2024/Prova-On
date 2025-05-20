using System.Security.Cryptography.X509Certificates;
using ApiProvaOnline.Model;
using MySql.Data.MySqlClient;

namespace ApiProvaOnline.Service
{
    public class AlunoRespostaService
    {

        public void CadastrarAlunoResposta(AlunoResposta alunoResposta)
        {
            string sql = $"insert into AlunoResposta (IdAluno, IdPergunta , Resposta, PontuacaoFinal , DataHoraRegistro) Values ('{alunoResposta.IdAluno}', '{alunoResposta.IdPergunta}','{alunoResposta.Resposta}', '{alunoResposta.PontuacaoFinal}','{alunoResposta.DataHoraRegistro.ToString("yyyy - MM - dd HH: mm:ss")}')";
        
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
            finally { 
            
                connection.Close();
            
            }


        }

        public bool RemoverAlunoResposta(int Id)
        {

            string sql = $"delete * from AlunoResposta where id = {Id}";

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
