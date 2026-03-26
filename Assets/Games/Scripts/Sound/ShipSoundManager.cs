

using UnityEngine;

public partial class SoundManager {
    [Header("==== PlayerSound ====")]
    private PlayerShotSFXInfor[] playerShotSounds;

    private float lastTimePlayerShot;

    private PlayerShotSFXInfor[] PlayerShotSounds {
        get {
            if (playerShotSounds == null) {
                playerShotSounds = new PlayerShotSFXInfor[3];
                for (int i = 0; i < playerShotSounds.Length; i++) {
                    playerShotSounds[i] = new PlayerShotSFXInfor(Resources.Load<AudioClip>(path + nameof(playerShotSounds) + i));
                }
            }
            return playerShotSounds;
        }
    }

    #region Player Sound
    public void PlayShotPlayer(int shipIndex) {
        if (shipIndex < 0 || shipIndex >= PlayerShotSounds.Length)
            return;
        if (playerSound == null || !SoundEffectEnable)
            return;
        PlayerShotSFXInfor infor = PlayerShotSounds[shipIndex];
        if (Time.time - lastTimePlayerShot > infor.deltaTime) {
            playerSound.volume = infor.volume;
            if (infor.isPlayOneShot) {
                playerSound.PlayOneShot(infor.clip);
            }
            else {
                playerSound.clip = infor.clip;
                playerSound.Play();
            }
            lastTimePlayerShot = Time.time;
        }
    }

    [System.Serializable]
    private class PlayerShotSFXInfor {
        public AudioClip clip;
        [Range(0, 1)] public float volume = 1;
        public bool isPlayOneShot = true;
        public float deltaTime = 0.1f;
        public PlayerShotSFXInfor(AudioClip temp) {
            volume = 1;
            isPlayOneShot = true;
            deltaTime = 0.1f;
            clip = temp;
        }
    }
    #endregion
}
