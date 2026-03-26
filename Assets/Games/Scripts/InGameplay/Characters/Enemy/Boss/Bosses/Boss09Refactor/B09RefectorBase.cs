
using UnityEngine;
[RequireComponent(typeof(B09RefectorAttack), typeof(B09RefectorMove), typeof(BossHealth))]
[RequireComponent(typeof(BossSkill), typeof(BossStat), typeof(BossHitbox))]
public class B09RefectorBase : BossBase {
    #region References
    private BaseLoader<B09RefectorAttack, BossAttack> b09RefectorAttack = new BaseLoader<B09RefectorAttack, BossAttack>();
    private BaseLoader<B09RefectorMove, BossMove> b09RefectorMove = new BaseLoader<B09RefectorMove, BossMove>();

    public B09RefectorAttack B09RefectorAttack => b09RefectorAttack.GetRef(BossAttack);
    public B09RefectorMove B09RefectorMove => b09RefectorMove.GetRef(BossMove);
    public BossHealth B09RefectorHealth => BossHealth;
    public BossStat B09RefectorStat => BossStat;
    public BossHitbox B09Hitbox => BossHitbox;
    public BossSkill B09RefectorSkill => BossSkill;
    public BossEffect B09RefectorEffect => BossEffect;
    #endregion
}