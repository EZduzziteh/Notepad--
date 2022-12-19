using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace test
{
    public partial class NotePadForm : Form
    {
        public string path;
        public bool hasUnsavedChanges = false;
        public NotePadForm()
        {
            InitializeComponent();
            Text = "Notepad-- (" + WindowCounter.fileNumber + ")";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NotePadForm form = new NotePadForm();
            WindowCounter.fileNumber++;
            form.Text = "Notepad-- (" + WindowCounter.fileNumber + ")";
            form.Show();

        }

        private async void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(path))
            {


                using (SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    ValidateNames = true,
                    Filter = "Text Documents | *.txt"
                })
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
                        {
                            await streamWriter.WriteLineAsync(rtb_Text_Display.Text);
                        }
                    }
                }
            }
            else
            {
                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    await streamWriter.WriteLineAsync(rtb_Text_Display.Text);
                }
            }

            hasUnsavedChanges = false;
        }

       

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hasUnsavedChanges)
            {
                e.Cancel = true;
                Console.WriteLine("Save First?");
                SaveChangesDialog dialog = new SaveChangesDialog();
                Controls.Add(dialog);
                dialog.BringToFront();
                dialog.SetNotePadForm(this);
                dialog.Location = new Point(200, 200);
            }
            else
            {
                e.Cancel = false;
                hasUnsavedChanges = false;
                Console.WriteLine("good to close!");
            }
        }

        private void rtb_Text_Display_TextChanged(object sender, EventArgs e)
        {
            hasUnsavedChanges = true;
        }


        private async void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Text Document | *.txt",
                ValidateNames = true,
                Multiselect = false
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader streamReader = new StreamReader(openFileDialog.FileName))
                    {

                        NotePadForm form = new NotePadForm();
                        WindowCounter.fileNumber++;
                        form.Text = "Notepad-- (" + WindowCounter.fileNumber + ")";
                        form.Show();
                        form.hasUnsavedChanges = false;
                        form.path = openFileDialog.FileName;
                        Task<string> text = streamReader.ReadToEndAsync();
                        form.rtb_Text_Display.Text = text.Result;




                    }
                }
            }
        }

        
    }
}
