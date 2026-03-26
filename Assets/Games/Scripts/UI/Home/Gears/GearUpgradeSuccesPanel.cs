using DG.Tweening;
using GameSystem.Common.UI;
using Helper;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearUpgradeSuccesPanel : DOTweenFrame {
    [SerializeField] private float timeAppear = 1;
    [SerializeField] private float timeMove = 0.5f;
    [SerializeField] private Image frame;
    [SerializeField] private Image icon;
    [SerializeField] private Image oldTag;
    [SerializeField] private Image newTag;
    [SerializeField] private Transform appearPoint;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform rankPoint;
    [SerializeField] private Transform topTrans;
    [SerializeField] private Transform rankTrans;
    [SerializeField] private Transform statsTrans;
    [SerializeField] private TextMeshProUGUI oldTagName;
    [SerializeField] private TextMeshProUGUI newTagName;
    [SerializeField] private ParticleSystem[] glowEffect;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject tabtoHideGO;
    [SerializeField] private List<GearDetailItemStat> gearDetailItemStats;

    private GearSoftData data;
    private List<Gear_Data.RankStat> statsData;
    private GearUpgradeItemContainer gearUpgradeItemContainer;
    private void Awake() {
        closeButton.onClick.AddListener(OnClose);
    }

    public void InitData(GearUpgradeItemContainer gearUpgradeItemContainer, GearSoftData data) {
        this.data = data;
        this.gearUpgradeItemContainer = gearUpgradeItemContainer;
        GetStatsData(data, data.SecondStatIds);
        UpdateUI();
        //ResetState();
    }
    private void UpdateUI() {
        var rank = data.CurrentRank;
        var color = data.GearHardData.GetRarety(rank).Color;
        var oldRarety = data.GearHardData.GetRarety(rank - 1);
        var newRarety = data.GearHardData.GetRarety(rank);
        icon.sprite = data.GearHardData.GetIcon(rank);
        frame.sprite = newRarety.Frame;
        oldTag.SetColor(oldRarety.Color);
        oldTagName.text = oldRarety.TagName;
        newTag.SetColor(color);
        newTagName.text = newRarety.TagName;
        ChangeColorEffect(color);
        HeadHUD.Instance.HideAll();
        Show();
        StatsUpdateUI();
    }
    private void ChangeColorEffect(Color color) {
        foreach (var item in glowEffect) {
            item.ChangeColorParticle(color);
        }
    }
    private void GetStatsData(GearSoftData datagear, List<int> data) {
        if (datagear.IsDrone) {
            statsData = new List<Gear_Data.RankStat>();
            var convertData = datagear.GearHardData as DroneGearHardData;
            for (int i = 0; i < datagear.CurrentRank + 1; i++) {
                statsData.Add(convertData.SecondStats.RankStat[i]);
            }
        }
        else {
            statsData = new List<Gear_Data.RankStat>();
            for (int i = 0; i < data.Count; i++) {
                statsData.Add(GameResources.Instance.GearData.RankStatData.GetRankStats(data[i]));
            }
        }
    }
    private void StatsUpdateUI() {
        if (gearDetailItemStats == null)
            return;
        var rank = data.CurrentRank - 1;
        var isArrow = true;
        for (int i = 0; i < gearDetailItemStats.Count; i++) {
            if (i < statsData.Count) {
                if (i == statsData.Count - 1) {
                    rank = data.CurrentRank;
                    isArrow = false;
                }
                gearDetailItemStats[i].UpdateUI(statsData[i], rank, data.IsMaxRank, !isArrow)
                                      .SetArrowStatus(isArrow);
            }
            else
                gearDetailItemStats[i].UpdateUI(null, -1, false, false)
                                      .SetArrowStatus(false);
        }
    }
    private void OnClose() {
        Hide();
        HeadHUD.Instance.Show<HeadPanel>();
        gearUpgradeItemContainer.ReturnStateAllItem();
        ResetState();
        gearUpgradeItemContainer.CloseGearPanel();
    }
    private void ResetState() {
        //topTrans.DOKill(true);
        //rankTrans.DOKill(true);
        //statsTrans.DOKill(true);
        topTrans.position = appearPoint.position;
        rankTrans.position = appearPoint.position;
        topTrans.localScale = Vector3.zero;
        rankTrans.localScale = Vector3.zero;
        statsTrans.localScale = Vector3.zero;
        tabtoHideGO.SetActive(false);
        closeButton.interactable = false;
    }
    #region Show Anim
    public void Show() {
        topTrans.gameObject.SetActive(true);
        topTrans.DOScale(Vector3.one, timeAppear).SetEase(Ease.Linear)
            .OnComplete(() => {
                topTrans.DOMove(topPoint.position, timeMove).OnComplete(() => {
                    rankTrans.gameObject.SetActive(true);
                    rankTrans.DOScale(Vector3.one, timeAppear).OnComplete(() => {
                        rankTrans.DOMove(rankPoint.position, timeMove).OnComplete(() => {
                            statsTrans.gameObject.SetActive(true);
                            statsTrans.DOScale(Vector3.one, timeAppear).OnComplete(() => {
                                tabtoHideGO.SetActive(true);
                                closeButton.interactable = true;
                            });
                        });
                    });
                });
            });
    }
    public override Frame OnBack() {
        OnClose();
        return this;
    }
    #endregion
}
