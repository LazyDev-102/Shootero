using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gemmob.Tutorial {
	public class TutorialController : MonoBehaviour {
		private static TutorialController _instance;
		public const string _doneKey = "TutorialDoneKeys";

		private TutorialUI _ui;
		private List<string> _lsDoneKey;
		private bool _isShowingTutorial;
		public bool InTutorialLvl;

		#region Properties

		public static TutorialController Instance {
			get {
				if (_instance == null) {
					GameObject obj = new GameObject(typeof(TutorialController).Name);
					_instance = obj.AddComponent<TutorialController>();
					DontDestroyOnLoad(obj);
				}
				return _instance;
			}
		}

		public bool IsShowingTutorial {
			get => _isShowingTutorial;
		}

		#endregion

		void Awake() {
			LoadKey();
		}

		public virtual void ShowTutorial(TutorialKey key, Action endTutorialExtend = null, Action actionWhenStart = null) {
			var keyString = key.ToString();
			TutorialInfor infor = TutorialUI.Instance.FindTutorialInfor(key);
			if (infor == null) {
				Log($"[Tutorial Register] Not have key: {key}");
				return;
			}

			if (CheckHasKey(infor.NotShowWhenHasKey) && String.Compare(infor.NotShowWhenHasKey, string.Empty, StringComparison.Ordinal) != 0) {
				_lsDoneKey.Add(keyString);
				SaveKey();
				Log($"[Tutorial Controller] Cant show {key} because {infor.NotShowWhenHasKey}");
				return;
			}


			if (CheckHasKey(infor.NeedToDoneKey)) {
				if (actionWhenStart != null)
					actionWhenStart.Invoke();

				_isShowingTutorial = true;

				if (infor.SaveWhenStart) {
					_lsDoneKey.Add(keyString);
					SaveKey();
				}

				Action endTutorial = () => {
					_lsDoneKey.Add(keyString);
					_isShowingTutorial = false;
					if (infor.SaveEndStage)
						SaveKey();

					if (endTutorialExtend != null)
						endTutorialExtend.Invoke();
				};

				if (infor.DescriptInfor.Length < 1) {
					endTutorial.Invoke();
				} else {
					TutorialUI.Instance.ShowTutorial(infor, endTutorial);
				}
			} else {
				Log($"[Tutorial Controller] Need to done {infor.NeedToDoneKey} first!");
			}
		}


		void LoadKey() {
			//TODO: Load key were seen
			string data = PlayerPrefs.GetString(_doneKey);
			if (!data.Equals(string.Empty))
				_lsDoneKey = JsonHelper.FromJson<string>(data);

			if (_lsDoneKey == null)
				_lsDoneKey = new List<string>();
		}

		void SaveKey() {
			//TODO: Save key were seen
            PlayerPrefs.SetString(_doneKey, JsonHelper.ToJson(_lsDoneKey));
		}

		public bool CheckHasKey(TutorialKey key) {
			return CheckHasKey(key.ToString());
		}

		public bool CheckHasKey(string key) {
			if (string.Compare(key, string.Empty, StringComparison.Ordinal) == 0)
				return true;

			return _lsDoneKey.Exists(doneKey => doneKey.Equals(key));
		}

		void Log(string message) {
#if LOG_TUTORIAL
			Logs.Log(message);
#endif
		}
	}
}
