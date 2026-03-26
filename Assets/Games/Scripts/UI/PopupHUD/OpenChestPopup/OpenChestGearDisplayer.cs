using Helper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenChestGearDisplayer : View<GearSoftData> {
    [SerializeField] private ParticleSystem[] effects;
    [SerializeField] private Image imgBorder;
    [SerializeField] private Image imgIcon;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private Image imgType;
    [SerializeField] private TextMeshProUGUI txtType;
    [SerializeField] private TextMeshProUGUI txtDescription;

    public override void Show() {
        if (Model == null) {
            return;
        }
        RaretyData curRaretyData = Model.CurrentRaretyData;
        SetColorEffects(curRaretyData.Color, true);
        SetBorder(curRaretyData.Frame, true);
        SetIcon(Model.GearHardData.Icon, true);
        SetContentName(Model.GearHardData.Name, true);
        SetColorName(curRaretyData.Color, true);
        SetType(curRaretyData.Color, curRaretyData.TagName, true);
        SetContentDescription(Model.GearHardData.Description, true);
    }


    private void SetColorEffects(Color color, bool show) {
        foreach (var e in effects) {
            if (e) {
                e.ChangeColorParticle(color);
                e.Play();
            }
        }
    }
    private void SetBorder(Sprite icon, bool show) {
        if (imgBorder) {
            imgBorder.gameObject.SetActive(show);
            if (show) {
                imgBorder.sprite = icon;
            }
        }
    }
    private void SetIcon(Sprite icon, bool show) {
        if (imgIcon) {
            imgIcon.gameObject.SetActive(show);
            if (show) {
                imgIcon.sprite = icon;
            }
        }
    }
    private void SetContentName(string content, bool show) {
        if (txtName) {
            txtName.gameObject.SetActive(show);
            if (show) {
                txtName.text = content;
            }
        }
    }
    private void SetColorName(Color color, bool show) {
        if (txtName) {
            txtName.gameObject.SetActive(show);
            if (show) {
                txtName.color = color;
            }
        }
    }
    private void SetType(Color color, string name, bool show) {
        if (imgType) {
            imgType.gameObject.SetActive(show);
            if (show) {
                imgType.SetColor(color);
                txtType.SetText(name);
            }
        }
    }
    private void SetContentDescription(string content, bool show) {
        if (txtDescription) {
            txtDescription.gameObject.SetActive(show);
            if (show) {
                txtDescription.text = content;
            }
        }
    }
}
