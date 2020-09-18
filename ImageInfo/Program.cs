using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ImageInfo
{
	public class Program
	{
		static void Main(string[] args)
		{
			string path = "";

			for (int i = 0; i < args.Count(); i++)
			{
				var arg = args[i];

				if (arg.StartsWith("-")) 
				{
					switch (arg.Substring(1, arg.Length - 1))
					{
						case "path":
							path = args[i + 1];
							i++;
							break;
					}
				}
			}
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentNullException();
			}

			var fileInfo = new FileInfo(path);

			if (!fileInfo.Exists)
			{
				throw new FileNotFoundException(path);
			}

			var image = Image.FromFile(fileInfo.FullName);

			Console.WriteLine($"Name: {fileInfo.Name}");
			Console.WriteLine($"Extension: {fileInfo.Extension}");
			Console.WriteLine($"Size: {(fileInfo.Length / (double)1024).ToString("N2")} KB");
			Console.WriteLine($"Creation date: {fileInfo.CreationTime}");
			Console.WriteLine($"Last write date: {fileInfo.LastWriteTime}");
			Console.WriteLine($"Resolution: {image.Width} x {image.Height}");
			Console.WriteLine($"Horizontal resolution: {image.HorizontalResolution}");
			Console.WriteLine($"Vertical resolution: {image.VerticalResolution}");
			Console.WriteLine($"Color depth: {GetColorDepthForPixelFormat(image.PixelFormat)}");
		}

		public static string GetColorDepthForPixelFormat(PixelFormat format)
		{
			switch (format)
			{
				case PixelFormat.Format8bppIndexed:
					return "8";
				case PixelFormat.Format16bppRgb555:
					return "15";
				case PixelFormat.Format16bppRgb565:
					return "16";
				case PixelFormat.Format24bppRgb:
					return "24";
				case PixelFormat.Format32bppArgb:
				case PixelFormat.Format32bppRgb:
					return "32";
				default:
					return format.ToString();
			}
		}
	}
}
