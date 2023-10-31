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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {

            SqlDataAdapter da = new SqlDataAdapter("Execute BankaBilgileri", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            

        }
        void firmalistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID,AD From TBL_FIRMA ", bgl.baglanti());
            da.Fill(dt);
            
            LkpFirma.Properties.ValueMember = "ID"; // arka planda kullanılacak olan alan
            LkpFirma.Properties.DisplayMember = "AD";  //ekranda bize gözükücek alan
            LkpFirma.Properties.DataSource = dt;

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
            TxtBankaAd.Text = "";
            Cmbxil.Text = "";
            Cmbxilce.Text = "";
            TxtSube.Text = "";
            Txtiban.Text = "";
            TxtHesapNo.Text = "";
            TxtYetkili.Text = "";
            MskTxtTelefon.Text = "";
            MskTxtTarih.Text = "";
            TxtHesapTuru.Text = "";
            LkpFirma.Text = "";
            
        }
       

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            //verileri veri tabanina kaydetme
            SqlCommand komut = new SqlCommand("insert into TBL_BANKA (BANKAAD, IL , ILCE, SUBE, IBAN, HESAPNO,YETKILI,TELEFON,TARIH,HESAPTUR, FIRMAID) values (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8,@p9,@p10,@p11 ) ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@p2", Cmbxil.Text);
            komut.Parameters.AddWithValue("@p3", Cmbxilce.Text);
            komut.Parameters.AddWithValue("@p4", TxtSube.Text);
            komut.Parameters.AddWithValue("@p5", Txtiban.Text);
            komut.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p8", MskTxtTelefon.Text);
            komut.Parameters.AddWithValue("@p9", MskTxtTarih.Text);
            komut.Parameters.AddWithValue("@p10", TxtHesapTuru.Text);
            komut.Parameters.AddWithValue("@p11", LkpFirma.EditValue);
            komut.ExecuteNonQuery(); //DML komutlarini calistirir
            bgl.baglanti().Close();
            MessageBox.Show("Banka sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
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
        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
            sehirlistesi();
            firmalistesi();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtId.Text = dr["ID"].ToString();
                TxtBankaAd.Text = dr["BANKAAD"].ToString();
                Cmbxil.Text = dr["IL"].ToString();
                Cmbxilce.Text = dr["ILCE"].ToString();
                TxtSube.Text = dr["SUBE"].ToString();
                Txtiban.Text = dr["IBAN"].ToString();
                TxtHesapNo.Text = dr["HESAPNO"].ToString();
                TxtYetkili.Text = dr["YETKILI"].ToString();
                MskTxtTelefon.Text = dr["TELEFON"].ToString();
                MskTxtTarih.Text = dr["TARIH"].ToString();
                TxtHesapTuru.Text = dr["HESAPTUR"].ToString();
                
                
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete from TBL_BANKA where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", TxtId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Banka silindi.", " Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_BANKA set BANKAAD=@p1, IL=@p2 , ILCE=@p3, SUBE=@p4, IBAN=@p5, HESAPNO=@p6,YETKILI=@p7,TELEFON=@p8,TARIH=@p9,HESAPTUR=@p10, FIRMAID=@p11 where ID=@p12", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@p2", Cmbxil.Text);
            komut.Parameters.AddWithValue("@p3", Cmbxilce.Text);
            komut.Parameters.AddWithValue("@p4", TxtSube.Text);
            komut.Parameters.AddWithValue("@p5", Txtiban.Text);
            komut.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p8", MskTxtTelefon.Text);
            komut.Parameters.AddWithValue("@p9", MskTxtTarih.Text);
            komut.Parameters.AddWithValue("@p10", TxtHesapTuru.Text);
            komut.Parameters.AddWithValue("@p11", LkpFirma.EditValue);
            komut.Parameters.AddWithValue("@p12", TxtId.EditValue);
            komut.ExecuteNonQuery(); //DML komutlarini calistirir
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }
    }
}
