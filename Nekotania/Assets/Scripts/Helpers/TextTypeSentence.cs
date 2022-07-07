using System.Collections;
using TMPro;
using UnityEngine;
using Lean.Localization;
using UnityEngine.SceneManagement;

public class TextTypeSentence : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalText;
    public LeanPhrase phrase;
    public LeanLocalization leanLocalization;
    private string text;
    private bool IsNext;
    public Transform SkipButton;
    void Start()
    {
        LeanLocalization.CurrentLanguages.TryGetValue(LeanLocalization.CurrentLanguage, out LeanLanguage language);

        int index = leanLocalization.Languages.IndexOf(language);
        text = phrase.Entries[index].Text;
        StartCoroutine(TypeSentence(text));
    }
    public void NextSceneButon()
    {
        IsNext = true;
        DontDestroyAudio.Instance.AnaSesCal(DontDestroyAudio.AudioType.Menu);
        SceneManager.LoadScene("MainMenu");
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
                        SkipButton.gameObject.SetActive(true);
                        //FunctionTimer.Create(() => { SceneManager.LoadScene("MainMenu"); }, 6f);
                    }
                    yield return new WaitForSeconds(.05f);
                }
            }
        }
    }
}
