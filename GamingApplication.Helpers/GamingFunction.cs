using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamingApplication.Interfaces;

namespace GamingApplication.Helpers
{
    public class GamingFunction : IGamingApplication
    {
        public bool InsertGamingData(string rou_rolled, int money, string player, ref string error)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=true;Initial Catalog=demandforecasting"))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Add_Bet", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@rou_rolled", SqlDbType.VarChar).Value =rou_rolled;
                        cmd.Parameters.Add("@money", SqlDbType.VarChar).Value = money;
                        cmd.Parameters.Add("@player", SqlDbType.VarChar).Value = player;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException ?? ex);
            }
        }
    }
}
