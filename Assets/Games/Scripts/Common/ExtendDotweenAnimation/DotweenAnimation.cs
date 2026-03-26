

using System.Collections.Generic;
using UnityEngine;

public class DotweenAnimation : MonoBehaviour {

    private class DOTweenTransitionComparer : IComparer<DotweenTransition> {
        public int Compare(DotweenTransition x, DotweenTransition y) {
            return y.TotalDuration.CompareTo(x.TotalDuration);
        }
    }

    [Header("[Transitions]")]
    [SerializeField] private DotweenTransition[] transitions;
    private static DOTweenTransitionComparer comparer = new DOTweenTransitionComparer();

    private void Reset() {
        transitions = GetComponentsInChildren<DotweenTransition>();
    }

    private void OnValidate() {
        Reset();
    }

    public void Initialize() {
        System.Array.Sort(transitions, comparer);
    }

    public void ResetState() {
        foreach (DotweenTransition transition in transitions) {
            transition.Stop();
            transition.ResetState();
        }
    }

    public void Stop(bool onComplete = false) {
        foreach (DotweenTransition transition in transitions) {
            transition.Stop(onComplete);
        }
    }

    public void Play() {
        ResetState();
        Play(null, true);
    }

    public void Play(System.Action onCompleted, bool restart) {
        ResetState();
        Stop(false);

        if (transitions.Length <= 0) {
            onCompleted?.Invoke();
        }
        else {
            transitions[0].DoTransition(onCompleted, restart);
            for (int i = 1; i < transitions.Length; i++) {
                transitions[i].DoTransition(null, restart);
            }
        }
    }
}
