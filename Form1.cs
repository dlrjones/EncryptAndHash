using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using KeyMaster;

namespace EncryptAndHash
{
    public partial class Form1 : Form
    {
        private static NameValueCollection ConfigData = null;

        public Form1()
        {
            InitializeComponent();
            ConfigData = (NameValueCollection)ConfigurationSettings.GetConfig("appSettings");
            tbString.Select();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string key = tbKey.Text.Trim();
            tbHash.Text = "";
            if(key.Length > 0)
                tbHash.Text = StringCipher.Encrypt(tbString.Text.Trim(),key);
            else
                //The default key (kept in the KeyMaster.StringCipher class) is used when the key field is left blank
                tbHash.Text = StringCipher.Encrypt(tbString.Text.Trim());    
        }
        /// <summary>
        /// ///////////%SystemRoot%\system32\gameux.dll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            string key = tbKey.Text.Trim();
            tbString.Text = "";
            try
            {
                if (key.Length > 0)
                    tbString.Text = StringCipher.Decrypt(tbHash.Text.Trim(), key);
                else
                    tbString.Text = StringCipher.Decrypt(tbHash.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("There's a problem with the encrypted text" + Environment.NewLine +
                                                ex.Message, "Decryption Problem", MessageBoxButtons.OK);
            }
        }

        private void btnHash_Click(object sender, EventArgs e)
        {             
            if (CheckHashCount())
            {
                if (CheckInput(tbString.Text.Trim()))
                {
                    tbHash.Text = "";
                    Cursor.Current = Cursors.AppStarting;
                    int count = Convert.ToInt32(tbHashCount.Text);
                    string source = CreateHash(tbKey.Text.Trim(), count);
                    source += CreateHash(tbString.Text.Trim(), count);
                    source = CreateHash(source, count);
                    tbHash.Text = source;
                }
            }
            else
            {
                MessageBox.Show("The hash count needs to be a number", "Bad Hash Count", MessageBoxButtons.OK);
            }
        }     

        private string CreateHash(string source, int count)
        {
            StringBuilder sb = new StringBuilder();
            Encoding enc = Encoding.UTF8;
            Byte[] result = null;
            using (SHA256 hash = SHA256Managed.Create())
            {
                while (count-- > 0)
                {
                    result = hash.ComputeHash(enc.GetBytes(source));
                    foreach (Byte b in result)
                        sb.Append(b.ToString("x2"));
                    source = sb.ToString();
                    sb.Remove(0, sb.Length);
                }
            }
            return source;
        }

        private bool CheckInput(string targetText)
        {
            bool goodToGo = true;
            if (targetText.Length == 0)
            {
                goodToGo = false;
                MessageBox.Show("Enter somthing to Hash", "Nothing To Do", MessageBoxButtons.OK);
            }
            return goodToGo;
        }

        private bool CheckHashCount()
        {
            bool goodToGo = false;
            int isInt = 0;
            try
            {
                isInt = Convert.ToInt32(tbHashCount.Text);
                goodToGo = true;
            }
            catch (Exception ex)
            {
            }
                return goodToGo;           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbHash.Text = "";
            tbString.Text = "";
            tbKey.Text = "";
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To Encrypt or Hash" + Environment.NewLine +
                                        "Enter the text that you want to Encrypt or Hash" + Environment.NewLine +
                                        "Enter an Encryption/Hash key. There is a default key so this is optional" + Environment.NewLine +
                                        "Click Encrypt or Hash. The result appears in the Encrypted Text box." + Environment.NewLine + Environment.NewLine +
                                        "To Decrypt" + Environment.NewLine +
                                        "Paste the encrypted text string into the Encrypted Text box" + Environment.NewLine +
                                        "Enter the key, if there is one. If you don't know it try leaving this blank." + Environment.NewLine +
                                        "Click the Decrypt button" + Environment.NewLine +
                                        "The decrypted text appears in the Encrypt or Hash text box." + Environment.NewLine + Environment.NewLine +
                                        "Encryption is done with the RijndaelManaged Cryptography class in .NET " + Environment.NewLine +
                                        "with a 256 block size, the current AES standard." + Environment.NewLine +
                                        "Hashing uses the SHA256Managed class which creates a 32 byte fixed length hash"
                                        , "How To Use This", MessageBoxButtons.OK);
        }
    }
}
