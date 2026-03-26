using UnityEngine;

[CreateAssetMenu(fileName = "CheckContainKeyCondition", menuName = "Resource/Conditions/Shop/CheckContainKeyCondition")]
public class CheckContainKeyCondition : GameCondition {
    [SerializeField] private ItemStack[] itemKeys;
    public override bool CheckCondition(object target) {
        foreach (var item in itemKeys) {
            if (GameResources.Instance.Inventory.GetItem(item.Id).Amount > 0)
                return true;
        }
        return false;
    }
}