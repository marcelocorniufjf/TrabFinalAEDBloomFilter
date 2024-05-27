using System.Windows.Forms;

namespace BloomFilterWindowsForms
{
    public partial class Form1 : Form
    {
        private BloomFilter bloomFilter;

        public Form1()
        {
            InitializeComponent();
            bloomFilter = new BloomFilter(50, 3, pictureBox1);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            int value = int.Parse(txtValue.Text);
            bloomFilter.Insert(value);
            txtValue.Focus();
            txtValue.SelectAll();
        }

        private void btnLookup_Click(object sender, EventArgs e)
        {
            int value = int.Parse(txtValue.Text);
            bloomFilter.Lookup(value);
            txtValue.Focus();
            txtValue.SelectAll();
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
    }
}
