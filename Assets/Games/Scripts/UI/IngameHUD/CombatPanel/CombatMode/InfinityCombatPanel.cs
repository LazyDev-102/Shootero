using Gemmob;
using DG.Tweening;
using UnityEngine;

public class InfinityCombatPanel : CombatPanel {
    [SerializeField] protected GameObject scoreSystem;
    [SerializeField] protected Transform scoreTarget;
    protected override void CombatAwake() {
        base.CombatAwake();
    }
    protected override void CombatStart() {
        base.CombatStart();
        txtLevel.gameObject.SetActive(true);
        waveLabel.gameObject.SetActive(false);
        txtCurrentWave.gameObject.SetActive(false);
        scoreSystem.transform.DOMove(scoreTarget.position, 1f);

    }
    protected override void AddListener() {
        base.AddListener();
        EventDispatcher.Instance.AddListener<EventKey.ScoreChangedParam>(OnScoreChanged);
    }
    protected override void OnDestroy() {
        base.OnDestroy();
        EventDispatcher.Instance.RemoveListener<EventKey.ScoreChangedParam>(OnScoreChanged);
    }
    protected override void InitData() {
        base.InitData();
        scoreSystem.SetActive(true);
    }
    protected override void OnWaveStart(EventKey.GameStartWaveParam param) {
        currentWave = param.currentWaveIndex + 1;
        txtCurrentWave.text = $" {currentWave}/{maxWave}";
    }
    private void OnScoreChanged(EventKey.ScoreChangedParam param) {
        SetContentScoreText($"{param.score}", true);
    }
    protected override void OnShipLevelChanged(int level) {
        SetContentLevelText($"Level {level}", true);
        cLevel = level;
    }
    private void SetContentScoreText(string content, bool show) {
        if (txtCurrentScore) {
            txtCurrentScore.gameObject.SetActive(show);
            if (show) {
                txtCurrentScore.text = content;
            }
        }
    }
}
