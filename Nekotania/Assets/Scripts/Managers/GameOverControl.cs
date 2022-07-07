using Lean.Localization;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalText;
    public LeanPhrase phrase;
    public LeanLocalization leanLocalization;
    private string text;
    private bool IsNext;
    public Transform SkipButton;
    public Transform RestartButton;
    public Transform MainMenuButton;
    void Start()
    {
        LeanLocalization.CurrentLanguages.TryGetValue(LeanLocalization.CurrentLanguage, out LeanLanguage language);

        int index = leanLocalization.Languages.IndexOf(language);
        text = phrase.Entries[index].Text;
        StartCoroutine(TypeSentence(text));
    }
    public void SkipButtonMethod()
    {
        IsNext = true;
        SkipButton.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(true);
        MainMenuButton.gameObject.SetActive(true);
    }
    public void MainMenuButonMethod()
    {
        DontDestroyAudio.Instance.AnaSesCal(DontDestroyAudio.AudioType.Menu);
        SceneController.Instance.LoadScene("MainMenu", true);
    }
    public void RestartButonMethod()
    {
        DontDestroyAudio.Instance.AnaSesCal(DontDestroyAudio.AudioType.Game);
        SceneController.Instance.LoadScene("Game", true, true);
    }
    public void ButtonSoundEffectPlay()
    {
        DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.ButtonClickEffectSource);
    }
    IEnumerator TypeSentence(string sentence)
    {
        finalText.text = "";
        if (!IsNext)
        {
            foreach (char letter in sentence.ToCharArray())
            {
                if (IsNext)
                {
                    finalText.text = sentence;
                    yield return null;
                }
                else
                {
                    finalText.text += letter;
                    if (finalText.text == sentence)
                    {
                        SkipButton.gameObject.SetActive(false);
                        RestartButton.gameObject.SetActive(true);
                        MainMenuButton.gameObject.SetActive(true);
                    }
                    yield return new WaitForSeconds(.05f);
                }
            }
        }
    }
}
