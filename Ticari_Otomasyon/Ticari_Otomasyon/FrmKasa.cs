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
using DevExpress.Charts;

namespace Ticari_Otomasyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void musterihareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute MusteriHareketler", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void firmahareket()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Execute FirmaHareketler", bgl.baglanti());
            da2.Fill(dt2);
            gridControl3.DataSource = dt2;
        }
        void kasacikis() { 
        SqlDataAdapter da = new SqlDataAdapter("select * from TBL_GIDER order by ID ASC", bgl.baglanti());
        DataTable dt = new DataTable();
        da.Fill(dt);
            gridControl2.DataSource = dt;
        }
        public string ad;

        private void FrmKasa_Load(object sender, EventArgs e)
        {
            LblAktifKullanici.Text = ad;
            musterihareket();

            firmahareket();
            kasacikis();

            //Toplam Tutar
            SqlCommand komut1 = new SqlCommand("select sum(tutar) From TBL_FATURADETAY", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                LblToplamTutar.Text = dr1[0].ToString()+" TL";     
            }
            bgl.baglanti().Close();

            //Odemeler
            SqlCommand komut2 = new SqlCommand("Select (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) from TBL_GIDER order by ID asc", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblOdemeler.Text = dr2[0].ToString() + " TL";
            }
            bgl.baglanti().Close(); 

            //Son Ayın Personel Maaşları
            SqlCommand komut3 = new SqlCommand("Select MAASLAR from TBL_GIDER order by ID asc", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                LblPersonelMaaslari.Text = dr3[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            //Müsteri Sayısı
            SqlCommand komut4 = new SqlCommand("Select Count(*) from TBL_MUSTERI", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                LblMusteriSayisi.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();

            //Firma Sayısı
            SqlCommand komut5 = new SqlCommand("Select Count(*) from TBL_FIRMA", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                LblFirmaSayisi.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();

            //Firma Şehir Sayısı
            SqlCommand komut6 = new SqlCommand("Select Count(Distinct(IL)) from TBL_FIRMA", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                LblSehirSayisi.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close(); 

            //Personel Sayısı
            SqlCommand komut7 = new SqlCommand("Select Count(*) from TBL_PERSONEL", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                LblPersonelSayisi.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();
            //Toplam urun Sayısı
            SqlCommand komut8 = new SqlCommand("Select sum(ADET) from TBL_URUN", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                LblStokSayisi.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();


        }
        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            //Elektrik
            if (sayac>0 && sayac<=5)
            {
                groupControl10.Text = "Elektrik";
                SqlCommand komut10 = new SqlCommand("Select top 4 AY, ELEKTRIK from  TBL_GIDER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            //Su
            if (sayac>5 && sayac<=10)
            {
                groupControl10.Text = "Su";
                chartControl1.Series["AYLAR"].Points.Clear();             
                SqlCommand komut10 = new SqlCommand("Select top 4 AY, SU from  TBL_GIDER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            //Dogalgaz
            if (sayac > 10 && sayac <= 15)
            {
                groupControl10.Text = "Doğalgaz";
                chartControl1.Series["AYLAR"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select top 4 AY, DOGALGAZ from  TBL_GIDER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            //Internet
            if (sayac > 15 && sayac <= 20)
            {
                groupControl10.Text = "Internet";
                chartControl1.Series["AYLAR"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select top 4 AY, INTERNET from  TBL_GIDER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            //Ekstra
            if (sayac > 20 && sayac <= 25)
            {
                groupControl10.Text = "Ekstra";
                chartControl1.Series["AYLAR"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select top 4 AY, EKSTRA from  TBL_GIDER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            if (sayac == 26)
            {
                sayac = 0;
            }


        }
        int sayac2 = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;
            //Elektrik
            if (sayac2 > 0 && sayac2 <= 5)
            {
                groupControl11.Text = "Elektrik";
                chartControl2.Series["AYLAR"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select top 4 AY, ELEKTRIK from  TBL_GIDER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            //Su
            if (sayac2 > 5 && sayac2 <= 10)
            {
                groupControl11.Text = "Su";
                chartControl2.Series["AYLAR"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select top 4 AY, SU from  TBL_GIDER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            //Dogalgaz
            if (sayac2 > 10 && sayac2 <= 15)
            {
                groupControl11.Text = "Doğalgaz";
                chartControl2.Series["AYLAR"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select top 4 AY, DOGALGAZ from  TBL_GIDER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            //Internet
            if (sayac2 > 15 && sayac2 <= 20)
            {
                groupControl11.Text = "Internet";
                chartControl2.Series["AYLAR"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select top 4 AY, INTERNET from  TBL_GIDER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            //Ekstra
            if (sayac2 > 20 && sayac2 <= 25)
            {
                groupControl11.Text = "Ekstra";
                chartControl2.Series["AYLAR"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select top 4 AY, EKSTRA from  TBL_GIDER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }

     
    }
}
