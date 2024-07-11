using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace EcommerceMVC.Helper
{
	public static class WorkingWithSession
	{
		public static void SetObjectAsJson(this ISession session,string key,Object value)
		{
			session.SetString(key, JsonConvert.SerializeObject(value));
		}
		public static T GetObjectFromJson<T>(this ISession session, string key)
		{
			var value=session.GetString(key);
			return value == null ? default : JsonConvert.DeserializeObject<T>(value);
		}
	}
}
