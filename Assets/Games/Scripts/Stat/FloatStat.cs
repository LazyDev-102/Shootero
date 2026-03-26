
using System;

[Serializable]

public class FloatStat : Stat<float> {
    public FloatStat() : base() {

    }

    public FloatStat(float baseValue) : base(baseValue) {

    }

    public FloatStat(Stat<float> stat) : base(stat) {

    }

    protected override float CalculateFinalValue() {
        float finalValue = baseValue;
        float sumPercentAdd = 0;

        for(int i = 0; i < statModifiers.Count; i++) {
            StatModifier mod = statModifiers[i];

            if(mod.Type == StatModType.Flat) {
                finalValue += mod.Value;
            }
            else if(mod.Type == StatModType.PercentAdd) {
                sumPercentAdd += mod.Value;

                if(i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd) {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if(mod.Type == StatModType.PercentMult) {
                finalValue *= 1 + mod.Value;
            }
        }

        return (float)Math.Round(finalValue, 4);
    }

    protected override float CalculateFinalValue1() {
        float baseValue = base.baseValue;
        float finalValue = base.baseValue;

        for(int i = 0; i < statModifiers.Count; i++) {
            StatModifier mod = statModifiers[i];

            if(mod.Type == StatModType.Flat) {
                finalValue += mod.Value;
            }
            else if(mod.Type == StatModType.PercentAdd) {
                finalValue += mod.Value * baseValue;
            }
            else if(mod.Type == StatModType.PercentMult) {
                finalValue *= 1 + mod.Value;
            }
        }
        return (float)Math.Round(finalValue, 4);
    }

    protected override float CalculateFinalValue2() {
        float finalValue = baseValue;

        for(int i = 0; i < statModifiers.Count; i++) {
            StatModifier mod = statModifiers[i];
            if(mod.Type == StatModType.Flat) {
                finalValue += mod.Value;
            }
            else if(mod.Type == StatModType.PercentAdd) {
                finalValue *= 1 + mod.Value;
            }
            else if(mod.Type == StatModType.PercentMult) {
                finalValue *= 1 + mod.Value;
            }
        }

        return (float)Math.Round(finalValue, 4);
    }

    protected override float TMinValue() {
        return float.MinValue;
    }
}
