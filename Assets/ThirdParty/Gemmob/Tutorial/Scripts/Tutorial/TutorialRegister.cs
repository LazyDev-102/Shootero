using UnityEngine;

namespace Gemmob.Tutorial {
	[CreateAssetMenu(fileName = "Tutorial Register", menuName = "Tutorial/Register", order = 1)]
	public class TutorialRegister : ScriptableObject {
		[HideInInspector] public TutorialInfor[] Infors = new TutorialInfor[0];
	}
}
