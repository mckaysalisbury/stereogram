using System;
using MagicGenerators;
namespace PilgrimageAscii
{
	
	class PilgrimageASCII
	{

		[STAThread]
		static void Main(string[] args)
		{
			MagicImage LImage = new PacMan();
			MagicBackground LBackground = new TextBackground(11,LImage.ImageHeight);
			TextGenerator LGenerator = new TextGenerator(LImage, LBackground);
			for (int i = 0; i < 300; i++)
			{
				Console.WriteLine(LGenerator.GetStereogram());
				Console.WriteLine();
				System.Threading.Thread.Sleep(new TimeSpan(1000000));
			}
			/*
			 * ANSI Escape Character tests
						for (int i = 0; i <= 32; i++)
						{
							Console.Write(" :");
							Console.Write(i);
							Console.Write(": ");
							Console.Write((char)i);
							Console.Write("[2J");

						}
			*/
			/*			0 non bold
						1 bold
						30-37;
						40-47;
							m
			*/
			//			Console.Write("\27[6;6H");
			//			Console.Write("\27[6;6H");
			Console.ReadLine();

		}
	}
}
