using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Gui;

public class TutorialScript : MonoBehaviour
{
    public static TutorialScript Instance;
    public List<Button> allButtons = new List<Button>();
    public List<Transform> tutorialPanelTransformList = new List<Transform>();
    public TutorialState tutorialState;
    public bool isTutorialFinished;
    public enum TutorialState
    {
        Started,
        SatoPuaniniGoster,
        SatoGoster,
        KedininSatoyaGemlmesiniBekle,
        OyunSuresiUIGoster,
        DenizFeneriGoster,
        KedininDenizFenerineGelmesiniBekle,
        NufusUIGoster,
        UremeMerkeziGoster,
        KedininUremeMerkezineGelmesiniBekle,
        EnerjiBarlariniGoster,
        DinlenmeMerkeziGoster,
        YagmaMerkeziGoster,
        KedininYagmayaGitmesiniBekle,
        MerkezUpgradeUIGoster,
        UpgradeEt,
        TasimaGemisiniGoster,
        FinalGemiPaneliGoster,
        FinalGemiSatinAlButonGoster,
        FinalGemiSatinAlEvetGoster,
        FinalGemiParcaSurukle,
        FinalGemiExit,
        TutorialFinished
    }


    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        switch (tutorialState)
        {
            case TutorialState.Started:
                break;
            case TutorialState.SatoPuaniniGoster:
                UretimPuaniGoster();
                break;
            case TutorialState.SatoGoster:
                GameManager.Instance.ChangeState(GameState.Continue);
                UretimMerkeziGoster();
                break;
            case TutorialState.KedininSatoyaGemlmesiniBekle:
               // Debug.Log("Bekle");
                break;
            case TutorialState.OyunSuresiUIGoster:
                InputScript.Instance.layerMask = LayerMask.GetMask("UI");
                OyunSuresiGoster();
                break;
            case TutorialState.DenizFeneriGoster:
                GameManager.Instance.ChangeState(GameState.Continue);
                DenizFeneriGoster();
                break;
            case TutorialState.KedininDenizFenerineGelmesiniBekle:
                //Debug.Log("DenizFenerinegelmesibekleniyor");
                break;
            case TutorialState.NufusUIGoster:
                InputScript.Instance.layerMask = LayerMask.GetMask("UI");
                NufusMiktariGoster();
                break;
            case TutorialState.UremeMerkeziGoster:
                GameManager.Instance.ChangeState(GameState.Continue);
                UremeMerkeziGoster();
                break;
            case TutorialState.KedininUremeMerkezineGelmesiniBekle:
                //Debug.Log("KedininUremeMerkezineGelmesiniBekle");
                break;
            case TutorialState.EnerjiBarlariniGoster:
                InputScript.Instance.layerMask = LayerMask.GetMask("UI");
                KediEnerjiMiktariGoster();
                break;
            case TutorialState.DinlenmeMerkeziGoster:
                GameManager.Instance.ChangeState(GameState.Continue);
                DinlenmeyeKediGonder();
                break;
            case TutorialState.YagmaMerkeziGoster:
                YagmaMerkeziGoster();
                break;
            case TutorialState.KedininYagmayaGitmesiniBekle:
                //Debug.Log("yagma bekle");
                break;
            case TutorialState.MerkezUpgradeUIGoster:
                InputScript.Instance.layerMask = LayerMask.GetMask("UI");
                MerkezUpgradeGoster();
                break;
            case TutorialState.UpgradeEt:
                UpgradeEt();
                break;
            case TutorialState.TasimaGemisiniGoster:
                GameManager.Instance.ChangeState(GameState.Continue);
                TasimaSandaliniGoster();
                break;
            case TutorialState.FinalGemiPaneliGoster:
                FinalGemiPaneliGoster();
                break;
            case TutorialState.FinalGemiSatinAlButonGoster:
                FinalGemiSatinAlButtonGoster();
                break;
            case TutorialState.FinalGemiSatinAlEvetGoster:
                FinalGemiSatinAlEvetButtonGoster();
                break;
            case TutorialState.FinalGemiParcaSurukle:
                FinalGemiParceSurukle();
                break;
            case TutorialState.FinalGemiExit:
                FinalGemiExit();
                break;
            case TutorialState.TutorialFinished:
                isTutorialFinished = true;
                this.enabled = false;
                break;
        }
    }
    private void UretimPuaniGoster()
    {
        allButtons.ForEach(b => b.interactable = false);
        //Yanlışlıkla tutorialı açık unutursa hızlıca menuye donebilmesi için dönüş butonlara tıklayabilir.
        allButtons[6].interactable = true;
        allButtons[10].interactable = true;

        InputScript.Instance.layerMask = LayerMask.GetMask("UI");
        GameManager.Instance.ChangeState(GameState.Pause);
        tutorialPanelTransformList[0].gameObject.SetActive(true);
        tutorialPanelTransformList[0].Find("GecisButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            tutorialState = TutorialState.SatoGoster;
        });
    }
    private void UretimMerkeziGoster()
    {
        InputScript.Instance.layerMask = LayerMask.GetMask("Sato");
        tutorialPanelTransformList[0].gameObject.SetActive(false);
        tutorialPanelTransformList[1].gameObject.SetActive(true);

        if (CatTransportManager.Instance.KaleyeGidenKediSayisi() == 1)
        {
            InputScript.Instance.layerMask = LayerMask.GetMask("Nothing");
            tutorialPanelTransformList[1].gameObject.SetActive(false);
            FunctionTimer.Create(() => { tutorialState = TutorialState.OyunSuresiUIGoster; }, 3.5f);
            tutorialState = TutorialState.KedininSatoyaGemlmesiniBekle;
        }
    }
    private void OyunSuresiGoster()
    {
        GameManager.Instance.ChangeState(GameState.Pause);
        tutorialPanelTransformList[1].gameObject.SetActive(false);
        tutorialPanelTransformList[2].gameObject.SetActive(true);
        tutorialPanelTransformList[2].Find("GecisButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            tutorialState = TutorialState.DenizFeneriGoster;
        });
    }
    private void DenizFeneriGoster()
    {
        InputScript.Instance.layerMask = LayerMask.GetMask("Gol");
        tutorialPanelTransformList[2].gameObject.SetActive(false);
        tutorialPanelTransformList[3].gameObject.SetActive(true);

        if (CatTransportManager.Instance.BaligaGidenKediSayisi() == 1)
        {
            InputScript.Instance.layerMask = LayerMask.GetMask("Nothing");
            tutorialPanelTransformList[3].gameObject.SetActive(false);
            FunctionTimer.Create(() => { tutorialState = TutorialState.NufusUIGoster; }, 3.5f);
            tutorialState = TutorialState.KedininDenizFenerineGelmesiniBekle;
        }
    }
    private void NufusMiktariGoster()
    {
        GameManager.Instance.ChangeState(GameState.Pause);
        tutorialPanelTransformList[2].gameObject.SetActive(false);
        tutorialPanelTransformList[3].gameObject.SetActive(false);
        tutorialPanelTransformList[4].gameObject.SetActive(true);
        tutorialPanelTransformList[4].Find("GecisButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            tutorialState = TutorialState.UremeMerkeziGoster;
        });
    }
    private void UremeMerkeziGoster()
    {
        InputScript.Instance.layerMask = LayerMask.GetMask("Ureme");
        tutorialPanelTransformList[4].gameObject.SetActive(false);
        tutorialPanelTransformList[5].gameObject.SetActive(true);

        if (CatTransportManager.Instance.UremeyeGidenKediSayisi() == 2)
        {
            InputScript.Instance.layerMask = LayerMask.GetMask("Nothing");
            tutorialPanelTransformList[5].gameObject.SetActive(false);
            FunctionTimer.Create(() => { tutorialState = TutorialState.EnerjiBarlariniGoster; }, 3.5f);
            tutorialState = TutorialState.KedininUremeMerkezineGelmesiniBekle;
        }
    }
    private void KediEnerjiMiktariGoster()
    {
        GameManager.Instance.ChangeState(GameState.Pause);
        tutorialPanelTransformList[5].gameObject.SetActive(false);
        tutorialPanelTransformList[6].gameObject.SetActive(true);
        tutorialPanelTransformList[6].Find("GecisButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            tutorialState = TutorialState.DinlenmeMerkeziGoster;
        });
    }
    private void DinlenmeyeKediGonder()
    {
        InputScript.Instance.layerMask = LayerMask.GetMask("Sato");
        tutorialPanelTransformList[6].gameObject.SetActive(false);
        tutorialPanelTransformList[7].gameObject.SetActive(true);

        if (Castle.Instance.InsideCatList.Count == 0)
            tutorialState = TutorialState.YagmaMerkeziGoster;
    }
    private void YagmaMerkeziGoster()
    {
        InputScript.Instance.layerMask = LayerMask.GetMask("Mezarlik");
        tutorialPanelTransformList[7].gameObject.SetActive(false);
        tutorialPanelTransformList[15].gameObject.SetActive(true);


        if (CatTransportManager.Instance.YagmayaGidenKediSayisi() == 1)
        {
            InputScript.Instance.layerMask = LayerMask.GetMask("Nothing");
            tutorialPanelTransformList[15].gameObject.SetActive(false);
            FunctionTimer.Create(() => { tutorialState = TutorialState.MerkezUpgradeUIGoster; }, 5f);
            tutorialState = TutorialState.KedininYagmayaGitmesiniBekle;
        }

    }
    private void MerkezUpgradeGoster()
    {
        GameManager.Instance.ChangeState(GameState.Pause);
        //tutorialPanelTransformList[7].gameObject.SetActive(false);
        tutorialPanelTransformList[8].gameObject.SetActive(true);
        allButtons[1].interactable = true;
        allButtons[1].onClick.AddListener(() =>
        {
            tutorialState = TutorialState.UpgradeEt;
        });
    }
    private void UpgradeEt()
    {
        tutorialPanelTransformList[8].gameObject.SetActive(false);
        UIScript.Instance.UILevelPanel.TutorialPatiTransform.gameObject.SetActive(true);
        if(Rest.Instance.MerkezSeviyesi == 2)
        {
            UIScript.Instance.UILevelPanel.TutorialPatiTransform.gameObject.SetActive(false);
            UIScript.Instance.UILevelPanel.PanelTurnOff();
            tutorialState = TutorialState.TasimaGemisiniGoster;
            allButtons[1].interactable = false;
        }
    }
    private void TasimaSandaliniGoster()
    {
        tutorialPanelTransformList[8].gameObject.SetActive(false);
        tutorialPanelTransformList[9].gameObject.SetActive(true);
        GameManager.Instance.ChangeState(GameState.Pause);
        allButtons.ForEach(b => b.interactable = false);
        allButtons[5].interactable = true;
        allButtons[5].onClick.AddListener(() =>
        {
            tutorialState = TutorialState.FinalGemiPaneliGoster;
        });
    }
    private void FinalGemiPaneliGoster()
    {
        tutorialPanelTransformList[9].gameObject.SetActive(false);
        tutorialPanelTransformList[10].gameObject.SetActive(true);

        UIScript.Instance.FinalGemiExitPanelButton.GetComponent<LeanButton>().interactable = false;
        UIScript.Instance.FinalGemiBuyButton.GetComponent<LeanButton>().interactable = false;
        allButtons.ForEach(b => b.interactable = false);

        if (PuzzleManager.Instance.PuzzleHandler != null)
        {
            if(PuzzleManager.Instance.PuzzleHandler.PuzzleIndex == 10)
            {
                tutorialState = TutorialState.FinalGemiSatinAlButonGoster;
            }
        }

    }
    private void FinalGemiSatinAlButtonGoster()
    {
        tutorialPanelTransformList[10].gameObject.SetActive(false);
        tutorialPanelTransformList[11].gameObject.SetActive(true);

        UIScript.Instance.FinalGemiExitPanelButton.GetComponent<LeanButton>().interactable = false;
        UIScript.Instance.FinalGemiBuyButton.GetComponent<LeanButton>().interactable = true;

        UIScript.Instance.FinalGemiBuyButton.GetComponent<LeanButton>().OnClick.AddListener(() =>
        {
            tutorialState = TutorialState.FinalGemiSatinAlEvetGoster;
        });

    }
    private void FinalGemiSatinAlEvetButtonGoster()
    {
        tutorialPanelTransformList[11].gameObject.SetActive(false);
        tutorialPanelTransformList[12].gameObject.SetActive(true);

        UIScript.Instance.FinalGemiExitPanelButton.GetComponent<LeanButton>().interactable = false;
        UIScript.Instance.FinalGemiBuyButton.GetComponent<LeanButton>().interactable = false;
        UIScript.Instance.FinalGemiBuyNoButton.GetComponent<LeanButton>().interactable = false;

        UIScript.Instance.FinalGemiBuyYesButton.GetComponent<LeanButton>().OnClick.AddListener(() =>
        {
            tutorialState = TutorialState.FinalGemiParcaSurukle;
        });

    }
    private void FinalGemiParceSurukle()
    {
        tutorialPanelTransformList[12].gameObject.SetActive(false);
        tutorialPanelTransformList[13].gameObject.SetActive(true);

        UIScript.Instance.FinalGemiExitPanelButton.GetComponent<LeanButton>().interactable = false;
        UIScript.Instance.FinalGemiBuyButton.GetComponent<LeanButton>().interactable = false;
        UIScript.Instance.FinalGemiBuyNoButton.GetComponent<LeanButton>().interactable = true;

        if(PuzzleManager.Instance.PuzzleList.Count == 1)
        {
            tutorialState = TutorialState.FinalGemiExit;
        }

    }
    private void FinalGemiExit()
    {
        tutorialPanelTransformList[13].gameObject.SetActive(false);
        tutorialPanelTransformList[14].gameObject.SetActive(true);

        UIScript.Instance.FinalGemiExitPanelButton.GetComponent<LeanButton>().interactable = true;
        UIScript.Instance.FinalGemiBuyButton.GetComponent<LeanButton>().interactable = false;
        UIScript.Instance.FinalGemiBuyNoButton.GetComponent<LeanButton>().interactable = true;

        //DontDestroyAudio.Instance.AnaSesCal(DontDestroyAudio.AudioType.Game);

        UIScript.Instance.FinalGemiExitPanelButton.GetComponent<LeanButton>().OnClick.AddListener(() =>
        {
            TutorialFinish();
        });

    }
    private void TutorialFinish()
    {
        tutorialPanelTransformList[14].gameObject.SetActive(false);
        UIScript.Instance.FinalGemiBuyButton.GetComponent<LeanButton>().interactable = true;
        GameManager.Instance.ChangeState(GameState.Continue);
        tutorialState = TutorialState.TutorialFinished;
        InputScript.Instance.layerMask = LayerMask.GetMask("Dinlendirici", "Sato", "Ureme", "Gol", "Mezarlik");
        allButtons.ForEach(b => b.interactable = true);
    }
}