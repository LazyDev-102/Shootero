using Helper;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTierData", menuName = "Resource/HardData/Tier/EnemyTierData")]
public class EnemyTierData : ScriptableObject {
    [SerializeField] private int tier0;
    [SerializeField] private int[] tier1;
    [SerializeField] private int[] tier2;
    [SerializeField] private int[] tier3;
    [SerializeField] private int[] tier4;
    public int[] RandomEnemy() {
        //int[] result = new int[] { 17, 17, 17, 17, 17 };
        //return result;
        int[] result = new int[5];
        result[0] = tier0;
        result[1] = RandomHelper.RandomInCollection(tier1);
        result[2] = RandomHelper.RandomInCollection(tier2);
        result[3] = RandomHelper.RandomInCollection(tier3);
        result[4] = RandomHelper.RandomInCollection(tier4);
        return result;
    }
}
