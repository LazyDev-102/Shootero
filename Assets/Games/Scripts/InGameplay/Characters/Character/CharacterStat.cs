using UnityEngine;

public abstract class CharacterStat : ObjectStat {
    [SerializeField] private IntStat maxHP;
    [SerializeField] private FloatStat colliderDamage;



    private CharacterBase characterBase;
    public CharacterBase CharacterBase {
        get {
            if (characterBase == null) {
                characterBase = ObjectBase as CharacterBase;
            }
            return characterBase;
        }
    }

    public IntStat MaxHP { get => maxHP; }
    public FloatStat ColliderDamage { get => colliderDamage; }


    public override void Initialize() {

    }

    public override void Destroy() {

    }

    public override void Updating() {

    }
}
