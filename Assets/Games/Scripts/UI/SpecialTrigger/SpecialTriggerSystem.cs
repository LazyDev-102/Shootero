using System.Collections.Generic;
using Gemmob;
using GameSystem.Common.UI;
using System;

public class SpecialTriggerSystem : SingletonBind<SpecialTriggerSystem> {
    private Queue<Frame> queuePopup = new Queue<Frame>();
    private Action onCompleted;
    private Action endAction;
    protected override void OnAwake() {
        base.OnAwake();
        //EventDispatcher.Instance.AddListener(EventKey.OnLoadHomeScene, Action);
    }
    protected override void OnDestroy() {
        base.OnDestroy();
        //EventDispatcher.Instance.RemoveListener(EventKey.OnLoadHomeScene, Action);
    }
    public void SetQueue(List<Frame> frame, Action onCompleted) {
        queuePopup = new Queue<Frame>(frame);
        this.onCompleted = onCompleted;
    }
    public void SetQueue(IEnumerable<Frame> frame, Action onCompleted) {
        foreach (var item in frame) {
            if (item != null && item.HasTriggerSpecial)
                queuePopup.Enqueue(item);
        }
        this.onCompleted = onCompleted;
    }
    public void SetOnComplete(Action onCompleted) {
        this.onCompleted = onCompleted;
    }
    public void Action() {
        if (!GameResources.Instance.TutorialSytemData.FinishFree)
            return;
        if (PopupHUD.HasInstance) {
            SetQueue(PopupHUD.Instance.GetFrames(), null);
        }
        Check();
    }
    private void Check() {
        if (queuePopup.Count > 0) {
            Frame f = queuePopup.Dequeue();
            if (f) {
                f.SpecialTrigger(Check);
            }
            else {
                Check();
            }
        }
        else {
            onCompleted?.Invoke();
            endAction?.Invoke();
            endAction = null;
        }
    }
    public void AddOnEnd(Action endAction) {
        this.endAction = endAction;
    }
}
