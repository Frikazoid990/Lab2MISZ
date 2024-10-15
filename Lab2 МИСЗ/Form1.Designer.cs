using System;
using System.Linq;
using System.Windows.Forms;

public class VigenereCipherForm : Form
{
    private TextBox textEntry;
    private TextBox keywordEntry;
    private TextBox numericalKeyEntry;
    private TextBox resultText;
    private Button encryptButton;
    private Button decryptButton;

    public VigenereCipherForm()
    {
        Text = "Шифрование методом Виженера";
        Width = 600;
        Height = 400;

        Label textLabel = new Label() { Text = "Введите текст:", Top = 10, Left = 10 };
        Controls.Add(textLabel);
        textEntry = new TextBox() { Multiline = true, Width = 500, Height = 100, Top = 30, Left = 10 };
        Controls.Add(textEntry);

        Label keywordLabel = new Label() { Text = "Введите ключевое слово:", Top = 140, Left = 10 };
        Controls.Add(keywordLabel);


    }
}