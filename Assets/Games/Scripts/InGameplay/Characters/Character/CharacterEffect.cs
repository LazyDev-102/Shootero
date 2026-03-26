using Gemmob;
using UnityEngine;

public class CharacterEffect : MonoBehaviour {
    private CharacterBase characterBase;
    public CharacterBase CharacterBase {
        get {
            if (characterBase == null) {
                characterBase = GetComponent<CharacterBase>();
            }
            return characterBase;
        }
    }

    [SerializeField] protected BurningEffect burningEffect;
    [SerializeField] protected BurningStackEffect burningStackPrefab;
    [SerializeField] protected Vector2 burnStackOffset;


    protected BurningStackEffect burningStackEffect;

    public virtual void PreloadIngame() {

    }

    public virtual void Initialize() {

    }

    public virtual void Destroy() {
        EndBurningEffect();
    }

    public virtual void Updating() {
    }

    public void StartBurningEffect(int numberStack, bool status = false) {
        if (!status)
            return;
        if (burningEffect) {
            burningEffect.StartEffect(status);
        }
        if (burningStackEffect == null) {
            if (burningStackPrefab == null) {
                return;
            }
            burningStackEffect = burningStackPrefab.Spawn(transform.position);
            burningStackEffect.SetTarget(transform, burnStackOffset);
        }
        burningStackEffect.ShowStack(numberStack);

    }

    public void EndBurningEffect(bool status = true) {
        if (!status)
            return;
        if (burningEffect) {
            burningEffect.StopEffect(status);
        }
        if (burningStackEffect) {
            burningStackEffect.Recycle();
            burningStackEffect = null;
        }
    }
}
