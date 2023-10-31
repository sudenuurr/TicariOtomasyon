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
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmRehber_Load(object sender, EventArgs e)
        {
            //Müşteri Bilgileri
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select AD,SOYAD, TELEFON,TELEFON2,MAIL from TBL_MUSTERI", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

            //Firma Bilgileri
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("select AD,YETKILIADSOYAD, TELEFON1,TELEFON2,TELEFON3,MAIL,FAX from TBL_FIRMA", bgl.baglanti());
            da1.Fill(dt1);
            gridControl2.DataSource = dt1;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            BtnGonder frm = new BtnGonder();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                frm.mail = dr["MAIL"].ToString();
            }
            frm.Show();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            BtnGonder frm = new BtnGonder();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                frm.mail = dr["MAIL"].ToString();
            }
            frm.Show();
        }
    }
}
