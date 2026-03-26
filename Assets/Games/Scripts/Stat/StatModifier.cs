

using Gemmob;
using System;

[System.Serializable]
public class StatModifier : ICloneable, IEventParams {
    public float Value;
    public StatModType Type;
    public int Order;
    public object Source; // Added this variable

    // "Main" constructor. Requires all variables.
    public StatModifier(float value, StatModType type, int order, object source) {
        Value = value;
        Type = type;
        Order = order;
        Source = source;
    }

    public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

    public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

    public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }

    public object Clone() {
        return new StatModifier(this.Value, this.Type, this.Order, this.Source);
    }
}


public enum StatModType {
    Flat,
    PercentAdd,
    PercentMult,
}
