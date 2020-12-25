using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientRestStudent_093_Musafak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnJumlah_Click(object sender, EventArgs e)
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);
            int length = data.Count();
            lblTotal.Text = Convert.ToString(length);
        }

        public class Mahasiswa
        {
            private string _nama, _nim, _prodi, _angkatan;
            public string nama
            {
                get { return _nama; }
                set { _nama = value; }
            }

            public string nim
            {
                get { return _nim; }
                set { _nim = value; }
            }

            public string prodi
            {
                get { return _prodi; }
                set { _prodi = value; }
            }

            public string angkatan
            {
                get { return _angkatan; }
                set { _angkatan = value; }
            }
        }



        public void DisplayData()
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            dtMahasiswa.DataSource = data;

        }
        public static string baseurl = "http://localhost:1907/";
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (textBoxNama.Text != "" && textBoxNIM.Text != "" && textBoxProdi.Text != "" && textBoxAngkatan.Text != "")
            {
                var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
                var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

                string nim = textBoxNIM.Text;
                var item = data.Where(x => x.nim == textBoxNIM.Text).FirstOrDefault();
                if (item != null)
                {
                    item.nama = textBoxNama.Text;
                    item.nim = textBoxNIM.Text;
                    item.prodi = textBoxProdi.Text;
                    item.angkatan = textBoxAngkatan.Text;

                    // Update
                    string output = JsonConvert.SerializeObject(item, Formatting.Indented);
                    var postdata = new WebClient();
                    postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    string response = postdata.UploadString(baseurl + "update", output);
                    Console.WriteLine(response);

                    textBoxNama.Text = "";
                    textBoxNIM.Text = "";
                    textBoxProdi.Text = "";
                    textBoxAngkatan.Text = "";
                    MessageBox.Show("Data Berhasil Di Update");
                    DisplayData();
                }
            }
            else
            {
                MessageBox.Show("Isi Data yang kosong");
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (textBoxHapus.Text != "")
            {
                var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
                var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

                if (MessageBox.Show("Anda yakin ingin menghapus", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string nim = textBoxHapus.Text;
                    var item = data.Where(x => x.nim == textBoxHapus.Text).FirstOrDefault();
                    if (item != null)
                    {
                        data.Remove(item);
                        string output = JsonConvert.SerializeObject(item, Formatting.Indented);
                        var postdata = new WebClient();
                        postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                        string response = postdata.UploadString(baseurl + "Mahasiswa/delete", output);
                        Console.WriteLine(response);
                        MessageBox.Show("Data Berhasil Di Hapus");
                        textBoxHapus.Text = "";
                        DisplayData();

                    }

                }
            }
            else
            {
                MessageBox.Show("Isi NIM yang ingin dicari !!!!");
            }

        }
    }
}
