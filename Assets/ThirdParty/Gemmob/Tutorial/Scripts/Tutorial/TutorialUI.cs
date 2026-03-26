using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace Gemmob.Tutorial {
    public class TutorialUI : MonoBehaviour {
        public static TutorialUI Instance;
        [SerializeField] private TutorialRegister _register;
        [SerializeField] private TextMeshProUGUI _txtDescript;
        [SerializeField] private Button _btnScreen;
        [SerializeField] private Button _btnSkip;
        [SerializeField] private TutorialPointer _pointer;
        [SerializeField] private Canvas _tutoCanvas;
        [SerializeField] private CharacterConservation[] _characterConservations;
        [SerializeField] private GameObject[] _hideWhenShowTarget;
        [SerializeField] private float timeActiveCanvas = 1f;
        [SerializeField] private GameObject background;
        private int _descriptIndex;
        private bool _isDoneShowText;
        private float _timeScaleCache = 1;
        private float backgroundButtonAlpha = 1;

        private TutorialInfor _currentInfor;
        private Coroutine _textCoroutine;
        private Coroutine _objectCoroutine;
        private Action _endTutorial;
        private readonly WaitForSecondsRealtime _wait = new WaitForSecondsRealtime(.02f);

        //Caching data
        private GameObject _oldTarget;
        private string _oldLayer;
        private int _oldIndex;
        private bool _previousShow;

        public bool HasDescription {
            get {
                return _descriptIndex < _currentInfor.DescriptInfor.Length - 1;
            }
        }
        public TutorialDescriptInfor CurDescription {
            get {
                return _currentInfor.DescriptInfor[_descriptIndex];
            }
        }
        public bool CanSaveAtStep {
            get {
                return _currentInfor.SaveAtStep != -1 && _currentInfor.SaveAtStep == _descriptIndex - 1;
            }
        }

        [Serializable]
        private class CharacterConservation {
            public CharacterTutorial Character;
            [SerializeField] private Renderer _charRenderer;
            [SerializeField] private MaskableGraphic _charGraphic;

            public bool IsActive() {
                return _charRenderer && _charRenderer.gameObject.activeInHierarchy || _charGraphic && _charGraphic.gameObject.activeInHierarchy;
            }

            public void SetActive(bool active) {
                if (_charRenderer)
                    _charRenderer.gameObject.SetActive(active);
                if (_charGraphic)
                    _charGraphic.gameObject.SetActive(active);
            }

            public void SetColor(Color newColor) {
                if (_charRenderer != null)
                    _charRenderer.material.color = newColor;
                if (_charGraphic != null)
                    _charGraphic.color = newColor;
            }
        }

        private void Awake() {
            Instance = this;

            _btnScreen.onClick.RemoveAllListeners();
            _btnScreen.onClick.AddListener(OnScreenClick);
            _btnScreen.gameObject.SetActive(false);
            _btnSkip.onClick.RemoveAllListeners();
            _btnSkip.onClick.AddListener(OnSkip);
            //gameObject.SetActive(!GameResourceLoader.Instance.NewTutorialSytemData.FinishAllTutorial);
        }

        public virtual void ShowTutorial(TutorialInfor infor, Action endTutorialAction = null) {
            _btnScreen.gameObject.SetActive(true);

            //INit showable
            _previousShow = true;
            ResetCharacter();
            _endTutorial = endTutorialAction;
            _descriptIndex = 0;
            _currentInfor = infor;
            _btnSkip.gameObject.SetActive(infor.IsSkip);
            ActiveCharacter(infor.Character);
            Pause();

            ShowDescription();
        }
        public void SetOnComplete(Action action) {
            _endTutorial = action;
        }
        public void AssignTartget(TutorialKey key, params Tuple<int, GameObject>[] target) {
            var tutInfo = FindTutorialInfor(key);
            if (tutInfo != null) {
                var allDesctiptions = tutInfo.DescriptInfor;
                for (int i = 0; i < target.Length; i++) {
                    var pair = target[i];
                    if (pair.Item2 == null)
                        continue;
                    Logs.Log($"[Tutorial] Assign gameobject {pair.Item2.name} to {key} in position {pair.Item1}", pair.Item2);
                    if (pair.Item1 < allDesctiptions.Length && 0 <= pair.Item1)
                        allDesctiptions[pair.Item1].Target = pair.Item2;
                    //else
                    //Logs.LogError(string.Format(format: "Dont have desctiption {pair.Item1} in key {key} "));
                }
            }
            else {
                //Logs.LogError(string.Format("Dont found tutorial have key {key}"));
            }
        }

        void Pause() {
            if (CurDescription.IsPause) {
                if (Time.timeScale > .5f)
                    _timeScaleCache = Time.timeScale;
                Time.timeScale = 0;
            }
        }
        #region Pointer
        public TutorialUI SetActiveBacground(bool status) {
            background.SetActive(status);
            return this;
        }
        public TutorialUI SetBackgroundButtonAlpha(float alpha) {
            backgroundButtonAlpha = alpha;
            return this;
        }
        public TutorialUI SetPointerDescription(string content) {
            _pointer.SetDescription(content);
            return this;
        }
        public TutorialUI InitPointer(Vector3 scale, float distance, string desription, float sizeEffect) {
            _pointer.SetData(scale, distance, desription, sizeEffect);
            return this;
        }

        #endregion
        #region Description
        void ShowDescription() {
            if (_textCoroutine != null)
                StopCoroutine(_textCoroutine);

            if (!string.IsNullOrEmpty(CurDescription.Description)) {
                _textCoroutine = StartCoroutine(ShowText(CurDescription.Description));
                ShowDialog();
            }

            ShowTarget();
            ShowCharacter();
        }

        void ShowTarget() {
            if (CurDescription.TargetType == TargetType.KeepOldTarget) {

            }
            else {
                RefreshOldTarget();

                if (CurDescription.TargetType == TargetType.NeedTarget) {
                    if (_objectCoroutine != null)
                        StopCoroutine(_objectCoroutine);
                    _objectCoroutine = StartCoroutine(ActiveCanvas());
                    //DOVirtual.DelayedCall(1f, ActiveCanvas);
                    //ActiveCanvas();
                }
            }
        }

        IEnumerator ActiveCanvas() {

            if (CurDescription == null) {
                yield break;
            }

            while (CurDescription == null || CurDescription.Target == null) {
                yield return null;
            }
            var wfss = new WaitForSecondsRealtime(timeActiveCanvas);
            yield return wfss;

            var target = CurDescription.Target;
            _oldTarget = target;
            if (!target) {
                yield break;
            }

            SetActiveBacground(true);
            Logs.Log($"[Tutorial] Show game object ", target);

            if (target.GetComponent<RectTransform>()) {
                var canvas = target.GetComponent<Canvas>();
                if (canvas == null)
                    canvas = target.AddComponent<Canvas>();
                var physic = target.GetComponent<GraphicRaycaster>();
                if (physic == null)
                    physic = target.AddComponent<GraphicRaycaster>();

                canvas.overridePixelPerfect = true;
                canvas.pixelPerfect = false;
                canvas.overrideSorting = true;
                canvas.sortingLayerName = _tutoCanvas.sortingLayerName;
                canvas.sortingOrder = _tutoCanvas.sortingOrder + 5;
                _pointer?.SetRootPos(CurDescription.PointerPos, _oldTarget.transform);
                _pointer.Active(true);
                //SetRootPos(CurDescription.PointerPos, _oldTarget.transform.position);

                DOVirtual.DelayedCall(1f, () => {
                    if (canvas)
                        canvas.overrideSorting = true;
                });

                var btn = target.GetComponent<Button>();
                ButtonBase btnBase = null;
                if (btn == null)
                    btnBase = target.GetComponent<ButtonBase>();
                if (btn) {
                    btn.onClick.AddListener(OnTargetClick);
                }
                else if (btnBase) {
                    btnBase.AddEvent(OnTargetClick);
                }
                else {
                    if (CurDescription.DescriptType == DescriptType.ClickTarget) {
                        btn = target.AddComponent<Button>();
                        btn.onClick.AddListener(OnTargetClick);
                    }
                }

                var imgs = target.GetComponentsInChildren<Image>();
                for (int i = 0; i < imgs.Length; i++) {
                    if (imgs[i].transform.name.CompareTo("Raycast") == 0)
                        continue;
                    var c = imgs[i].color;
                    c.a = backgroundButtonAlpha;
                    imgs[i].color = c;
                    SetBackgroundButtonAlpha(1);
                }
            }
            else {
                var render = target.GetComponent<SpriteRenderer>();

                _oldLayer = render.sortingLayerName;
                _oldIndex = render.sortingOrder;
                render.sortingLayerName = _tutoCanvas.sortingLayerName;
                render.sortingOrder = _tutoCanvas.sortingOrder + 5;
            }
        }

        void RefreshOldTarget() {
            if (!_oldTarget)
                return;

            if (_oldTarget.GetComponent<RectTransform>()) {
                Destroy(_oldTarget.GetComponent<GraphicRaycaster>());
                Destroy(_oldTarget.GetComponent<Canvas>());

                var btn = _oldTarget.GetComponent<Button>();
                var btnBase = _oldTarget.GetComponent<ButtonBase>();
                if (btn)
                    btn.onClick.RemoveListener(OnTargetClick);
                else if (btnBase)
                    btnBase.onClick.RemoveListener(OnTargetClick);
            }
            else {
                var render = _oldTarget.GetComponent<SpriteRenderer>();
                render.sortingLayerName = _oldLayer;
                render.sortingOrder = _oldIndex;
            }

            _oldTarget = null;
        }

        void ShowCharacter() {
            ChangeColorCharacter(CurDescription.MainCharacter);
        }

        void ShowDialog() {
            var showDescription = !(CurDescription.Target && CurDescription.TargetType == TargetType.NeedTarget && string.IsNullOrEmpty(CurDescription.Description));
            //if (CurDescription.Target)
            //	Logs.Log("[Tutorial] show target " + CurDescription.Target.transform.name, CurDescription.Target);
            if (showDescription ^ (_hideWhenShowTarget[0]?.activeInHierarchy ?? true)) {
                for (int i = 0; i < _hideWhenShowTarget.Length; i++)
                    _hideWhenShowTarget[i].SetActive(showDescription);

                _previousShow = showDescription;
            }

            if (!showDescription)
                DOVirtual.DelayedCall(.6f, () => _pointer.Active(true));
            else
                _pointer.Deactive();
        }

        IEnumerator ShowText(string des) {
            int length = 0;

            _txtDescript.text = string.Empty;
            _isDoneShowText = false;
            //var nameCharacter = StaticDataController.Instance.GetCharacterName();
            //des = des.Replace("[PlayerName]", $"<color=green>{nameCharacter}</color>");
            while (length <= des.Length) {
                var textShow = des.Substring(0, length);
                if (textShow.Length > 0 && textShow[textShow.Length - 1] == '<') {
                    var indexEnd = des.IndexOf('>', length);
                    var nextIndex = des.IndexOf('>', indexEnd + 1);
                    textShow = des.Substring(0, nextIndex + 1);
                    length = nextIndex + 1;
                }

                _txtDescript.text = textShow;
                length++;
                yield return _wait;
            }

            _isDoneShowText = true;
            _textCoroutine = null;
        }

        void NextDescription() {

            if (_isDoneShowText) {
                _descriptIndex++;
                ShowDescription();
            }
            else {
                if (_textCoroutine != null)
                    StopCoroutine(_textCoroutine);

                //var nameCharacter = StaticDataController.Instance.GetCharacterName();
                var des = CurDescription.Description;//.Replace("[PlayerName]", $"<color=green>{nameCharacter}</color>");
                _isDoneShowText = true;
                _txtDescript.text = des;
            }
        }

        #endregion

        #region Button event

        void ClickEvent(DescriptType descriptType) {
            if (!HasDescription && _isDoneShowText) {
                if (CurDescription.DescriptType == descriptType) {
                    _btnScreen.gameObject.SetActive(false);
                    Time.timeScale = _timeScaleCache;
                    RefreshOldTarget();
                    if (_endTutorial != null) {
                        _endTutorial.Invoke();
                        _pointer.Deactive();
                        SetActiveBacground(false);
                    }
                }
            }
            else if (CurDescription.DescriptType == descriptType) {
                if (CurDescription.DescriptType == DescriptType.ClickTarget)
                    if (descriptType != DescriptType.TapToNext)
                        _isDoneShowText = true;
                NextDescription();
                _pointer.Deactive();
                SetActiveBacground(false);
                if (CanSaveAtStep && _endTutorial != null) {
                    _endTutorial.Invoke();
                }
            }
        }

        void OnScreenClick() {
            ClickEvent(DescriptType.TapToNext);
        }

        void OnTargetClick() {
            _pointer.Deactive();
            ClickEvent(DescriptType.ClickTarget);
        }

        void OnSkip() {
            _pointer.Deactive();
            RefreshOldTarget();
            if (_endTutorial != null)
                _endTutorial.Invoke();
            _btnScreen.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        #endregion


        public TutorialInfor FindTutorialInfor(TutorialKey key) {
            var keyString = key.ToString();

            for (int i = 0; i < _register.Infors.Length; i++)
                if (String.Compare(_register.Infors[i].Key, keyString, StringComparison.Ordinal) == 0)
                    return _register.Infors[i];

            return null;
        }
        public void SetTimeActiveCanvas(float time) {
            timeActiveCanvas = time;
        }
        #region Character
        private int FindChar(CharacterTutorial character, CharacterTutorial[] list) {
            for (int i = 0; i < list.Length; i++)
                if (list[i] == character)
                    return i;

            return -1;
        }

        private void ResetCharacter() {
            for (int i = 0; i < _characterConservations.Length; i++)
                _characterConservations[i].SetActive(false);
        }

        void ActiveCharacter(params CharacterTutorial[] characterkeep) {
            var listCharacter = _characterConservations.Select(item => item.Character).ToArray();

            for (int j = 0; j < characterkeep.Length; j++) {
                var index = FindChar(characterkeep[j], listCharacter);
                if (index != -1)
                    _characterConservations[index].SetActive(true);
            }
        }

        void ChangeColorCharacter(params CharacterTutorial[] characterkeep) {
            for (int i = 0; i < _characterConservations.Length; i++)
                if (_characterConservations[i].IsActive()) {
                    var index = FindChar(_characterConservations[i].Character, characterkeep);
                    _characterConservations[i].SetColor(index != -1 ? Color.white : Color.gray);
                }
        }
        #endregion
    }
}