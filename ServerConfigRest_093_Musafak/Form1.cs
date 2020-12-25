using ServiceRest_093_Musafak;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerConfigRest_093_Musafak
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnON.Enabled = true;
            btnOFF.Enabled = false;
        }

        private void btnON_Click(object sender, EventArgs e)
        {
            ServiceHost hostObjek = null;

            try
            {
                hostObjek = new ServiceHost(typeof(TI_UMY));
                hostObjek.Open();
                label1.Text = "ON";
                label2.Text = "Klik OFF Untuk Mematikan Server";
                btnON.Enabled = false;
                btnOFF.Enabled = true;
            }
            catch (Exception ex)
            {
                hostObjek = null;
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                label2.Text = "Server Erorrrr Run Kembali Server";
            }
        }

        private void btnOFF_Click(object sender, EventArgs e)
        {
            ServiceHost hostObjek;

            try
            {
                hostObjek = new ServiceHost(typeof(TI_UMY));
                hostObjek.Close();
                label1.Text = "OFF";
                label2.Text = "Klik ON Untuk Menghidupkan Server";
                btnON.Enabled = true;
                btnOFF.Enabled = false;
            }
            catch (Exception ex)
            {
                hostObjek = null;
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                label2.Text = "Server Erorrrr Run Kembali Server";
            }
        }
    }
}
