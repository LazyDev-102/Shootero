using Gemmob.Common.Data;
using System;
using UnityEngine;


public partial class PrefSaver {
    public partial class Key {
        private const string ldp = "ldp";

        public static string LastDayPlayed => ldp;
    }

    public DateTime GetLastDayPlay() {
        return SecurePlayerPrefs.GetDateTime(Key.LastDayPlayed, DateTime.Now);
    }

    public void SetLastDayPlay(DateTime value) {
        SecurePlayerPrefs.SetDateTime(Key.LastDayPlayed, value);
    }

    public bool IsNewDay() {
        DateTime now = DateTime.Now;
        DateTime lastPlay = GetLastDayPlay();
        return now.CompareTo(lastPlay) > 0 && (now.Day != lastPlay.Day || now.Month != lastPlay.Month || now.Year != lastPlay.Year);
    }
}
