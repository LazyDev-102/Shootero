

using Gemmob.Common.Data;
using System;

public partial class PrefSaver {
    public partial class Key {
        private const string etk = "etk";
        private const string ier = "ier";
        public static string EnergyTimeKey => etk;
        public static string IsEnergyRegen => ier;
    }

    public DateTime EnergyTimeReady {
        get {
            return PersitenData.GetDateTime(Key.EnergyTimeKey, DateTime.Now);
        }
        set => PersitenData.SetDateTime(Key.EnergyTimeKey, value);
    }

    public bool IsEnergyRegen {
        get => PersitenData.GetBool(Key.IsEnergyRegen, false);
        set => PersitenData.SetBool(Key.IsEnergyRegen, value);
    }
}
