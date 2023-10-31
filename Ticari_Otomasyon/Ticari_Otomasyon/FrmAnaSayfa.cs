using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;

namespace Ticari_Otomasyon
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void stoklar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select URUNAD ,Sum(ADET) as 'ADET' from TBL_URUN group by URUNAD having sum(ADET)<=20 order by Sum(ADET)", bgl.baglanti());
            da.Fill(dt);
            GrdCntrlStok.DataSource = dt;
        }
        void ajanda()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select TOP 5 TARIH,SAAT,BASLIK from TBL_NOT order by ID desc ", bgl.baglanti());
            da.Fill(dt);
            GrdCntrlAjanda.DataSource = dt;
        }
        void FirmaHareketler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("EXEC FirmaHareketler", bgl.baglanti());
            da.Fill(dt);
            GrdCntrlHareket.DataSource = dt;

        }
        void Fihrist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select AD, TELEFON1 from TBL_FIRMA", bgl.baglanti());
            da.Fill(dt);
            GrdCntrlFihrist.DataSource = dt;

        }
        void haberler()
        {
            XmlTextReader xmloku = new XmlTextReader("https://www.hurriyet.com.tr/rss/anasayfa");
            while (xmloku.Read())
            {
                if (xmloku.Name=="title")
                {
                    listBox1.Items.Add(xmloku.ReadString());
                }

            }
        }


        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stoklar();

            ajanda();

            FirmaHareketler();

            Fihrist();

            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/today.xml");
            haberler();

        }

        
    }
}
