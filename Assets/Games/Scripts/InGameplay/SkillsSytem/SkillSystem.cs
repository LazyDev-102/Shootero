using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SkillSystem : MonoBehaviour {
    [SerializeField] private ButtonExplorer activeSkillButton;
    [SerializeField] private Image skillIcon;
    [SerializeField] private Image cooldownProgress;
    [SerializeField] private DOTweenAnimation passiveRotate;

    private bool active;
    private bool isPassive;
    private SkillSystemData data;
    private float maxCooldownTime;
    private Countdowner cooldownCd = new Countdowner();

    private void Start() {
        activeSkillButton.AddEvent(Active);
    }
    private void Update() {
        if (active && Time.timeScale != 0 && GameManager.Instance.GameState == GameState.Playing) {
            data.Updating();
            if (!isPassive && cooldownCd.IsCountdowning()) {
                cooldownCd.Countdowning(Time.deltaTime);
                UpdateCooldown();
            }
            else {
                if (!isPassive)
                    activeSkillButton.interactable = true;
            }
        }
    }

    public void Initialize() {
        SetData();
        bool status = data.HasSkill;
        SetActive(status);

        if (status) {
            ActiceImediate();
            UpdateUI();
        }
    }
    private void SetActive(bool status) {
        gameObject.SetActive(status);
    }
    private void ActiceImediate() {
        isPassive = data.IsPassive;
        passiveRotate.gameObject.SetActive(isPassive);
        cooldownProgress.gameObject.SetActive(!isPassive);
        if (isPassive) {
            passiveRotate.DORestart(true);
            Active();
        }
    }
    private void UpdateUI() {
        cooldownProgress.fillAmount = isPassive ? 1 : 0;
        skillIcon.sprite = data.GetSkillSelectIcon();
    }
    private void UpdateCooldown() {
        cooldownProgress.fillAmount = cooldownCd.Countdown / maxCooldownTime;
    }

    public void SetData() {
        if (data == null) {
            data = GameResources.Instance.SkillSystemData;
            data.ApplyTo();
        }
    }


    public void Active() {
        ShipBase ship = GameManager.Instance.GameLoader.Ship;
        if (!data.IsReady(ship)) {
            NotificationText.Instance.Show("Skill not ready!", NotificationText.NoticeType.Error);
            return;
        }
        data.StartAttack(ship);
        active = true;
        activeSkillButton.interactable = false;
        if (!isPassive) {
            maxCooldownTime = data.GetTimeCooldown();
            cooldownCd.StartCountdown(maxCooldownTime);
        }
    }
    public void Deactive() {
        SetData();
        if (GameManager.Initialized && GameManager.Instance.GameLoader.Ship != null) {
            data.EndAttack(GameManager.Instance.GameLoader.Ship);
        }
        active = false;
    }
}
