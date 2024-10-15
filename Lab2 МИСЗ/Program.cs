using System;
using System.Linq;
using System.Windows.Forms;

namespace VigenereCipherApp
{
    public partial class MainForm1 : Form
    {
        public MainForm1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
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
            try
            {
                string keyword = keywordTextBox.Text;
                string numericalKeyInput = numericalKeyTextBox.Text;
                int[] numericalKey = numericalKeyInput.Split(',')
                    .Select(int.Parse).ToArray();
                string plaintext = inputTextBox.Text;

                if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(plaintext) || numericalKey.Length == 0)
                {
                    MessageBox.Show("Введите текст, ключевое слово и числовой ключ.", "Внимание");
                    return;
                }

                string encryptedText = VigenereEncrypt(plaintext, keyword, numericalKey);
                resultTextBox.Text = encryptedText;
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите числовой ключ в формате: 1,2,3", "Ошибка");
            }
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = keywordTextBox.Text;
                string numericalKeyInput = numericalKeyTextBox.Text;
                int[] numericalKey = numericalKeyInput.Split(',')
                    .Select(int.Parse).ToArray();
                string ciphertext = inputTextBox.Text;

                if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(ciphertext) || numericalKey.Length == 0)
                {
                    MessageBox.Show("Введите текст, ключевое слово и числовой ключ.", "Внимание");
                    return;
                }

                string decryptedText = VigenereDecrypt(ciphertext, keyword, numericalKey);
                resultTextBox.Text = decryptedText;
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите числовой ключ в формате: 1,2,3", "Ошибка");
            }
        }
    }
}
