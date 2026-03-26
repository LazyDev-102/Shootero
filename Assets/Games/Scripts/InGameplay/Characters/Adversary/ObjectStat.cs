

using UnityEngine;

public abstract class ObjectStat : MonoBehaviour {
    private ObjectBase objectBase;
    public ObjectBase ObjectBase {
        get {
            if(objectBase == null) {
                objectBase = GetComponent<ObjectBase>();
            }
            return objectBase;
        }
    }

    [SerializeField] private IntStat atk;

    public IntStat Atk { get => atk; }


    public virtual void Initialize() {
    }

    public virtual void Destroy() {

    }

    public virtual void Updating() {

    }
}
