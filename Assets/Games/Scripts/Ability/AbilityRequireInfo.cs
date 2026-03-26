using UnityEngine;
using System;

[Serializable]
public class AbilityRequireInfo {
    [SerializeField] private NewAbilityItemData ability;
    [SerializeField] private int levelRequire;

    public bool EnoughCondition() {
        return ability.Level >= levelRequire;
    }
}
