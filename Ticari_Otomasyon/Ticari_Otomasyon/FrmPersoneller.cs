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

namespace Ticari_Otomasyon
{
    public partial class FrmPersoneller : Form
    {
        public FrmPersoneller()
        {
            InitializeComponent();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void personellistele()
        {

            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_PERSONEL", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtId.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTxtTelefon.Text = dr["TELEFON"].ToString();
                MskTxtTc.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                Cmbxil.Text = dr["IL"].ToString();
                Cmbxilce.Text = dr["ILCE"].ToString();
                RchTxtAdres.Text = dr["ADRES"].ToString();
                TxtGorev.Text = dr["GOREV"].ToString();

            }
        }
        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR from TBL_IL", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbxil.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        void temizle()
        {
            TxtId.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            MskTxtTelefon.Text = "";
            MskTxtTc.Text = "";
            TxtMail.Text = "";
            Cmbxil.Text = "";
            Cmbxilce.Text = "";
            RchTxtAdres.Text = "";
            TxtGorev.Text = "";
            
        }
        private void FrmPersoneller_Load(object sender, EventArgs e)
        {
           
            personellistele();
            sehirlistesi();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            //verileri veri tabanina kaydetme
            SqlCommand komut = new SqlCommand("insert into TBL_PERSONEL (AD, SOYAD , TELEFON,TC,MAIL, IL, ILCE, ADRES, GOREV) values (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8,@p9 ) ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTxtTelefon.Text);
            komut.Parameters.AddWithValue("@p4", MskTxtTc.Text);
            komut.Parameters.AddWithValue("@p5", TxtMail.Text);
            komut.Parameters.AddWithValue("@p6", Cmbxil.Text);
            komut.Parameters.AddWithValue("@p7", Cmbxilce.Text);
            komut.Parameters.AddWithValue("@p8", RchTxtAdres.Text);
            komut.Parameters.AddWithValue("@p9", TxtGorev.Text);     
            komut.ExecuteNonQuery(); //DML komutlarini calistirir
            bgl.baglanti().Close();
            MessageBox.Show("Personel sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personellistele();
            //temizle();
        }

        private void Cmbxil_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbxilce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("Select ILCE from TBL_ILCE where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Cmbxil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbxilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {

            SqlCommand komutsil = new SqlCommand("Delete from TBL_PERSONEL where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", TxtId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            personellistele();
            MessageBox.Show("Personel silindi.", " Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_PERSONEL set AD=@p1, SOYAD=@p2 , TELEFON=@p3, TC=@p4, MAIL=@p5, IL=@p6,ILCE=@p7,ADRES=@p8,GOREV=@p9 where ID=@p10", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTxtTelefon.Text);
            komut.Parameters.AddWithValue("@p4", MskTxtTc.Text);
            komut.Parameters.AddWithValue("@p5", TxtMail.Text);
            komut.Parameters.AddWithValue("@p6", Cmbxil.Text);
            komut.Parameters.AddWithValue("@p7", Cmbxilce.Text);
            komut.Parameters.AddWithValue("@p8", RchTxtAdres.Text);
            komut.Parameters.AddWithValue("@p9", TxtGorev.Text);
            komut.Parameters.AddWithValue("@p10", TxtId.Text);
            komut.ExecuteNonQuery(); //DML komutlarini calistirir
            bgl.baglanti().Close();
            MessageBox.Show("Personel Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            personellistele();
            temizle();
        }
    }
}
