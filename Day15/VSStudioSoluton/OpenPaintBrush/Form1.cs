namespace OpenPaintBrush
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg= new ColorDialog();
            dlg.ShowDialog();
        }
    }
}