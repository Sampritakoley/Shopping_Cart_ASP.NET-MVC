namespace EcommerceMVC.Services
{
	public class Password
	{
		public static string EncryptPassword(string password)
		{
			var plaintext = System.Text.Encoding.UTF8.GetBytes(password);
			return Convert.ToBase64String(plaintext);
		}
	}
}
