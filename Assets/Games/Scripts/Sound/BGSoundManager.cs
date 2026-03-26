using Helper;
using UnityEngine;

public partial class SoundManager
{
    [Header("==== Background Music ====")]
    private AudioClip[] homeBGSounds;
    private AudioClip[] ingameBGSounds;
    private AudioClip[] bossBGSounds;
    private AudioClip supportBGSound;
    //[SerializeField] private AudioClip winSound;
    //[SerializeField] private AudioClip loseSound;


    private float homeVolume = 1;
    private float ingameVolume = 1;
    private float bossVolume = 0.8f;
    private float supportVolume = 0.8f;

    public AudioClip[] HomeBGSounds
    {
        get
        {
            if (homeBGSounds == null)
            {
                homeBGSounds = new AudioClip[2];
                for (int i = 0; i < homeBGSounds.Length; i++)
                {
                    homeBGSounds[i] = Resources.Load<AudioClip>(path + nameof(homeBGSounds) + i);
                }
            }
            return homeBGSounds;
        }
    }
    public AudioClip[] IngameBGSounds
    {
        get
        {
            if (ingameBGSounds == null)
            {
                ingameBGSounds = new AudioClip[2];
                for (int i = 0; i < ingameBGSounds.Length; i++)
                {
                    ingameBGSounds[i] = Resources.Load<AudioClip>(path + nameof(ingameBGSounds) + i);
                }
            }
            return ingameBGSounds;
        }
    }
    public AudioClip[] BossBGSounds
    {
        get
        {
            if (bossBGSounds == null)
            {
                bossBGSounds = new AudioClip[2];
                for (int i = 0; i < bossBGSounds.Length; i++)
                {
                    bossBGSounds[i] = Resources.Load<AudioClip>(path + nameof(bossBGSounds) + i);
                }
            }
            return bossBGSounds;
        }
    }
    public AudioClip SupportBGSound
    {
        get
        {
            if (supportBGSound == null)
                supportBGSound = Resources.Load<AudioClip>(path + nameof(supportBGSound));
            return supportBGSound;
        }
    }

    public void PlayOneShotBackgroundMusic(AudioClip clip, bool fadein = false, float fadeDuration = 1, float volume = 1f)
    {
        if (backgroundMusic == null || !BackgroundMusicEnable || clip == null)
            return;
        backgroundMusic.clip = clip;
        PlayBackgroundMusic(false, fadein, fadeDuration, false, volume);
    }

    public void PlayBackgroundHome(bool resume = false, bool fadein = false, float fadeDuration = 1)
    {
        backgroundMusic.clip = RandomHelper.RandomInCollection(HomeBGSounds);
        PlayBackgroundMusic(resume, fadein, fadeDuration, volume: homeVolume);
    }

    public void PlayBackgroundIngame(bool resume = false, bool fadein = false, float fadeDuration = 1)
    {
        backgroundMusic.clip = RandomHelper.RandomInCollection(IngameBGSounds);
        PlayBackgroundMusic(resume, fadein, fadeDuration, volume: ingameVolume);
    }

    public void PlayBackgroundSupport(bool resume = false, bool fadein = false, float fadeDuration = 1)
    {
        backgroundMusic.clip = SupportBGSound;
        PlayBackgroundMusic(resume, fadein, fadeDuration, volume: supportVolume);
    }

    public void PlayBackgroundBoss(bool resume = false, bool fadein = false, float fadeDuration = 1)
    {
        backgroundMusic.clip = RandomHelper.RandomInCollection(BossBGSounds);
        PlayBackgroundMusic(resume, fadein, fadeDuration, volume: bossVolume);
    }

    //public void PlayBackgroundWin(bool fadein = false, float fadeDuration = 1) {
    //    PlayOneShotBackgroundMusic(winSound, volume: winVolume);
    //}

    //public void PlayBackgroundLose(bool fadein = false, float fadeDuration = 1) {
    //    PlayOneShotBackgroundMusic(loseSound, volume: homeVolume);

    //}
}
