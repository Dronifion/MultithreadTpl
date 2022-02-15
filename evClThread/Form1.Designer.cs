namespace evClThread
{
    partial class Form1
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
            this.btnThreadStart = new System.Windows.Forms.Button();
            this.btnThreadStop = new System.Windows.Forms.Button();
            this.btnThreadRevive = new System.Windows.Forms.Button();
            this.btnThreadDestroy = new System.Windows.Forms.Button();
            this.lbDisplay = new System.Windows.Forms.ListBox();
            this.btnQueueRead = new System.Windows.Forms.Button();
            this.btnWaitForItStart = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCancelTPLNow = new System.Windows.Forms.Button();
            this.btnQueueClear = new System.Windows.Forms.Button();
            this.btnTaskCoor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnThreadStart
            // 
            this.btnThreadStart.Location = new System.Drawing.Point(12, 394);
            this.btnThreadStart.Name = "btnThreadStart";
            this.btnThreadStart.Size = new System.Drawing.Size(96, 23);
            this.btnThreadStart.TabIndex = 0;
            this.btnThreadStart.Text = "Start TPL task";
            this.btnThreadStart.UseVisualStyleBackColor = true;
            this.btnThreadStart.Click += new System.EventHandler(this.btnTPLStart_Click);
            // 
            // btnThreadStop
            // 
            this.btnThreadStop.Location = new System.Drawing.Point(114, 394);
            this.btnThreadStop.Name = "btnThreadStop";
            this.btnThreadStop.Size = new System.Drawing.Size(105, 23);
            this.btnThreadStop.TabIndex = 1;
            this.btnThreadStop.Text = "Continue TPL task";
            this.btnThreadStop.UseVisualStyleBackColor = true;
            this.btnThreadStop.Click += new System.EventHandler(this.btnTPLContinue_Click);
            // 
            // btnThreadRevive
            // 
            this.btnThreadRevive.Location = new System.Drawing.Point(351, 394);
            this.btnThreadRevive.Name = "btnThreadRevive";
            this.btnThreadRevive.Size = new System.Drawing.Size(105, 23);
            this.btnThreadRevive.TabIndex = 2;
            this.btnThreadRevive.Text = "APM TPL Task";
            this.btnThreadRevive.UseVisualStyleBackColor = true;
            this.btnThreadRevive.Click += new System.EventHandler(this.btnTPLAPM_Click);
            // 
            // btnThreadDestroy
            // 
            this.btnThreadDestroy.Location = new System.Drawing.Point(225, 394);
            this.btnThreadDestroy.Name = "btnThreadDestroy";
            this.btnThreadDestroy.Size = new System.Drawing.Size(120, 23);
            this.btnThreadDestroy.TabIndex = 3;
            this.btnThreadDestroy.Text = "Scheduler TPL Task";
            this.btnThreadDestroy.UseVisualStyleBackColor = true;
            this.btnThreadDestroy.Click += new System.EventHandler(this.btnTPLScheduler_Click);
            // 
            // lbDisplay
            // 
            this.lbDisplay.FormattingEnabled = true;
            this.lbDisplay.HorizontalScrollbar = true;
            this.lbDisplay.Location = new System.Drawing.Point(12, 12);
            this.lbDisplay.Name = "lbDisplay";
            this.lbDisplay.Size = new System.Drawing.Size(828, 342);
            this.lbDisplay.TabIndex = 4;
            // 
            // btnQueueRead
            // 
            this.btnQueueRead.Location = new System.Drawing.Point(12, 362);
            this.btnQueueRead.Name = "btnQueueRead";
            this.btnQueueRead.Size = new System.Drawing.Size(96, 23);
            this.btnQueueRead.TabIndex = 5;
            this.btnQueueRead.Text = "Read Queue";
            this.btnQueueRead.UseVisualStyleBackColor = true;
            this.btnQueueRead.Click += new System.EventHandler(this.btnQueueRead_Click);
            // 
            // btnWaitForItStart
            // 
            this.btnWaitForItStart.Location = new System.Drawing.Point(12, 423);
            this.btnWaitForItStart.Name = "btnWaitForItStart";
            this.btnWaitForItStart.Size = new System.Drawing.Size(164, 23);
            this.btnWaitForItStart.TabIndex = 6;
            this.btnWaitForItStart.Text = "Thread with Monitor WaitForIt";
            this.btnWaitForItStart.UseVisualStyleBackColor = true;
            this.btnWaitForItStart.Click += new System.EventHandler(this.btnThreadWaitFor_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(462, 394);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Test Cancellation TPL Task";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.TPLCancellation_Click);
            // 
            // btnCancelTPLNow
            // 
            this.btnCancelTPLNow.Location = new System.Drawing.Point(462, 423);
            this.btnCancelTPLNow.Name = "btnCancelTPLNow";
            this.btnCancelTPLNow.Size = new System.Drawing.Size(137, 23);
            this.btnCancelTPLNow.TabIndex = 8;
            this.btnCancelTPLNow.Text = "Cancel TPL Task Now";
            this.btnCancelTPLNow.UseVisualStyleBackColor = true;
            this.btnCancelTPLNow.Click += new System.EventHandler(this.btnCancelTPLNow_Click);
            // 
            // btnQueueClear
            // 
            this.btnQueueClear.Location = new System.Drawing.Point(114, 362);
            this.btnQueueClear.Name = "btnQueueClear";
            this.btnQueueClear.Size = new System.Drawing.Size(96, 23);
            this.btnQueueClear.TabIndex = 9;
            this.btnQueueClear.Text = "Clear Queue";
            this.btnQueueClear.UseVisualStyleBackColor = true;
            this.btnQueueClear.Click += new System.EventHandler(this.btnQueueClear_Click);
            // 
            // btnTaskCoor
            // 
            this.btnTaskCoor.Location = new System.Drawing.Point(182, 423);
            this.btnTaskCoor.Name = "btnTaskCoor";
            this.btnTaskCoor.Size = new System.Drawing.Size(230, 23);
            this.btnTaskCoor.TabIndex = 10;
            this.btnTaskCoor.Text = "TPL Task Coordination with Monitor WaitForIt";
            this.btnTaskCoor.UseVisualStyleBackColor = true;
            this.btnTaskCoor.Click += new System.EventHandler(this.btnTaskCoor_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 458);
            this.Controls.Add(this.btnTaskCoor);
            this.Controls.Add(this.btnQueueClear);
            this.Controls.Add(this.btnCancelTPLNow);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnWaitForItStart);
            this.Controls.Add(this.btnQueueRead);
            this.Controls.Add(this.lbDisplay);
            this.Controls.Add(this.btnThreadDestroy);
            this.Controls.Add(this.btnThreadRevive);
            this.Controls.Add(this.btnThreadStop);
            this.Controls.Add(this.btnThreadStart);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Task Parallel Library and Multi-thread";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnThreadStart;
        private System.Windows.Forms.Button btnThreadStop;
        private System.Windows.Forms.Button btnThreadRevive;
        private System.Windows.Forms.Button btnThreadDestroy;
        private System.Windows.Forms.ListBox lbDisplay;
        private System.Windows.Forms.Button btnQueueRead;
        private System.Windows.Forms.Button btnWaitForItStart;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCancelTPLNow;
        private System.Windows.Forms.Button btnQueueClear;
        private System.Windows.Forms.Button btnTaskCoor;
    }
}

