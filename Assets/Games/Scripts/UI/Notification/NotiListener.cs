using Gemmob;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotiListener : MonoBehaviour {
    [SerializeField] private ButtonExplorer clickButton;
    [SerializeField] private Button normalButton;
    [SerializeField] private GameObject notificationGraphic;
    [SerializeField] private NotiRegister[] registers;
    [SerializeField] private GameCondition[] conditions;

    public IEnumerable<NotiRegister> GetRegisters() {
        return registers;
    }
    public IEnumerable<GameCondition> GetConditions() {
        return conditions;
    }

    private void OnEnable() {
        CheckToShow();
    }

    private void Start() {
        foreach (NotiRegister register in GetRegisters()) {
            if (register) {
                register.OnUpdate.AddListener(CheckToShow);
            }
        }
        AssignClick();
    }

    private void OnDestroy() {
        try {
            foreach (NotiRegister register in GetRegisters()) {
                if (register) {
                    register.OnUpdate.RemoveListener(CheckToShow);
                }
            }
            UnassignClick();
        }
        catch {

        }
    }

    public void CheckToShow() {
        if (!gameObject.activeInHierarchy)
            return;
        SetNotificationGraphicState(CheckConditions());
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
        if (notificationGraphic) {
            notificationGraphic.gameObject.SetActive(show);
        }
    }
    private void AssignClick() {
        if (clickButton) {
            clickButton.AddEvent(CheckToShow);
        }
        else if (normalButton) {
            normalButton.onClick.AddListener(CheckToShow);
        }
    }
    private void UnassignClick() {
        if (clickButton) {
            clickButton.onClick.RemoveListener(CheckToShow);
        }
        else if (normalButton) {
            normalButton.onClick.RemoveListener(CheckToShow);
        }
    }
}

public abstract class NotiListener<T> : MonoBehaviour where T : IEventParams {
    [SerializeField] private GameObject notificationGraphic;

    public abstract IEnumerable<NotiRegister<T>> GetRegisters();
    public abstract IEnumerable<GameCondition<T>> GetConditions();

    private void Start() {
        foreach (NotiRegister<T> register in GetRegisters()) {
            if (register) {
                register.OnUpdate.AddListener(CheckToShow);
            }
        }
    }

    private void OnDestroy() {
        try {
            foreach (NotiRegister<T> register in GetRegisters()) {
                if (register) {
                    register.OnUpdate.RemoveListener(CheckToShow);
                }
            }

        }
        catch {

        }
    }

    public void CheckToShow(T param) {
        SetNotificationGraphicState(CheckConditions(param));
    }

    public virtual bool CheckConditions(T param) {
        foreach (GameCondition<T> condition in GetConditions()) {
            if (condition.CheckCondition(param)) {
                return true;
            }
        }
        return false;
    }

    public void SetNotificationGraphicState(bool show = true) {
        if (notificationGraphic) {
            notificationGraphic.gameObject.SetActive(show);
        }
    }
}

