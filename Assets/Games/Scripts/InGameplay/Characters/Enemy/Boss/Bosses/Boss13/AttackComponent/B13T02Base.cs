
using Gemmob;
using System.Collections;

public class B13T02Base : T02Base {
    [UnityEngine.SerializeField] private B13T02Laser b13T02LaserPrefab;

    private B13Base owner;
    private bool spawning;

    public override void Spawn() {
        if (spawning)
            return;
        spawning = true;
        StartCoroutine(DelaySpawn(owner == null ? 0.5f : 0.02f));
    }
    public void SetOwner(B13Base owner) {
        this.owner = owner;
    }
    private IEnumerator DelaySpawn(float delayTime) {
        yield return Yielder.Wait(delayTime);
        B13T02Laser newLaser = b13T02LaserPrefab.Spawn(transform);
        newLaser.SetOwner(owner);
        newLaser.SetT02Base(this);
        newLaser.SetHitInfo((int)(T02Stat.Atk.Value * laserAtkPercent), null, this);
        newLaser.SetBaseHitInfo((int)(T02Stat.Atk.Value * baseAtkPercent), null, this);
        newLaser.Spawn(spawnBorderType, spawnBorderOffset, true);
        spawning = false;
    }
}
