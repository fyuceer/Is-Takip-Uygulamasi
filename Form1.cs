using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ÖDEV2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        } 
        
        static bool isBlue = true;

        public void normalpanel()
        {
            this.panel1.BackColor = Color.White;
            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
            button4.Visible = false;

            textBox3.Text = "Bugün " + DateTime.Now.ToString("dd.MM.yyyy dddd ") + "Saat: " + DateTime.Now.ToString("HH:mm:ss");


        }

       
        public void alarmpaneli(string ad)
        {
            pictureBox2.Visible = true;
            pictureBox1.Visible = false;
            button4.Visible = true;


            if (isBlue)
            {
                this.panel1.BackColor = Color.LightSkyBlue;
            }

            else
            {
                this.panel1.BackColor = Color.Blue;

            }

            isBlue = !isBlue;

        }


        public void playmp3()
        {

            ses.URL = Path.Combine(Application.StartupPath, "alarm.mp3");

        }


        public void stopmp3()
        {
            ses.Ctlcontrols.stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm:s";
            label4.Text = listBox1.Items.Count.ToString();

            pictureBox1.Visible = true;

            button4.Visible = false;//durdur butonu

            timer1.Enabled = true;
            timer1.Interval = 3000;

            try
            {
                if(File.Exists("liste.txt"))
                {
                    StreamReader dosya = new StreamReader("liste.txt");
                    while (true)
                    {
                        string line = dosya.ReadLine();

                        if (line != null)
                        {
                            listBox1.Items.Add(line);


                        }
                        else
                            break;

                    }
                    dosya.Close();
                    label4.Text = listBox1.Items.Count.ToString();
                }

            }

            catch
            {
                MessageBox.Show("dosya okunamadı...");
            }









        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)//KISA YOLLAR
        {



            if (e.Alt && e.KeyCode == Keys.Y)//yeni kayıt
            {
                button1.PerformClick();

            }

            if (e.Alt && e.KeyCode == Keys.K)//kaydet
            {
                button2.PerformClick();

            }
            if (e.Alt && e.KeyCode == Keys.Z)//düzenle
            {
                button3.PerformClick();

            }

            if (e.Alt && e.KeyCode == Keys.S)//seçileni sil
            {
                button5.PerformClick();

            }

            if (e.Alt && e.KeyCode == Keys.H)//hepsini sil
            {
                button6.PerformClick();

            }
            if (e.Alt && e.KeyCode == Keys.D)//dosyaya kaydet
            {
                button7.PerformClick();

            }

        }


        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            this.textBox1.BackColor = Color.LightYellow;

        }

        private void textBox2_MouseHover(object sender, EventArgs e)
        {
            this.textBox2.BackColor = Color.LightYellow;
        }

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            this.textBox2.BackColor = Color.White;
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            this.textBox1.BackColor = Color.White;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker1.Visible = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)//YENİ KAYIT
        {

            textBox1.Text = "";
            textBox2.Text = "";


        }

        private void button2_Click(object sender, EventArgs e)//KAYDET
        {




            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Lütfen iş adını ve açıklamasını boş geçmeyiniz", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (checkBox1.Checked)
                {
                    listBox1.Items.Add(textBox1.Text + "-" + textBox2.Text + "-" + dateTimePicker1.Text);

                }
                else
                {
                    listBox1.Items.Add(textBox1.Text + "-" + textBox2.Text);
                }
                textBox2.Text = "";
                textBox1.Text = "";
                textBox1.Focus();

                dateTimePicker1.Visible = false;
                checkBox1.Checked = false;

            }
            label4.Text = listBox1.Items.Count.ToString();
        }

        private void button7_Click(object sender, EventArgs e)//DOSYAYA KAYDET
        {


            if (File.Exists("liste.txt") && File.Exists("ayarlar.dat"))//dosya varsa
            {

                SaveFileDialog dosyakaydet = new SaveFileDialog();
                dosyakaydet.Filter = "txt Dosyaları (*.txt)|*.txt";

                dosyakaydet.RestoreDirectory = true;

                if (dosyakaydet.ShowDialog() == DialogResult.OK)
                {

                    string dosyaAdi = dosyakaydet.FileName;
                    string yol = Path.GetFullPath(dosyaAdi);


                    TextWriter dosya = new StreamWriter(dosyaAdi);
                    TextWriter dosyayolu = new StreamWriter("ayarlar.dat");
                    dosyayolu.WriteLine(yol);

                    dosyayolu.Flush();
                    dosyayolu.Close();

                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        dosya.WriteLine(listBox1.Items[i].ToString());
                    }
                    dosya.Flush();
                    dosya.Close();

                    MessageBox.Show("başarıyla dosyaya kaydedildi", "kayıt başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);



                }



            }

            else
            {
                if (listBox1.Items.Count > 0)//liste doluysa ve henüz dosya oluşturulmamışsa
                {
                    StreamWriter dosya = new StreamWriter("liste.txt");
                    StreamWriter ayarlar = new StreamWriter("ayarlar.dat");
                    ayarlar.Close();



                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {

                        dosya.WriteLine(listBox1.Items[i].ToString());

                    }

                    dosya.Flush();
                    dosya.Close();

                    MessageBox.Show("başarıyla dosyaya kaydedildi", "kayıt başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                else
                    MessageBox.Show("dosyayanız boş", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }






        }
        private void button5_Click(object sender, EventArgs e)//SİLME
        {
            int a = listBox1.SelectedIndex;

            DialogResult secenek = MessageBox.Show("silmek istediğinize emin misiniz", "uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {

                if (a >= 0)
                {
                    listBox1.Items.RemoveAt(a);

                }
                else
                    MessageBox.Show("lütfen bir eleman seçiniz", "uıyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }

            label4.Text = listBox1.Items.Count.ToString();


        }


        private void button6_Click(object sender, EventArgs e)//HEPSİNİ SİL
        {



            DialogResult secenek = MessageBox.Show(" hepsini silmek istediğinize emin misiniz", "uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                listBox1.Items.Clear();
            }


            label4.Text = listBox1.Items.Count.ToString();



        }

        private void button3_Click(object sender, EventArgs e)//DÜZELT
        {
            textBox1.Text = null;
            textBox2.Text = null;




            if (listBox1.SelectedIndex >= 0)
            {


                DialogResult secilen = MessageBox.Show("seçilen kayıt değiştirilsin mi", "uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (secilen == DialogResult.Yes)
                {
                    if (checkBox1.Checked)
                    {
                        listBox1.Items.Add(textBox1.Text + " " + textBox2.Text + " " + dateTimePicker1.Text);

                    }

                    else
                        listBox1.Items[listBox1.SelectedIndex] = (textBox1.Text + " " + textBox2.Text).ToString();

                }

            }


        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)//ListBox'ta seçilen satırı iş adı ve açıklamaya doldurma
        {
            if (listBox1.SelectedIndex >= 0)
            {
                string satir = listBox1.SelectedItem.ToString();
                string[] secilen = satir.Split('-');
                textBox1.Text = secilen[0];
                textBox2.Text = secilen[1];




                if (secilen.Length > 2)
                {
                    dateTimePicker1.Text = secilen[2];
                    checkBox1.Checked = true;
                    dateTimePicker1.Enabled = true;

                }

                else
                {
                    checkBox1.Checked = false;
                    dateTimePicker1.Enabled = false;

                }

            }


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)//Form kapanırken kaydet
        {
            if (File.Exists("liste.txt") && listBox1.Items.Count > 0)
            {


                TextWriter dosya = new StreamWriter("liste.txt");

                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    dosya.WriteLine(listBox1.Items[i].ToString());
                }
                dosya.Flush();
                dosya.Close();
                MessageBox.Show("başarıyla dosyaya kaydedildi", "kayıt başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);



            }

            else
            {

                if (listBox1.Items.Count > 0)//liste doluysa ve henüz dosya oluşturulmamışsa
                {
                    StreamWriter dosya = new StreamWriter("liste.txt");

                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {

                        dosya.WriteLine(listBox1.Items[i].ToString());

                    }

                    dosya.Flush();
                    dosya.Close();

                    MessageBox.Show("başarıyla dosyaya kaydedildi", "kayıt başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }



            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string satir;
            int flag = 0;
            textBox3.Text = "Bugün " + DateTime.Now.ToString("dd.MM.yyyy dddd ") + "Saat: " + DateTime.Now.ToString("HH:mm:ss");


            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                satir = listBox1.Items[i].ToString();
                string[] dizi = satir.Split('-');

                if (dizi.Length < 2)
                {
                    continue;//hatırlat seçilmemiş
                }

                else if (dizi.Length > 2)
                {

                    if (DateTime.Now.ToString("dd.MM.yyyy HH:mm").Trim() == DateTime.Parse(dizi[2]).ToString("dd.MM.yyyy HH:mm").Trim())
                    {
                        flag++;
                        listBox1.SelectedIndex = i;
                        string alarmmesaj = dizi[0] + " " + dizi[1];
                        alarmpaneli(alarmmesaj);

                        playmp3();

                    }



                }



            }
        }
    }
}
