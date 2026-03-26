

public abstract class TrapMove : ObjectMove {
    private TrapBase trapBase;
    public TrapBase TrapBase {
        get {
            if(trapBase == null) {
                trapBase = ObjectBase as TrapBase;
            }
            return trapBase;
        }
    }
}
