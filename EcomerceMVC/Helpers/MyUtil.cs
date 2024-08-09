using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace EcomerceMVC.Helpers
{
	public class MyUtil
	{
		public static string GenerateRamdomKey(int length = 5)
		{
			var pattern = @"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM!@#$%~<>?";
			var sb = new StringBuilder();
			var rd = new Random();
			for (int i = 0; i < length; i++)
			{
				sb.Append(pattern[rd.Next(0, pattern.Length)]);
			}
			return sb.ToString();
		}
		public static string ToMd5Hash(string password, string? saltKey)
		{
			using (var md5 = MD5.Create())
			{
				byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(password, saltKey)));
				StringBuilder sBuilder = new StringBuilder();
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}
				return sBuilder.ToString();
			}
		}
		public static string UpLoadHinh(IFormFile hinh, string folder)
		{
			try
			{
				// lấy ra đường dẫn tới folder 
				var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folder, hinh.FileName);
				if (File.Exists(fullPath))
				{
					using (var myfile = new FileStream(fullPath, FileMode.Open))
					{
						hinh.CopyTo(myfile);
					}
				}
				else
				{
					using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
					{
						hinh.CopyTo(myfile);
					}
				}		
				return hinh.FileName;
			}
			catch (Exception ex)
			{
				return string.Empty;
			}
		}

		public static void DeleteHinh(string hinh, string folder)
		{
			try
			{
				var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folder, hinh);
				File.Delete(fullPath);
			}
			catch(Exception ex)
			{

			}
		}

	}
}
