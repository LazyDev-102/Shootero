

public abstract class ChestMove : ObjectMove {
    private ChestBase chestBase;
    public ChestBase ChestBase {
        get {
            if (chestBase == null) {
                chestBase = ObjectBase as ChestBase;
            }
            return chestBase;
        }
    }
}
