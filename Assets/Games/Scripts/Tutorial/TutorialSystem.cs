using Gemmob;
using Gemmob.Tutorial;
using System;
using UnityEngine;

public class TutorialSystem : SingletonBind<TutorialSystem> {
    private TutorialInfor tutorialRegister;

    public TutorialSystem ShowTutorial(Action onComplete) {
        TutorialUI.Instance.ShowTutorial(tutorialRegister, () => onComplete?.Invoke());
        return this;
    }

    public TutorialSystem SetOnComplete(Action onComplete) {
        TutorialUI.Instance.SetOnComplete(onComplete);
        return this;
    }

    public TutorialSystem AssignTarget(TutorialKey key, int index, GameObject target) {
        TutorialUI.Instance.AssignTartget(key, new Tuple<int, GameObject>(index, target));
        return this;
    }

    public TutorialSystem GetData(TutorialKey key) {
        tutorialRegister = TutorialUI.Instance.FindTutorialInfor(key);
        return this;
    }

    public TutorialSystem SetTimeActiveCanvas(float time) {
        TutorialUI.Instance.SetTimeActiveCanvas(time);
        return this;
    }

    public TutorialSystem InitPointer(Vector3 scale, float distance, string desription, float sizeEffect) {
        TutorialUI.Instance.InitPointer(scale, distance, desription, sizeEffect);
        return this;
    }

    public TutorialSystem SetBackgroundButtonAlpha(float alpha) {
        TutorialUI.Instance.SetBackgroundButtonAlpha(alpha);
        return this;
    }

    public TutorialSystem SetCamera() {
        HeadHUD.Instance.GetComponentInParent<Canvas>().worldCamera = Helper.CameraHelper.Camera;
        return this;
    }
}
