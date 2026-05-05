using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneApp
{
    public class KitapDal
    {
        string connString = "Server = (localdb)\\MSSQLLocalDB; Database = Kitaplik;" +
            "Trusted_Connection = True;";

        SqlConnection BaglantiAc()
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            return conn;
        }

        void Execute(string sql, Dictionary<string, object> parametreler)
        {
            using (SqlConnection conn = BaglantiAc())
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                foreach (var param in parametreler)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
                cmd.ExecuteNonQuery();
            }
        }

        public void KitapEkle(string ad)
        {
            Execute("insert into Kitaplar(KitapAdi) values (@p1)",
                new Dictionary<string, object> { { "@p1", ad } });
        }
        public List<Kitap> Listele()
        {
            List<Kitap> kitaplar = new List<Kitap>();
            using (SqlConnection conn = BaglantiAc())
            {
                SqlCommand cmdListele = new SqlCommand("SELECT * FROM Kitaplar", conn);
                SqlDataReader reader = cmdListele.ExecuteReader();

                while (reader.Read())
                {
                    Kitap k = new Kitap();
                    k.Id = Convert.ToInt32(reader["Id"]);
                    k.KitapAdi = reader["KitapAdi"].ToString();

                    kitaplar.Add(k);
                }
            }
            return kitaplar;
        }
        public void KitapSil(int Id)
        {
            using (SqlConnection conn = BaglantiAc())
            {
                SqlCommand cmdSil = new SqlCommand("delete from Kitaplar where Id=@p1", conn);
                cmdSil.Parameters.AddWithValue("@p1", Id);
                cmdSil.ExecuteNonQuery();
            }

        }
        public List<Kitap> KitapAra(string kelime)
        {
            List<Kitap> bulunanKitaplar = new List<Kitap>();
            using (SqlConnection conn = BaglantiAc())
            {
                SqlCommand cmdAra = new SqlCommand("select * from Kitaplar where KitapAdi like @kelime", conn);
                cmdAra.Parameters.AddWithValue("@kelime", "%" + kelime + "%");
                SqlDataReader reader = cmdAra.ExecuteReader();
                while (reader.Read())
                {
                    Kitap k = new Kitap();
                    k.Id = Convert.ToInt32(reader["Id"]);
                    k.KitapAdi = reader["KitapAdi"].ToString();
                    bulunanKitaplar.Add(k);

                }
            }
            return bulunanKitaplar;
        }

        public void KitapGuncelle(int Id, string yeniAd)
        {
            using (SqlConnection conn = BaglantiAc())
            {
                SqlCommand cmdGuncelle = new SqlCommand("update Kitaplar set KitapAdi=@p1 where Id=@p2", conn);
                cmdGuncelle.Parameters.AddWithValue("@p1", yeniAd);
                cmdGuncelle.Parameters.AddWithValue("@p2", Id);
                cmdGuncelle.ExecuteNonQuery();
            }
        }
    }
}

