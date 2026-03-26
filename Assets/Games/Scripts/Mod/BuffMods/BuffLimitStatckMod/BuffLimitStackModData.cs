

public abstract class BuffLimitStackModData : ModData {

}


public class BuffLimitStackModInfo : ModInfor<BuffLimitStackModData>, IModable {
    public BuffLimitStackModInfo(BuffLimitStackModData mod) : base(mod) {

    }

    public BuffLimitStackModInfo(BuffLimitStackModInfo mod) : base(mod) {

    }

    public ModInfor GetModInfor() {
        return this;
    }

    public object Clone() {
        return new BuffLimitStackModInfo(this);
    }
}
