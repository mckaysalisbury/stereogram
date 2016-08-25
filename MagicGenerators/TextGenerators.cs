#define _UGLYHACK

using System;
using System.Collections;


namespace MagicGenerators
{

	public class TextGenerator : Generator
	{
		// Constructor
		public TextGenerator(MagicImage AImage, MagicBackground ABackground) : base(AImage, ABackground)
		{

		}

/*		// Public Methods
		public string GetIntro()
		{
			string LReturn = 
				@"                    Welcome to ChiaPetOfBorg's Demo for Pilgrimage!

  This image is a Stereoscopic Magic Eye Image. This means that you will have to look beyond the image (or whatever it is that you do to get [or try] to see the 3D image. The characters will be " + FBackground.Width + @" characters apart. A sample image of the same width is presented below.

  Some people write demos and code all the 3D pipeline themselves. Others, utilize hardware on the machine designed to achieve the same results. I've taken that a step further, and utilize the hardware (wetware) built into the human body, the eye.

     The program accepts the following commands:
        > p,s,<space>,<enter> = Pause, Play, or Start (Press 1 to begin the demo).
        > q,<esc> = Quit
                                        " + "*";
			for (int i = 1; i < FBackground.Width; i++)
				LReturn += " ";
			LReturn += "*";
			return LReturn;
	
		}
*/
		public new string GetStereogram()
		{
			object[,] LArray = base.GetStereogram();

			string LReturner = "";
			for (int i = 0; i < LArray.GetLength(0); i++)
			{
				for (int j = 0; j < LArray.GetLength(1); j++)
				{
					LReturner += LArray[i,j];
				}
				LReturner += "\r\n";
			}
			return LReturner;
		}

		public string GetTextOLD()
		{
			// Set up return
			string LReturn = "";

			// initialize Image
			int[,] LDelta = MagicImage.GetSlopes(FImage.GetImage());

			// initialize locals
			Stack LStack = new Stack();
/*
			LStack.Push('M');
			LStack.Push('C');
			LStack.Push('K');
			LStack.Push('A');
			LStack.Push('Y');
*/
			int LHeight = FImage.ImageHeight;
			int LWidth = (int)LDelta.GetLongLength(1);

			// Hack for % not working on negative numbers. But we don't go below 0 anyway?
			//			int LFix = (int)AStuff[0].Length * 10;
			//			LFix = 0;// should still work right?
			// Do I even use this anymore?

			for(int i = 0; i < LHeight; i++)
			{
				object[] LCurrentBackgroundRow = FBackground[i];

/*
#if (UGLYHACK || true)
				object[] LClone = FBackground[i];
#endif
*/
				// Print out k bars of left border
				for (int k = 0; k < 1; k++)
					for(int j = 0; j < LCurrentBackgroundRow.Length; j++)
						LReturn += (LCurrentBackgroundRow[j]);	

				for(int LCount = 0, j = 0; LCount < LWidth; LCount++, j++)
				{
					// j is position in inner array
					j %= LCurrentBackgroundRow.Length; 
					// LCount is positon across screen.
					
					int LDeltaTemp;
					// LDeltaTemp is the current slope from ADelta, or 0 if ADelta is invalid

					LDeltaTemp = LDelta[i,LCount];
					// old checking code. Now is done automatically.
					//					if (i >= LDelta.GetLongLength(0) || LCount >= ADelta.GetLongLength(1))
					//						LDeltaTemp = 0;
					//					else 
					//						LDeltaTemp = LDelta[i, LCount];

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

					LReturn += (LCurrentBackgroundRow[j]);	
				}


				// Print out k bars of Right border
				for (int k = 0; k < 1; k++)
					for(int j = 0; j < LCurrentBackgroundRow.Length; j++)
						LReturn += (LCurrentBackgroundRow[j]);	

				LReturn += "\r\n";
/*
#if (!UGLYHACK)				
				while (LStack.Count > 0)
				{
					Character LChar = (Character)LStack.Pop();
					Character[] LTemp = new Character[FBackground[i].Length + 1];
					for (int l = 0; l < FBackground[i].Length; l++)
					{
						LTemp[l] = (Character)FBackground[i][l];
					}
					LTemp[LTemp.Length - 1] = LChar;
					FBackground[i] = LTemp;
				}

				LStack.Clear();
#else
				FBackground[i] = LClone;
#endif
*/
			}
			return LReturn;
		}


	}

}
