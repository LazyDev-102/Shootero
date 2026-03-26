using Helper;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterHitbox : ObjectHitbox, IHitbox {
    private CharacterBase characterBase;
    public CharacterBase CharacterBase {
        get {
            if (characterBase == null) {
                characterBase = ObjectBase as CharacterBase;
            }
            return characterBase;
        }
    }

    protected Action<int> onTakeHit;
    protected Action<bool> onInvulnerable;
    protected Action<float> onInvulnerableEffect;
    protected Action stopInvulnerableEffect;
    protected bool isInvulnerable;
    protected bool isLockTurnOffShield;
    protected float invulnerableCountdown;
    protected ObjectBase lastCauser;
    protected List<ObjectBase> assisCausers = new List<ObjectBase>();

    public ObjectBase LastCauser {
        get {
            return lastCauser;
        }
    }

    public bool IsInvulnerable {
        get {
            return isInvulnerable;
        }
    }
    public List<ObjectBase> AssisCausers { get => assisCausers; }


    #region Listener Event
    public void AddOnTakeHit(Action<int> onTakeHit) {
        this.onTakeHit += onTakeHit;
    }

    public void RemoveOnTakeHit(Action<int> onTakeHit) {
        this.onTakeHit -= onTakeHit;
    }

    public void AddOnInvulnerable(Action<bool> onInvulnerable) {
        this.onInvulnerable += onInvulnerable;
    }

    public void RemoveOnInvulnerable(Action<bool> onInvulnerable) {
        this.onInvulnerable -= onInvulnerable;
    }
    public void AddOnInvulnerableEffect(Action<float> onInvulnerableEffect) {
        this.onInvulnerableEffect += onInvulnerableEffect;
    }

    public void RemoveOnInvulnerableEffect(Action<float> onInvulnerableEffect) {
        this.onInvulnerableEffect -= onInvulnerableEffect;
    }

    public void StopInvulnerableEffect(Action stopInvulnerableEffect) {
        this.stopInvulnerableEffect = stopInvulnerableEffect;
    }
    #endregion

    public override void Initialize() {
        base.Initialize();
        assisCausers.Clear();
        lastCauser = null;
        isInvulnerable = false;
        isLockTurnOffShield = false;
    }

    public override void Destroy() {
        base.Destroy();
        onTakeHit = null;
        onInvulnerable = null;
    }

    public override void Updating() {
        base.Updating();
        if (invulnerableCountdown > 0 && isInvulnerable) {
            invulnerableCountdown -= Time.deltaTime;
            if (invulnerableCountdown < 0) {
                isInvulnerable = false;
                if (!isLockTurnOffShield)
                    onInvulnerable?.Invoke(false);
            }
        }
    }

    public void TurnOnInvulnerable(float durantion = 0, bool effect = true) {
        isInvulnerable = true;
        invulnerableCountdown = durantion;
        onInvulnerable?.Invoke(true);
        if (effect)
            onInvulnerableEffect?.Invoke(durantion);
    }

    public void TurnOffInvulnerable() {
        if (!isLockTurnOffShield) {
            isInvulnerable = false;
            onInvulnerable?.Invoke(false);
        }
    }
    public void SetLockTurnOffStatus(bool status) {
        isLockTurnOffShield = status;
    }
    public virtual void TakeHitDamage(HitInfor hit, Vector2 positionCollider, HitType type = HitType.Normal) {
        if (IsBlockTakeHit()) {
            return;
        }
        else if (hit == null) {
            return;
        }
        else if (RandomHelper.RandomWithProbability(hit.CritChance)) {
            TakeHitDamage(Mathf.CeilToInt(hit.Damage.Value * hit.CritDamage), transform.position, hit.Causer, HitType.Crit);
            //Debug.Log($"Crit {(int)(hit.Damage.Value * hit.CritDamage)}");
        }
        else {
            TakeHitDamage(hit.Damage.Value, positionCollider, hit.Causer, type);
        }
        if (hit.Effects != null) {
            foreach (var effect in hit.Effects) {
                effect.EffectTo(CharacterBase, hit.Causer, hit.Damage, positionCollider);
            }
        }
    }

    protected override bool IsBlockTakeHit() {
        return base.IsBlockTakeHit() || isInvulnerable;
    }

    protected virtual void TakeHitDamage(int damage, Vector2 positionCollider, ObjectBase causer, HitType type = HitType.Normal) {
        AddAssisCauser(lastCauser);
        RemoveAssisCauser(causer);
        lastCauser = causer;
        CharacterBase.CharacterHealth.HPReduce(damage);
        onTakeHit?.Invoke(damage);
        TextShowupManager.Instance.ShowHitText(type, damage.ToString(), transform.position);
        //TextHitManager.Instance.ShowTextHit(type, damage, position);
    }

    protected virtual void Evasion(ObjectBase causer) {
        AddAssisCauser(causer);
        TextShowupManager.Instance.ShowEvasionText(CharacterBase.CharacterMove.MyRigi.position);
        //TextHitManager.Instance.ShowTextHit(type, damage, position);
    }

    protected virtual void AddAssisCauser(ObjectBase assiser) {
        if (assiser == null) {
            return;
        }
        if (!assisCausers.Contains(assiser)) {
            assisCausers.Add(assiser);
        }
    }

    protected virtual void RemoveAssisCauser(ObjectBase laster) {
        if (assisCausers.Contains(laster)) {
            assisCausers.Remove(laster);
        }
    }

    public void TakeHit(HitInfor hit, Vector2 positionCollider, HitType type = HitType.Normal) {
        TakeHitDamage(hit, positionCollider, type);
    }

    public Transform Transform() {
        return transform;
    }
}


public enum HitType {
    Normal, Crit, Burn, OneShot,
}