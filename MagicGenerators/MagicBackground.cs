using System;
using System.Drawing;


namespace MagicGenerators
{
	/// <summary>
	/// This contains an array of Chixels to be used as random data
	/// </summary>
	public abstract class MagicBackground
	{
		protected MagicBackground()
		{
            
		}

		protected object[,] FBackgroundData;
		public object[] this[int ARow]
		{
			get
			{ 
				object[] LReturner = new object[FBackgroundData.GetLength(1)];
				for (int i = 0; i < LReturner.Length; i++)
				{
					LReturner[i] = FBackgroundData[ARow % FBackgroundData.GetLength(0),i];
				}
				return LReturner;
			}
		}

		public int Width { get { return FBackgroundData.GetLength(1); } }

	}

	public class ImageBackground : MagicBackground
	{
		public ImageBackground(int AHeight, int AWidth)
		{
			FBackgroundData = GetRandomImage(AHeight, AWidth);
		}

		private object[,] GetRandomImage(int AHeight, int AWidth)
		{
			object[,] LColors = new object[AHeight,AWidth];
			McKayRandom LRandom = new McKayRandom();

			for (int i = 0; i < AHeight; i++)
				for (int j = 0; j < AWidth; j++)
				{
					LColors[i,j] = LRandom.RandomColor();
					// This stuff added for a more consistent color scheme
					// removed for faster rendering
					//LColors[i,j] = LRandom.RandomColor(Color.Green);
				}

			return LColors;

		}

	}

	public class TextBackground : MagicBackground
	{
		public TextBackground(int AHeight, int AWidth)
		{
			FBackgroundData = GetWords(AHeight, AWidth);
		}

		private Character[,] GetWords(int AHeight, int AWidth)
		{
			Character[,] LReturn = new Character[AHeight, AWidth];
			string[] LWords;
			switch (AWidth)
			{
				case 7:
					LWords = new string[] { "PILGRIM",
											  "GRAPHIC",
											  "DRAGONS",
											  "PAC-MAN",
											  "TEAPOTS",
											  "VIDGAME",
											  "CODE247",
											  "CAFEINE",
											  "GEKNERD",
											  "HACKERS",
											  "PROGRAM"};
					break;
				case 10:
					LWords = new string[] { "PILGRIMAGE",
											  "DEMO-SCENE",
											  "VIDEOGAMES",
											  "UTAHTEAPOT",
											  "RETROGAMES",
											  "UGLYDUCK3D",
											  "STEREOGRAM",
											  "DEMOPARTY!",
											  "WORLDCRAFT",
											  "SALTLAKEUT" };
					break;
				case 11:
					LWords = new string[] { "PILGRIMAGE!",
											  "DEMOSCENERS",
											  "EATBLOWFISH",
											  "SQUEZEFRUIT",
											  "JAPANPOISON",
											  "PRCUPINFISH",
											  "BLOWFISHING",
											  "ORANGEJUICE",
											  "FUGULIVERME",
											  "PASHDOWNRLZ",
											  "SALTLKECITY",
					};

					break;
				default: 
					return GetGarbage(AHeight, AWidth);
			}
			for (int i = 0; i < AHeight; i++)
			{
				for (int j = 0; j < AWidth; j++)
				{
					LReturn[i,j] = new Character(LWords[i % LWords.Length][j]);
				}
			}
			return LReturn;
		}
		private Character[,] GetGarbage(int AHeight, int AWidth)
		{
			McKayRandom LRandom = new McKayRandom();
			Character[,] LReturn = new Character[AHeight, AWidth];
			for(int i = 0;i < AHeight; i++)
			{
				for (int j = 0; j < AWidth; j++)
				{
					bool LRepeat;
					do
					{
						LRepeat = false;
						LReturn[i,j] = new Character(LRandom.RandomLetter());
						for (int LTemp = 0; LTemp < j; LTemp++)
							if (LReturn[i,j] == LReturn[i, LTemp])
								LRepeat = true;
					} while (LRepeat);

				}
			}
			return LReturn;
		}

	}
}
