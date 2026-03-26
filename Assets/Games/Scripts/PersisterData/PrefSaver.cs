

using Gemmob.Common.Data;

public partial class PrefSaver {
    public partial class Key {
        private const string moveType = "movetype";
        private const string firstOpenGame = "firstopengame";
        private const string firstOpenGameAfterConvert = "fogac";
        private const string registerAccount = "registeraccount";
        private const string playAsAccount = "playAsAccount";
        private const string convertedData = "converteddata";

        public static string MoveType => moveType;
        public static string FirstOpenGame => firstOpenGame;
        public static string FirstOpenGameAfterConvert => firstOpenGameAfterConvert;
        public static string RegisterAccount => registerAccount;
        public static string PlayAsAccount => playAsAccount;
        public static string ConvertedData => convertedData;
    }

    public static readonly PrefSaver Instance = new PrefSaver();

    public static bool MoveFocus {
        get => PersitenData.GetBool(Key.MoveType, true);
        set => PersitenData.SetBool(Key.MoveType, value);
    }
    public static bool FirstOpenGame {
        get => PersitenData.GetBool(Key.FirstOpenGame, true);
        set => PersitenData.SetBool(Key.FirstOpenGame, value);
    }
    public static bool FirstOpenGameAfterConvert {
        get => PersitenData.GetBool(Key.FirstOpenGameAfterConvert, true);
        set => PersitenData.SetBool(Key.FirstOpenGameAfterConvert, value);
    }
    public static bool RegisterAccount {
        get => PersitenData.GetBool(Key.RegisterAccount, false);
        set => PersitenData.SetBool(Key.RegisterAccount, value);
    }
    public static bool PlayAsAccount {
        get => PersitenData.GetBool(Key.PlayAsAccount, false);
        set => PersitenData.SetBool(Key.PlayAsAccount, value);
    }
    public static bool ConvertedData {
        get => PersitenData.GetBool(Key.ConvertedData, false);
        set => PersitenData.SetBool(Key.ConvertedData, value);
    }

}
