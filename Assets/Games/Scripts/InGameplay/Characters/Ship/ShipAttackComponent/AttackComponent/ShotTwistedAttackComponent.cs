using Gemmob;
using UnityEngine;

public class ShotTwistedAttackComponent : ShotShipAttackComponent {
    [SerializeField] private ShotTwistedBasicPattern basicPattern;
    [SerializeField] private SinBullet bullet;
    [SerializeField] private SinFrontBullet sinFrontBullet;
    [SerializeField] private bool useFront;
    [SerializeField] private int numberPreload;
    public SinBullet Bullet { get => bullet; set => bullet = value; }
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
