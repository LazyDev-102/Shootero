

public class T01PartStat : ObjectStat {
    private T01PartBase t01PartBase;
    public T01PartBase T01PartBase {
        get {
            if(t01PartBase == null) {
                t01PartBase = ObjectBase as T01PartBase;
            }
            return t01PartBase;
        }
    }
}
