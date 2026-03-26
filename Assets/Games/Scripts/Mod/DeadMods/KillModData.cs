

public abstract class KillModData : ModData {

}


public abstract class KillModInfor<T> : ModInfor<T>, IKillMod where T : KillModData {
    public KillModInfor(T mod) : base(mod) {

    }

    public KillModInfor(KillModInfor<T> mod) : base(mod) {

    }

    public abstract void ActionKill(ShipBase killer, CharacterBase victim);

    public abstract object Clone();

    public ModInfor GetModInfor() {
        return this;
    }
}

public interface IKillMod : IModable {
    void ActionKill(ShipBase killer, CharacterBase victim);
}
