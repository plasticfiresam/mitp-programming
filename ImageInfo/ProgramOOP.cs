using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ImageInfoObjectOriented
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

			var imageInfo = new ImageInfo(path);

			imageInfo.PrintInformation();
		}
	}

	public class ImageInfo
	{
		private FileInfo FileInfo { get; }
		private Image Image { get; }

		public ImageInfo(string path)
		{
			FileInfo = new FileInfo(path);
			if (!FileInfo.Exists)
			{
				throw new FileNotFoundException(path);
			}
			Image = Image.FromFile(FileInfo.FullName);
		}

		public string GetPath()
		{
			return FileInfo.FullName;
		}

		public void PrintInformation()
		{
			Console.WriteLine($"Name: {FileInfo.Name}");
			Console.WriteLine($"Extension: {FileInfo.Extension}");
			Console.WriteLine($"Size: {(FileInfo.Length / (double)1024).ToString("N2")} KB");
			Console.WriteLine($"Creation date: {FileInfo.CreationTime}");
			Console.WriteLine($"Last write date: {FileInfo.LastWriteTime}");
			Console.WriteLine($"Resolution: {Image.Width} x {Image.Height}");
			Console.WriteLine($"Horizontal resolution: {Image.HorizontalResolution}");
			Console.WriteLine($"Vertical resolution: {Image.VerticalResolution}");
			Console.WriteLine($"Color depth: {GetColorDepthForPixelFormat(Image.PixelFormat)}");
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
