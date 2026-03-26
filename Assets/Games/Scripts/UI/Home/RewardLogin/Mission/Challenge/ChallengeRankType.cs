using UnityEngine;

[CreateAssetMenu(fileName = "ChallengeRankType", menuName = "Resource/HardData/Challenge/ChallengeRankType")]
public class ChallengeRankType : ScriptableObject {

    public ChallengeType Type;
    public string Name;
    public Color Color;
    public int Order;

    [System.Serializable]
    public enum ChallengeType {
        Easy, Medium, Hard,
    }
}