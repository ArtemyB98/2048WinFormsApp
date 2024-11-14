using Newtonsoft.Json;

namespace _2048.Common
{
    public class UserResults()
    {
        public static string Path = "Results.json";
        public static List<User> GetAll()
        {
            if (FileProvider.Exists(Path))
            {
                var jsonResuts = FileProvider.GetValue(Path);
                return JsonConvert.DeserializeObject<List<User>>(jsonResuts);
            }
             return new List<User>();
        }
        public static void Add(User newUser)
        {
            var users = GetAll();
            users.Add(newUser);

            var jsonResults = JsonConvert.SerializeObject(users);
            FileProvider.Replace(Path, jsonResults);
        }
    }
}