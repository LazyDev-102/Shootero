using UnityEngine;
using UnityEngine.UI;

namespace Gemmob.Common {
	public class DisableWhen : MonoBehaviour {
		[SerializeField] private bool iapEnable;
		[SerializeField] private bool iapDisable;

		[SerializeField] private bool adsEnable;
		[SerializeField] private bool adsDisable;


		private void OnEnable() {
			if (iapEnable && Config.IsIapEnable) {
				gameObject.SetActive(false);
			} else if (iapDisable && !Config.IsIapEnable) {
				gameObject.SetActive(false);
			} else if (adsEnable && Config.IsAdsEnable) {
				gameObject.SetActive(false);
			} else if (adsDisable && !Config.IsAdsEnable) {
				gameObject.SetActive(false);
			}
		}
	}
}