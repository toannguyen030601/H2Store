using Newtonsoft.Json;

namespace asm1.Models
{
    public static class SessionHelper
    {
        public static void SetOjectAsJson(this ISession session, string key, Object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetOjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
