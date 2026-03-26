using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINotiListeners : MonoBehaviour {
    [System.Serializable]
    public class UINotiListener {
        [SerializeField] private GameObject notificationGraphic;
        [SerializeField] private ButtonExplorer clickButton;
        [SerializeField] private Button normalButton;
        [SerializeField] private NotiRegister[] registers;
        [SerializeField] private GameCondition[] conditions;

        public GameObject NotificationGraphic { get => notificationGraphic; }
        public NotiRegister[] Registers { get => registers; }
        public GameCondition[] Conditions { get => conditions; }
        public ButtonExplorer ClickButton { get => clickButton; }
        public Button NormalButton { get => normalButton; }
    }

    [SerializeField] private UINotiListener[] uiNotiListner;
    private int index = 0;
    public IEnumerable<NotiRegister> GetRegisters() {
        return uiNotiListner[index].Registers;
    }
    private ButtonExplorer GetButtons() {
        return uiNotiListner[index].ClickButton;
    }
    private Button GetNormalButton() {
        return uiNotiListner[index].NormalButton;
    }
    public IEnumerable<GameCondition> GetConditions() {
        return uiNotiListner[index].Conditions;
    }

    private void OnEnable() {
        CheckToShow();
    }

    private void Start() {
        if (uiNotiListner == null || uiNotiListner.Length == 0)
            return;
        index = 0;
        for (int i = 0; i < uiNotiListner.Length; i++) {
            foreach (NotiRegister register in GetRegisters()) {
                if (register) {
                    register.OnUpdate.AddListener(CheckToShow);
                }
            }
            AssignClick();
            index++;
        }
    }

    private void OnDestroy() {
        try {
            if (uiNotiListner == null || uiNotiListner.Length == 0)
                return;
            index = 0;
            for (int i = 0; i < uiNotiListner.Length; i++) {
                foreach (NotiRegister register in GetRegisters()) {
                    if (register) {
                        register.OnUpdate.RemoveListener(CheckToShow);
                    }
                }
                UnassignClick();
                index++;
            }
        }
        catch {

        }
    }

    public void CheckToShow() {
        if (!gameObject.activeInHierarchy)
            return;
        for (int i = 0; i < uiNotiListner.Length; i++) {
            index = i;
            if (CheckConditions()) {
                SetNotificationGraphicState(true);
                return;
            }
            else {
                SetNotificationGraphicState(false);
            }
        }
    }

    public virtual bool CheckConditions() {
        foreach (GameCondition condition in GetConditions()) {
            if (condition.CheckCondition(null)) {
                return true;
            }
        }
        return false;
    }

    public void SetNotificationGraphicState(bool show = true) {
        for (int i = 0; i < uiNotiListner.Length; i++) {
            if (uiNotiListner[i].NotificationGraphic) {
                uiNotiListner[i].NotificationGraphic.gameObject.SetActive(i == index && show);
            }
        }
    }
    private void AssignClick() {
        var btn = GetButtons();
        if (btn != null) {
            btn.AddEvent(CheckToShow);
        }
        else {
            var btnNormal = GetNormalButton();
            if (btnNormal != null)
                btnNormal.onClick.AddListener(CheckToShow);
        }
    }
    private void UnassignClick() {
        var btn = GetButtons();
        if (btn != null) {
            btn.onClick.RemoveListener(CheckToShow);
        }
        else {
            var btnNormal = GetNormalButton();
            if (btnNormal != null)
                btnNormal.onClick.RemoveListener(CheckToShow);
        }
    }
}

