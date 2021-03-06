﻿using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using KeyMaster;
using LogDefault;

namespace EncryptAndHash
{
    public partial class Form1 : Form
    {
        private static NameValueCollection ConfigData = null;
        private bool deleteSource = false;
        private LogManager lm = LogManager.GetInstance();

        public Form1()
        {
            InitializeComponent();            
            AllowDrop = true;
            ConfigData = (NameValueCollection)ConfigurationManager.GetSection("appSettings");
            lm.LogFilePath = ConfigData.Get("logFilePath");
            lm.LogFile = ConfigData.Get("logFile");
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

        //private string GetKey()
        //{
        //    //returns the default encryption key which is itself encrypted and stored in the text file "setup.dll"
        //    string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //    string[] key = File.ReadAllLines(appDirectory + "setup.dll");
        //    return StringCipher.Decrypt(key[0]);
        //}

        private void btnFileEncrypt_Click(object sender, EventArgs e)
        {
            byte[] salt = new byte[] { 0x49, 0x54, 0x54, 0x56, 0x49, 0x51, 0x55, 0x49 }; // Must be at least eight bytes
            int iterations = 1052; // should be >= 1000.
            string password = tbFileKey.Text.Length > 0 ? tbFileKey.Text.Trim() : "";     // GetKey();
            string destinationFilename = "";
            string sourceFilename = tbFilePath.Text;
            string[] sourceFiles = sourceFilename.Split(Environment.NewLine.ToCharArray());
            try
            {
                foreach (string source in sourceFiles)
                {
                    if (source.Length > 0)
                    {
                        destinationFilename = source + ".dlr";
                        if (File.Exists(destinationFilename))
                            File.Delete(destinationFilename);

                        StringCipher.EncryptFile(source, destinationFilename, password, salt, iterations);
                        if (deleteSource)
                            File.Delete(source);
                    }
                }
                tbFilePath.Text = "";
                cbDelete.Checked = false;
            }catch(Exception ex)
            {
                MessageBox.Show("Something went wrong." + Environment.NewLine + "Check the log for the error message");
                lm.Write(ex.Message);
            }
        }       
     
        private void btnFileDecrypt_Click(object sender, EventArgs e)
        {
            byte[] salt = new byte[] { 0x49, 0x54, 0x54, 0x56, 0x49, 0x51, 0x55, 0x49 }; // Must be at least eight bytes
            int iterations = 1052; // >= 1000.
            string password = tbFileKey.Text.Length > 0 ? tbFileKey.Text.Trim() : "";  // GetKey();    
            string destinationFilename = "";
            string sourceFilename = tbFilePath.Text;
            string[] sourceFiles = sourceFilename.Split(Environment.NewLine.ToCharArray());
            try
            {
                foreach (string source in sourceFiles)
                {
                    if (source.Length > 0)
                    {
                        destinationFilename = source.Substring(0, source.Length - 4);
                        if (File.Exists(destinationFilename)) //if the original unencrypted file is present, delete it.
                            File.Delete(destinationFilename);
                        StringCipher.DecryptFile(source, destinationFilename, password, salt, iterations);
                        File.Delete(source);
                    }
                }
                tbFilePath.Text = "";
            }catch(Exception ex)
            {
                MessageBox.Show("Did you mean to click \"Encrypt\"?" + Environment.NewLine + "if not then check the log for the error message.");
                lm.Write(ex.Message);
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
                                       "The original file is restored to the same directory as the encrypted version and the encrypted version is deleted. If the original file is present it will be replaced." + Environment.NewLine + Environment.NewLine +
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
                                        "Click Encrypt or Hash. The result appears in the Encrypted Text box." + Environment.NewLine +
                                        "If you provide a key or change the number of hash iterations then you'll" + Environment.NewLine+
                                        "have to keep track of them, they aren't saved." + Environment.NewLine + Environment.NewLine +
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

        private void linkToLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = lm.LogFilePath + lm.LogFile;
            process.Start();
        }
    }
}
