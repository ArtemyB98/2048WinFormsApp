using _2048.Common;

namespace _2048WinFormsApp
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text.Length > 0)
            {
                Close();
            }
            else
            {
                MessageBox.Show("Fill the name");
            }
        }

        
    }
}
