using QLicense;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iAdpetFFTDiagnostic
{
    public partial class DataselectionForm : Form
    {
        public DataselectionForm()
        {
            InitializeComponent();
        }

        private void comboBox1_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = radioButton2.Checked ? true : false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isOK = true;
            try
            {
                if (radioButton1.Checked)
                {
                    Form1 form = new Form1();
                    this.Visible = false;
                    form.ShowDialog();
                    this.Visible = true;
                }
                else if (radioButton3.Checked)
                {
                    if (string.IsNullOrEmpty((string)comboBox2.SelectedItem))
                    {
                        MessageBox.Show("Type of file not selected. Kindly retry.");
                        return;
                    }
                    else
                    {
                        FileSelectionForm fileSelectionForm = new FileSelectionForm();
                        fileSelectionForm.SelectedFileType = (string)comboBox2.SelectedItem;

                        if (fileSelectionForm.SelectedFileType == "CSV")
                        {
                            MessageBox.Show("CSV file types are not included in demo. Kindly contact administrator","Demo application info", MessageBoxButtons.OK,MessageBoxIcon.Information);
                            isOK = false;
                        }
                        //if (fileSelectionForm.SelectedFileType.Contains("Time"))
                        //{
                        //    MessageBox.Show("Analysis for files with TimeWave data are not included in demo. Kindly contact administrator", "Demo application info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    isOK = false;
                        //}
                        if(isOK)
                            fileSelectionForm.ShowDialog();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Visible = radioButton3.Checked ? true : false;
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        byte[] _certPubicKeyData;
        private void Form1_Shown(object sender, EventArgs e)
        {
            //IsLicensed();
        }
        private void IsLicensed()
        { 
            MyLicense _lic = null;
            string _msg = string.Empty;
            LicenseStatus _status = LicenseStatus.UNDEFINED;

            //Read public key from assembly
            Assembly _assembly = Assembly.GetExecutingAssembly();
            using (MemoryStream _mem = new MemoryStream())
            {
                string[] names = _assembly.GetManifestResourceNames();

                _assembly.GetManifestResourceStream("iAdpetFFTDiagnostic.LicenseVerify.cer").CopyTo(_mem);

                _certPubicKeyData = _mem.ToArray();
            }

            //Check if the XML license file exists
            if (File.Exists("license.lic"))
            {
                _lic = (MyLicense)LicenseHandler.ParseLicenseFromBASE64String(
                    typeof(MyLicense),
                    File.ReadAllText("license.lic"),
                    _certPubicKeyData,
                    out _status,
                    out _msg);
            }
            else
            {
                _status = LicenseStatus.INVALID;
                _msg = "Your copy of this application is not activated";
            }
            switch (_status)
            {
                case LicenseStatus.VALID:

                    //TODO: If license is valid, you can do extra checking here
                    if(_lic.CreateDateTime.AddDays(30)<DateTime.Today)
                    {
                        MessageBox.Show("Your License is Expired.", "Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Application.Exit();
                    }
                    //TODO: E.g., check license expiry date if you have added expiry date property to your license entity
                    //TODO: Also, you can set feature switch here based on the different properties you added to your license entity 

                    //Here for demo, additional checking       
                    //licInfo.ShowLicenseInfo(_lic);
                    

                    return;

                default:
                    //for the other status of license file, show the warning message
                    //and also popup the activation form for user to activate your application
                    MessageBox.Show(_msg, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    using (frmActivation frm = new frmActivation())
                    {
                        frm.CertificatePublicKeyData = _certPubicKeyData;
                        frm.ShowDialog();

                        //Exit the application after activation to reload the license file 
                        //Actually it is not nessessary, you may just call the API to reload the license file
                        //Here just simplied the demo process

                        Application.Exit();
                    }
                    break;

            }
        }
    }
}
