using Lean.Gui;
using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuControler : MonoBehaviour
{
    public static MenuControler Instance;

    [SerializeField] private LeanToggle tutorialToggle;
    public Slider themeSlider;
    [SerializeField] private Slider effectSlider;

    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private LeanButton continueButton;

    [SerializeField] private Image musicSliderBackground;
    [SerializeField] private Image effectSliderBackground;
    [SerializeField] private Color sliderBackColor;
    [SerializeField] private LeanWindow creditsWindow;
    [SerializeField] private List<LeanButton> mainMenuButtons = new List<LeanButton>();
    private string[] diller = new string[2] { "English", "Turkish" };
    int dilIndex = 0;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _menuCanvas.SetActive(true);
        Time.timeScale = 1;
        DontDestroyAudio.Instance.AnaSesCal(DontDestroyAudio.AudioType.Menu, .2f);
        
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);

            SetVolume(0f);
            themeSlider.value = 0f;
            SetEffectVolume(0f);
            effectSlider.value = 0f;
        }
        else
        {
            SetVolume(PlayerPrefs.GetFloat("MusicVolume"));
            themeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            SetEffectVolume(PlayerPrefs.GetFloat("EffectVolume"));
            effectSlider.value = PlayerPrefs.GetFloat("EffectVolume");
        }
        TutorialToggleStarted();
        
    }
    private void Update()
    {
        TutorialStateUpdate();
        CreditsUpdate();

        if (SaveLoadSystem.Load("SystemSave") == null)
        {
            continueButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
        }
    }
    public void NewGameButonMetot()
    {
        if (tutorialToggle.On)
        {
            GameManager.Control = GameControl.Tutorial;
            SceneController.Instance.LoadScene("Game", true, true);
        }
        else
        {
            GameManager.Control = GameControl.NewGame;
            SceneController.Instance.LoadScene("Game", true, false);
        }
        _menuCanvas.SetActive(false);
    }
    public void LoadGameButonMetot()
    {
        GameManager.Control = GameControl.LoadGame;
        SceneController.Instance.LoadScene("Game", true);
    }
    public void ExitGameMetot()
    {
        Application.Quit();
    }
    public void YeniDilGetir()
    {
        if (dilIndex + 1 >= diller.Length)
        {
            dilIndex = 0;
        }
        else
        {
            dilIndex++;
        }

        LeanLocalization.CurrentLanguage = diller[dilIndex];
    }

    public void SetVolume(float volume)
    {
        DontDestroyAudio.Instance.AudioMixer.SetFloat("volume", volume);

        if(volume == themeSlider.minValue)
            DontDestroyAudio.Instance.AudioMixer.SetFloat("volume", -80f);

        sliderBackColor.a = ((volume + 20) / 20) + .2f;
        musicSliderBackground.color = sliderBackColor;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetEffectVolume(float volume)
    {
        DontDestroyAudio.Instance.EffectMixer.SetFloat("volume", volume);

        if (volume == effectSlider.minValue)
            DontDestroyAudio.Instance.EffectMixer.SetFloat("volume", -80);

        sliderBackColor.a = ((volume + 20) / 20) + .2f;
        effectSliderBackground.color = sliderBackColor;
        PlayerPrefs.SetFloat("EffectVolume", volume);
    }
    private void TutorialStateUpdate()
    {
        if (tutorialToggle.On)
            PlayerPrefs.SetInt("TT", 0);
        else
            PlayerPrefs.SetInt("TT", 1);
    }
    private void TutorialToggleStarted()
    {
        if (PlayerPrefs.GetInt("TT") == 0)
            tutorialToggle.TurnOn();
        else
            tutorialToggle.TurnOff();
    }
    public void CreditsOpen()
    {
        if (creditsWindow.On)
        {
            creditsWindow.TurnOff();

        }
        else
        {
            creditsWindow.TurnOn();
        }
    }
    private void CreditsUpdate()
    {

        if (creditsWindow.On)
        {
            if (creditsWindow.gameObject.transform.GetChild(2).localPosition.y >= 300)
                creditsWindow.TurnOff();
            mainMenuButtons.ForEach(b => b.enabled = false);
        }
        else
        {
            mainMenuButtons.ForEach(b => b.enabled = true);
        }
    }
    public void ButtonSoundEffectPlay()
    {
        DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.ButtonClickEffectSource);
    }
}
