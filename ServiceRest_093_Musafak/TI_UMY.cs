using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceRest_093_Musafak
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TI_UMY : ITI_UMY
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-5L26KM2;Initial Catalog=TI UMY;Persist Security Info=True;User ID=sa; Password=musafak93");
        public string CreateMahasiswa(Mahasiswa mhs)
        {
            string msg = "GAGAL";
            string query = String.Format("insert into dbo.Mahasiswa values ('{0}','{1}','{2}','{3}')", mhs.nama, mhs.nim, mhs.prodi, mhs.angkatan);
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                Console.WriteLine(query);
                cmd.ExecuteNonQuery();
                conn.Close();
                msg = "Sukses";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
                msg = "GAGAL";
            }


            return msg;
        }

        public string DeleteData(Mahasiswa mhs)
        {
            string query = string.Format("delete from Mahasiswa where NIM = '{0}'", mhs.nim);
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return "Data Berhasil Di Hapus";
            }
            catch (Exception e)
            {
                return "Data Gagal Di Hapus :" + e.Message;
            }
        }

        public List<Mahasiswa> GetAllMahasiswa()
        {
            List<Mahasiswa> mahas = new List<Mahasiswa>();
            string query = "select Nama, NIM, Prodi, Angkatan from dbo.Mahasiswa";
            SqlCommand com = new SqlCommand(query, conn); //yang dikirim ke sql

            try
            {
                conn.Open();
                SqlDataReader reader = com.ExecuteReader(); //mendapatkan data yang telah dieksekusi, dari select, hasil query ditaro direader

                while (reader.Read())
                {
                    Mahasiswa mhs = new Mahasiswa();
                    mhs.nama = reader.GetString(0); //0 itu array pertama diambil dari iservice
                    mhs.nim = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);

                    mahas.Add(mhs);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return mahas;
        }

        public Mahasiswa GetMahasiswaByNIM(string nim)
        {

            Mahasiswa mhs = new Mahasiswa();
            string query = String.Format("select Nama, NIM, Prodi, Angkatan from dbo.Mahasiswa where NIM = '{0}'", nim);
            SqlCommand com = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    mhs.nama = reader.GetString(0); //0 array pertama diambil dari iservice
                    mhs.nim = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return mhs;
        }

        public string Update(Mahasiswa mhs)
        {
            //string msg = "Gagal";
            string query = string.Format("update Mahasiswa set Nama = '{0}', Prodi = '{1}', Angkatan = '{2}' where NIM ='{3}'", mhs.nama, mhs.prodi, mhs.angkatan, mhs.nim);
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return "Data Berhasil di Update";
                //msg = "Sukses";
            }
            catch (Exception e)
            {
                //msg = "GAGAL";
                return "gagal" + e.Message;
            }
            //return msg;
        }
    }
}
