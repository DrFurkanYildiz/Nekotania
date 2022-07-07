using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DontDestroyAudio : MonoBehaviour
{
    public static DontDestroyAudio Instance;
    public enum AudioType
    {
        Menu,
        Game,
        Victory,
        GameOver
    }
    public enum EffectType
    {
        NewEventEffectSource,
        VictoryEffectSource,
        GameOverEffectSource,
        CatKillEffectSource,
        AskerOlmaEffectSource,
        KediKaydirmaEffectSource,
        HerTurKesmeEffectSource,
        UpgradeEffectSource,
        MerkezAcmaEffectSource,
        GemiPanelAcmaEffectSource,
        GemiParcaSecmeEffectSource,
        GemiParcaYerlestirmeEffectSource,
        ButtonClickEffectSource
    }

    public AudioSource MenuAudioSource;
    public AudioSource GameAudioSource;
    public AudioSource VictoryAudioSource;
    public AudioSource GameoverAudioSource;

    public AudioMixer AudioMixer;
    public AudioMixer EffectMixer;


    public AudioSource NewEventEffectSource;
    public AudioSource VictoryEffectSource;
    public AudioSource GameOverEffectSource;
    public AudioSource CatKillEffectSource;
    public AudioSource AskerOlmaEffectSource;
    public AudioSource KediKaydirmaEffectSource;
    public AudioSource HerTurKesmeEffectSource;
    public AudioSource UpgradeEffectSource;
    public AudioSource MerkezAcmaEffectSource;
    public AudioSource GemiPanelAcmaEffectSource;
    public AudioSource GemiParcaSecmeEffectSource;
    public AudioSource GemiParcaYerlestirmeEffectSource;
    public AudioSource ButtonClickEffectSource;



    private double soundTime;
    private float MasterVolumeSlider;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
            Destroy(gameObject);

    }
    private void Update()
    {
        MasterVolumeSlider = MenuControler.Instance.themeSlider.value;
        if (MasterVolumeSlider == -20)
            MasterVolumeSlider = -80;
    }

    public void SesDuraklat()
    {
        GameAudioSource.Pause();
        soundTime = GameAudioSource.time;
    }
    public void SesDevamEt()
    {
        if (GameAudioSource.isPlaying || !TutorialScript.Instance.isTutorialFinished)
            return;
        GameAudioSource.PlayScheduled(soundTime);
    }
    public void AnaSesCal(AudioType targetType, float fadeTime = 1.25f)
    {
        StartCoroutine(SesKis(targetType, fadeTime));
    }
    IEnumerator SesKis(AudioType targetType, float fadeTime)
    {
        // AudioMixer.GetFloat("volume", out float masterVolume);

        float timeToFade = .25f;
        float timeElapsed = 0f;

        while (timeElapsed < timeToFade)
        {
            AudioMixer.SetFloat("volume", Mathf.Lerp(MasterVolumeSlider, -80, timeElapsed / timeToFade));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        MenuAudioSource.Stop();
        GameAudioSource.Stop();
        VictoryAudioSource.Stop();
        GameoverAudioSource.Stop();
        GetAudioSource(targetType).Play();

        StartCoroutine(SesAc(fadeTime));

    }
    IEnumerator SesAc(float fadeTime)
    {
        float timeToFade = fadeTime;
        float timeElapsed = 0f;

        while (timeElapsed < timeToFade)
        {
            AudioMixer.SetFloat("volume", Mathf.Lerp(-80, MasterVolumeSlider, timeElapsed / timeToFade));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
    private AudioSource GetAudioSource(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.Menu:
                return MenuAudioSource;
            case AudioType.Game:
                return GameAudioSource;
            case AudioType.Victory:
                return VictoryAudioSource;
            case AudioType.GameOver:
                return GameoverAudioSource;
            default:
                return null;
        }
    }
    private AudioSource GetEffectSource(EffectType effect)
    {
        switch (effect)
        {
            case EffectType.NewEventEffectSource:
                return NewEventEffectSource;
            case EffectType.VictoryEffectSource:
                return VictoryEffectSource;
            case EffectType.GameOverEffectSource:
                return GameOverEffectSource;
            case EffectType.CatKillEffectSource:
                return CatKillEffectSource;
            case EffectType.AskerOlmaEffectSource:
                return AskerOlmaEffectSource;
            case EffectType.KediKaydirmaEffectSource:
                return KediKaydirmaEffectSource;
            case EffectType.HerTurKesmeEffectSource:
                return HerTurKesmeEffectSource;
            case EffectType.UpgradeEffectSource:
                return UpgradeEffectSource;
            case EffectType.MerkezAcmaEffectSource:
                return MerkezAcmaEffectSource;
            case EffectType.GemiPanelAcmaEffectSource:
                return GemiPanelAcmaEffectSource;
            case EffectType.GemiParcaSecmeEffectSource:
                return GemiParcaSecmeEffectSource;
            case EffectType.GemiParcaYerlestirmeEffectSource:
                return GemiParcaYerlestirmeEffectSource;
            case EffectType.ButtonClickEffectSource:
                return ButtonClickEffectSource;
            default:
                return null;
        }
    }
    public void SesEfectiCal(EffectType effectType)
    {
        GetEffectSource(effectType).Play();
    }
}
