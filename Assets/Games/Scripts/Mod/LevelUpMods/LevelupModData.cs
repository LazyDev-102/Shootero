

public abstract class LevelupModData : ModData {

}


public abstract class LevelupModInfo<T> : ModInfor<T>, ILevelupMod where T : LevelupModData {
    public LevelupModInfo(T mod) : base(mod) {

    }

    public LevelupModInfo(LevelupModInfo<T> mod) : base(mod) {

    }

    public abstract void ActionLevelup(ShipBase ship);

    public abstract object Clone();

    public ModInfor GetModInfor() {
        return this;
    }
}

public interface ILevelupMod : IModable {
    void ActionLevelup(ShipBase ship);
}