using System.Collections.Generic;
using UnityEngine;

public class BurnEffect : CountdownEffect {
    public static string burnId = "burn";
    protected float deltaBurn;
    protected HitInfor hit;

    private float deltaBurnTimer;
    private bool startedBurn;
    private int maxStack;

    private int currentStack;
    private int damage;

    HitInfor Hit {
        get {
            hit.SetInfor(damage * currentStack, null, causer);
            return hit;
        }
    }

    public BurnEffect(CharacterBase victim, ObjectBase causer, float duration, float deltaAttack, int damage, int maxStack) : base(victim, causer, duration) {
        id = burnId;
        this.deltaBurn = deltaAttack;
        this.damage = damage;
        hit = new HitInfor();
        deltaBurnTimer = 0;
        this.maxStack = maxStack;
        currentStack = 1;
        if (causer is ShipBase ship) {
            damage = Mathf.CeilToInt((damage * (1 + ship.ShipStat.BurnDamagePercent.Value)));
            maxStack += ship.ShipStat.BurnStack.Value;
        }
    }

    private void StartBurn() {
        startedBurn = true;
    }

    public override void EffectTo() {
        StartBurn();
        if (victim.CharacterEffect) {
            victim.CharacterEffect.StartBurningEffect(currentStack);
        }
    }

    public override void AddDupllicate(List<CountdownEffect> effects) {
        CountdownEffect effect = effects.Find(i => i.Equals(this));
        if (effect is BurnEffect burnEffect) {
            burnEffect.maxStack = this.maxStack;
            if (burnEffect.currentStack < burnEffect.maxStack) {
                burnEffect.currentStack++;
                if (victim.CharacterEffect) {
                    victim.CharacterEffect.StartBurningEffect(burnEffect.currentStack);
                }
            }
        }
        effect.Reset();
    }

    protected override void RemoveFrom() {
        victim.CharacterSkill.RemoveCountdownEffect(this);
        if (victim.CharacterEffect) {
            victim.CharacterEffect.EndBurningEffect();
        }
    }

    private void Burn(HitInfor hit) {
        if (!victim.CharacterHitbox.IsInvulnerable)
            victim.CharacterHitbox.TakeHitDamage(hit, victim.transform.position, HitType.Burn);
    }

    public override void Updating(float deltaTime) {
        base.Updating(deltaTime);
        if (startedBurn) {
            deltaBurnTimer -= deltaTime;
            if (deltaBurnTimer <= 0) {
                Burn(Hit);
                deltaBurnTimer = deltaBurn;
            }
        }
    }
}
