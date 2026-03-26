using UnityEngine;

public abstract class GetDescriptionStat : ScriptableObject {
    public abstract string GetDescriotion(string description, float value);
    public abstract string GetValueString(float value);
}
