using Class_FSM;
using Gemmob;
using UnityEngine;

public abstract class ObjectBase : MonoBehaviour {
    #region References
    private ObjectAttack objectAttack;
    public ObjectAttack ObjectAttack {
        get {
            if (objectAttack == null) {
                objectAttack = GetComponent<ObjectAttack>();
            }
            return objectAttack;
        }
    }

    private ObjectMove objectMove;
    public ObjectMove ObjectMove {
        get {
            if (objectMove == null) {
                objectMove = GetComponent<ObjectMove>();
            }
            return objectMove;
        }
    }

    private ObjectStat objectStat;
    public ObjectStat ObjectStat {
        get {
            if (objectStat == null) {
                objectStat = GetComponent<ObjectStat>();
            }
            return objectStat;
        }
    }

    private ObjectHitbox objectHitbox;
    public ObjectHitbox ObjectHitbox {
        get {
            if (objectHitbox == null) {
                objectHitbox = GetComponent<ObjectHitbox>();
            }
            return objectHitbox;
        }
    }

    private StateController stateController;
    public StateController StateController {
        get {
            if (stateController == null) {
                stateController = GetComponent<StateController>();
            }
            return stateController;
        }
    }

    #endregion
    protected bool isInitialized;
    public bool IsInitialized { get => isInitialized; }

    public virtual void PreloadIngame() {
        ObjectAttack.PreloadIngame();
    }

    public virtual void Initialize() {
        ObjectStat.Initialize();
        ObjectAttack.Initialize();
        ObjectMove.Initialize();
        ObjectHitbox.Initialize();
        StateController.Initialize();
        isInitialized = true;
    }

    public virtual void Destroy() {
        isInitialized = false;
        ObjectAttack.Destroy();
        ObjectMove.Destroy();
        ObjectStat.Destroy();
        ObjectHitbox.Destroy();
        StateController.Destroy();
    }

    public virtual void Updating() {
        ObjectMove.Updating();
        ObjectAttack.Updating();
        ObjectStat.Updating();
        ObjectHitbox.Updating();
        StateController.Updating();
    }

    protected virtual void Update() {
        if (!isInitialized) {
            return;
        }
        Updating();

    }

    public virtual void Killing(CharacterBase victim) {
        //Logs.Log($"{this.name} killing {victim.name}");
    }

    public virtual void Assising(CharacterBase victim) {
        //Logs.Log($"{this.name} assising {victim.name}");
    }
}
