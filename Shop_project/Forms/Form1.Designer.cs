
namespace Shop_project
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxPriceBottom = new System.Windows.Forms.TextBox();
            this.textBoxPriceTop = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.grbox = new System.Windows.Forms.GroupBox();
            this.radioButtonExpansive = new System.Windows.Forms.RadioButton();
            this.radioButtonPure = new System.Windows.Forms.RadioButton();
            this.radioButtonPopularity = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNameSearch = new System.Windows.Forms.TextBox();
            this.listViewCatalog = new System.Windows.Forms.ListView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grbox.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox);
            this.splitContainer1.Panel1.Controls.Add(this.grbox);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxNameSearch);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listViewCatalog);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.groupBox1);
            this.groupBox.Controls.Add(this.label4);
            this.groupBox.Controls.Add(this.comboBoxCategory);
            resources.ApplyResources(this.groupBox, "groupBox");
            this.groupBox.Name = "groupBox";
            this.groupBox.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxPriceBottom);
            this.groupBox1.Controls.Add(this.textBoxPriceTop);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // textBoxPriceBottom
            // 
            resources.ApplyResources(this.textBoxPriceBottom, "textBoxPriceBottom");
            this.textBoxPriceBottom.Name = "textBoxPriceBottom";
            this.textBoxPriceBottom.TextChanged += new System.EventHandler(this.textBoxPriceBottom_TextChanged);
            this.textBoxPriceBottom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPriceBottom_KeyPress);
            // 
            // textBoxPriceTop
            // 
            resources.ApplyResources(this.textBoxPriceTop, "textBoxPriceTop");
            this.textBoxPriceTop.Name = "textBoxPriceTop";
            this.textBoxPriceTop.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPriceTop_KeyPress);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxCategory, "comboBoxCategory");
            this.comboBoxCategory.Name = "comboBoxCategory";
            // 
            // grbox
            // 
            this.grbox.Controls.Add(this.radioButtonExpansive);
            this.grbox.Controls.Add(this.radioButtonPure);
            this.grbox.Controls.Add(this.radioButtonPopularity);
            resources.ApplyResources(this.grbox, "grbox");
            this.grbox.Name = "grbox";
            this.grbox.TabStop = false;
            // 
            // radioButtonExpansive
            // 
            resources.ApplyResources(this.radioButtonExpansive, "radioButtonExpansive");
            this.radioButtonExpansive.Name = "radioButtonExpansive";
            this.radioButtonExpansive.UseVisualStyleBackColor = true;
            // 
            // radioButtonPure
            // 
            resources.ApplyResources(this.radioButtonPure, "radioButtonPure");
            this.radioButtonPure.Name = "radioButtonPure";
            this.radioButtonPure.UseVisualStyleBackColor = true;
            // 
            // radioButtonPopularity
            // 
            resources.ApplyResources(this.radioButtonPopularity, "radioButtonPopularity");
            this.radioButtonPopularity.Checked = true;
            this.radioButtonPopularity.Name = "radioButtonPopularity";
            this.radioButtonPopularity.TabStop = true;
            this.radioButtonPopularity.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxNameSearch
            // 
            resources.ApplyResources(this.textBoxNameSearch, "textBoxNameSearch");
            this.textBoxNameSearch.Name = "textBoxNameSearch";
            // 
            // listViewCatalog
            // 
            resources.ApplyResources(this.listViewCatalog, "listViewCatalog");
            this.listViewCatalog.HideSelection = false;
            this.listViewCatalog.LargeImageList = this.imageList;
            this.listViewCatalog.MultiSelect = false;
            this.listViewCatalog.Name = "listViewCatalog";
            this.listViewCatalog.SmallImageList = this.imageList;
            this.listViewCatalog.TileSize = new System.Drawing.Size(570, 100);
            this.listViewCatalog.UseCompatibleStateImageBehavior = false;
            this.listViewCatalog.View = System.Windows.Forms.View.Tile;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imageList, "imageList");
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbox.ResumeLayout(false);
            this.grbox.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNameSearch;
        private System.Windows.Forms.ListView listViewCatalog;
        private System.Windows.Forms.GroupBox grbox;
        private System.Windows.Forms.RadioButton radioButtonExpansive;
        private System.Windows.Forms.RadioButton radioButtonPure;
        private System.Windows.Forms.RadioButton radioButtonPopularity;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPriceBottom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPriceTop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ImageList imageList;
    }
}

