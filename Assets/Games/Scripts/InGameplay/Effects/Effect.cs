
using System;
using System.Collections.Generic;

public abstract class Effect {
    protected string id;
    protected CharacterBase victim;
    protected ObjectBase causer;


    protected Effect(CharacterBase victim, ObjectBase causer) {
        this.victim = victim;
        this.causer = causer;
    }

    public abstract void EffectTo();
    protected abstract void RemoveFrom();

    public abstract void Destroy();

    public override bool Equals(object other) {
        if (other == null)
            return false;
        Effect effectOther = other as Effect;
        return this.id.Equals(effectOther.id);//&& victim == effectOther.victim && causer == effectOther.causer;
    }

    public abstract void Updating(float deltaTime);

    public override int GetHashCode() {
        return this.id.GetHashCode();
    }
}

public abstract class CountdownEffect : Effect {
    protected float effectDuration;
    protected Action onTimeOut;

    protected Countdowner effectCountdowner = new Countdowner();
    protected CountdownEffect(CharacterBase victim, ObjectBase causer, float duration) : base(victim, causer) {
        effectDuration = duration;
        if (causer is ShipBase ship) {
            effectDuration *= (1 + ship.ShipStat.BurnDurationPercent.Value);
        }
        effectCountdowner.StartCountdown(duration);
        onTimeOut += RemoveFrom;
    }

    public virtual void AddDupllicate(List<CountdownEffect> effects) {
        CountdownEffect effect = effects.Find(i => i.Equals(this));
        effect.Reset();
    }

    public virtual void AddTime(float time) {
        effectCountdowner.Addtime(time);
    }

    public virtual void Reset() {
        effectCountdowner.StartCountdown(effectDuration);
        //UnityEngine.Debug.Log("Effect countdown reset" + id);
    }

    public void ListenOnComplete(Action onComplete) {
        onTimeOut += onComplete;
    }

    public override void Updating(float deltaTime) {
        effectCountdowner.Countdowning(deltaTime);
        if (effectCountdowner.IsTimeOut()) {
            if (onTimeOut != null) {
                onTimeOut.Invoke();
            }
        }
    }

    public override void Destroy() {
        onTimeOut = null;
    }
}
