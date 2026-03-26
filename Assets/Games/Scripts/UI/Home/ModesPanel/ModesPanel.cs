using GameSystem.Common.UI;
using UnityEngine;

public class ModesPanel : DOTweenFrame {
    [SerializeField] private LockbarNotify lockbar;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation showLeftToRight;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation showRightToLeft;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation hideLeftToRight;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation hideRightToLeft;

    [SerializeField] private MaterialModeFrame materialMode;
    [SerializeField] private GearModeFrame gearMode;
    [SerializeField] private BossModeFrame bossMode;
    [SerializeField] private HalloweenModeFrame halloweenMode;
    [SerializeField] private XmasModeFrame xmasMode;
    public override Frame SetAnimShow(bool leftToRight) {
        showAnimation = leftToRight ? showLeftToRight : showRightToLeft;
        return this;
    }
    public override Frame SetAnimHide(bool leftToRight) {
        hideAnimation = leftToRight ? hideLeftToRight : hideRightToLeft;
        return this;
    }
}
