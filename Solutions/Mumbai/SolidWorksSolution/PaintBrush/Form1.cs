namespace PaintBrush
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome to Windows app");
        }

        private void OnFileExit(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {

        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {

        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            Point point1=new Point(e.X, e.Y);
            string result=string.Format("X="+ point1.X + "Y="+point1.Y);
            this.Text = result;
        }
    }
}
