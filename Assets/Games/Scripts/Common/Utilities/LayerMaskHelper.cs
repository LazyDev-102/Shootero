using UnityEngine;

namespace GameSystem.Common.Utilities {

    public static class LayerMaskHelper  {
        public static bool HasLayer(this LayerMask layerMask, int layer) {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}
