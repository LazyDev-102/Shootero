
using DG.Tweening;
using Gemmob;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillRemoveObstacle", menuName = "Resource/HardData/Skill/SkillRemoveObstacle")]
public class SkillRemoveObstacle : ItemSkillData {
    [SerializeField] private GameObject effect;

    public override void Preload() {
        if (effect != null)
            effect.RegisterPool(1);
    }

    public override void StartAttack(ShipBase ship) {
        base.StartAttack(ship);
        PlayEffect();
        RemoveObstacle();
    }
    private void RemoveObstacle() {
        GameManager.Instance.GameLoader.DespawnAllEnemyBullet();
    }
    private void PlayEffect() {
        if (effect != null) {
            var ef = effect.Spawn(ship.transform.position);
            ef.transform.localScale = Vector3.zero;
            ef.transform.DOScale(Vector3.one * 25, 1f).SetAutoKill(true);
        }
    }
    public override string GetDescriptionByIndex(int index) {
        return string.Format(Description, GetStat(SkillRankItemType.CoolDown, index));
    }
    protected override string GetCurrentDescription() {
        return string.Format(Description, GetStat(SkillRankItemType.CoolDown));
    }
    protected override string GetNextDescription() {
        return string.Format(Description,
                            $"{GetStat(SkillRankItemType.CoolDown)}<color=green>({GetNextStat(SkillRankItemType.CoolDown)})</color>");
    }
}
