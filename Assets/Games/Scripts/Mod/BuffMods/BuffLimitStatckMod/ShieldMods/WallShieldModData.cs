using UnityEngine;


[CreateAssetMenu(fileName = "WallShieldModData", menuName = "Mod/Buff/Limited/WallShield")]
public class WallShieldModData : BuffLimitStackModData {
    [SerializeField] private int hp = 1000;
    [SerializeField] private int damage = 100;
    [SerializeField] private float timeReborn = 10;
    [SerializeField] private float speed;
    [Range(0f, 1f)]
    [SerializeField] private float posSpawn;

    public int Hp { get => hp; }
    public int Damage { get => damage; }
    public float TimeReborn { get => timeReborn; }
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.SpawnWallShield(hp, damage, timeReborn, 20 / speed + 0.1f, posSpawn);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}