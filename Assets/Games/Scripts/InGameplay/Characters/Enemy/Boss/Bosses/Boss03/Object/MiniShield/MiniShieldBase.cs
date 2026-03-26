using UnityEngine;

public class MiniShieldBase : CharacterBase {
    #region References
    private MiniShieldAttack miniShieldAttack;
    public MiniShieldAttack MiniShieldAttack {
        get {
            if (miniShieldAttack == null) {
                miniShieldAttack = CharacterAttack as MiniShieldAttack;
            }
            return miniShieldAttack;
        }
    }

    private MiniShieldMove miniShieldMove;
    public MiniShieldMove MiniShieldMove {
        get {
            if (miniShieldMove == null) {
                miniShieldMove = CharacterMove as MiniShieldMove;
            }
            return miniShieldMove;
        }
    }

    private MiniShieldHealth miniShieldHealth;
    public MiniShieldHealth MiniShieldHealth {
        get {
            if (miniShieldHealth == null) {
                miniShieldHealth = CharacterHealth as MiniShieldHealth;
            }
            return miniShieldHealth;
        }
    }

    private MiniShieldStat miniShieldStat;
    public MiniShieldStat MiniShieldStat {
        get {
            if (miniShieldStat == null) {
                miniShieldStat = CharacterStat as MiniShieldStat;
            }
            return miniShieldStat;
        }
    }

    private MiniShieldHitbox miniShieldHitbox;
    public MiniShieldHitbox MiniShieldHitbox {
        get {
            if (miniShieldHitbox == null) {
                miniShieldHitbox = CharacterHitbox as MiniShieldHitbox;
            }
            return miniShieldHitbox;
        }
    }

    private MiniShieldSkill miniShieldSkill;
    public MiniShieldSkill MiniShieldSkill {
        get {
            if (miniShieldSkill == null) {
                miniShieldSkill = CharacterSkill as MiniShieldSkill;
            }
            return miniShieldSkill;
        }
    }
    private MiniShieldEffect miniShieldEffect;
    public MiniShieldEffect MiniShieldEffect {
        get {
            if (miniShieldEffect == null) {
                miniShieldEffect = CharacterEffect as MiniShieldEffect;
            }
            return miniShieldEffect;
        }
    }


    #endregion

    [SerializeField] private LightningLine lightningLine;
    [SerializeField] private ParticleSystem appearEffect;
    public LightningLine LightningLine { get => lightningLine; }



    public override void Die() {
        base.Die();
        if (explosion) {
            GameManager.Instance.GameLoader.SpawnEffectExplosions(explosion, transform.position, numberExplosion, radiusExplosion, deltaExplosion);
        }
        lightningLine.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Restore() {
        lightningLine.SetActive(false);
        bool mustPlayEffect = !gameObject.activeInHierarchy;
        gameObject.SetActive(true);
        if (mustPlayEffect) {
            appearEffect.Play();
        }
        MiniShieldMove.SpawnInPosition();
        MiniShieldHealth.ForceChangeCurrentHp(MiniShieldStat.MaxHP.Value);
    }

    protected override void RemoveMe() {
    }
}
