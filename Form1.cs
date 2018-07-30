using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.IO;
using KeyMaster;

namespace EncryptAndHash
{
    public partial class Form1 : Form
    {
        private static NameValueCollection ConfigData = null;
        private bool deleteSource = false;
        public Form1()
        {
            InitializeComponent();
            AllowDrop = true;
            ConfigData = (NameValueCollection)ConfigurationSettings.GetConfig("appSettings");
            tbString.Select();
        }

//------------------------------------------- Password Encryption -----------------------------------------

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

      



//------------------------------------------- File Encryption -----------------------------------------

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgBrowse = new OpenFileDialog();
            dlgBrowse.Title = "Select one or more files";
            dlgBrowse.SupportMultiDottedExtensions = true;
            dlgBrowse.Multiselect = true;
            if (dlgBrowse.ShowDialog() == DialogResult.OK)
            {
                foreach (string fname in dlgBrowse.FileNames)
                {
                    tbFilePath.Text += fname + Environment.NewLine;
                }
            }
        }

        private string GetKey()
        {
            //returns the default encryption key which is itself encrypted and stored in the text file "setup.dll"
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string[] key = File.ReadAllLines(appDirectory + "setup.dll");
            return StringCipher.Decrypt(key[0]);
        }

        private void btnFileEncrypt_Click(object sender, EventArgs e)
        {
            byte[] salt = new byte[] { 0x49, 0x54, 0x54, 0x56, 0x49, 0x51, 0x55, 0x49 }; // Must be at least eight bytes
            int iterations = 1052; // should be >= 1000.
            string password = tbFileKey.Text.Length > 0 ? tbFileKey.Text.Trim() : GetKey();
            string destinationFilename = "";
            string sourceFilename = tbFilePath.Text;
            string[] sourceFiles = sourceFilename.Split(Environment.NewLine.ToCharArray()); 
            foreach (string source in sourceFiles)
            {
                if(source.Length > 0){
                    destinationFilename = source + ".dlr";
                    if (File.Exists(destinationFilename))
                        File.Delete(destinationFilename);

                    EncryptFile(source, destinationFilename, password, salt, iterations);
                    if (deleteSource)
                        File.Delete(source);
                }                
            }
            tbFilePath.Text = "";
        }       

        private void EncryptFile(string sourceFilename, string destinationFilename, string password, byte[] salt, int iterations)
        {
            AesManaged aes = new AesManaged();
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            aes.Mode = CipherMode.CBC;
            ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);

            using (FileStream destination = new FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                using (CryptoStream cryptoStream = new CryptoStream(destination, transform, CryptoStreamMode.Write))
                {
                    using (FileStream source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        source.CopyTo(cryptoStream);
                    }
                }
            }
        }

        private void btnFileDecrypt_Click(object sender, EventArgs e)
        {
            byte[] salt = new byte[] { 0x49, 0x54, 0x54, 0x56, 0x49, 0x51, 0x55, 0x49 }; // Must be at least eight bytes
            int iterations = 1052; // >= 1000.
            string password = tbFileKey.Text.Length > 0 ? tbFileKey.Text.Trim() : GetKey();    
            string destinationFilename = "";
            string sourceFilename = tbFilePath.Text;
            string[] sourceFiles = sourceFilename.Split(Environment.NewLine.ToCharArray());

            foreach (string source in sourceFiles)
            {
                if (source.Length > 0)
                {
                    destinationFilename = source.Substring(0, source.Length - 4);
                    if (File.Exists(destinationFilename))
                        File.Delete(destinationFilename);
                    DecryptFile(source, destinationFilename, password, salt, iterations);
                    File.Delete(source);
                 }
            }
            tbFilePath.Text = "";
        }

        private void DecryptFile(string sourceFilename, string destinationFilename, string password, byte[] salt, int iterations)
        {           
            AesManaged aes = new AesManaged();
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            aes.Mode = CipherMode.CBC;
            ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);

            using (FileStream destination = new FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                using (CryptoStream cryptoStream = new CryptoStream(destination, transform, CryptoStreamMode.Write))
                {
                    try
                    {
                        using (FileStream source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            source.CopyTo(cryptoStream);
                        }
                    }
                    catch (CryptographicException exception)
                    {
                        if (exception.Message == "Padding is invalid and cannot be removed.")
                            throw new ApplicationException("Universal Microsoft Cryptographic Exception (Not to be believed!)", exception);
                        else
                            throw;
                    }
                }
            }
        }

        private void cbDelete_CheckedChanged(object sender, EventArgs e)
        {
            deleteSource = !deleteSource;
        }

        private void tbFilePath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void tbFilePath_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                tbFilePath.Text += file + Environment.NewLine;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To Encrypt a File" + Environment.NewLine +
                                       "You can drag the file or files into the text area or use the Browse button to " +
                                       "select the file(s). Enter an Encryption key. There is a default key so this is optional. When you supply a key " +
                                       "it is on you to keep track of it, it's not saved. Click Encrypt." + Environment.NewLine +
                                       "The encrypted file appears in the same directory as the original. It will have the extension \".dlr\" appended to it." + Environment.NewLine +
                                       "You can optionally elect to delete the original file by checking that box." + Environment.NewLine + Environment.NewLine +
                                       "To Decrypt" + Environment.NewLine +
                                       "Drag or Browse the encrypted files to the text area." + Environment.NewLine +
                                       "Enter the key, if there is one. If you don't know it try leaving this blank." + Environment.NewLine +
                                       "Click the Decrypt button" + Environment.NewLine +
                                       "The original file is restored to the same directory as the encrypted version and the encrypted version is deleted." + Environment.NewLine + Environment.NewLine +
                                       "Encryption is done with the RijndaelManaged Cryptography class in .NET " + Environment.NewLine +
                                       "with a 256 block size, the current AES standard." + Environment.NewLine +
                                       "Hashing uses the SHA256Managed class which creates a 32 byte fixed length hash"
                                       , "How To Use This", MessageBoxButtons.OK);

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
