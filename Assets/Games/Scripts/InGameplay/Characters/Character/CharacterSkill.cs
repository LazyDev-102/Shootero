

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CharacterSkill : MonoBehaviour {
    private CharacterBase characterBase;
    public CharacterBase CharacterBase {
        get {
            if (characterBase == null) {
                characterBase = GetComponent<CharacterBase>();
            }
            return characterBase;
        }
    }

    private List<CountdownEffect> countdownEffects = new List<CountdownEffect>();
    public List<CountdownEffect> CountdownEffects { get => countdownEffects; }

    public virtual void Initalize() {
        countdownEffects.Clear();
    }

    public virtual void Destroy() {
        foreach (var effect in countdownEffects) {
            effect.Destroy();
        }
        countdownEffects.Clear();
    }

    public virtual void Updating() {
        OnUpdateCountdownEffects();
    }

    public void OnUpdateCountdownEffects() {
        for (int i = 0; i < countdownEffects.Count; ++i) {
            countdownEffects[i].Updating(Time.deltaTime);
        }
    }

    public void AddCountdownEffect(CountdownEffect effect) {
        if (effect != null) {
            if (countdownEffects.Contains(effect)) {
                effect.AddDupllicate(countdownEffects);
            }
            else {
                countdownEffects.Add(effect);
                effect.EffectTo();
            }
        }
    }

    public void RemoveCountdownEffect(CountdownEffect effect, bool removeAll = false) {
        if (effect != null) {
            if (removeAll) {
                int numberSkillInthis = countdownEffects.Count(s => s.Equals(effect));
                for (int i = 0; i < numberSkillInthis; ++i) {
                    countdownEffects.Remove(effect);
                }
            }
            else {
                countdownEffects.Remove(effect);
            }
        }
    }
}
