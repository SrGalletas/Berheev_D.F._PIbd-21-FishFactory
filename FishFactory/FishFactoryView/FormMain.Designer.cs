namespace FishFactoryView
{
    partial class FormMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.клиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.компонентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изделияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.складыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пополнитьСкладToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonCreateRequest = new System.Windows.Forms.Button();
            this.buttonTakeRequestInWork = new System.Windows.Forms.Button();
            this.buttonRequestReady = new System.Windows.Forms.Button();
            this.buttonPayRequest = new System.Windows.Forms.Button();
            this.buttonRef = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem,
            this.пополнитьСкладToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.клиентыToolStripMenuItem,
            this.компонентыToolStripMenuItem,
            this.изделияToolStripMenuItem,
            this.складыToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // клиентыToolStripMenuItem
            // 
            this.клиентыToolStripMenuItem.Name = "клиентыToolStripMenuItem";
            this.клиентыToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.клиентыToolStripMenuItem.Text = "Клиенты";
            this.клиентыToolStripMenuItem.Click += new System.EventHandler(this.клиентыToolStripMenuItem_Click);
            // 
            // компонентыToolStripMenuItem
            // 
            this.компонентыToolStripMenuItem.Name = "компонентыToolStripMenuItem";
            this.компонентыToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.компонентыToolStripMenuItem.Text = "Компоненты";
            this.компонентыToolStripMenuItem.Click += new System.EventHandler(this.компонентыToolStripMenuItem_Click);
            // 
            // изделияToolStripMenuItem
            // 
            this.изделияToolStripMenuItem.Name = "изделияToolStripMenuItem";
            this.изделияToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.изделияToolStripMenuItem.Text = "Изделия";
            this.изделияToolStripMenuItem.Click += new System.EventHandler(this.изделияToolStripMenuItem_Click);
            // 
            // складыToolStripMenuItem
            // 
            this.складыToolStripMenuItem.Name = "складыToolStripMenuItem";
            this.складыToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.складыToolStripMenuItem.Text = "Склады";
            this.складыToolStripMenuItem.Click += new System.EventHandler(this.складыToolStripMenuItem_Click);
            // 
            // пополнитьСкладToolStripMenuItem
            // 
            this.пополнитьСкладToolStripMenuItem.Name = "пополнитьСкладToolStripMenuItem";
            this.пополнитьСкладToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
            this.пополнитьСкладToolStripMenuItem.Text = "Пополнить склад";
            this.пополнитьСкладToolStripMenuItem.Click += new System.EventHandler(this.пополнитьСкладToolStripMenuItem_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(7, 36);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(583, 388);
            this.dataGridView.TabIndex = 1;
            // 
            // buttonCreateRequest
            // 
            this.buttonCreateRequest.Location = new System.Drawing.Point(632, 64);
            this.buttonCreateRequest.Name = "buttonCreateRequest";
            this.buttonCreateRequest.Size = new System.Drawing.Size(156, 23);
            this.buttonCreateRequest.TabIndex = 2;
            this.buttonCreateRequest.Text = "Создать заказ";
            this.buttonCreateRequest.UseVisualStyleBackColor = true;
            this.buttonCreateRequest.Click += new System.EventHandler(this.buttonCreateRequest_Click);
            // 
            // buttonTakeRequestInWork
            // 
            this.buttonTakeRequestInWork.Location = new System.Drawing.Point(632, 115);
            this.buttonTakeRequestInWork.Name = "buttonTakeRequestInWork";
            this.buttonTakeRequestInWork.Size = new System.Drawing.Size(156, 23);
            this.buttonTakeRequestInWork.TabIndex = 3;
            this.buttonTakeRequestInWork.Text = "Отдать на выполнение";
            this.buttonTakeRequestInWork.UseVisualStyleBackColor = true;
            this.buttonTakeRequestInWork.Click += new System.EventHandler(this.buttonTakeRequestInWork_Click);
            // 
            // buttonRequestReady
            // 
            this.buttonRequestReady.Location = new System.Drawing.Point(632, 168);
            this.buttonRequestReady.Name = "buttonRequestReady";
            this.buttonRequestReady.Size = new System.Drawing.Size(156, 23);
            this.buttonRequestReady.TabIndex = 4;
            this.buttonRequestReady.Text = "Заказ готов";
            this.buttonRequestReady.UseVisualStyleBackColor = true;
            this.buttonRequestReady.Click += new System.EventHandler(this.buttonRequestReady_Click);
            // 
            // buttonPayRequest
            // 
            this.buttonPayRequest.Location = new System.Drawing.Point(632, 220);
            this.buttonPayRequest.Name = "buttonPayRequest";
            this.buttonPayRequest.Size = new System.Drawing.Size(156, 23);
            this.buttonPayRequest.TabIndex = 5;
            this.buttonPayRequest.Text = "Заказ оплачен";
            this.buttonPayRequest.UseVisualStyleBackColor = true;
            this.buttonPayRequest.Click += new System.EventHandler(this.buttonPayRequest_Click);
            // 
            // buttonRef
            // 
            this.buttonRef.Location = new System.Drawing.Point(632, 263);
            this.buttonRef.Name = "buttonRef";
            this.buttonRef.Size = new System.Drawing.Size(156, 23);
            this.buttonRef.TabIndex = 6;
            this.buttonRef.Text = "Обновить список";
            this.buttonRef.UseVisualStyleBackColor = true;
            this.buttonRef.Click += new System.EventHandler(this.buttonRef_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonRef);
            this.Controls.Add(this.buttonPayRequest);
            this.Controls.Add(this.buttonRequestReady);
            this.Controls.Add(this.buttonTakeRequestInWork);
            this.Controls.Add(this.buttonCreateRequest);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Рыбный завод";
          
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem клиентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem компонентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изделияToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonCreateRequest;
        private System.Windows.Forms.Button buttonTakeRequestInWork;
        private System.Windows.Forms.Button buttonRequestReady;
        private System.Windows.Forms.Button buttonPayRequest;
        private System.Windows.Forms.Button buttonRef;
        private System.Windows.Forms.ToolStripMenuItem складыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пополнитьСкладToolStripMenuItem;
    }
}

