using Gemmob;
using UnityEngine;

public class ShotFrontAttackComponent : ShotShipAttackComponent {
    [SerializeField] private ShotFrontBasicShipPattern basicPattern;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private SinFrontBullet sinFrontBullet;
    [SerializeField] private int numberPreload;

    public FrontBullet Bullet { get => bullet; set => bullet = value; }
    public SinFrontBullet FrontBullet { get => sinFrontBullet; set => sinFrontBullet = value; }

    protected override ShipPattern GetBasicPattern() {
        return basicPattern;
    }

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }
        if (sinFrontBullet) {
            sinFrontBullet.PreloadIngame();
            sinFrontBullet.RegisterPool(numberPreload);
        }
    }
}
