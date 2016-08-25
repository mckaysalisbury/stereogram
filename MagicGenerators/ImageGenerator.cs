using System;
using System.Drawing;


namespace MagicGenerators
{
	/// <summary>
	/// Summary description for ImageGenerator.
	/// </summary>
	public class ImageGenerator : Generator
	{

		public ImageGenerator(MagicImage AImage, MagicBackground ABackground) : base(AImage, ABackground)
		{

		}

		public new System.Drawing.Image GetStereogram()
		{
			object[,] LArray = base.GetStereogram();

			Bitmap LImage = new Bitmap(LArray.GetLength(1), LArray.GetLength(0));
			for (int i = 0; i < LImage.Height; i++)
			{
				for (int j = 0; j < LImage.Width; j++)
				{
					LImage.SetPixel(j,i,(Color)(LArray[i,j]));
				}
			}
			return LImage;
		}
	}
}
