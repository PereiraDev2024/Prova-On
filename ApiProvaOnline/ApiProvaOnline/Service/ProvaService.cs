using ApiProvaOnline.Model;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace ApiProvaOnline.Service
{
    public class ProvaService
    {
        public void CadastrarProva(Prova prova)
        {
         string sql = $"insert into Prova (id , Titulo, DataHoraInicio, DataHoraFim, DataHoraRegistro, Codigo, IdProfessor) VALUES '{prova.Id}', '{prova.Titulo}','{prova.DataHoraInicio.ToString("yyyy-MM-dd HH:mm:ss")}','{prova.DataHoraFim.ToString("yyyy-MM-dd HH:mm:ss")}','{prova.DataHoraRegistro.ToString("yyyy-MM-dd HH:mm:ss")}','{prova.Codigo}','{prova.IdProfessor}'";

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
        public bool RemoverProva(int id)
        {

            string sql = $"delete from prova where id = '{id}'";

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

        public Prova BuscarProva(int id)
        {
            string sql = $"select * from Prova where id = {id}";

            MySqlConnection connection = DbConnection.GetConnection();

            try
            {
                MySqlCommand command = new MySqlCommand(sql, connection);

                MySqlDataReader reader = command.ExecuteReader();

                Prova prova = new Prova();

                while (reader.Read())
                {
                    

                    prova.Id = reader.GetInt32("id");
                    prova.Titulo = reader.GetString("Titulo");
                    prova.DataHoraInicio = reader.GetDateTime("DataHoraInicio");
                    prova.DataHoraFim = reader.GetDateTime("DataHoraFim");
                    prova.DataHoraRegistro = reader.GetDateTime("DataHoraRegistro");


                }
                return prova;

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
        public List<Prova> ListarProva()
        {
            string sql = $"select * from Prova order by Titulo ASC";

            MySqlConnection connection = DbConnection.GetConnection();
            
            
            List<Prova> provas = new List<Prova>();

            try
            {
                MySqlCommand command = new MySqlCommand(sql, connection);

                MySqlDataReader reader = command.ExecuteReader();



                while (reader.Read())
                {
                    Prova prova = new Prova();

                    prova.Id = reader.GetInt32("id");
                    prova.Titulo = reader.GetString("Titulo");
                    prova.DataHoraRegistro = reader.GetDateTime("DataHoraRegistro");
                    prova.DataHoraFim = reader.GetDateTime("DataHoraFim");
                    prova.DataHoraInicio = reader.GetDateTime("DataHoraInicio");
                    prova.Codigo = reader.GetString("Codigo");
                    prova.IdProfessor = reader.GetInt32("IdProfessor");

                    provas.Add(prova);

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
            return provas;


        }
        public void AtualizarProva(Prova prova)
        {
            string sql = $"update into prova set Titulo = '{prova.Titulo}', Id = '{prova.Id}', DataHoraInicio = '{prova.DataHoraInicio}' , DataHoraFim = '{prova.DataHoraFim}' , DataHoraRegistro = '{prova.DataHoraRegistro}', Codigo = '{prova.Codigo}', IdProfessor = '{prova.IdProfessor}'";

            MySqlConnection connection = DbConnection.GetConnection();



        
        }

    }
}
