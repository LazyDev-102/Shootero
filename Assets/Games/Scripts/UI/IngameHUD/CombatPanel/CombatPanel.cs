using GameSystem.Common.UI;
using Gemmob;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System;

public class CombatPanel : Frame {
    #region Variables
    [SerializeField] private GameObject top;
    [SerializeField] private GameObject topLeft;
    [SerializeField] private GameObject topRight;
    [SerializeField] private Transform infoGroup;
    [SerializeField] private Transform pauseTarget;
    [SerializeField] private Transform infoTarget;
    [SerializeField] private Transform topRightTarget;
    [SerializeField] private ButtonBase btnPause;
    [SerializeField] protected TextMeshProUGUI txtChip;
    [SerializeField] protected TextMeshProUGUI txtCurrentWave;
    [SerializeField] protected TextMeshProUGUI waveLabel;
    [SerializeField] protected TextMeshProUGUI txtCurrentScore;
    [SerializeField] protected TextMeshProUGUI txtLevel;
    [SerializeField] protected TextMeshProUGUI nextWaveText;
    [SerializeField] private TextMeshProUGUI enemyLeftText;
    [SerializeField] private BossHealthBar bossHealthBar;
    [SerializeField] private PlayerLevelBar playerLevelBar;
    [SerializeField] private PlayerHealthEffect playerHealthEffect;
    [SerializeField] private PlayerTakeHitEffect playerTakeHitEffect;
    [SerializeField] private PlayerInvunerableEffect playerInvunerableEffect;
    [SerializeField] private ParticleSystem clearEffect;
    [SerializeField] private RectTransform nextWaveTrans;
    [SerializeField] protected string animCount = "Count";
    [SerializeField] protected Spine.Unity.SkeletonGraphic animGotoPlay;
    [SerializeField] protected ShowModInfoDisplay showModInfo;
    [SerializeField] private SkillSystem skillSystem;
    [SerializeField] private AbilityStartedPattern startedPattern;

    [Header("Cheat")]
    [SerializeField] private ButtonBase btnInvul;
    [SerializeField] private Image imgCheatInvul;
    [SerializeField] private ButtonBase btnNextWave;
    [SerializeField] private ButtonBase cheatAttackUp;
    [SerializeField] private ButtonBase cheatAutoPlay;

    protected int currentWave = 0;
    protected int cLevel;
    protected int maxWave;
    private ConquerorData conquerorData;
    private TutorialSytemData tutData;
    private ShipBase ship;
    private Countdowner allTimeCd = new Countdowner();
    private int allTime;
    public PlayerLevelBar PlayerLevelBar { get => playerLevelBar; }
    public ShowModInfoDisplay ShowModInfo { get => showModInfo; }
    public SkillSystem SkillSystem { get => skillSystem; }
    #endregion
    private void Awake() {
        CombatAwake();
    }
    private void Start() {
        CombatStart();
    }
    private void Update() {
        if (allTimeCd.IsTimeOut()) {
            allTime += 1;
            allTimeCd.StartCountdown(60);
        }
        allTimeCd.Countdowning(Time.deltaTime);
    }
    public virtual int GetTime() {
        return allTime;
    }

    protected virtual void CombatAwake() {
#if !CHEAT
        btnInvul.gameObject.SetActive(false);
        btnNextWave.gameObject.SetActive(false);
        enemyLeftText.gameObject.SetActive(false);
        cheatAttackUp.gameObject.SetActive(false);
        cheatAutoPlay.gameObject.SetActive(false);
#endif
        showModInfo.Assign();
        conquerorData = GameResources.Instance.ConquerorData;
        tutData = GameResources.Instance.TutorialSytemData;
        allTime = 1;
        allTimeCd.StartCountdown(60);
    }
    protected virtual void CombatStart() {
        btnPause.AddEvent(OnPauseButtonClicked);
        animGotoPlay.gameObject.SetActive(false);
        btnPause.transform.DOMove(pauseTarget.position, 1f);
        infoGroup.transform.DOMove(infoTarget.position, 1f);
        topRight.transform.DOMove(topRightTarget.position, 1f);
        ship = GameManager.Instance.GameLoader.Ship;
    }
    protected virtual void AddListener() {
        EventDispatcher.Instance.AddListener<EventKey.GameStartWaveParam>(OnWaveStart);
        EventDispatcher.Instance.AddListener<EventKey.OnBossSpawnParam>(OnBossSpawned);
        EventDispatcher.Instance.AddListener<EventKey.OnMinibossSpawnParam>(OnMinibossSpawned);
        EventDispatcher.Instance.AddListener<EventKey.OnStartGame>(InitData);
        EventDispatcher.Instance.AddListener<EventKey.OnBossRage>(OnBossRage);
        EventDispatcher.Instance.AddListener<EventKey.OnStartGame>(OnStartGame);
        btnInvul.AddEvent(OnInvulCheatClicked);
        btnNextWave.AddEvent(OnNexWaveClicked);
        cheatAttackUp.AddEvent(OnCheatAttackUp);
        cheatAutoPlay.AddEvent(OnCheatAutoPlay);
        cheatAutoPlay.GetComponent<Image>().SetAlpha(GameResources.Instance.AutoPlay ? 1 : 0.5f);
    }
    protected virtual void OnDestroy() {
        EventDispatcher.Instance.RemoveListener<EventKey.GameStartWaveParam>(OnWaveStart);
        EventDispatcher.Instance.RemoveListener<EventKey.OnBossSpawnParam>(OnBossSpawned);
        EventDispatcher.Instance.RemoveListener<EventKey.OnMinibossSpawnParam>(OnMinibossSpawned);
        EventDispatcher.Instance.RemoveListener<EventKey.OnStartGame>(InitData);
        EventDispatcher.Instance.RemoveListener<EventKey.OnBossRage>(OnBossRage);
        EventDispatcher.Instance.RemoveListener<EventKey.OnStartGame>(OnStartGame);
    }
    protected virtual void InitData() {
        SetMaxWave();
        txtCurrentWave.text = $" 1/{maxWave}";
        ship = GameManager.Instance.GameLoader.Ship;
        playerLevelBar.ForceFillAmountBar(0);
        playerLevelBar.AddListeners(ship);
        ship.AddOnChipChanged(OnChipChanged);
        ship.ShipHitbox.AddOnTakeHit(OnPlayerTakeHit);
        ship.ShipHitbox.AddOnInvulnerableEffect(OnPlayerInvunerableEffect);
        ship.ShipHitbox.StopInvulnerableEffect(OffPlayerInvunerableEffect);
        ship.ShipHealth.AddOnHpUp(OnPlayerHealthUp);
        ship.ShipHealth.AddOnBloodSucking(OnPlayerBloodSucking);
        ship.ShipLevel.AddOnLevelChanged(OnShipLevelChanged);
        ship.ShipHealth.AddPlayerTakeHit(playerTakeHitEffect);
    }
    public void SetMaxWave(int maxWave) {
        this.maxWave = maxWave;
    }
    public virtual void SetMaxWave() {
        if (GameManager.Instance.IsTrial)
            maxWave = conquerorData.TrialZone.MaxWave;
        else
            maxWave = tutData.FinishTutorialIntroduce ? conquerorData.CurrentZone.MaxWave : conquerorData.TutorialZone.MaxWave;
    }
#if CHEAT
    public void EnemyLeft(int value) {
        enemyLeftText.text = $"E count: {value}";
    }
#endif
    protected virtual void OnWaveStart(EventKey.GameStartWaveParam param) {
        currentWave = param.currentWaveIndex + 1;
        txtCurrentWave.text = $" {currentWave}/{maxWave}";
        conquerorData.CurrentZone.SetCurrentWave(currentWave);
    }
    private void OnBossSpawned(EventKey.OnBossSpawnParam param) {
        SpawnBars(param.isSpawn, param.bossBase);
    }
    private void OnMinibossSpawned(EventKey.OnMinibossSpawnParam param) {
        SpawnBars(param.isSpawn, param.minibossBase);
    }
    private void SpawnBars(bool isSpawn, EnemyBase enemy) {
        if (isSpawn) {
            bossHealthBar.AddListenerHealthChanged(enemy);
            playerLevelBar.Hide(() => {
                bossHealthBar.gameObject.SetActive(true);
                bossHealthBar.ForceFillAmountBar(1);
                bossHealthBar.Show(null);
                playerLevelBar.gameObject.SetActive(false);
            });
        }
        else {
            bossHealthBar.RemoveListenerHealthChanged(enemy);
            bossHealthBar.Hide(() => {
                bossHealthBar.gameObject.SetActive(false);
                playerLevelBar.gameObject.SetActive(true);
                playerLevelBar.Show(null);
            });
        }
    }
    private void OnBossRage(EventKey.OnBossRage param) {
        if (param.isStart) {
            bossHealthBar.FadeOut(null);
        }
        else {
            bossHealthBar.FadeIn(null);
        }
    }
    private void OnChipChanged(int chip) {
        txtChip.text = chip.ToString();
    }
    private void OnPlayerTakeHit(int damage) {
        playerTakeHitEffect.ShowFade();
    }
    private void OnPlayerHealthUp(int hp) {
        playerHealthEffect.ShowFade();
    }
    public void OnPlayerHealSmallEffect() {
        playerHealthEffect.ShowHealSmallEffect();
    }
    public void OnPlayerHealthUp() {
        playerHealthEffect.ShowFade();
    }
    private void OnPlayerBloodSucking(int hp) {
        playerHealthEffect.BloodSuking();
    }
    private void OnPlayerInvunerableEffect(float timeDuration) {
        playerInvunerableEffect.ShowFade(timeDuration);
    }
    private void OffPlayerInvunerableEffect() {
        playerInvunerableEffect.StopShowFadeConfig();
    }
    protected virtual void OnShipLevelChanged(int level) {
        SetContentLevelText($"Level {level}", false);
        cLevel = level;
    }

    public override Frame OnBack() {
        if (btnPause.gameObject.activeInHierarchy)
            PopupHUD.Instance.Show<PausePopup>();
        return this;
    }
    private void OnPauseButtonClicked() {
        PopupHUD.Instance.Show<PausePopup>();
    }
    public void SetActivePauseButton(bool interaction, bool show) {
        if (btnPause) {
            btnPause.gameObject.SetActive(show);
            if (show) {
                btnPause.SetState(interaction);
            }
        }
    }
    public string GetLevelText() {
        if (GameManager.Instance.GameLoader.Ship == null) {
            return string.Empty;
        }
        return $"LEVEL {cLevel - ship.ShipLevel.UpgradePoint + 1}!";
    }
    public int GetCurrentLevelInGame() {
        return cLevel;
    }
    protected void SetContentLevelText(string content, bool show) {
        if (txtLevel) {
            txtLevel.gameObject.SetActive(show);
            if (show) {
                txtLevel.text = content;
            }
        }
    }
    public void ShowClearWaveText() {
        if (!tutData.FinishTutorialIntroduce && currentWave < 1)
            return;
        if (clearEffect != null)
            clearEffect.Play();
    }
    public void ShowNextWave() {
        var finishTutorial = tutData.FinishTutorialIntroduce;
        if (!finishTutorial && currentWave < 1) {
            return;
        }
        nextWaveText.text = /*GameResourcesIG.Instance.ConquerorData.IsTut ? $"WAVE {currentWave}" : */$"WAVE {currentWave + 1}";
        nextWaveTrans.gameObject.SetActive(true);
        nextWaveTrans.DOSizeDelta(new Vector2(550, 180), 0.5f).OnComplete(() => {
            DOVirtual.DelayedCall(0.5f, () => {
                nextWaveTrans.DOSizeDelta(new Vector2(550, 0), 0.5f).OnComplete(() => {
                    nextWaveTrans.gameObject.SetActive(false);
                    nextWaveTrans.sizeDelta = new Vector2(0, 180);
                });
            });
        });
    }
    public void ShowContentWave(string content, int lengthX = 1000, int lengthY = 180, int fontSize = 100, float timeDelay = 0.5f, bool setUpdate = false, System.Action onComplete = null) {
        nextWaveText.fontSize = fontSize;
        nextWaveText.text = content;
        nextWaveTrans.gameObject.SetActive(true);
        nextWaveTrans.DOSizeDelta(new Vector2(lengthX, lengthY), 0.5f).SetUpdate(setUpdate).OnComplete(() => {
            DOVirtual.DelayedCall(timeDelay, () => {
                nextWaveTrans.DOSizeDelta(new Vector2(lengthX, 0), 0.5f).SetUpdate(setUpdate).OnComplete(() => {
                    nextWaveTrans.gameObject.SetActive(false);
                    nextWaveTrans.sizeDelta = new Vector2(0, lengthY);
                    onComplete?.Invoke();
                });
            }).SetUpdate(setUpdate);
        });
    }

    public void AddCanvasPlayerLevelBar() {
        var canvas = playerLevelBar.gameObject.GetComponent<Canvas>();
        if (canvas == null)
            canvas = playerLevelBar.gameObject.AddComponent<Canvas>();

        canvas.overridePixelPerfect = true;
        canvas.pixelPerfect = false;
        canvas.overrideSorting = true;
        canvas.sortingLayerName = GameSortingLayer.Tutorial;
        canvas.sortingOrder = -1;
    }
    public void RemoveCanvasPlayerLevelBar() {
        Destroy(playerLevelBar.gameObject.GetComponent<Canvas>());
    }
    #region Tutorial
    public void HideAllUI() {
        SetActivePauseButton(false, false);
        top.transform.DOMoveY(top.transform.position.y + 5, 1f).SetUpdate(true);
        topRight.transform.DOMoveX(topRight.transform.position.x + 5, 1f).SetUpdate(true);
        topLeft.transform.DOMoveX(topLeft.transform.position.x - 5, 1f).SetUpdate(true);
    }
    #endregion

    #region OnStartGame
    public virtual IEnumerator IEPlayGame(Action onComplete) {
        AddListener();
        yield return null;
        animGotoPlay.gameObject.SetActive(true);
        if (animGotoPlay.AnimationState == null)
            yield break;
        animGotoPlay.AnimationState.SetAnimation(1, animCount, false);
        var duration = animGotoPlay.AnimationState.Data.SkeletonData.Animations.Items[0].Duration;
        yield return Yielder.Wait(duration);
        ShowUI();
        onComplete?.Invoke();
    }
    protected void ShowUI() {
        top.SetActive(true);
        topLeft.SetActive(true);
        playerLevelBar.gameObject.SetActive(true);
        SetActivePauseButton(true, true);
        topRight.SetActive(true);
    }
    private void OnStartGame(EventKey.OnStartGame info) {
        DOVirtual.DelayedCall(1f, () => {
            if (startedPattern != null && startedPattern.Active) {
                PopupHUD.Instance.Show<ChooseModPopup>().SetStartPattern();
            }
        });

    }
    #endregion

    #region Cheat
    private void OnInvulCheatClicked() {
        ship.ShipHitbox.InvulnerableCheat = !ship.ShipHitbox.InvulnerableCheat;
        imgCheatInvul.gameObject.SetActive(!ship.ShipHitbox.InvulnerableCheat);
    }

    private void OnNexWaveClicked() {
        GameManager.Instance.NextWaveCheat();
    }
    private void OnCheatAttackUp() {
        if (ship != null)
            ship.ShipStat.Atk.AddModifier(new StatModifier(500, StatModType.Flat));
    }
    private void OnCheatAutoPlay() {
        GameResources.Instance.AutoPlay = !GameResources.Instance.AutoPlay;
        cheatAutoPlay.GetComponent<Image>().SetAlpha(GameResources.Instance.AutoPlay ? 1 : 0.5f);
        GameManager.Instance.GameLoader.Ship.ShipMove.AutoMove();
    }
    #endregion

#if UNITY_EDITOR
    [SerializeField] CombatPanel reference;

    [ContextMenu("Convert")]
    private void Convert() {
        top = reference.top;
        topLeft = reference.topLeft;
        topRight = reference.topRight;
        infoGroup = reference.infoGroup;
        pauseTarget = reference.pauseTarget;
        infoTarget = reference.infoTarget;
        topRightTarget = reference.topRightTarget;
        btnPause = reference.btnPause;
        txtChip = reference.txtChip;
        txtCurrentWave = reference.txtCurrentWave;
        waveLabel = reference.waveLabel;
        txtCurrentScore = reference.txtCurrentScore;
        txtLevel = reference.txtLevel;
        nextWaveText = reference.nextWaveText;
        enemyLeftText = reference.enemyLeftText;
        bossHealthBar = reference.bossHealthBar;
        playerLevelBar = reference.playerLevelBar;
        playerHealthEffect = reference.playerHealthEffect;
        playerTakeHitEffect = reference.playerTakeHitEffect;
        playerInvunerableEffect = reference.playerInvunerableEffect;
        clearEffect = reference.clearEffect;
        nextWaveTrans = reference.nextWaveTrans;
        animGotoPlay = reference.animGotoPlay;
        showModInfo = reference.showModInfo;
        skillSystem = reference.skillSystem;
        startedPattern = reference.startedPattern;
        btnInvul = reference.btnInvul;
        imgCheatInvul = reference.imgCheatInvul;
        btnNextWave = reference.btnNextWave;
        cheatAttackUp = reference.cheatAttackUp;
        cheatAutoPlay = reference.cheatAutoPlay;
    }
#endif
}
