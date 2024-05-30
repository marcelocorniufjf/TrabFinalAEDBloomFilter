using System.Drawing.Imaging;
using System.Windows.Forms;

namespace BloomFilterWindowsForms
{
    public partial class Form1 : Form
    {
        private BloomFilter bloomFilter;
        private string operacao = "";

        public Form1()
        {
            InitializeComponent();
            bloomFilter = new BloomFilter(32, 4, pictureBox1);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            int value = int.Parse(txtValue.Text);
            bloomFilter.Insert(value);
            txtValue.Focus();
            txtValue.SelectAll();
            operacao = $"inserir_{txtValue.Text}";
        }

        private void btnLookup_Click(object sender, EventArgs e)
        {
            int value = int.Parse(txtValue.Text);
            bloomFilter.Lookup(value);
            txtValue.Focus();
            txtValue.SelectAll();
            operacao = $"buscar_{txtValue.Text}";
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnInsert_Click(sender, e);
            }
        }

        private void txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt == true && e.KeyCode == Keys.P)
            {
                pictureBox1.Image.Save($"E:\\Mestrado\\Disciplinas\\AED\\Trabalho Final\\Imagens\\BF_{operacao}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.jpg", ImageFormat.Png);
            }
        }
    }
}
