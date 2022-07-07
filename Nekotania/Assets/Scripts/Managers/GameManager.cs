using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;
    public static GameControl Control { get; set; }
    public GameState State { get; private set; }


    [SerializeField] private Volume _globalVolume;
    private Vignette vignette;
    private bool isGameOver;
    void Awake() => Instance = this;
    
    void Start() => ChangeState(GameState.Starting);
    void Update()
    {
        if (isGameOver)
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 1f, 3f * Time.deltaTime);
    }

    public void ChangeState(GameState newState)
    {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;

        switch (newState)
        {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.SpawningCats:
                HandleSpawningCats();
                break;
            case GameState.Continue:
                ContinueGame();
                break;
            case GameState.Pause:
                PauseGame();
                break;
            case GameState.SpeedUp:
                SpeedUpGame();
                break;
            case GameState.Restart:
                RestartGame();
                break;
            case GameState.MainMenu:
                MainMenu();
                break;
            case GameState.Win:
                Win();
                break;
            case GameState.Lose:
                Lose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnAfterStateChanged?.Invoke(newState);

        //Debug.Log($"New state: {newState}");
    }



    private void HandleStarting()
    {

        switch (Control)
        {
            case GameControl.NewGame:
                GameBalanceValues.isTutorialActive = false;
                ChangeState(GameState.SpawningCats);
                ChangeState(GameState.Continue);
                TutorialScript.Instance.isTutorialFinished = true;
                DontDestroyAudio.Instance.AnaSesCal(DontDestroyAudio.AudioType.Game);
                break;
            case GameControl.LoadGame:
                GameBalanceValues.isTutorialActive = false;
                SaveManager.Instance.OnLoad();
                ChangeState(GameState.Continue);
                TutorialScript.Instance.isTutorialFinished = true;
                DontDestroyAudio.Instance.AnaSesCal(DontDestroyAudio.AudioType.Game);
                break;
            case GameControl.Tutorial:
                GameBalanceValues.isTutorialActive = true;
                DontDestroyAudio.Instance.AnaSesCal(DontDestroyAudio.AudioType.Game);
                GameEventHandler.EventCreate(GameEventManager.Instance.TutorialEvent);
                ChangeState(GameState.SpawningCats);
                ChangeState(GameState.Pause);
                break;
        }

        if (_globalVolume.profile.TryGet<Vignette>(out var vig))
            vignette = vig;
    }

    private void HandleSpawningCats()
    {
        BuildManager.Instance.StartedCatGenerate(GameBalanceValues.StartedCatAmanont());
    }
    private void ContinueGame()
    {
        UIScript.Instance.PlayButtonImage.color = UIScript.Instance.ButtonPressedColor;
        UIScript.Instance.PauseButtonImage.color = UIScript.Instance.ButtonDefaultColor;
        UIScript.Instance.SpeedButtonImage.color = UIScript.Instance.ButtonDefaultColor;
        Time.timeScale = 1;
    }
    private void PauseGame()
    {
        UIScript.Instance.PlayButtonImage.color = UIScript.Instance.ButtonDefaultColor;
        UIScript.Instance.PauseButtonImage.color = UIScript.Instance.ButtonPressedColor;
        UIScript.Instance.SpeedButtonImage.color = UIScript.Instance.ButtonDefaultColor;
        Time.timeScale = 0;
    }
    private void SpeedUpGame()
    {
        UIScript.Instance.PlayButtonImage.color = UIScript.Instance.ButtonDefaultColor;
        UIScript.Instance.PauseButtonImage.color = UIScript.Instance.ButtonDefaultColor;
        UIScript.Instance.SpeedButtonImage.color = UIScript.Instance.ButtonPressedColor;
        Time.timeScale = 2;
    }
    private void RestartGame()
    {
        ChangeState(GameState.Starting);
        SceneController.Instance.LoadScene("Game", true);
    }
    private void MainMenu()
    {
        SaveManager.Instance.OnSave();
        SceneController.Instance.LoadScene("MainMenu", false);
    }
    private void Lose()
    {
        CycleManager.Instance.gameObject.SetActive(false);
        UIScript.Instance.BaseUICanvas.SetActive(false);
        TutorialScript.Instance.allButtons.ForEach(b => b.interactable = false);
        InputScript.Instance.layerMask = LayerMask.GetMask("UI");
        isGameOver = true;
        DontDestroyAudio.Instance.GameAudioSource.Stop();
        DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.GameOverEffectSource);
        FunctionTimer.Create(() => { DontDestroyAudio.Instance.AnaSesCal(DontDestroyAudio.AudioType.GameOver); }, 2f);
        
        SceneController.Instance.animator.SetTrigger("FadeOut");
        SaveLoadSystem.DeleteSaveFile();
        ChangeState(GameState.Continue);
        FunctionTimer.Create(() => { SceneController.Instance.LoadScene("GameOver", false);}, 2.5f);
    }
    private void Win()
    {

        CycleManager.Instance.gameObject.SetActive(false);
        TutorialScript.Instance.allButtons.ForEach(b => b.interactable = false);

        DontDestroyAudio.Instance.GameAudioSource.Stop();
        DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.VictoryEffectSource);
        FunctionTimer.Create(() => { DontDestroyAudio.Instance.AnaSesCal(DontDestroyAudio.AudioType.Victory); }, 5f);


        
        
        SceneController.Instance.animator.SetTrigger("FadeOut");
        FunctionTimer.Create(() => { SceneController.Instance.LoadScene("WinScene", false); }, 6f);
    }

    #region Menu_Buton_Method
    public void PauseButtonMethod() 
    {
        DontDestroyAudio.Instance.SesDuraklat();
        ChangeState(GameState.Pause);
    }

    public void ContinueButtonMethod()
    {
        ChangeState(GameState.Continue);
        DontDestroyAudio.Instance.SesDevamEt();
    }
    public void SpeedUpButtonMethod() {
        ChangeState(GameState.SpeedUp);
        if (!DontDestroyAudio.Instance.GameAudioSource.isPlaying)
            DontDestroyAudio.Instance.SesDevamEt();
    }
    public void MainMenuButtonMethod() => ChangeState(GameState.MainMenu);
    #endregion
}
public enum GameControl
{
    NewGame,
    LoadGame,
    Tutorial
}

[Serializable]
public enum GameState
{
    Starting,
    SpawningCats,
    Continue,
    Pause,
    SpeedUp,
    Restart,
    MainMenu,
    Win,
    Lose,
}

