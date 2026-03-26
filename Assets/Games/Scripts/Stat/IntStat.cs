
using System;
using UnityEngine;

[Serializable]
public class IntStat : Stat<int> {
    public IntStat() : base() {

    }

    public IntStat(int value) : base(value) {

    }

    public IntStat(Stat<int> stat) : base(stat) {

    }
    protected override int CalculateFinalValue() {
        float finalValue = baseValue;
        float sumPercentAdd = 0;
        for (int i = 0; i < statModifiers.Count; i++) {
            StatModifier mod = statModifiers[i];
            if (mod.Type == StatModType.Flat) {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd) {
                sumPercentAdd += mod.Value;
                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd) {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if (mod.Type == StatModType.PercentMult) {
                finalValue *= 1 + mod.Value;
            }
        }

        return Mathf.CeilToInt(finalValue);
    }

    protected override int CalculateFinalValue1() {
        float baseValue = base.baseValue;
        float finalValue = base.baseValue;

        for (int i = 0; i < statModifiers.Count; i++) {
            StatModifier mod = statModifiers[i];

            if (mod.Type == StatModType.Flat) {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd) {
                finalValue += mod.Value * baseValue;
            }
            else if (mod.Type == StatModType.PercentMult) {
                finalValue *= 1 + mod.Value;
            }
        }
        return Mathf.CeilToInt(finalValue);
    }

    protected override int CalculateFinalValue2() {
        float finalValue = baseValue;

        for (int i = 0; i < statModifiers.Count; i++) {
            StatModifier mod = statModifiers[i];
            if (mod.Type == StatModType.Flat) {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd) {
                finalValue *= 1 + mod.Value;
            }
            else if (mod.Type == StatModType.PercentMult) {
                finalValue *= 1 + mod.Value;
            }
        }

        return Mathf.CeilToInt(finalValue);
    }

    protected override int TMinValue() {
        return int.MinValue;
    }
}
