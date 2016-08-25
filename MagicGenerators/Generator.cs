using System;
using System.Collections;

namespace MagicGenerators
{
	/// <summary>
	/// This is the parent Generator class that both text and image generataors will use. 
	/// </summary>
	public abstract class Generator
	{
		public Generator(MagicImage AImage, MagicBackground ABackground)
		{
			FImage = AImage;
			FBackground = ABackground;
		}

		protected MagicBackground FBackground;

		/// <summary>
		/// This is the image which stores the height data for the image.
		/// </summary>
		protected MagicImage FImage;

		/// <summary>
		/// How many "Chixel"s high the image will be.
		/// </summary>
		public int Height { get { return FImage.ImageHeight; } }

		/// <summary>
		/// How many "Chixel"s widt the image will be.
		/// </summary>
		public int Width { get { return FImage.ImageWidth + FBackground.Width - 1; } } // minus one for the deltas

		protected object[,] GetStereogram()
		{
			// initialize Image
			int[,] LDelta = MagicImage.GetSlopes(FImage.GetImage());

			// initialize locals
			Stack LStack = new Stack();
			// (Add objects to the stack?)
//			int LHeight = FImage.ImageHeight;
//			int LWidth = (int)LDelta.GetLongLength(1);

			// Set up return
			object[,] LReturn = new object[Height, Width];

			for(int i = 0; i < Height; i++)
			{
				object[] LCurrentBackgroundRow = FBackground[i];

				// The position in the image
				int x = 0;

				// Print out k bars of left border

				for (int k = 0; k < 1; k++)
					for(int j = 0; j < LCurrentBackgroundRow.Length; j++)
						LReturn[i,x++] = (LCurrentBackgroundRow[j]);	
				int LOffset = x;

				for(int j = 0; x < Width; x++, j++)
				{
					// j is position in background row
					j %= LCurrentBackgroundRow.Length; 
					
					// LDeltaTemp is the current slope from ADelta, or 0 if ADelta is invalid
					int LDeltaTemp = LDelta[i, x - LOffset];

					// if slope is positive
					if (LDeltaTemp > 0)
					{
						for (int k = 0; k < LDeltaTemp; k++) // for each value of slope
						{
							LStack.Push(LCurrentBackgroundRow[j]); // save the current value for later

							// and remove it from the current row
							object[] LTemp = new object[LCurrentBackgroundRow.Length - 1]; 
							for (int l = 0; l < LTemp.Length; l++)
							{
								LTemp[l] = LCurrentBackgroundRow[l + (l >= j ? 1 : 0)];
							}
							LCurrentBackgroundRow = LTemp;

							// update j as necessary
							j %= LCurrentBackgroundRow.Length; 
						}
					}

					// if slope is positive
					if (LDeltaTemp < 0)
					{
						for (int k = 0; k > LDeltaTemp; k--) // for each value of the slope
						{
							Object LChixel = LStack.Pop(); // grab the value on the stack

							// put it back in the current row at the current position
							object[] LTemp = new object[LCurrentBackgroundRow.Length + 1];
							for (int l = 0; l < LTemp.Length; l++)
							{
								LTemp[l] = (l == j ? LChixel : LCurrentBackgroundRow[(l > j ? l - 1 : l )]);
							}
							LCurrentBackgroundRow = LTemp;

							// update j as necessary.
							j %= LCurrentBackgroundRow.Length; 

						}
					}



					LReturn[i,x] = (LCurrentBackgroundRow[j]);	
				}

				/*

				// K more sets
				for (int k = 0; k < 1; k++)
					for(int j = 0; j < LCurrentBackgroundRow.Length; j++)
						LReturn[i,x++] = (LCurrentBackgroundRow[j]);	
						*/
			
			}
			return LReturn;
		}



	}
}
