namespace TestNumbers;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        btnStart = new Button();
        txtBits = new TextBox();
        txtResults = new TextBox();
        label1 = new Label();
        label2 = new Label();
        txtIterations = new TextBox();
        label3 = new Label();
        txtAverage = new TextBox();
        label4 = new Label();
        txtRange = new TextBox();
        label5 = new Label();
        txtMedian = new TextBox();
        radCPURandom = new RadioButton();
        radAlgoRandom = new RadioButton();
        lblOther = new Label();
        txtOther = new TextBox();
        progressBar1 = new ProgressBar();
        SuspendLayout();
        // 
        // btnStart
        // 
        btnStart.Location = new Point(12, 47);
        btnStart.Margin = new Padding(3, 2, 3, 2);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(101, 24);
        btnStart.TabIndex = 0;
        btnStart.Text = "Generate";
        btnStart.UseVisualStyleBackColor = true;
        btnStart.Click += Button1_Click;
        // 
        // txtBits
        // 
        txtBits.Location = new Point(200, 47);
        txtBits.Margin = new Padding(3, 2, 3, 2);
        txtBits.MaxLength = 2;
        txtBits.Name = "txtBits";
        txtBits.Size = new Size(42, 23);
        txtBits.TabIndex = 1;
        txtBits.Text = "8";
        txtBits.TextChanged += Txt_TextChanged;
        txtBits.KeyPress += Txt_KeyPress;
        txtBits.Leave += TxtBits_Leave;
        // 
        // txtResults
        // 
        txtResults.Location = new Point(12, 84);
        txtResults.Margin = new Padding(3, 2, 3, 2);
        txtResults.Multiline = true;
        txtResults.Name = "txtResults";
        txtResults.ScrollBars = ScrollBars.Both;
        txtResults.Size = new Size(402, 237);
        txtResults.TabIndex = 2;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(165, 52);
        label1.Name = "label1";
        label1.Size = new Size(29, 15);
        label1.TabIndex = 3;
        label1.Text = "Bits:";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(278, 52);
        label2.Name = "label2";
        label2.Size = new Size(59, 15);
        label2.TabIndex = 5;
        label2.Text = "Iterations:";
        // 
        // txtIterations
        // 
        txtIterations.Location = new Point(343, 48);
        txtIterations.Margin = new Padding(3, 2, 3, 2);
        txtIterations.Name = "txtIterations";
        txtIterations.Size = new Size(71, 23);
        txtIterations.TabIndex = 4;
        txtIterations.Text = "100";
        txtIterations.TextChanged += Txt_TextChanged;
        txtIterations.KeyPress += Txt_KeyPress;
        txtIterations.Leave += txtIterations_Leave;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(493, 108);
        label3.Name = "label3";
        label3.Size = new Size(53, 15);
        label3.TabIndex = 7;
        label3.Text = "Average:";
        // 
        // txtAverage
        // 
        txtAverage.Location = new Point(493, 125);
        txtAverage.Margin = new Padding(3, 2, 3, 2);
        txtAverage.Name = "txtAverage";
        txtAverage.ReadOnly = true;
        txtAverage.Size = new Size(195, 23);
        txtAverage.TabIndex = 6;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(444, 52);
        label4.Name = "label4";
        label4.Size = new Size(43, 15);
        label4.TabIndex = 9;
        label4.Text = "Range:";
        // 
        // txtRange
        // 
        txtRange.Location = new Point(493, 48);
        txtRange.Margin = new Padding(3, 2, 3, 2);
        txtRange.Name = "txtRange";
        txtRange.ReadOnly = true;
        txtRange.Size = new Size(195, 23);
        txtRange.TabIndex = 8;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(493, 182);
        label5.Name = "label5";
        label5.Size = new Size(50, 15);
        label5.TabIndex = 11;
        label5.Text = "Median:";
        // 
        // txtMedian
        // 
        txtMedian.Location = new Point(493, 199);
        txtMedian.Margin = new Padding(3, 2, 3, 2);
        txtMedian.Name = "txtMedian";
        txtMedian.ReadOnly = true;
        txtMedian.Size = new Size(195, 23);
        txtMedian.TabIndex = 10;
        // 
        // radCPURandom
        // 
        radCPURandom.AutoSize = true;
        radCPURandom.Checked = true;
        radCPURandom.Location = new Point(262, 13);
        radCPURandom.Margin = new Padding(3, 2, 3, 2);
        radCPURandom.Name = "radCPURandom";
        radCPURandom.Size = new Size(96, 19);
        radCPURandom.TabIndex = 12;
        radCPURandom.TabStop = true;
        radCPURandom.Text = "CPU Random";
        radCPURandom.UseVisualStyleBackColor = true;
        // 
        // radAlgoRandom
        // 
        radAlgoRandom.AutoSize = true;
        radAlgoRandom.Location = new Point(374, 13);
        radAlgoRandom.Margin = new Padding(3, 2, 3, 2);
        radAlgoRandom.Name = "radAlgoRandom";
        radAlgoRandom.Size = new Size(98, 19);
        radAlgoRandom.TabIndex = 13;
        radAlgoRandom.Text = "Algo Random";
        radAlgoRandom.UseVisualStyleBackColor = true;
        // 
        // lblOther
        // 
        lblOther.AutoSize = true;
        lblOther.Location = new Point(493, 250);
        lblOther.Name = "lblOther";
        lblOther.Size = new Size(0, 15);
        lblOther.TabIndex = 15;
        // 
        // txtOther
        // 
        txtOther.Location = new Point(493, 268);
        txtOther.Margin = new Padding(3, 2, 3, 2);
        txtOther.Name = "txtOther";
        txtOther.ReadOnly = true;
        txtOther.Size = new Size(195, 23);
        txtOther.TabIndex = 14;
        // 
        // progressBar1
        // 
        progressBar1.Location = new Point(12, 339);
        progressBar1.Name = "progressBar1";
        progressBar1.Size = new Size(676, 23);
        progressBar1.TabIndex = 16;
        // 
        // Form1
        // 
        AutoScaleMode = AutoScaleMode.None;
        ClientSize = new Size(700, 366);
        Controls.Add(progressBar1);
        Controls.Add(lblOther);
        Controls.Add(txtOther);
        Controls.Add(radAlgoRandom);
        Controls.Add(radCPURandom);
        Controls.Add(label5);
        Controls.Add(txtMedian);
        Controls.Add(label4);
        Controls.Add(txtRange);
        Controls.Add(label3);
        Controls.Add(txtAverage);
        Controls.Add(label2);
        Controls.Add(txtIterations);
        Controls.Add(label1);
        Controls.Add(txtResults);
        Controls.Add(txtBits);
        Controls.Add(btnStart);
        Margin = new Padding(3, 2, 3, 2);
        MaximizeBox = false;
        MaximumSize = new Size(716, 405);
        Name = "Form1";
        Text = "CPU Random vs ALGO Random";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button btnStart;
    private TextBox txtBits;
    private TextBox txtResults;
    private Label label1;
    private Label label2;
    private TextBox txtIterations;
    private Label label3;
    private TextBox txtAverage;
    private Label label4;
    private TextBox txtRange;
    private Label label5;
    private TextBox txtMedian;
    private RadioButton radCPURandom;
    private RadioButton radAlgoRandom;
    private Label lblOther;
    private TextBox txtOther;
    private ProgressBar progressBar1;
}
