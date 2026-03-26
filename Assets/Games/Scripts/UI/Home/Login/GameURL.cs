public static class GameURL {
#if CHEAT
    public const string ServiceUrl = "https://s1.h2c.us/api/v1/";
#else
    public const string ServiceUrl = "https://s1.h2c.us/api/v1/";
#endif
    public static string DataPath = UnityEngine.Application.persistentDataPath + @"/PlayerData.txt";
    public static string UserIdPath = UnityEngine.Application.persistentDataPath + @"/UserId.txt";

    public static class UserData {
        public const string UploadData = ServiceUrl + "game/save-data";
        public const string DownloadData = ServiceUrl + "game/request-data";
        public const string UploadUserProfile = ServiceUrl + "users/submit";
        public const string DownloadUserProfile = ServiceUrl + "users/get-info";
    }

    public static class Leaderboard {
        public const string UploadData = ServiceUrl + "leaderboard/submit";
        public const string DownloadData = ServiceUrl + "leaderboard/get-rank";
    }
}

public class APIConfig {
    public const string uid = "uid";
    public const string node = "node";
    public const string data = "data";
    public const string Version = "version";
    public const string UserName = "username";// name from GPS
    public const string Email = "email";// email from GPS
    public const string NameIngame = "name";// playername ingame
    public const string Score = "point";
    public const string Level = "level";
    public const string Info = "info";
    public const string Token = "token";

    public const string UserProfile = "UserProfile";
    public const string UserData = "UserData";
    public const string Leaderboard = "Leaderboard";
}