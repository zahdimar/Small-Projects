namespace DynamicCMF.Client
{
    partial class DatabaseInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.DataTables = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.DatabaseName = new System.Windows.Forms.Label();
            this.ReloadButton = new System.Windows.Forms.Button();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ViewInfo = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataTables)).BeginInit();
            this.SuspendLayout();
            // 
            // DataTables
            // 
            this.DataTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataTables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.TableName,
            this.ViewInfo});
            this.DataTables.Location = new System.Drawing.Point(266, 12);
            this.DataTables.Name = "DataTables";
            this.DataTables.RowTemplate.Height = 33;
            this.DataTables.Size = new System.Drawing.Size(994, 849);
            this.DataTables.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Database name";
            // 
            // DatabaseName
            // 
            this.DatabaseName.AutoSize = true;
            this.DatabaseName.Location = new System.Drawing.Point(51, 84);
            this.DatabaseName.Name = "DatabaseName";
            this.DatabaseName.Size = new System.Drawing.Size(124, 25);
            this.DatabaseName.TabIndex = 1;
            this.DatabaseName.Text = "placeholder";
            // 
            // ReloadButton
            // 
            this.ReloadButton.Location = new System.Drawing.Point(34, 142);
            this.ReloadButton.Name = "ReloadButton";
            this.ReloadButton.Size = new System.Drawing.Size(189, 47);
            this.ReloadButton.TabIndex = 2;
            this.ReloadButton.Text = "Reload scheme";
            this.ReloadButton.UseVisualStyleBackColor = true;
            // 
            // Number
            // 
            this.Number.HeaderText = "№";
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            this.Number.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // TableName
            // 
            this.TableName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TableName.HeaderText = "Table name";
            this.TableName.Name = "TableName";
            this.TableName.ReadOnly = true;
            // 
            // ViewInfo
            // 
            this.ViewInfo.HeaderText = "Info";
            this.ViewInfo.Name = "ViewInfo";
            this.ViewInfo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DatabaseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 873);
            this.Controls.Add(this.ReloadButton);
            this.Controls.Add(this.DatabaseName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DataTables);
            this.Name = "DatabaseInfo";
            this.Text = "DatabaseInfo";
            ((System.ComponentModel.ISupportInitialize)(this.DataTables)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataTables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DatabaseName;
        private System.Windows.Forms.Button ReloadButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableName;
        private System.Windows.Forms.DataGridViewButtonColumn ViewInfo;
    }
}