

using UnityEngine;


public abstract class ObjectMove : MonoBehaviour {
    private ObjectBase objectBase;
    public ObjectBase ObjectBase {
        get {
            if (objectBase == null) {
                objectBase = GetComponent<ObjectBase>();
            }
            return objectBase;
        }
    }

    [SerializeField] protected Rigidbody2D myRigi;

    public Rigidbody2D MyRigi {
        get {
            return myRigi;
        }
    }

    public virtual void Initialize() {

    }

    public virtual void Destroy() {

    }

    public virtual void Updating() {

    }

    public virtual bool HasOutBorder() {
        return false;
    }
}
