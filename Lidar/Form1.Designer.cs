using System.ComponentModel;
using System.Windows.Forms;

namespace Lidar
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.webCameraImage1 = new System.Windows.Forms.PictureBox();
            this.webCameraImage2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.distance = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.resultMap = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.webCameraImage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.webCameraImage2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultMap)).BeginInit();
            this.SuspendLayout();
            // 
            // webCameraImage1
            // 
            this.webCameraImage1.Location = new System.Drawing.Point(12, 51);
            this.webCameraImage1.Name = "webCameraImage1";
            this.webCameraImage1.Size = new System.Drawing.Size(320, 240);
            this.webCameraImage1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.webCameraImage1.TabIndex = 0;
            this.webCameraImage1.TabStop = false;
            // 
            // webCameraImage2
            // 
            this.webCameraImage2.Location = new System.Drawing.Point(338, 50);
            this.webCameraImage2.Name = "webCameraImage2";
            this.webCameraImage2.Size = new System.Drawing.Size(320, 240);
            this.webCameraImage2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.webCameraImage2.TabIndex = 0;
            this.webCameraImage2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Camera 1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(354, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Camera 2:";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(377, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Distance to point:";
            // 
            // distance
            // 
            this.distance.AutoSize = true;
            this.distance.Location = new System.Drawing.Point(474, 4);
            this.distance.Name = "distance";
            this.distance.Size = new System.Drawing.Size(31, 13);
            this.distance.TabIndex = 3;
            this.distance.Text = "none";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(583, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // resultMap
            // 
            this.resultMap.BackColor = System.Drawing.Color.White;
            this.resultMap.Location = new System.Drawing.Point(12, 372);
            this.resultMap.Name = "resultMap";
            this.resultMap.Size = new System.Drawing.Size(412, 302);
            this.resultMap.TabIndex = 5;
            this.resultMap.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(16, 330);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Stop and get result";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(670, 681);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.resultMap);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.distance);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.webCameraImage2);
            this.Controls.Add(this.webCameraImage1);
            this.Name = "Form1";
            this.Text = "Robo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.webCameraImage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.webCameraImage2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox webCameraImage1;
        private PictureBox webCameraImage2;
        private Label label1;
        private Label label2;
        private Timer timer1;
        private Label label3;
        private Label distance;
        private Button button1;
        private PictureBox resultMap;
        private Button button2;
    }
}

