using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using MagicGenerators;

namespace PilgrimageWinImage
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private ObjectTimer FTimer;
		private ImageGenerator FGenerator;

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox PilgrimImg;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


			MagicImage LImage = new PictureGrabber(PilgrimImg.Image,50);
			MagicBackground LBackground = new ImageBackground(LImage.ImageHeight, 100);

			this.FGenerator = new ImageGenerator(LImage, LBackground);

			
			this.pictureBox1.Image = ((ImageGenerator)FGenerator).GetStereogram();



			FTimer = new ObjectTimer(this);
			FTimer.Interval = 33;
			//			FTimer.Interval = 100;
			FTimer.Tick += new EventHandler(UpdateData);

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.PilgrimImg = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(1224, 774);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// PilgrimImg
			// 
			this.PilgrimImg.Image = ((System.Drawing.Image)(resources.GetObject("PilgrimImg.Image")));
			this.PilgrimImg.Location = new System.Drawing.Point(328, 168);
			this.PilgrimImg.Name = "PilgrimImg";
			this.PilgrimImg.Size = new System.Drawing.Size(408, 352);
			this.PilgrimImg.TabIndex = 1;
			this.PilgrimImg.TabStop = false;
			this.PilgrimImg.Visible = false;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(1224, 774);
			this.Controls.Add(this.PilgrimImg);
			this.Controls.Add(this.pictureBox1);
			this.Name = "Form1";
			this.Text = "Form1";
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

		private static void UpdateData(Object AObject, EventArgs AEventArgs)
		{
			Form1 LForm = (Form1)((ObjectTimer)AObject).ObjectRef;
			System.Drawing.Graphics LGraphics = LForm.pictureBox1.CreateGraphics();

			LGraphics.DrawImage(LForm.FGenerator.GetStereogram(),0,0);
			//			LForm.Box.Text = LForm.FGenerator.GetStereogram();

		}


		private void Form1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			KeyHandler(e);
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
