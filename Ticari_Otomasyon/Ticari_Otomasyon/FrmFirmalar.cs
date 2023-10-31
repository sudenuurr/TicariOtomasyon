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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void firmalistele()
        {
           
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_FIRMA", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            TxtId.Text = "";
            TxtAd.Text = "";
            TxtGorev.Text = "";
            TxtYetkili.Text = "";
            MskTxtTc.Text = "";
            TxtSektör.Text = "";
            MskTxtTelefon1.Text = "";
            MskTxtTelefon2.Text = "";
            MskTxtTelefon3.Text = "";
            TxtMail.Text = "";
            MskTxtTelefonFax.Text = "";
            CmbxIl.Text = "";
            CmbxIlce.Text = "";
            TxtVergiD.Text = "";
            RchTxtAdres.Text = "";
            TxtKod1.Text = "";
            TxtKod2.Text = "";
            TxtKod3.Text = "";
        }
        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR from TBL_IL", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbxIl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        void ozelkodaciklamalar()
        {
            SqlCommand komut = new SqlCommand("Select FIRMAKOD1 from TBL_KOD", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                RchTxtKod1.Text=dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmalistele();
            temizle();
            sehirlistesi();
            ozelkodaciklamalar();
        }
        

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtId.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtGorev.Text = dr["YETKILISTATU"].ToString();
                TxtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                MskTxtTc.Text = dr["YETKILITC"].ToString();
                TxtSektör.Text = dr["SEKTOR"].ToString();
                MskTxtTelefon1.Text = dr["TELEFON1"].ToString();
                MskTxtTelefon2.Text = dr["TELEFON2"].ToString();
                MskTxtTelefon3.Text = dr["TELEFON3"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                MskTxtTelefonFax.Text = dr["FAX"].ToString();
                CmbxIl.Text = dr["IL"].ToString();
                CmbxIlce.Text = dr["ILCE"].ToString();
                TxtVergiD.Text = dr["VERGIDAIRE"].ToString();
                RchTxtAdres.Text = dr["ADRES"].ToString();            
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            //verileri veri tabanina kaydetme
            SqlCommand komut = new SqlCommand("insert into TBL_FIRMA (AD, YETKILISTATU , YETKILIADSOYAD, YETKILITC, SEKTOR, TELEFON1,TELEFON2,TELEFON3,MAIL,FAX, IL, ILCE,VERGIDAIRE, ADRES, OZELKOD1,OZELKOD2,OZELKOD3) values (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17 ) ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtGorev.Text);
            komut.Parameters.AddWithValue("@p3", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p4", MskTxtTc.Text);
            komut.Parameters.AddWithValue("@p5", TxtSektör.Text);
            komut.Parameters.AddWithValue("@p6", MskTxtTelefon1.Text);
            komut.Parameters.AddWithValue("@p7", MskTxtTelefon2.Text);
            komut.Parameters.AddWithValue("@p8", MskTxtTelefon3.Text);
            komut.Parameters.AddWithValue("@p9", TxtMail.Text);
            komut.Parameters.AddWithValue("@p10", MskTxtTelefonFax.Text);
            komut.Parameters.AddWithValue("@p11", CmbxIl.Text);
            komut.Parameters.AddWithValue("@p12", CmbxIlce.Text);
            komut.Parameters.AddWithValue("@p13", TxtVergiD.Text);
            komut.Parameters.AddWithValue("@p14", RchTxtAdres.Text);
            komut.Parameters.AddWithValue("@p15", TxtKod1.Text);
            komut.Parameters.AddWithValue("@p16", TxtKod2.Text);
            komut.Parameters.AddWithValue("@p17", TxtKod3.Text);
            komut.ExecuteNonQuery(); //DML komutlarini calistirir
            bgl.baglanti().Close();
            MessageBox.Show("Firma sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
           firmalistele();
            temizle();
        }

        private void CmbxIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbxIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("Select ILCE from TBL_ILCE where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbxIl.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbxIlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete from TBL_FIRMA where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", TxtId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            firmalistele();
            MessageBox.Show("Firma silindi.", " Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_FIRMA set AD=@p1, YETKILISTATU=@p2 , YETKILIADSOYAD=@p3, YETKILITC=@p4, SEKTOR=@p5, TELEFON1=@p6,TELEFON2=@p7,TELEFON3=@p8,MAIL=@p9,FAX=@p10, IL=@p11, ILCE=@p12,VERGIDAIRE=@p13, ADRES=@p14, OZELKOD1=@p15,OZELKOD2=@p16,OZELKOD3=@p17 where ID=@p18", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtGorev.Text);
            komut.Parameters.AddWithValue("@p3", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p4", MskTxtTc.Text);
            komut.Parameters.AddWithValue("@p5", TxtSektör.Text);
            komut.Parameters.AddWithValue("@p6", MskTxtTelefon1.Text);
            komut.Parameters.AddWithValue("@p7", MskTxtTelefon2.Text);
            komut.Parameters.AddWithValue("@p8", MskTxtTelefon3.Text);
            komut.Parameters.AddWithValue("@p9", TxtMail.Text);
            komut.Parameters.AddWithValue("@p10", MskTxtTelefonFax.Text);
            komut.Parameters.AddWithValue("@p11", CmbxIl.Text);
            komut.Parameters.AddWithValue("@p12", CmbxIlce.Text);
            komut.Parameters.AddWithValue("@p13", TxtVergiD.Text);
            komut.Parameters.AddWithValue("@p14", RchTxtAdres.Text);
            komut.Parameters.AddWithValue("@p15", TxtKod1.Text);
            komut.Parameters.AddWithValue("@p16", TxtKod2.Text);
            komut.Parameters.AddWithValue("@p17", TxtKod3.Text);
            komut.Parameters.AddWithValue("@p18", TxtId.Text);
            komut.ExecuteNonQuery(); //DML komutlarini calistirir
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            firmalistele();
            temizle();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            temizle();
        }

      
    }
}
