using Gemmob;
using UnityEngine;
public class TextShowupManager : SingletonBind<TextShowupManager> {
    [SerializeField] private TextIngame normalHitText;
    [SerializeField] private TextIngame critHitText;
    [SerializeField] private TextIngame oneShotText;
    [SerializeField] private TextIngame burnHitText;
    [SerializeField] private TextIngame evasionText;
    [SerializeField] private TextIngame healingText;
    [SerializeField] private TextIngame addChipText;
    [SerializeField] private float radiusOffset;

    public override void Preload() {
        base.Preload();
        normalHitText.RegisterPool(40);
        critHitText.RegisterPool(10);
        oneShotText.RegisterPool(2);
        burnHitText.RegisterPool(20);
        evasionText.RegisterPool(10);
        healingText.RegisterPool(40);
        addChipText.RegisterPool(10);
    }

    public void ShowHitText(HitType type, string text, Vector2 position) {
        switch (type) {
            case HitType.Normal: {
                normalHitText.Spawn(transform, GetPosition(position)).Show(text);
                break;
            }
            case HitType.Crit: {
                critHitText.Spawn(transform, GetPosition(position)).Show(text);
                break;
            }
            case HitType.Burn: {
                burnHitText.Spawn(transform, position).Show(text);
                break;
            }
            case HitType.OneShot: {
                oneShotText.Spawn(transform, GetPosition(position)).Show(text);
                break;
            }

        }
    }

    public void ShowEvasionText(Vector2 position) {
        evasionText.Spawn(transform, GetPosition(position)).Show("Evasion");
    }

    public void ShowHealingText(string text, Vector2 position) {
        healingText.Spawn(transform, GetPosition(position)).Show(text);
    }

    public void ShowAddChipText(string text, Vector2 position) {
        addChipText.Spawn(transform, GetPosition(position)).Show(text);
    }

    public Vector2 GetPosition(Vector2 position) {
        return Random.insideUnitCircle * radiusOffset + position;
    }
}
