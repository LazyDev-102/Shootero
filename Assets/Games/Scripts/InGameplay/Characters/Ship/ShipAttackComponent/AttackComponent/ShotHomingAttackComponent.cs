

using Gemmob;
using UnityEngine;

public class ShotHomingAttackComponent : ShotShipAttackComponent {
    [SerializeField] private ShotHomingBasicPattern basicPattern;
    [SerializeField] private HomingBullet bullet;
    [SerializeField] private SinHomingBullet sinHomingBullet;
    [SerializeField] private int numberPreload;

    public HomingBullet Bullet { get => bullet; set => bullet = value; }
    public SinHomingBullet SinHomingBullet { get => sinHomingBullet; set => sinHomingBullet = value; }

    protected override ShipPattern GetBasicPattern() {
        return basicPattern;
    }
    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }
        if (sinHomingBullet) {
            sinHomingBullet.PreloadIngame();
            sinHomingBullet.RegisterPool(numberPreload);
        }
    }
}
