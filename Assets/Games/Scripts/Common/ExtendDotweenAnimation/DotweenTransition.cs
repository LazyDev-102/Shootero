using UnityEngine;
using System;
using DG.Tweening;
//using DG.DOTweenEditor;

public abstract class DotweenTransition : MonoBehaviour {
    [SerializeField, Range(0f, 10f)] private float delay = 0f;
    [SerializeField, Range(0f, 10f)] private float duration = 0.5f;
    [SerializeField] private bool speedBase;
    [SerializeField] private Ease ease = Ease.Linear;
    [SerializeField] private bool useCuver;
    [SerializeField] private AnimationCurve cuver;
    [SerializeField] private bool ignoreTimeScale = false;
    [SerializeField] private bool autoPlay;
    [SerializeField] private bool autoHide;
    [SerializeField] private int loops = 1;
    [SerializeField] private LoopType loopType;

    public float Duration { get => duration; }
    public bool SpeedBase { get => speedBase; }
    public float Delay { get => delay; }
    public float TotalDuration { get => Loops > 0 ? Duration * Loops + Delay : int.MaxValue; }
    public Ease Ease { get => ease; }
    public bool UseCuver { get => useCuver; }
    public AnimationCurve Cuver { get => cuver; }
    public bool IgnoreTimeScale { get => ignoreTimeScale; }
    public int Loops { get => loops; }
    public LoopType LoopType { get => loopType; }

    public Tween Tween { get; protected set; }

    private void OnEnable() {
        if (autoPlay) {
            DoTransition(null, true);
        }
    }

    private void OnDisable() {
        if (autoHide) {
            Stop();
        }
    }

    public void Stop(bool onComplete = false) {
        if (Tween != null)
            Tween.Kill(onComplete);
    }
    public abstract void ResetState();
    public abstract void DoTransition(Action onCompleted, bool restart);

#if UNITY_EDITOR
    [SerializeField] private bool play;
    private bool isPlaying;
    //private void Update() {
    //    if (play) {
    //        play = false;
    //        isPlaying = true;
    //        //DOTweenEditorPreview.PrepareTweenForPreview();
    //    }

    //}
#endif

}
