using UnityEngine;

public class SkillUpgradeableNotify : MonoBehaviour {
    [SerializeField] private SkillsUpgradeableCondition skillsUpgradeableCondition;
    [SerializeField] private GameObject notify;
    private void OnEnable() {
        notify.SetActive(skillsUpgradeableCondition.CheckCondition(null));
    }
}
