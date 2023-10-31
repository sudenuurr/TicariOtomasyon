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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void giderlistele()
        {

            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_GIDER order by ID ASC", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            TxtId.Text = "";
            CmbxAy.Text = "";
            CmbxYil.Text = "";
            TxtElektrik.Text = "";
            txtSu.Text = "";
            TxtDogalgaz.Text = "";
            TxtInternet.Text = "";
            TxtMaaslar.Text = "";
            TxtEkstralar.Text = "";
            RchTxtNotlar.Text = "";

        }

        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            giderlistele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_GIDER (AY, YIL , ELEKTRIK,SU,DOGALGAZ, INTERNET, MAASLAR, EKSTRA, NOTLAR) values (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8,@p9 ) ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbxAy.Text);
            komut.Parameters.AddWithValue("@p2", CmbxYil.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse( txtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(TxtDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtInternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaaslar.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstralar.Text));
            komut.Parameters.AddWithValue("@p9", RchTxtNotlar.Text);
            komut.ExecuteNonQuery(); //DML komutlarini calistirir
            bgl.baglanti().Close();
            MessageBox.Show("Gider tabloya eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            giderlistele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtId.Text = dr["ID"].ToString();
                CmbxAy.Text = dr["AY"].ToString();
                CmbxYil.Text = dr["YIL"].ToString();
                TxtElektrik.Text = dr["ELEKTRIK"].ToString();
                txtSu.Text = dr["SU"].ToString();
                TxtDogalgaz.Text = dr["DOGALGAZ"].ToString();
                TxtInternet.Text = dr["INTERNET"].ToString();
                TxtMaaslar.Text = dr["MAASLAR"].ToString();
                TxtEkstralar.Text = dr["EKSTRA"].ToString();
                RchTxtNotlar.Text = dr["NOTLAR"].ToString();

            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {

            SqlCommand komutsil = new SqlCommand("Delete from TBL_GIDER where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", TxtId.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            giderlistele();
            MessageBox.Show("Gider silindi.", " Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_GIDER set AY=@p1, YIL=@p2 , ELEKTRIK=@p3, SU=@p4, DOGALGAZ=@p5, INTERNET=@p6,MAASLAR=@p7,EKSTRA=@p8,NOTLAR=@p9 where ID=@p10", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbxAy.Text);
            komut.Parameters.AddWithValue("@p2", CmbxYil.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(TxtDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtInternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaaslar.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstralar.Text));
            komut.Parameters.AddWithValue("@p9", RchTxtNotlar.Text);
            komut.Parameters.AddWithValue("@p10", TxtId.Text);
            komut.ExecuteNonQuery(); //DML komutlarini calistirir
            bgl.baglanti().Close();
            MessageBox.Show("Gider Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            giderlistele();
            temizle();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}
