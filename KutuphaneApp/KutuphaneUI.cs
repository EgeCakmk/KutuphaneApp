using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneApp
{
    public partial class KutuphaneUI : Form
    {
        public KutuphaneUI()
        {
            InitializeComponent();
        }
        KitapDal dal = new KitapDal();
        void Listele()
        {
            listBox1.Items.Clear();
            try
            {
                var kitaplar = dal.Listele();
                if (kitaplar != null)
                {
                    foreach (var k in kitaplar)
                    {
                        listBox1.Items.Add(k.Id + "-" + k.KitapAdi);
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = kitaplar;
                    lblToplamKitap.Text = "Toplam Kitap Sayisi: " + kitaplar.Count.ToString();
                }
                else
                {
                    lblToplamKitap.Text = "Toplam Kitap Sayisi: 0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme hatasi: " + ex.Message);
            }

        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            string aranan = txtKitapAra.Text.Trim();
            if (string.IsNullOrEmpty(aranan))
            {
                MessageBox.Show("Lutfen aranacak kelimeyi girin: ");
                return;
            }
            try
            {
                listBox1.Items.Clear();
                dataGridView1.DataSource = null;
                var sonuclar = dal.KitapAra(aranan);
                
                if (sonuclar.Count > 0)
                {
                    foreach (var k in sonuclar)
                    {
                        listBox1.Items.Add(k.Id + "-" + k.KitapAdi);
                    }

                    dataGridView1.DataSource = sonuclar;
                }
                else
                {
                    MessageBox.Show("Aranan kitap bulunamadi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arama islemi sirasinda hata olustu: " + ex.Message);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dal.Listele().Count == 0)
            {
                MessageBox.Show("Veri tabaninda silinecek herhangi bir kitap bulunmamaktadir.");
                return;
            }
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Lutfen silmek istediginiz kitabi secin.");
                return;

            }
            string seciliSatir = listBox1.SelectedItem.ToString();
            int seciliId = Convert.ToInt32(seciliSatir.Split('-')[0].Trim());
            try
            {
                dal.KitapSil(seciliId);
                MessageBox.Show("Kitap silindi.");
                Listele();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme islemi sirasinda hata olustu: " + ex.Message);

            }
        }

        private void btnEkle_Click_1(object sender, EventArgs e)
        {
            string ad = txtKitapAd.Text.Trim();
            if (string.IsNullOrWhiteSpace(ad))
            {
                MessageBox.Show("Lutfen bir kitap adi girin.");
                return;
            }
            foreach (var k in dal.Listele())
            {
                if(k.KitapAdi.Equals(ad, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Ayni kitap birden fazla kez eklenemez.");
                    return;
                }
            }
            try
            {
                dal.KitapEkle(ad);
                MessageBox.Show("Kitap veri tabanina eklendi.");
                txtKitapAd.Clear();
                Listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnListele_Click_1(object sender, EventArgs e)
        {
            if(dal.Listele().Count == 0)
            {
                MessageBox.Show("Veri tabaninda listelenecek herhangi bir kitap bulunmamaktadir.");
                return;
            }
            try
            {
                Listele();
                dataGridView1.DataSource = dal.Listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme sirasinda bir hata olustu:" + ex.Message);
            }
        }

        private void KutuphaneUI_Load_1(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Lutfen guncellemek istediginiz kitabi secin.");
                return;

            }
            if(string.IsNullOrWhiteSpace(txtKitapAd.Text))
            {
                MessageBox.Show("Lutfen guncel kitap adini girin.");
                return;
            }
            string guncellenecek = listBox1.SelectedItem.ToString();
            int seciliId = Convert.ToInt32(guncellenecek.Split('-')[0].Trim());
            try
            {
                string yeniKitapAdi = txtKitapAd.Text.Trim();
                dal.KitapGuncelle(seciliId, yeniKitapAdi);
                MessageBox.Show("Kitap guncellendi.");
                txtKitapAd.Clear();
                Listele();
            }
            catch (Exception ex) { MessageBox.Show("Guncelleme islemi sirasinda hata olustu: " + ex.Message); }
        }
    }
}
