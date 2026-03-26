
using UnityEngine;

public class HalloweenModeFrame : MonoBehaviour {
    [SerializeField] private ButtonExplorer playButton;
    private void Awake() {
        playButton.AddEvent(OnPlayGame);
    }
    private void OnEnable() {
        bool status = GameResources.Instance.Halloween.Status();
        gameObject.SetActive(status);
        if(status)
            GameResources.Instance.HalloweenShopData.CheckResetData();
    }
    private void OnPlayGame() {
        PanelHUD.Instance.Show<HalloweenPanel>();
    }
}
