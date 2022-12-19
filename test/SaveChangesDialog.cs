using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class SaveChangesDialog : UserControl
    {

        public NotePadForm form;
        public SaveChangesDialog()
        {
            InitializeComponent();
        }

        public void SetNotePadForm(NotePadForm form)
        {
            this.form = form;
        }
      

        private void btn_close_Click(object sender, EventArgs e)
        {
            form.hasUnsavedChanges = false;
            form.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            form.hasUnsavedChanges = false;
            form.Controls.Remove(this);
        }
    }
}
