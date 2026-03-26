

using UnityEngine;

public abstract class CharacterAttack : ObjectAttack {
    private CharacterBase characterBase;
    public CharacterBase CharacterBase {
        get {
            if(characterBase == null) {
                characterBase = ObjectBase as CharacterBase;
            }
            return characterBase;
        }
    }

    public override void Initialize() {

    }

    public override void Destroy() {

    }

    public override void Updating() {

    }
}
