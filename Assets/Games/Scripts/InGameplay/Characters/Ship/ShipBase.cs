using DG.Tweening;
using Gemmob;
using Helper;
using System;
using UnityEngine;

[RequireComponent(typeof(ShipAttack))]
[RequireComponent(typeof(ShipMove))]
[RequireComponent(typeof(ShipHealth))]
[RequireComponent(typeof(ShipStat))]
[RequireComponent(typeof(ShipHitbox))]
[RequireComponent(typeof(ShipSkill))]
[RequireComponent(typeof(ShipStateController))]
[RequireComponent(typeof(ShipLevel))]
[RequireComponent(typeof(ShipEffect))]

public class ShipBase : CharacterBase {
    #region References
    public Transform DroneLeftPos;
    public Transform DroneRightPos;

    private ShipAttack shipAttack;
    public ShipAttack ShipAttack {
        get {
            if (shipAttack == null) {
                shipAttack = CharacterAttack as ShipAttack;
            }
            return shipAttack;
        }
    }

    private ShipMove shipMove;
    public ShipMove ShipMove {
        get {
            if (shipMove == null) {
                shipMove = CharacterMove as ShipMove;
            }
            return shipMove;
        }
    }

    private ShipHealth shipHealth;
    public ShipHealth ShipHealth {
        get {
            if (shipHealth == null) {
                shipHealth = CharacterHealth as ShipHealth;
            }
            return shipHealth;
        }
    }

    private ShipStat shipStat;
    public ShipStat ShipStat {
        get {
            if (shipStat == null) {
                shipStat = CharacterStat as ShipStat;
            }
            return shipStat;
        }
    }

    private ShipHitbox shipHitbox;
    public ShipHitbox ShipHitbox {
        get {
            if (shipHitbox == null) {
                shipHitbox = CharacterHitbox as ShipHitbox;
            }
            return shipHitbox;
        }
    }

    private ShipSkill shipSkill;
    public ShipSkill ShipSkill {
        get {
            if (shipSkill == null) {
                shipSkill = CharacterSkill as ShipSkill;
            }
            return shipSkill;
        }
    }

    private ShipEffect shipEffect;
    public ShipEffect ShipEffect {
        get {
            if (shipEffect == null) {
                shipEffect = GetComponent<ShipEffect>();
            }
            return shipEffect;
        }
    }

    private ShipLevel shipLevel;
    public ShipLevel ShipLevel {
        get {
            if (shipLevel == null) {
                shipLevel = GetComponent<ShipLevel>();
            }
            return shipLevel;
        }
    }
    private ShipSkin shipSkin;
    public ShipSkin ShipSkin {
        get {
            if (shipSkin == null) {
                shipSkin = GetComponent<ShipSkin>();
            }
            return shipSkin;
        }
    }
    #endregion
    [SerializeField] private ShipPreDieEffect shipPreDieEffect;


    [SerializeField] private Transform shipHealthPoint;
    private int chipCollection;
    private Action<int> onChipChanged;
    private bool isReviving;
    private int shipLives = 0;
    public int ShipLives { get => shipLives; }
    public Transform ShipHealthPoint { get => shipHealthPoint; }
    public Transform ShipTopBar;
    public Transform ShipTopLeft;
    public Transform ShipTopTrans;

    public bool IsReviving {
        get => isReviving;
        set => isReviving = value;
    }


    public void AddOnChipChanged(Action<int> onChipChanged) {
        this.onChipChanged += onChipChanged;
    }

    public void RemoveOnChipChanged(Action<int> onChipChanged) {
        this.onChipChanged -= onChipChanged;
    }
    public int ChipCollection { get => chipCollection; }


    public void AddChip(int chip) {
        //addChipEffect?.Play(true);
        chipCollection += chip;
        onChipChanged?.Invoke(chipCollection);
    }



    public override void Initialize() {
        base.Initialize();
        ShipEffect.Initialize();
        ShipLevel.Initalize();
        ShipSkin.Initialized();
        EventDispatcher.Instance.Dispatch(new EventKey.OnShipInitilized() {
            ship = this
        });
    }

    public override void Updating() {
        base.Updating();
        ShipEffect.Updating();
        ShipLevel.Updating();
    }

    public override void Destroy() {
        base.Destroy();
        ShipEffect.Destroy();
        ShipLevel.Destroy();
    }

    protected override void RemoveMe() {
    }

    public override void SelfDestruction() {
        ShipHealth.SeflDestroy();
        this.Recycle();
    }

    public override void Die() {
        if (ShipHealth.PlayerHPBar) {
            ShipHealth.PlayerHPBar.FadeToDisable();
            ShipHealthPoint.gameObject.SetActive(false);
        }
        if (GameManager.Instance.IsTrial || CanReviveImediate()) {
            SubLives();
            StartRevive();
            return;
        }
        PopupHUD.Instance.ChooseMod.Hide();
        shipPreDieEffect.StartEffect();
        if (GameResources.Instance.ConquerorData.IsTut) {
            var enemies = GameManager.Instance.GameLoader.Enemies;
            if (enemies != null) {
                for (int i = enemies.Count - 1; i >= 0; --i) {
                    if (enemies[i] is MinibossBase) {
                        enemies[i].Die();
                    }
                    else
                        GameManager.Instance.GameLoader.DespawnEnemy(enemies[i], true);
                }
            }
            var p = IngameHUD.Instance.GetCombat<ConquerorCombatPanel>();
            if (p != null) {
                p.HideAllUI();
                DOVirtual.DelayedCall(3f, () => p.Spawn4ngel()).SetUpdate(true);
            }
            this.DelayWait(1, () => {
                gameObject.SetActive(false);
                CameraShakeManager.Instance.ShakeCamera(shakeType);
                shipPreDieEffect.StopEffect();
                if (explosion) {
                    GameManager.Instance.GameLoader.SpawnEffectExplosions(explosion, transform.position, numberExplosion, radiusExplosion, deltaExplosion);
                }
                SoundManager.Instance.PlayShipDestroy();

            });
            return;
        }
        else {
            GameManager.Instance.PlayerDie();
            this.DelayWait(1, () => {
                gameObject.SetActive(false);
                CameraShakeManager.Instance.ShakeCamera(shakeType);
                shipPreDieEffect.StopEffect();
                if (explosion) {
                    GameManager.Instance.GameLoader.SpawnEffectExplosions(explosion, transform.position, numberExplosion, radiusExplosion, deltaExplosion);
                }
                SoundManager.Instance.PlayShipDestroy();

            });
        }



        onDie?.Invoke();
        RemoveAllOnDie();
        RemoveMe();
    }

    public void AddLives() {
        shipLives++;
    }
    public void SubLives() {
        if (shipLives > 0)
            shipLives--;
    }
    public bool CanReviveImediate() {
        return shipLives > 0;
    }


    public override void Killing(CharacterBase victim) {
        base.Killing(victim);
        foreach (var mod in ShipSkill.KillMods) {
            mod.ActionKill(this, victim);
        }
    }

    public override void Assising(CharacterBase victim) {
        base.Assising(victim);
        foreach (var mod in ShipSkill.KillMods) {
            mod.ActionKill(this, victim);
        }
    }



    public void StartRevive() {
        gameObject.SetActive(true);
        isReviving = true;
        transform.position = new Vector3(0, -(ConfigIngameData.borderH / 2 + 2), 0);
        ShipAttack.Revive();
        ShipMove.Revive();
        ShipHealth.Revive();
        ShipHitbox.Revive();
        ShipStat.Revive();
        ShipSkill.Revive();
        ShipLevel.Revive();
        ShipEffect.Revive();

        //if (ShipLevel.HasUpgradePoint) {
        //    PopupHUD.Instance.Show<ChooseModPopup>();
        //}
    }

    public void EndRevive() {
        isReviving = false;
        //ShipHitbox.TurnOffProtectShield();
        //ShipHitbox.TurnOffInvulnerable();
    }

}
