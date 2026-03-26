using GameSystem.Common.UI;
using System;
using UnityEngine;
public class TestPatternPopup : DOTweenFrame {
    [Header("Pattern")]
    [SerializeField] private PatternModData doubleMod;
    [SerializeField] private ShipPattern[] doublePattern;

    [SerializeField] private PatternModData splitterMod;
    [SerializeField] private ShipPattern[] splitterPattern;

    [SerializeField] private PatternModData gatlingMod;
    [SerializeField] private ShipPattern[] gatlingPattern;

    [SerializeField] private PatternModData shotXMod;
    [SerializeField] private ShipPattern[] shotXPattern;

    [SerializeField] private PatternModData shotGunMod;
    [SerializeField] private ShipPattern[] shotGunPattern;

    [SerializeField] private PatternModData shotTwistedPlasmaMod;
    [SerializeField] private ShipPattern[] shotTwistedPlasmaPattern;

    [SerializeField] private PatternModData shotSingleStrikeMod;
    [SerializeField] private ShipPattern[] shotSingleStrikePattern;

    private ShipBase ship;
    private Action onComplete;
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        ship = GameManager.Instance.GameLoader.Ship;
        GameManager.Instance.Pause();
    }

    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
        GameManager.Instance.Resume();
        onComplete?.Invoke();
    }


    public void AddDouble() {
        ship = GameManager.Instance.GameLoader.Ship;
        doubleMod.SetShipPattern(doublePattern[GameResources.Instance.Ship.CurrentShip - 1]);
        doubleMod.ApplyTo(ship);
        Hide();
    }

    public void AddSplitter() {
        ship = GameManager.Instance.GameLoader.Ship;
        splitterMod.SetShipPattern(splitterPattern[GameResources.Instance.Ship.CurrentShip - 1]);
        splitterMod.ApplyTo(ship);
        Hide();
    }

    public void AddGatling() {
        ship = GameManager.Instance.GameLoader.Ship;
        gatlingMod.SetShipPattern(gatlingPattern[GameResources.Instance.Ship.CurrentShip - 1]);
        gatlingMod.ApplyTo(ship);
        Hide();
    }
    public void AddShotX() {
        ship = GameManager.Instance.GameLoader.Ship;
        shotXMod.SetShipPattern(shotXPattern[GameResources.Instance.Ship.CurrentShip - 1]);
        shotXMod.ApplyTo(ship);
        Hide();
    }
    public void AddShotTwistedPlasma() {
        ship = GameManager.Instance.GameLoader.Ship;
        shotTwistedPlasmaMod.SetShipPattern(shotTwistedPlasmaPattern[GameResources.Instance.Ship.CurrentShip - 1]);
        shotTwistedPlasmaMod.ApplyTo(ship);
        Hide();
    }
    public void AddShotGun() {
        ship = GameManager.Instance.GameLoader.Ship;
        shotGunMod.SetShipPattern(shotGunPattern[GameResources.Instance.Ship.CurrentShip - 1]);
        shotGunMod.ApplyTo(ship);
        Hide();
    }
    public void AddSingleStrike() {
        ship = GameManager.Instance.GameLoader.Ship;
        shotSingleStrikeMod.SetShipPattern(shotSingleStrikePattern[GameResources.Instance.Ship.CurrentShip - 1]);
        shotSingleStrikeMod.ApplyTo(ship);
        Hide();
    }
    public void SetData(Action onComplete) {
        this.onComplete = onComplete;
    }

}
