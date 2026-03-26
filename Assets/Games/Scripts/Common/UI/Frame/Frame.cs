using System;
using UnityEngine;

namespace GameSystem.Common.UI {
    [DisallowMultipleComponent]
    public class Frame : MonoBehaviour {
        [Header("[Events]")]
        [SerializeField] private FrameEvent onShowed;
        [SerializeField] private FrameEvent onHidden;
        [SerializeField] private FrameEvent onPaused;
        [SerializeField] private FrameEvent onResumed;
        [SerializeField] private string screenID;
        [SerializeField] private bool hasTriggerSpecial;
        public string ScreenID { get => screenID; }
        public string ScreenName;

        private bool initialized;
        private bool showed;
        private bool paused;
        private HUD hud;
        private string namePreviousFrame;
        private Action onOneShotHide;

        public FrameEvent OnShowed { get { return onShowed; } }
        public FrameEvent OnHidden { get { return onHidden; } }
        public FrameEvent OnPaused { get { return onPaused; } }
        public FrameEvent OnResumed { get { return onResumed; } }

        public HUD Hud { get => hud; private set => hud = value; }
        public Action OnOneShotHide { get => onOneShotHide; set => onOneShotHide = value; }
        public bool HasTriggerSpecial { get => hasTriggerSpecial; }
        public bool Initialized { get => initialized; private set => initialized = value; }
        public bool Showed {
            get => showed;
            private set {
                showed = value;

                if (showed) {
                    OnShowed?.Invoke(this);
                }
                else {
                    OnHidden?.Invoke(this);
                }
            }
        }
        public bool Paused {
            get => paused;
            private set {
                paused = value;

                if (paused) {
                    OnPaused?.Invoke(this);
                }
                else {
                    OnResumed?.Invoke(this);
                }
            }
        }


        public Frame Initialize(HUD hud) {
            if (!Initialized) {
                this.Hud = hud;

                Initialized = true;
                Showed = false;
                Paused = false;

                OnInitialize(hud);
            }
            return this;
        }

        public Frame Show(Action onCompleted = null, bool instant = false) {
            Showed = true;
            Paused = false;
            OnShow(onCompleted, instant);
            return this;
        }

        public Frame Hide(Action onCompleted = null, bool instant = false) {
            Showed = false;
            OnHide(onCompleted, instant);
            return this;
        }

        public Frame Pause(Action onCompleted = null, bool instant = false) {
            if (!Paused) {
                Paused = true;
                //onPaused?.Invoke(this);

                OnPause(onCompleted, instant);
            }
            return this;
        }

        public Frame Resume(Action onCompleted = null, bool instant = false) {
            if (Paused) {
                Paused = false;
                onResumed?.Invoke(this);

                OnResume(onCompleted, instant);
            }
            return this;
        }

        public virtual Frame OnBack() {
            Hide();
            return this;
        }

        protected virtual void OnInitialize(HUD hud) { }

        protected virtual void OnShow(Action onCompleted = null, bool instant = false) {
            gameObject.SetActive(true);
            onCompleted?.Invoke();
        }

        protected virtual void OnHide(Action onCompleted = null, bool instant = false) {
            gameObject.SetActive(false);
            onCompleted?.Invoke();
            onOneShotHide?.Invoke();
            onOneShotHide = null;
        }

        protected virtual void OnPause(Action onCompleted = null, bool instant = false) {
            onCompleted?.Invoke();
        }

        protected virtual void OnResume(Action onCompleted = null, bool instant = false) {
            onCompleted?.Invoke();
        }

        // For-Tracking
        public virtual string GetCurrentNameFrame() {
            return "Frame-Class";
        }

        public Frame SetPreviousNameFrame(string name) {
            namePreviousFrame = name;
            return this;
        }

        public virtual string GetPreviousNameFrame() {
            return string.IsNullOrEmpty(namePreviousFrame) ? "null" : namePreviousFrame;
        }
        public virtual void SpecialTrigger(Action onCompleted) {
            onCompleted?.Invoke();
        }
    }
}
