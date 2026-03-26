using UnityEngine;
using DG.Tweening;
using Gemmob;

public class AngelBoss : MonoBehaviour {
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 midPos;
    [SerializeField] private Vector2 endPos;
    [SerializeField] private DotweenAnimation anim;
    [SerializeField] private AnimationCurve moveCurve;
    [SerializeField] private ParticleSystem chargeWhite1;
    [SerializeField] private ParticleSystem chargeWhite2;
    [SerializeField] private ParticleSystem glowBurst1;
    [SerializeField] private ParticleSystem glowBurst2;
    [SerializeField] private ParticleSystem burstEffect;
    public void Init(bool isTutorial = false) {
        transform.localPosition = startPos;
        if (isTutorial) {
            StartMoveTutorial();
        }
        else {
            StartMove();
        }
    }
    private void StartMove() {
        transform.localPosition = startPos;
        transform.DOMove(midPos, 1f).SetEase(moveCurve).OnComplete(() => {
            if (anim != null)
                anim.Play();
            transform.DOMove(midPos - Vector2.up * 2, 1f).SetEase(Ease.Linear).SetUpdate(true);
            DOVirtual.DelayedCall(1f, () => {
                chargeWhite1?.Stop();
                chargeWhite2?.Stop();
                glowBurst1?.Play();
                glowBurst2?.Play();
                if (anim != null)
                    anim.Stop();
                IngameHUD.Instance.Show<AngleOfferPopup>().SetAngelBoss(this);
            });
        });
    }
    public void EndMove(System.Action onComplete) {
        transform.DOMove(midPos - Vector2.up * 5, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
            burstEffect?.Play();
            transform.DOMove(endPos, 1f).OnComplete(() => {
                this.Recycle();
                onComplete?.Invoke();
            });
        });
    }

    private void StartMoveTutorial() {
        var shipDied = !GameManager.Instance.GameLoader.Ship.gameObject.activeInHierarchy;
        var pos = Helper.CameraHelper.Camera.transform.position;
        pos.z = transform.position.z;
        Time.timeScale = 1;
        transform.localPosition = new Vector3(pos.x, pos.y + 20, pos.z);
        transform.DOMove(pos + Vector3.up * 4, 1f).SetEase(moveCurve).SetUpdate(true).OnComplete(() => {
            Time.timeScale = 1;
            if (anim != null)
                anim.Play();
            var p = IngameHUD.Instance.Combat;
            if (p != null) {
                if (shipDied) {
                    p.ShowContentWave("Death is not end..", 700, 140, 60, 2, true, () => p.ShowContentWave("...You will rise  again to claim your galaxy", 1000, 140, 45, 2, true, EndMoveTutorial));
                }
                else {
                    p.ShowContentWave("You are ready to claim your galaxy", 900, 140, 45, 2, true, EndMoveTutorial);
                }
            }
        });
    }
    public void EndMoveTutorial() {
        Time.timeScale = 1;
        GameSystem.Common.UI.HUDManager.IgnoreUserInput(false);
        chargeWhite1?.Stop();
        chargeWhite2?.Stop();
        glowBurst1?.Play();
        glowBurst2?.Play();
        if (anim != null)
            anim.Stop();
        PopupHUD.Instance.Show<TutorialIntroducePopup>();
    }
}
