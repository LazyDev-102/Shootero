
using UnityEngine;

[CreateAssetMenu(fileName = "MinibossPrefabData", menuName = "Resource/HardData/EnemyData/MinibossPrefabData")]
public class MinibossPrefabData : ScriptableObject {
    [SerializeField] private MinibossData[] minibosses;

    public MinibossData[] Minibosses { get => minibosses; }

    public MinibossBase GetMiniboss(int index) {
        return minibosses[index].minibossBase;
    }
    public Color GetMinibossBGColor(int index) {
        return minibosses[index].minibossBG;
    }
    [System.Serializable]
    public class MinibossData {
        public Color minibossBG;
        public MinibossBase minibossBase;
    }
}
