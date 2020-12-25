using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientRestMahasiswa_093_Musafak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //class mahasiswa
        [DataContract]
        public class Mahasiswa
        {
            private string _nama, _nim, _prodi, _angkatan;
            [DataMember]
            public string nama
            {
                get { return _nama; }
                set { _nama = value; }
            }
            [DataMember]
            public string nim
            {
                get { return _nim; }
                set { _nim = value; }
            }
            [DataMember]
            public string prodi
            {
                get { return _prodi; }
                set { _prodi = value; }
            }
            [DataMember]
            public string angkatan
            {
                get { return _angkatan; }
                set { _angkatan = value; }
            }
        }


        //Untuk Mencari Data 
        public void SearchData()
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);
            string nim = textBoxSearch.Text;
            if (nim == null || nim == "")
            {
                dtMahasiswa.DataSource = data;
            }
            else
            {
                var item = data.Where(x => x.nim == textBoxSearch.Text).ToList();

                dtMahasiswa.DataSource = item;
            }
        }

        //Untuk melihat data
        public void DisplayData()
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            dtMahasiswa.DataSource = data;

        }

        string baseurl = "http://localhost:1907/";
        private void btnTambahData_Click(object sender, EventArgs e)
        {
            if (textBoxNama.Text != "" && textBoxNIM.Text != "" && textBoxProdi.Text != "" && textBoxAngkatan.Text != "")
            {
                Mahasiswa mhs = new Mahasiswa();
                mhs.nama = textBoxNama.Text;
                mhs.nim = textBoxNIM.Text;
                mhs.prodi = textBoxProdi.Text;
                mhs.angkatan = textBoxAngkatan.Text;

                var data = JsonConvert.SerializeObject(mhs);
                var postdata = new WebClient();
                postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                string response = postdata.UploadString(baseurl + "Mahasiswa", data);
                Console.WriteLine(response);

                //untuk membuat textbox kosong ketika input data
                textBoxNama.Text = "";
                textBoxNIM.Text = "";
                textBoxProdi.Text = "";
                textBoxAngkatan.Text = "";
                MessageBox.Show("Data Berhasil Ditambahkan");
                DisplayData();
            }
            else
            {
                MessageBox.Show("Isi Data yang kosong");
            }
        }

        private void buttonCariData_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void buttonLihatData_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
