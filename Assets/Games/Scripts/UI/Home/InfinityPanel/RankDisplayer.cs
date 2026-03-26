using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankDisplayer : View<MiniRankData> {
    [SerializeField] private TextMeshProUGUI txtNameRank;
    [SerializeField] private Image imgIconRank;
    [SerializeField] private ProgressBarBase rankProgress;
    [SerializeField] private Image graphicLockProgress;

    public override void Show() {
        if (Model == null) {

            return;
        }
        SetContentNameRank(Model.RankName);
        SetIconRank(Model.Icon);
    }


    public RankDisplayer SetContentNameRank(string content, bool show = true) {
        if (txtNameRank) {
            txtNameRank.gameObject.SetActive(show);
            if (show) {
                txtNameRank.text = content;
            }
        }
        return this;
    }

    public RankDisplayer SetIconRank(Sprite icon, bool show = true) {
        if (imgIconRank) {
            imgIconRank.gameObject.SetActive(show);
            if (show) {
                imgIconRank.sprite = icon;
            }
        }
        return this;
    }

    public RankDisplayer SetRankProgressBar(float pct, bool show) {
        if (rankProgress) {
            rankProgress.gameObject.SetActive(show);
            if (show) {
                rankProgress.HandleBarChanged(pct);
            }
        }
        return this;
    }

    public RankDisplayer SetStateLockProgress(bool show) {
        if (graphicLockProgress) {
            graphicLockProgress.gameObject.SetActive(show);
        }
        return this;
    }


}
