using UnityEngine;
using System.Collections;

namespace Gemmob.Api.Analytics {
    [CreateAssetMenu(fileName = "AdjustSettings", menuName = "Gemmob/Api/Analytics/AdjustSettings")]
    public class AdjustSettings : ScriptableObject {
        [SerializeField] string appToken;

        public string AppToken => appToken;
    }


}

