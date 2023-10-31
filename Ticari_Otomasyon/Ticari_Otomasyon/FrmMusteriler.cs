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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_MUSTERI", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            TxtId.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            MskTxtTelefon1.Text = "";
            MskTxtTelefon2.Text = "";
            MskTxtTc.Text = "";
            Cmbxil.Text = "";
            Cmbxilce.Text = "";
            TxtMail.Text = "";
            RchTxtAdres.Text = "";
            TxtVergiD.Text = "";

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
        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            sehirlistesi();
            temizle();
        }

        private void Cmbxil_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbxilce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("Select ILCE from TBL_ILCE where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Cmbxil.SelectedIndex+1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbxilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            //verileri veri tabanina kaydetme
            SqlCommand komut = new SqlCommand("insert into TBL_MUSTERI (AD, SOYAD , TELEFON, TELEFON2, TC, MAIL, IL, ILCE, ADRES, VERGIDAIRE) values (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8,@p9,@p10 ) ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTxtTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", MskTxtTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", MskTxtTc.Text);
            komut.Parameters.AddWithValue("@p6", TxtMail.Text);
            komut.Parameters.AddWithValue("@p7", Cmbxil.Text);
            komut.Parameters.AddWithValue("@p8", Cmbxilce.Text);
            komut.Parameters.AddWithValue("@p9", RchTxtAdres.Text);
            komut.Parameters.AddWithValue("@p10", TxtVergiD.Text);
            komut.ExecuteNonQuery(); //DML komutlarini calistirir
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                TxtId.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTxtTelefon1.Text = dr["TELEFON"].ToString();
                MskTxtTelefon2.Text = dr["TELEFON2"].ToString();
                MskTxtTc.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                Cmbxil.Text = dr["IL"].ToString();
                Cmbxilce.Text = dr["ILCE"].ToString();
                RchTxtAdres.Text = dr["ADRES"].ToString();
                TxtVergiD.Text = dr["VERGIDAIRE"].ToString();
                
            }
            
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete from TBL_MUSTERI where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", TxtId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri silindi.", " Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_MUSTERI set AD=@P1, SOYAD=@P2 , TELEFON=@P3, TELEFON2=@P4, TC=@P5, MAIL=@P6, IL=@P7, ILCE=@P8,ADRES=@P9,VERGIDAIRE=@P10 where ID=@P11", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTxtTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", MskTxtTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", MskTxtTc.Text);
            komut.Parameters.AddWithValue("@p6", TxtMail.Text);
            komut.Parameters.AddWithValue("@p7", Cmbxil.Text);
            komut.Parameters.AddWithValue("@p8", Cmbxilce.Text);
            komut.Parameters.AddWithValue("@p9", RchTxtAdres.Text);
            komut.Parameters.AddWithValue("@p10", TxtVergiD.Text);
            komut.Parameters.AddWithValue("@p11", TxtId.Text);
            komut.ExecuteNonQuery(); //DML komutlarini calistirir
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
