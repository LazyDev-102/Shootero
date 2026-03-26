
public abstract class ChangeBulletModData : ModData {
}

public abstract class ChangeBulletModInfor<T> : ModInfor<T>, IChangeBulletModable where T : ChangeBulletModData {
    public ChangeBulletModInfor(T mod) : base(mod) {

    }

    public ChangeBulletModInfor(ChangeBulletModInfor<T> mod) : base(mod) {

    }

    public abstract void ChangeBullet(BulletBase bullet);

    public abstract object Clone();

    public ModInfor GetModInfor() {
        return this;
    }
}

public interface IChangeBulletModable : IModable {
    void ChangeBullet(BulletBase bullet);
}
