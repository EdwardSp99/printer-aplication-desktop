namespace printer_aplication_desktop
{
    partial class frmPrinterForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPrintPage = new System.Windows.Forms.Button();
            this.btnPathImage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPrintPage
            // 
            this.btnPrintPage.Location = new System.Drawing.Point(148, 182);
            this.btnPrintPage.Name = "btnPrintPage";
            this.btnPrintPage.Size = new System.Drawing.Size(155, 40);
            this.btnPrintPage.TabIndex = 0;
            this.btnPrintPage.Text = "Imprimir";
            this.btnPrintPage.UseVisualStyleBackColor = true;
            this.btnPrintPage.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnPathImage
            // 
            this.btnPathImage.Location = new System.Drawing.Point(148, 256);
            this.btnPathImage.Name = "btnPathImage";
            this.btnPathImage.Size = new System.Drawing.Size(155, 38);
            this.btnPathImage.TabIndex = 1;
            this.btnPathImage.Text = "Logo";
            this.btnPathImage.UseVisualStyleBackColor = true;
            this.btnPathImage.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmPrinterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(436, 394);
            this.Controls.Add(this.btnPathImage);
            this.Controls.Add(this.btnPrintPage);
            this.Name = "frmPrinterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aplicación web para impresión";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPrintPage;
        private System.Windows.Forms.Button btnPathImage;
    }
}

