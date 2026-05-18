using System.ComponentModel;

namespace OOP_laba7;

partial class WelcomeForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        label1 = new System.Windows.Forms.Label();
        button1 = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
        label1.Location = new System.Drawing.Point(186, 28);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(438, 213);
        label1.TabIndex = 0;
        label1.Text = ("Лабораторная работа №7. Выполнили студенты группы 24ВП1 Песков Р. С. и Шадчина Е." + " С.");
        // 
        // button1
        // 
        button1.Location = new System.Drawing.Point(303, 327);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(175, 47);
        button1.TabIndex = 1;
        button1.Text = "Продолжить";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click_1;
        // 
        // WelcomeForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(button1);
        Controls.Add(label1);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Text = "OOP7";
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button button1;

    private System.Windows.Forms.Label label1;

    #endregion
}