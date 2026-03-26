

using UnityEngine;

public class TestMod : MonoBehaviour {
    [Header("Pattern")]
    [SerializeField] private PatternModData doubleMod;
    [SerializeField] private ShipPattern doublePattern;

    [SerializeField] private PatternModData splitterMod;
    [SerializeField] private ShipPattern splitterPattern;

    [SerializeField] private PatternModData gatlingMod;
    [SerializeField] private ShipPattern gatlingPattern;

	[SerializeField] private PatternModData shotgunMod;
    [SerializeField] private ShipPattern shotgunPattern;

    [Header("Normal")]
    [SerializeField] private ModData mod;
    [SerializeField] private int number = 1;


    [SerializeField] private ShipBase ship;


    [ContextMenu("Add Mod")]
    private void AddMod() {
        ship = GameManager.Instance.GameLoader.Ship;
        for (int i = 0; i < number; ++i) {
            mod.ApplyTo(ship);
        }
        number = 1;
    }


    [ContextMenu("Add Double")]
    private void AddDouble() {
        ship = GameManager.Instance.GameLoader.Ship;
        doubleMod.SetShipPattern(doublePattern);
        doubleMod.ApplyTo(ship);
    }

    [ContextMenu("Add Splitter")]
    private void AddSplitter() {
        ship = GameManager.Instance.GameLoader.Ship;
        splitterMod.SetShipPattern(splitterPattern);
        splitterMod.ApplyTo(ship);
    }

    [ContextMenu("Add Gatling")]
    private void AddGatling() {
        ship = GameManager.Instance.GameLoader.Ship;
        gatlingMod.SetShipPattern(gatlingPattern);
        gatlingMod.ApplyTo(ship);
    }

	[ContextMenu("Add Shotgun")]
    private void AddShotgun() {
        ship = GameManager.Instance.GameLoader.Ship;
        shotgunMod.SetShipPattern(shotgunPattern);
        shotgunMod.ApplyTo(ship);
    }

}
