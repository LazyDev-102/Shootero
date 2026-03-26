

using Gemmob;
using Helper;
using UnityEngine;

public partial class SoundManager {
    //[Header("==== SFX ====")]
    private AudioClip clickEffect;
    //[SerializeField] private AudioClip bulletDestroy;
    //[SerializeField] private AudioClip shieldTakeHit;
    private AudioClip[] enemyDestroys;
    private AudioClip bossDestroy;
    private AudioClip shipDestroy;
    private AudioClip chipTake;
    private AudioClip healTake;
    private AudioClip levelUp;
    private AudioClip randomMod;

    //[Header("Priovity")]
    private int bossDestroyPriority = 0;
    private int shipDestroyPriority = 0;
    private int chipTakePriority = 1;
    private int healTakePriority = 2;
    private int levelUpPriority = 3;
    private int chooseModPriority = 3;

    private float lastTimePlaySoundEnemyDestroy;

    private int previousPriovity = int.MaxValue;

    public AudioClip ClickEffect {
        get {
            if (clickEffect == null)
                clickEffect = Resources.Load<AudioClip>(path + nameof(clickEffect));
            return clickEffect;
        }
    }
    public AudioClip[] EnemyDestroys {
        get {
            if (enemyDestroys == null) {
                enemyDestroys = new AudioClip[1];
                for (int i = 0; i < enemyDestroys.Length; i++) {
                    enemyDestroys[i] = Resources.Load<AudioClip>(path + nameof(enemyDestroys) + i);
                }
            }
            return enemyDestroys;
        }
    }
    public AudioClip BossDestroy {
        get {
            if (bossDestroy == null)
                bossDestroy = Resources.Load<AudioClip>(path + nameof(bossDestroy));
            return bossDestroy;
        }
    }
    public AudioClip ShipDestroy {
        get {
            if (shipDestroy == null)
                shipDestroy = Resources.Load<AudioClip>(path + nameof(shipDestroy));
            return shipDestroy;
        }
    }
    public AudioClip ChipTake {
        get {
            if (chipTake == null)
                chipTake = Resources.Load<AudioClip>(path + nameof(chipTake));
            return chipTake;
        }
    }
    public AudioClip HealTake {
        get {
            if (healTake == null)
                healTake = Resources.Load<AudioClip>(path + nameof(healTake));
            return healTake;
        }
    }
    public AudioClip LevelUp {
        get {
            if (levelUp == null)
                levelUp = Resources.Load<AudioClip>(path + nameof(levelUp));
            return levelUp;
        }
    }
    public AudioClip RandomMod {
        get {
            if (randomMod == null)
                randomMod = Resources.Load<AudioClip>(path + nameof(randomMod));
            return randomMod;
        }
    }
    public AudioClip ChooseMod { get => RandomMod; }

    public void PreloadOpenApp() {
        soundEffect.RegisterPool(10);
    }

    public void PlaySoundEffect(AudioClip audio, PlaySoundType playSoundType = PlaySoundType.Override, float scaleVolume = 1f, int priovity = 0) {
        if (soundEffect == null || !SoundEffectEnable)
            return;
        if (soundEffect.isPlaying) {
            if (playSoundType == PlaySoundType.Override && priovity <= previousPriovity) {
                previousPriovity = priovity;
                soundEffect.Stop();
            }
            else if (playSoundType == PlaySoundType.Duplicate) {
                var dup = soundEffect.Spawn();
                if (dup) {
                    AutoDestroy auto = dup.GetComponent<AutoDestroy>();
                    if (auto == null) {
                        auto = dup.gameObject.AddComponent<AutoDestroy>();
                    }
                    auto.StartAutoDestroy(audio.length, AutoDestroy.HideType.Pool);
                    dup.PlayOneShot(audio, SoundEffectVolume * scaleVolume);
                }
                return;
            }
            else {
                return;
            }
        }
        soundEffect.PlayOneShot(audio, SoundEffectVolume * scaleVolume);
    }

    public virtual void PlayClickEffect() {
        PlaySoundEffect(ClickEffect, 0);
    }


    public void PlayBulletDestroy() {
        //if (bulletDestroy == null)
        //    return;
        //if (Time.time - lastTimePlaySoundBulletDestroy > 0.3f) {
        //    PlaySoundEffect(bulletDestroy, PlaySoundType.Duplicate);
        //    lastTimePlaySoundBulletDestroy = Time.time;
        //}
    }

    public void PlayShieldTakehit() {
        //if (shieldTakeHit == null)
        //    return;
        //if (Time.time - lastTimePlaySoundShieldTakehit > 0.3f) {
        //    PlaySoundEffect(shieldTakeHit, PlaySoundType.Duplicate);
        //    lastTimePlaySoundShieldTakehit = Time.time;
        //}
    }

    public void PlayEnemyDestroy() {
        AudioClip enemyDestroy = RandomHelper.RandomInCollection(EnemyDestroys);
        if (enemyDestroy == null)
            return;
        if (Time.time - lastTimePlaySoundEnemyDestroy > 0.3f) {
            PlaySoundEffect(enemyDestroy, PlaySoundType.Duplicate);
            lastTimePlaySoundEnemyDestroy = Time.time;
        }
    }

    public virtual void PlayBossDestroy() {
        if (BossDestroy == null)
            return;
        PlaySoundEffect(BossDestroy, priovity: bossDestroyPriority);
    }

    public virtual void PlayShipDestroy() {
        if (ShipDestroy == null)
            return;
        PlaySoundEffect(ShipDestroy, priovity: shipDestroyPriority);
    }

    public virtual void PlayChipTake() {
        if (ChipTake == null)
            return;
        PlaySoundEffect(ChipTake, priovity: chipTakePriority);
    }

    public virtual void PlayHealTake() {
        if (HealTake == null)
            return;
        PlaySoundEffect(HealTake, priovity: healTakePriority);
    }

    public virtual void PlayLevelup() {
        if (LevelUp == null)
            return;
        PlaySoundEffect(LevelUp, priovity: levelUpPriority);
    }

    public virtual void PlayRandomMod() {
        if (RandomMod == null)
            return;
        PlaySoundEffect(RandomMod, PlaySoundType.Duplicate);
    }

    public virtual void PlayChooseMod() {
        if (ChooseMod == null)
            return;
        PlaySoundEffect(ChooseMod, priovity: chooseModPriority, scaleVolume: 0.35f);
    }
}
