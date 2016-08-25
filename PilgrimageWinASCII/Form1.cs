using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
//using System.Threading;
using MagicGenerators;

namespace PilgrimageWinASCII
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private ObjectTimer FTimer;
		private System.Windows.Forms.TextBox Box;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.PictureBox pictureBox1;

		private TextGenerator FGenerator;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			MagicImage LImage = new PacMan();
			MagicBackground LBack = new TextBackground(LImage.ImageHeight,10);
			FGenerator = new TextGenerator(LImage, LBack);

			FTimer = new ObjectTimer(this);
			FTimer.Interval = 33;
//			FTimer.Interval = 100;
			FTimer.Tick += new EventHandler(UpdateData);

			MagicImage LSampleImage = new PictureGrabber(this.pictureBox1.Image, 5);
			MagicBackground LBackground = new TextBackground(LSampleImage.ImageHeight,10);
			TextGenerator LGenerator = new TextGenerator(LSampleImage, LBackground);
//			this.Box.Text = FGenerator.GetIntro() + "\r\n"
			this.Box.Text += LGenerator.GetStereogram();

//			UpdateData(FTimer,null);
			this.Box.Select(0,0);
//			FTimer.Start();

			this.Width = (int)(FGenerator.Width * (this.Box.Font.Size * 0.75 + 1) + 15);
			this.Height = (int)(FGenerator.Height * (this.Box.Font.Size * 1.25 + 1) + 40);

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.Box = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// Box
			// 
			this.Box.AcceptsReturn = true;
			this.Box.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Box.Font = new System.Drawing.Font("Lucida Console", 16F);
			this.Box.Location = new System.Drawing.Point(0, 0);
			this.Box.Multiline = true;
			this.Box.Name = "Box";
			this.Box.ReadOnly = true;
			this.Box.Size = new System.Drawing.Size(496, 510);
			this.Box.TabIndex = 0;
			this.Box.Text = "";
			this.Box.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Box_KeyPress);
			this.Box.TextChanged += new System.EventHandler(this.Box_TextChanged);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(208, 112);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(176, 160);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Visible = false;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(13, 21);
			this.ClientSize = new System.Drawing.Size(496, 510);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.Box);
			this.Font = new System.Drawing.Font("Lucida Console", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "Form1";
			this.Text = "Magic Eye";
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			KeyHandler(e);
		}
		private static void UpdateData(Object AObject, EventArgs AEventArgs)
		{
			Form1 LForm = (Form1)((ObjectTimer)AObject).ObjectRef;
			LForm.Box.Text = LForm.FGenerator.GetStereogram();
		}

		private void KeyHandler(KeyPressEventArgs AArgs)
		{
			switch (AArgs.KeyChar)
			{
				case 'q':
				case 'Q':
				case '\x1B':
					Application.Exit();
					break;
				case 'p':
				case 'P':
				case 's':
				case 'S':
				case '\r':
				case '\n':
				case ' ':
					FTimer.Enabled = !FTimer.Enabled;
					break;

			}

		}

		private void Box_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			KeyHandler(e);
		}

		private void Box_TextChanged(object sender, System.EventArgs e)
		{
		
		}
	}
	class ObjectTimer : Timer
	{
		private Object FObjectRef;
		public Object ObjectRef { get { return FObjectRef; } }
		public ObjectTimer(Object AObjectRef) : base()
		{
			FObjectRef = AObjectRef;
		}
	}

}
