
using UnityEngine;

public class XmasModeFrame : MonoBehaviour {
    [SerializeField] private ButtonExplorer playButton;
    private void Awake() {
        playButton.AddEvent(OnPlayGame);
    }
    private void OnEnable() {
        bool status = GameResources.Instance.Xmas.Status();
        gameObject.SetActive(status);
        //if (status)
        //    GameResources.Instance.XmasShopData.CheckResetData();
    }
    private void OnPlayGame() {
        PanelHUD.Instance.Show<XmasPanel>();
    }
}
