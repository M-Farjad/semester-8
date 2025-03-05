using Microsoft.Data.SqlClient;
using System.Data;
namespace SE_Cosole_App
{
    public class Program()
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter username");
            string? uname = Console.ReadLine();
            Console.WriteLine("Enter password");
            string? upass = Console.ReadLine();

            string query = $"select * from uetusers where username='{uname}' and password='{upass}'";
            //string query = $"insert into uetusers (username, password) values ('{uname}', '{upass}')";

            string constring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SE21;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            
            SqlConnection conn = new SqlConnection(constring);
            try {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);

                //ExecuteReader for Get Method
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    Console.WriteLine("Loged in");
                }
                else
                {
                    Console.WriteLine("unauthenticatioed");
                }

                //ExecuteReader for Insert, Delete, Update Method
                //int temp = cmd.ExecuteNonQuery();

                //if(temp > 0 )
                //{
                //    Console.WriteLine("okay");
                //}

                //while (dr.Read())
                //{
                //    Console.WriteLine($"my use name is {dr[1]} and my password is {dr[2]}");
                //}

                conn.Close();
            }
            catch (SqlException e) {
                Console.WriteLine(e.Message);
            }
        }

    }

}
