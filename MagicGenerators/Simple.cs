using System;
using System.Drawing;

namespace MagicGenerators
{
	/// <summary>
	/// Summary description for Character.
	/// </summary>
	public class Character
	{
		public Character(char AChar)
		{
			FChar = AChar;
		}

		private char FChar;

		public char Value { get { return FChar; } set { value = FChar; } }

		public override string ToString()
		{
			return FChar.ToString();
		}

	}

	class McKayRandom : Random 
	{
		public McKayRandom() : base()
		{
		}

		public char RandomLetter()
		{
			return (char)('A' + Next(26));
		}
		public Color RandomColor(Color FMatchHue)
		{
			Color LColor;
			do
			{
				LColor = RandomColor();
				
			} while (LColor.GetHue() != FMatchHue.GetHue());
			return LColor;

		}
		public Color RandomColor()
		{
			return Color.FromArgb(Next(255),Next(255),Next(255));
		}
	}

}
