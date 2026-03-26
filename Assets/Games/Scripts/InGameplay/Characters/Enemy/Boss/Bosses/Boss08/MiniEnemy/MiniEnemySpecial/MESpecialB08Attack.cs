using Gemmob;
using Helper;
using UnityEngine;

public class MESpecialB08Attack : EnemyAttack {
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private float speedBullet;
    [SerializeField] private float accelerBullet;
    [SerializeField] private int numberBullet;
    [SerializeField] private int numberPreload;

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }
    }

    public override bool CanAttack() {
        return true;
    }

    protected override void Attacking() {
        Vector2 directionBase = transform.up;
        float deltaAngle = 360f / numberBullet;
        for (int i = 0; i < numberBullet; ++i) {
            Vector2 direction = directionBase.RotateDirection(i * deltaAngle);
            FrontBullet newBullet = GameManager.Instance.GameLoader.SpawnBullet(bullet, transform.position);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.Shoot(speedBullet, direction, accelerBullet);
            }
        }
    }
}
