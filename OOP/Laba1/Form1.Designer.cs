namespace Laba1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.AND = new System.Windows.Forms.Button();
            this.OR = new System.Windows.Forms.Button();
            this.IsklOR = new System.Windows.Forms.Button();
            this.NOT = new System.Windows.Forms.Button();
            this.Result = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Number1 = new System.Windows.Forms.TextBox();
            this.Number2 = new System.Windows.Forms.TextBox();
            this.Finish = new System.Windows.Forms.Button();
            this.buttonSeconary = new System.Windows.Forms.Button();
            this.buttonSixteens = new System.Windows.Forms.Button();
            this.buttonEigth = new System.Windows.Forms.Button();
            this.buttonTenth = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AND
            // 
            this.AND.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.AND.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AND.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AND.ForeColor = System.Drawing.Color.White;
            this.AND.Location = new System.Drawing.Point(12, 7);
            this.AND.Name = "AND";
            this.AND.Size = new System.Drawing.Size(184, 89);
            this.AND.TabIndex = 2;
            this.AND.Text = "И";
            this.AND.UseVisualStyleBackColor = false;
            this.AND.Click += new System.EventHandler(this.AND_Click);
            // 
            // OR
            // 
            this.OR.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.OR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OR.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OR.ForeColor = System.Drawing.Color.White;
            this.OR.Location = new System.Drawing.Point(12, 102);
            this.OR.Name = "OR";
            this.OR.Size = new System.Drawing.Size(184, 89);
            this.OR.TabIndex = 3;
            this.OR.Text = "ИЛИ";
            this.OR.UseVisualStyleBackColor = false;
            this.OR.Click += new System.EventHandler(this.OR_Click);
            // 
            // IsklOR
            // 
            this.IsklOR.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.IsklOR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IsklOR.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IsklOR.ForeColor = System.Drawing.Color.White;
            this.IsklOR.Location = new System.Drawing.Point(12, 197);
            this.IsklOR.Name = "IsklOR";
            this.IsklOR.Size = new System.Drawing.Size(184, 89);
            this.IsklOR.TabIndex = 4;
            this.IsklOR.Text = "Искл. ИЛИ";
            this.IsklOR.UseVisualStyleBackColor = false;
            this.IsklOR.Click += new System.EventHandler(this.IsklOR_Click);
            // 
            // NOT
            // 
            this.NOT.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.NOT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NOT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NOT.ForeColor = System.Drawing.Color.White;
            this.NOT.Location = new System.Drawing.Point(12, 297);
            this.NOT.Name = "NOT";
            this.NOT.Size = new System.Drawing.Size(184, 89);
            this.NOT.TabIndex = 5;
            this.NOT.Text = "НЕ";
            this.NOT.UseVisualStyleBackColor = false;
            this.NOT.Click += new System.EventHandler(this.NOT_Click);
            // 
            // Result
            // 
            this.Result.BackColor = System.Drawing.Color.Black;
            this.Result.Font = new System.Drawing.Font("Microsoft YaHei", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Result.ForeColor = System.Drawing.Color.White;
            this.Result.Location = new System.Drawing.Point(353, 233);
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(208, 61);
            this.Result.TabIndex = 7;
            this.Result.Text = "0";
            this.Result.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.DimGray;
            this.label4.Font = new System.Drawing.Font("Microsoft Tai Le", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label4.Location = new System.Drawing.Point(220, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 61);
            this.label4.TabIndex = 10;
            this.label4.Text = "Число 1";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DimGray;
            this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label2.Location = new System.Drawing.Point(220, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 61);
            this.label2.TabIndex = 11;
            this.label2.Text = "Число 2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.DimGray;
            this.label6.Font = new System.Drawing.Font("Microsoft Tai Le", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label6.Location = new System.Drawing.Point(220, 233);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 61);
            this.label6.TabIndex = 12;
            this.label6.Text = "Результат";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Number1
            // 
            this.Number1.Font = new System.Drawing.Font("Microsoft YaHei", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Number1.Location = new System.Drawing.Point(353, 53);
            this.Number1.Name = "Number1";
            this.Number1.Size = new System.Drawing.Size(208, 60);
            this.Number1.TabIndex = 14;
            // 
            // Number2
            // 
            this.Number2.Font = new System.Drawing.Font("Microsoft YaHei", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Number2.Location = new System.Drawing.Point(353, 146);
            this.Number2.Name = "Number2";
            this.Number2.Size = new System.Drawing.Size(208, 60);
            this.Number2.TabIndex = 16;
            // 
            // Finish
            // 
            this.Finish.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Finish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Finish.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Finish.ForeColor = System.Drawing.Color.White;
            this.Finish.Location = new System.Drawing.Point(224, 318);
            this.Finish.Name = "Finish";
            this.Finish.Size = new System.Drawing.Size(337, 56);
            this.Finish.TabIndex = 17;
            this.Finish.Text = "Посчитать";
            this.Finish.UseVisualStyleBackColor = false;
            this.Finish.Click += new System.EventHandler(this.Finish_Click);
            // 
            // buttonSeconary
            // 
            this.buttonSeconary.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.buttonSeconary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSeconary.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSeconary.ForeColor = System.Drawing.Color.White;
            this.buttonSeconary.Location = new System.Drawing.Point(586, 12);
            this.buttonSeconary.Name = "buttonSeconary";
            this.buttonSeconary.Size = new System.Drawing.Size(184, 89);
            this.buttonSeconary.TabIndex = 18;
            this.buttonSeconary.Text = "Двоичная";
            this.buttonSeconary.UseVisualStyleBackColor = false;
            this.buttonSeconary.Click += new System.EventHandler(this.buttonSeconary_Click);
            // 
            // buttonSixteens
            // 
            this.buttonSixteens.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.buttonSixteens.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSixteens.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSixteens.ForeColor = System.Drawing.Color.White;
            this.buttonSixteens.Location = new System.Drawing.Point(586, 306);
            this.buttonSixteens.Name = "buttonSixteens";
            this.buttonSixteens.Size = new System.Drawing.Size(184, 89);
            this.buttonSixteens.TabIndex = 19;
            this.buttonSixteens.Text = "Шестнадцатеричная";
            this.buttonSixteens.UseVisualStyleBackColor = false;
            this.buttonSixteens.Click += new System.EventHandler(this.buttonSixteens_Click);
            // 
            // buttonEigth
            // 
            this.buttonEigth.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.buttonEigth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEigth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonEigth.ForeColor = System.Drawing.Color.White;
            this.buttonEigth.Location = new System.Drawing.Point(586, 107);
            this.buttonEigth.Name = "buttonEigth";
            this.buttonEigth.Size = new System.Drawing.Size(184, 89);
            this.buttonEigth.TabIndex = 20;
            this.buttonEigth.Text = "Восьмеричная";
            this.buttonEigth.UseVisualStyleBackColor = false;
            this.buttonEigth.Click += new System.EventHandler(this.buttonEigth_Click);
            // 
            // buttonTenth
            // 
            this.buttonTenth.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.buttonTenth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTenth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonTenth.ForeColor = System.Drawing.Color.White;
            this.buttonTenth.Location = new System.Drawing.Point(586, 205);
            this.buttonTenth.Name = "buttonTenth";
            this.buttonTenth.Size = new System.Drawing.Size(184, 89);
            this.buttonTenth.TabIndex = 21;
            this.buttonTenth.Text = "Десятичная";
            this.buttonTenth.UseVisualStyleBackColor = false;
            this.buttonTenth.Click += new System.EventHandler(this.buttonTenth_Click);
            // 
            // Clear
            // 
            this.Clear.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Clear.ForeColor = System.Drawing.Color.White;
            this.Clear.Location = new System.Drawing.Point(333, 398);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(122, 40);
            this.Clear.TabIndex = 22;
            this.Clear.Text = "Очистка";
            this.Clear.UseVisualStyleBackColor = false;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.buttonTenth);
            this.Controls.Add(this.buttonEigth);
            this.Controls.Add(this.buttonSixteens);
            this.Controls.Add(this.buttonSeconary);
            this.Controls.Add(this.Finish);
            this.Controls.Add(this.Number2);
            this.Controls.Add(this.Number1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Result);
            this.Controls.Add(this.NOT);
            this.Controls.Add(this.IsklOR);
            this.Controls.Add(this.OR);
            this.Controls.Add(this.AND);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Бинарный калькулятор";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AND;
        private System.Windows.Forms.Button OR;
        private System.Windows.Forms.Button IsklOR;
        private System.Windows.Forms.Button NOT;
        private System.Windows.Forms.Label Result;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Number1;
        private System.Windows.Forms.TextBox Number2;
        private System.Windows.Forms.Button Finish;
        private System.Windows.Forms.Button buttonSeconary;
        private System.Windows.Forms.Button buttonSixteens;
        private System.Windows.Forms.Button buttonEigth;
        private System.Windows.Forms.Button buttonTenth;
        private System.Windows.Forms.Button Clear;
    }
}

