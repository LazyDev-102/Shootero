using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : MonoBehaviour {
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private Sprite buffSprite;
    [SerializeField] private Sprite debuffSprite;
    [SerializeField] private Collider2D myCollider;

    private MaterialWaveObstacle obstacleData;
    private float durationTime;
    private Countdowner durationCd = new Countdowner();
    private Countdowner effectCoundowner = new Countdowner();
    private Countdowner immortalCd = new Countdowner();
    private bool interactive;
    private bool immortal;
    private float immortalDurationTime;
    public virtual void Destroy() {
    }
    public virtual void Initialize() {
        interactive = false;
        immortal = false;
        myCollider.enabled = !immortal;
        icon.gameObject.SetActive(!immortal);
    }
    public void SetData(MaterialWaveObstacle obstacleData) {
        this.obstacleData = obstacleData;
        obstacleData.InitData(this);
        SetIcon(obstacleData.IsBuff);
        immortalCd.StartCountdown(immortalDurationTime);
    }
    private void SetIcon(bool isBuff) {
        icon.sprite = isBuff ? buffSprite : debuffSprite;
    }
    public void SetDurationWithUnlimitStat(float duration) {
        durationTime = duration;
    }
    public virtual void ChangeRange(float multiScale = 1) {
        transform.localScale = Vector3.one * multiScale;
    }
    public virtual void SetImmortalState(bool immortal, float delayAttack) {
        this.immortal = immortal;
        immortalDurationTime = delayAttack;
    }
    private void Update() {
        if (immortal) {
            immortalCd.Countdowning(Time.deltaTime);
            if (immortalCd.IsTimeOut()) {
                StartCoroutine(PlayImmortal());
                immortalCd.StartCountdown(immortalDurationTime);

            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag(GameTag.Player) && collision.name.Contains("Ship")) {
            if (!obstacleData.IsLimit) {
                if (durationCd.IsTimeOut()) {
                    obstacleData.Active(this);
                    durationCd.StartCountdown(durationTime);
                }
                else {
                    durationCd.Countdowning(Time.fixedDeltaTime);
                }
            }
            else if (!interactive) {
                obstacleData.Active(this);
                interactive = true;
            }
            if (effectCoundowner.IsTimeOut()) {
                effectCoundowner.StartCountdown(10000f);
                PlayEffect();
            }
            effectCoundowner.Countdowning(Time.deltaTime);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag(GameTag.Player)) {
            obstacleData.Deactive(this);
            StopEffect();
            interactive = false;
        }
    }
    public void PlayEffect() {
        IngameHUD.Instance.GetCombat<MaterialModeCombatPanel>().PlayModesBuffEffect(obstacleData.IsBuff, obstacleData.Description);
    }
    public void StopEffect() {
        IngameHUD.Instance.GetCombat<MaterialModeCombatPanel>().StopModesBuffEffect();
        effectCoundowner.StartCountdown(0);
    }

    private IEnumerator PlayImmortal() {
        myCollider.enabled = true;
        icon.gameObject.SetActive(true);
        yield return Yielder.Wait(3);
        myCollider.enabled = false;
        icon.gameObject.SetActive(false);
    }
}
