using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(B13Attack), typeof(B13Health), typeof(B13Move))]
[RequireComponent(typeof(B13Skill), typeof(B13Stat), typeof(B13HitBox))]
[RequireComponent(typeof(B13StateController), typeof(B13Effect))]
public class B13Base : BossBase {
    [SerializeField] protected ParticleSystem iceExplosion;
    [SerializeField] protected Transform fireBossTrans;
    [SerializeField] protected Transform iceBossTrans;
    [SerializeField] protected Transform fireDefaultTrans;
    [SerializeField] protected Transform iceDefaultTrans;


    #region References
    private B13Attack b13Attack;
    public B13Attack B13Attack {
        get {
            if (b13Attack == null) {
                b13Attack = BossAttack as B13Attack;
            }
            return b13Attack;
        }
    }

    private B13Move b13Move;
    public B13Move B13Move {
        get {
            if (b13Move == null) {
                b13Move = BossMove as B13Move;
            }
            return b13Move;
        }
    }

    private B13Health b13Health;
    public B13Health B13Health {
        get {
            if (b13Health == null) {
                b13Health = BossHealth as B13Health;
            }
            return b13Health;
        }
    }

    private B13Stat b13Stat;
    public B13Stat B13Stat {
        get {
            if (b13Stat == null) {
                b13Stat = BossStat as B13Stat;
            }
            return b13Stat;
        }
    }

    private B13HitBox b13Hitbox;
    public B13HitBox B13Hitbox {
        get {
            if (b13Hitbox == null) {
                b13Hitbox = BossHitbox as B13HitBox;
            }
            return b13Hitbox;
        }
    }

    private B13Skill b13Skill;
    public B13Skill B13Skill {
        get {
            if (b13Skill == null) {
                b13Skill = BossSkill as B13Skill;
            }
            return b13Skill;
        }
    }
    #endregion
    public override void Initialize() {
        base.Initialize();
        fireBossTrans.GetComponent<SpriteRenderer>().SetAlpha(1);
        iceBossTrans.GetComponent<SpriteRenderer>().SetAlpha(1);
        fireBossTrans.transform.position = fireDefaultTrans.position;
        iceBossTrans.transform.position = iceDefaultTrans.position;
    }
    public override void Spawn() {
        base.Spawn();
    }
    public override void Die() {
        BossMove.EndMove();
        BossAttack.StopAttack();
        BossEffect.StartPreDieBoss(() => {
            ObjectBase lastCauser = CharacterHitbox.LastCauser;
            if (lastCauser) {
                lastCauser.Killing(this);
            }
            foreach (var assister in CharacterHitbox.AssisCausers) {
                assister.Assising(this);
            }
            if (explosion) {
                GameManager.Instance.GameLoader.SpawnEffectExplosions(explosion, fireBossTrans.position, numberExplosion, radiusExplosion, deltaExplosion);
            }
            if (iceExplosion) {
                GameManager.Instance.GameLoader.SpawnEffectExplosions(explosion, iceBossTrans.position, numberExplosion, radiusExplosion, deltaExplosion);
            }
            CameraShakeManager.Instance.ShakeCamera(shakeType);

            if (enableDropChip && !GameManager.Instance.isTest) {
                GameResources.Instance.Drop.Droping(transform.position, this);
                if (canDropChip) {
                    GameResources.Instance.Drop.DropingChip(transform.position, this);
                    canDropChip = false;
                }
            }
            if (!GameManager.Instance.isTest) {
                SoundManager.Instance.PlayBossDestroy();
            }
            onDie?.Invoke();
            RemoveAllOnDie();
            RemoveMe();
        });
        DispatchOnDie();
    }
}
