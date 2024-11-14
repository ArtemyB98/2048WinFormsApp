using _2048.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048WinFormsApp
{
    public partial class ResutlsForm : Form
    {
        public ResutlsForm()
        {
            InitializeComponent();
        }

        private void ResutlsForm_Load(object sender, EventArgs e)
        {
            var users = UserResults.GetAll();
            foreach (var user in users)
            {
                resultsDataGridView.Rows.Add(user.Name, user.Score);
            }
        }
    }
}
