using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Gemmob.Tutorial {
    public class TutorialPointer : MonoBehaviour {
        [SerializeField] private float _secondsForOneLength = 20;
        [SerializeField] private float _distance = 1;
        [SerializeField] private PointerPos _pointDir;
        [SerializeField] private MoveType _pointMoveType = MoveType.PingPong;
        [SerializeField] private ParticleSystem efx;
        [SerializeField] private TextMeshProUGUI descriptionText;
        private Vector2 _rootPos;
        private Vector2 _front;
        private Vector2 _back;
        private float _baseAngle = 225;
        private Transform _target;
        private float multiDistance = 1;

        public ParticleSystem Efx { get => efx; }

        public void SetRootPos(PointerPos pointerPos, Vector2 pointedObjPos) {
            transform.SetAsLastSibling();
            float radAngle = Mathf.Abs(_baseAngle + 90) * Mathf.Deg2Rad;
            Vector2 tempRootPos = new Vector2(pointedObjPos.x - 1 * Mathf.Sin(radAngle), pointedObjPos.y + 1 * Mathf.Cos(radAngle));

            switch (pointerPos) {
                case PointerPos.TopRight:
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, _baseAngle);
                    _rootPos = new Vector2(pointedObjPos.x - 1 * Mathf.Sin(radAngle), pointedObjPos.y + 1 * Mathf.Cos(radAngle));
                    break;
                case PointerPos.BottomLeft:
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, _baseAngle + 180);
                    _rootPos = new Vector2(pointedObjPos.x + 1 * Mathf.Sin(radAngle), pointedObjPos.y - 1 * Mathf.Cos(radAngle));
                    break;
            }

            _back = new Vector2(_rootPos.x - _distance * Mathf.Sin(radAngle), _rootPos.y + _distance * Mathf.Cos(radAngle));
            _front = new Vector2(_rootPos.x + _distance * Mathf.Sin(radAngle), _rootPos.y - _distance * Mathf.Cos(radAngle));

            DoMove();
            Active(true);
        }


        public void SetRootPos(PointerPos pointerPos, Transform target) {
            transform.SetAsLastSibling();
            efx.gameObject.SetActive(true);
            efx.transform.position = target.position;
            _target = target;
            _pointDir = pointerPos;
            Calculate();
            DoMove();
            Active(true);
        }
        public void SetData(Vector3 scale, float distanceValue, string description, float sizeEffect) {
            SetScale(scale);
            SetMultiDistance(distanceValue);
            SetDescription(description);
            SetEffectSize(sizeEffect);
        }
        public void SetScale(Vector3 value) {
            transform.localScale = value;
        }
        public void SetMultiDistance(float value) {
            multiDistance = value;
        }
        private void SetEffectSize(float multi = 1) {
            var size = efx.main;
            size.startSize = multi;
        }
        void Calculate() {
            if (!_target)
                return;

            var pointedObjPos = _target.position;
            float radAngle = Mathf.Abs(_baseAngle + 90) * Mathf.Deg2Rad;

            switch (_pointDir) {
                case PointerPos.TopRight:
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, _baseAngle);
                    _rootPos = new Vector2(pointedObjPos.x - multiDistance * Mathf.Sin(radAngle), pointedObjPos.y + multiDistance * Mathf.Cos(radAngle));
                    _back = new Vector2(_rootPos.x - _distance * Mathf.Sin(radAngle), _rootPos.y + _distance * Mathf.Cos(radAngle));
                    _front = new Vector2(_rootPos.x + _distance * Mathf.Sin(radAngle), _rootPos.y - _distance * Mathf.Cos(radAngle));
                    break;
                case PointerPos.BottomLeft:
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, _baseAngle + 180);
                    _rootPos = new Vector2(pointedObjPos.x + multiDistance * Mathf.Sin(radAngle), pointedObjPos.y - multiDistance * Mathf.Cos(radAngle));
                    _back = new Vector2(_rootPos.x - _distance * Mathf.Sin(radAngle), _rootPos.y + _distance * Mathf.Cos(radAngle));
                    _front = new Vector2(_rootPos.x + _distance * Mathf.Sin(radAngle), _rootPos.y - _distance * Mathf.Cos(radAngle));
                    break;
                case PointerPos.Top:
                    _rootPos = pointedObjPos + Vector3.up * multiDistance;
                    _back = new Vector2(_rootPos.x, _rootPos.y + 1);
                    _front = _rootPos;
                    break;
                case PointerPos.Bottom:
                    _rootPos = pointedObjPos + Vector3.down * multiDistance;
                    _back = new Vector2(_rootPos.x, _rootPos.y - 1);
                    _front = _rootPos;
                    break;
                case PointerPos.Left:
                    _rootPos = pointedObjPos + Vector3.left * multiDistance;
                    _back = new Vector2(_rootPos.x - 1, _rootPos.y);
                    _front = _rootPos;
                    break;
                case PointerPos.Right:
                    _rootPos = pointedObjPos + Vector3.right * multiDistance;
                    _back = new Vector2(_rootPos.x + 1, _rootPos.y);
                    _front = _rootPos;
                    break;
            }

        }
        public void SetDescription(string content) {
            descriptionText.text = content;
        }
        public void SetPosDescription() {
            var origin = transform.rectTransform().anchoredPosition;
            descriptionText.rectTransform.anchoredPosition = new Vector2(origin.x, origin.y + 150);
            descriptionText.SetAlpha(0);
            descriptionText.DOFade(1, 0.5f).SetUpdate(true);
        }
        public void Active(bool active) {
            gameObject.SetActive(active);
            efx.gameObject.SetActive(active);
            descriptionText.gameObject.SetActive(active);
            SetPosDescription();
        }

        public void Deactive() {
            Active(false);
        }

        void DoMove() {
            switch (_pointMoveType) {
                case MoveType.PingPong:
                    transform.position = Vector3.Lerp(_front, _back, Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.unscaledTime / _secondsForOneLength, 1f)));
                    break;
                case MoveType.None:
                    if (!_target && efx && efx.isPlaying) {
                        efx.Stop();
                    }
                    if (_target) {
                        if (efx && !efx.isPlaying) {
                            efx.Play();
                        }
                        transform.position = _target.transform.position;
                    }
                    break;
            }
        }

        void LateUpdate() {
            Calculate();
            DoMove();
        }
    }

    public enum PointerPos {
        TopRight, BottomLeft, Top, Bottom, Left, Right, None
    }
    public enum MoveType {
        PingPong, None
    }
}

