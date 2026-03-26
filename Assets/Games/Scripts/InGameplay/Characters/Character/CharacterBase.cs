using Gemmob;
using UnityEngine;


public abstract class CharacterBase : ObjectBase {
    #region
    private CharacterAttack characterAttack;
    public CharacterAttack CharacterAttack {
        get {
            if (characterAttack == null) {
                characterAttack = ObjectAttack as CharacterAttack;
            }
            return characterAttack;
        }
    }

    private CharacterMove characterMove;
    public CharacterMove CharacterMove {
        get {
            if (characterMove == null) {
                characterMove = ObjectMove as CharacterMove;
            }
            return characterMove;
        }
    }

    private CharacterHealth characterHealth;
    public CharacterHealth CharacterHealth {
        get {
            if (characterHealth == null) {
                characterHealth = GetComponent<CharacterHealth>();
            }
            return characterHealth;
        }
    }

    private CharacterStat characterStat;
    public CharacterStat CharacterStat {
        get {
            if (characterStat == null) {
                characterStat = ObjectStat as CharacterStat;
            }
            return characterStat;
        }
    }

    private CharacterHitbox characterHitbox;
    public CharacterHitbox CharacterHitbox {
        get {
            if (characterHitbox == null) {
                characterHitbox = ObjectHitbox as CharacterHitbox;
            }
            return characterHitbox;
        }
    }

    private CharacterSkill characterSkill;
    public CharacterSkill CharacterSkill {
        get {
            if (characterSkill == null) {
                characterSkill = GetComponent<CharacterSkill>();
            }
            return characterSkill;
        }
    }

    private CharacterEffect characterEffect;
    public CharacterEffect CharacterEffect {
        get {
            if (characterEffect == null) {
                characterEffect = GetComponent<CharacterEffect>();
            }
            return characterEffect;
        }
    }

    #endregion
    [SerializeField] protected ParticleSystem explosion;
    [SerializeField] protected int numberExplosion;
    [SerializeField] protected float deltaExplosion;
    [SerializeField] protected float radiusExplosion;
    [SerializeField] protected CameraShakeType shakeType;
    protected System.Action onDie;



    #region Listener Events
    public void AddOnDie(System.Action onDie) {
        this.onDie += onDie;
    }

    public void RemoveOnDie(System.Action onDie) {
        this.onDie -= onDie;
    }

    public void RemoveAllOnDie() {
        onDie = null;
    }

    #endregion

    public override void PreloadIngame() {
        base.PreloadIngame();
        if (CharacterEffect == null) {
            Logs.Log(this.gameObject.name);
        }
        CharacterEffect?.PreloadIngame();
        if (explosion) {
            explosion.RegisterPool(numberExplosion);
        }
    }

    public override void Initialize() {
        base.Initialize();
        CharacterHealth.Initalize();
        CharacterHitbox.Initialize();
        CharacterSkill.Initalize();
        CharacterEffect?.Initialize();
    }

    public override void Destroy() {
        base.Destroy();
        CharacterHealth.Destroy();
        CharacterHitbox.Destroy();
        CharacterSkill.Destroy();
        CharacterEffect?.Destroy();
    }

    public override void Updating() {
        base.Updating();
        CharacterHealth.Updating();
        CharacterHitbox.Updating();
        CharacterSkill.Updating();
        CharacterEffect?.Updating();
    }

    public virtual void Die() {
        CameraShakeManager.Instance.ShakeCamera(shakeType);
        ObjectBase lastCauser = CharacterHitbox.LastCauser;
        if (lastCauser) {
            lastCauser.Killing(this);
        }
        foreach (var assister in CharacterHitbox.AssisCausers) {
            assister.Assising(this);
        }
        onDie?.Invoke();
        RemoveAllOnDie();
        RemoveMe();
    }
    public virtual void SelfDestruction() {
        RemoveMe();
    }

    protected abstract void RemoveMe();

    public virtual bool IsDie() {
        return CharacterHealth.CurrentHp <= 0;
    }
}
