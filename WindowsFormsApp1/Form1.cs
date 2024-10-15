using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private int[] GenerateNumericalKey(string keyword)
        {
            return keyword.ToLower().Select(c =>
                c >= 'а' && c <= 'я' ? c - 'а' : c >= 'a' && c <= 'z' ? c - 'a' : -1).ToArray();
        }

        private string VigenereEncrypt(string plaintext, string keyword, int[] numericalKey)
        {
            char[] ciphertext = new char[plaintext.Length];
            int keyLength = numericalKey.Length;

            for (int i = 0; i < plaintext.Length; i++)
            {
                char charToEncrypt = plaintext[i];
                if (charToEncrypt >= 'а' && charToEncrypt <= 'я')
                {
                    int shift = numericalKey[i % keyLength];
                    ciphertext[i] = (char)((charToEncrypt - 'а' + shift) % 32 + 'а');
                }
                else if (charToEncrypt >= 'a' && charToEncrypt <= 'z')
                {
                    int shift = numericalKey[i % keyLength];
                    ciphertext[i] = (char)((charToEncrypt - 'a' + shift) % 26 + 'a');
                }
                else
                {
                    ciphertext[i] = charToEncrypt; // Non-alphabetic characters are not changed
                }
            }

            return new string(ciphertext);
        }

        private string VigenereDecrypt(string ciphertext, string keyword, int[] numericalKey)
        {
            char[] plaintext = new char[ciphertext.Length];
            int keyLength = numericalKey.Length;

            for (int i = 0; i < ciphertext.Length; i++)
            {
                char charToDecrypt = ciphertext[i];
                if (charToDecrypt >= 'а' && charToDecrypt <= 'я')
                {
                    int shift = numericalKey[i % keyLength];
                    plaintext[i] = (char)((charToDecrypt - 'а' - shift + 32) % 32 + 'а');
                }
                else if (charToDecrypt >= 'a' && charToDecrypt <= 'z')
                {
                    int shift = numericalKey[i % keyLength];
                    plaintext[i] = (char)((charToDecrypt - 'a' - shift + 26) % 26 + 'a');
                }
                else
                {
                    plaintext[i] = charToDecrypt; // Non-alphabetic characters are not changed
                }
            }

            return new string(plaintext);
        }


        private void EncryptButton_Click(object sender, EventArgs e)
        {
            string keyword = keywordTextBox.Text;
            string plaintext = textBoxInput.Text;

            if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(plaintext))
            {
                MessageBox.Show("Введите текст и ключевое слово.", "Внимание");
                return;
            }

            int[] numericalKey = GenerateNumericalKey(keyword);
            string key = "";
            for (int i = 0; i < numericalKey.Length; i++)
            {
                key = key + Convert.ToString(numericalKey[i]) + " ";
            }
            numericalKeyTextBox.Text = key;
            string encryptedText = VigenereEncrypt(plaintext, keyword, numericalKey);
            textBoxOutput.Text = encryptedText;
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            string keyword = keywordTextBox.Text;
            string ciphertext = textBoxInput.Text;

            if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(ciphertext))
            {
                MessageBox.Show("Введите текст и ключевое слово.", "Внимание");
                return;
            }

            int[] numericalKey = GenerateNumericalKey(keyword);
            string key="";
            for (int i =0; i < numericalKey.Length; i++)
            {
                key = key + Convert.ToString(numericalKey[i]) + " ";
            }
            numericalKeyTextBox.Text = key;
            string decryptedText = VigenereDecrypt(ciphertext, keyword, numericalKey);
            textBoxOutput.Text = decryptedText;
        }

    }
}
