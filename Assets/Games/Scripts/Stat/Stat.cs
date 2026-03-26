using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Helper;

[System.Serializable]
public abstract class Stat<T> {
    [SerializeField] protected T baseValue;
    [SerializeField] protected List<StatModifier> statModifiers = new List<StatModifier>();
    public List<StatModifier> StatModifiers { get => statModifiers; }

    private bool isDirty = true;
    private T value;
    private T lastBaseValue;

    public Stat() {
        lastBaseValue = TMinValue();
        statModifiers = new List<StatModifier>();
    }

    public Stat(T baseValue) : this() {
        this.baseValue = baseValue;
    }

    public Stat(Stat<T> stat) {
        baseValue = stat.baseValue;
        statModifiers = stat.statModifiers.Clone();
        isDirty = stat.isDirty;
        value = stat.value;
        lastBaseValue = stat.lastBaseValue;
    }

    public void SetBaseValue(T baseValue, bool reset = false) {
        if (reset) {
            Reset();
        }
        this.baseValue = baseValue;
    }

    public T GetBaseValue() {
        return this.baseValue;
    }


    public T Value {
        get {
            if (isDirty || !lastBaseValue.Equals(baseValue)) {
                lastBaseValue = baseValue;
                statModifiers.Sort(CompareModifierOrder);
                value = CalculateFinalValue();
                isDirty = false;
            }
            return value;
        }
    }

    public T Value1 {
        get {
            if (isDirty || !lastBaseValue.Equals(baseValue)) {
                lastBaseValue = baseValue;
                value = CalculateFinalValue1();
                isDirty = false;
            }
            return value;
        }
    }

    public T Value2 {
        get {
            if (isDirty || !lastBaseValue.Equals(baseValue)) {
                lastBaseValue = baseValue;
                value = CalculateFinalValue2();
                isDirty = false;
            }
            return value;
        }
    }

    public void Reset() {
        lastBaseValue = TMinValue();
        statModifiers.Clear();
    }

    public void AddModifier(StatModifier mod) {
        isDirty = true;
        statModifiers.Add(mod);
    }

    public bool RemoveModifier(StatModifier mod) {
        if (statModifiers.Remove(mod)) {
            isDirty = true;
            return true;
        }
        return false;
    }

    public bool RemoveAllModifiersFromSource(object source) {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--) {
            if (statModifiers[i].Source == source) {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    protected abstract T CalculateFinalValue();

    protected abstract T CalculateFinalValue1();

    protected abstract T CalculateFinalValue2();

    protected abstract T TMinValue();


    private int CompareModifierOrder(StatModifier a, StatModifier b) {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0;
    }
}
