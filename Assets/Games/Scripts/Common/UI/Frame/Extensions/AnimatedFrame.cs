using System;
using System.Collections;
using UnityEngine;

namespace GameSystem.Common.UI {
    [RequireComponent(typeof(Animator))]
    public class AnimatedFrame : Frame {

        [Header("[Animations]")]
        [SerializeField] private string showAnimationClip = "Show";
        [SerializeField] private string hideAnimationClip = "Hide";
        [SerializeField] private string pauseAnimationClip = "Pause";
        [SerializeField] private string resumeAnimationClip = "Resume";

        private int showTriggerHash;
        private int hideTriggerHash;
        private int pauseTriggerHash;
        private int resumeTriggerHash;

        [System.NonSerialized] private Animator animator;
        [System.NonSerialized] private Coroutine animationCoroutine;
        [System.NonSerialized] private readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

        public Animator Animator { get => animator; }

        protected override void OnInitialize(HUD hud) {
            animator = GetComponent<Animator>();

            showTriggerHash = Animator.StringToHash(showAnimationClip);
            hideTriggerHash = Animator.StringToHash(hideAnimationClip);
            pauseTriggerHash = Animator.StringToHash(pauseAnimationClip);
            resumeTriggerHash = Animator.StringToHash(resumeAnimationClip);
        }

        protected override void OnShow(Action onCompleted = null, bool instant = false) {
            this.gameObject.SetActive(true);
            if (instant) {
                onCompleted?.Invoke();
            } else {
                if (animationCoroutine != null) {
                    StopCoroutine(animationCoroutine);
                }

                animationCoroutine = StartCoroutine(PlayAnimation(showTriggerHash, showAnimationClip, () => {
                    onCompleted?.Invoke();
                }));
            }
        }

        protected override void OnHide(Action onCompleted = null, bool instant = false) {
            if (instant) {
                this.gameObject.SetActive(false);
                onCompleted?.Invoke();

            } else {
                if (animationCoroutine != null) {
                    StopCoroutine(animationCoroutine);
                }

                animationCoroutine = StartCoroutine(PlayAnimation(hideTriggerHash, hideAnimationClip, () => {
                    this.gameObject.SetActive(false);
                    onCompleted?.Invoke();
                }));
            }
        }

        protected override void OnPause(Action onCompleted = null, bool instant = false) {
            if (instant) {
                onCompleted?.Invoke();
            } else {
                if (animationCoroutine != null) {
                    StopCoroutine(animationCoroutine);
                }

                animationCoroutine = StartCoroutine(PlayAnimation(pauseTriggerHash, pauseAnimationClip, () => {
                    onCompleted?.Invoke();
                }));
            }
        }

        protected override void OnResume(Action onCompleted = null, bool instant = false) {
            if (instant) {
                onCompleted?.Invoke();
            } else {
                if (animationCoroutine != null) {
                    StopCoroutine(animationCoroutine);
                }

                animationCoroutine = StartCoroutine(PlayAnimation(resumeTriggerHash, resumeAnimationClip, () => {
                    onCompleted?.Invoke();
                }));
            }
        }

        private IEnumerator PlayAnimation(int trigger, string animationClip, Action onCompleted = null) {
            Animator.SetTrigger(trigger);

            bool stateReached = false;
            while (!stateReached) {
                if (!Animator.IsInTransition(0)) {
                    stateReached = Animator.GetCurrentAnimatorStateInfo(0).IsName(animationClip);
                }
                yield return waitForEndOfFrame;
            }

            onCompleted?.Invoke();
        }
    }
}
