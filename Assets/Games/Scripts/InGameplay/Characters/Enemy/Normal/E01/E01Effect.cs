

public class E01Effect : EnemyEffect {
    private E01Base e01Base;
    public E01Base E01Base {
        get {
            if (e01Base == null) {
                e01Base = CharacterBase as E01Base;
            }
            return e01Base;
        }
    }


    public override void StartEnemyHitEffect() {
        if (enemyHitEffect) {
            enemyHitEffect.StartEffect(E01Base.E01Skin.GetSkin());
        }
    }
}
