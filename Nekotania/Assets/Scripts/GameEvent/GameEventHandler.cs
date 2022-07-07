using Lean.Gui;
using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class GameEventHandler : MonoBehaviour
{
    public static GameEventHandler EventCreate(OlaylarSO olaylarSO)
    {
        Transform EventObject = Instantiate(GameEventManager.Instance.EventPanelPrefab);
        EventObject.SetParent(BuildManager.Instance.UIPanelSafeAreaTransform, false);
        EventObject.GetComponent<LeanWindow>().TurnOn();
        GameEventHandler @event = EventObject.GetComponent<GameEventHandler>();
        GameEventManager.Instance.GameEventHandler = @event;
        @event.MyEventOlaylarSO = olaylarSO;

        @event.EventLocalizedInit(olaylarSO);
        @event.AnswersCreate(olaylarSO);
        UIScript.Instance.ArkaPlanKarartmaLeanWindow.TurnOn();
        UIScript.Instance.EngellemeLeanWindow.TurnOn();

        return @event;
    }
    public OlaylarSO MyEventOlaylarSO;
    public Transform cevapSeceneklerPanelTransform;
    public Transform OlayCevapButonPrefab;
    [SerializeField] private TextMeshProUGUI olayBasligiText;
    [SerializeField] private TextMeshProUGUI olayAciklamasiText;
    [SerializeField] private List<GameObject> eventAnswersList = new List<GameObject>();
    public MerkezlerBase.ProductionType ProductionType1;
    public MerkezlerBase.ProductionType ProductionType2;
    public int? ProductionAmount1;
    public int? ProductionAmount2;
    public bool ArttirdiMi;
    private void EventLocalizedInit(OlaylarSO olay)
    {
        olayBasligiText.transform.GetComponent<LeanLocalizedTextMeshPro>().TranslationName = olay.olayBasligiPhrase.name;
        olayAciklamasiText.transform.GetComponent<LeanLocalizedTextMeshPro>().TranslationName = olay.olayAciklamasiPhrase.name;
    }
    public void ContinueEvent(int kacinciOlaySecildi, string tokenName = null, string value = null, string tokenName1 = null, string value1 = null, bool isCreateButton = true)
    {
        DeleteEventAnswers();
        olayAciklamasiText.transform.GetComponent<LeanLocalizedTextMeshPro>().TranslationName = MyEventOlaylarSO.olaySonucAciklamaListesi[kacinciOlaySecildi].name;

        LeanLocalization.SetToken(tokenName, value);
        LeanLocalization.SetToken(tokenName1, value1);

        UretimMiktariFirlat(ProductionType1, ProductionAmount1, ArttirdiMi);
        FunctionTimer.Create(() => { UretimMiktariFirlat(ProductionType2, ProductionAmount2, ArttirdiMi); }, .5f);
        

        if (isCreateButton)
        {
            Transform Buton = Instantiate(OlayCevapButonPrefab);
            Buton.SetParent(cevapSeceneklerPanelTransform, false);
            Buton.GetComponentInChildren<LeanLocalizedTextMeshPro>().TranslationName = "DevamText";

            Buton.GetComponent<LeanButton>().OnClick.AddListener(() =>
            {
                CycleManager.Instance.EventSelected();
                
                EventKill();
            });
        }
    }
    private void AnswersCreate(OlaylarSO olay)
    {
        for (int i = 0; i < olay.olayCevaplariPhraseListesi.Count; i++)
        {
            Transform Buton = Instantiate(OlayCevapButonPrefab);
            Buton.SetParent(cevapSeceneklerPanelTransform, false);

            Buton.GetComponentInChildren<LeanLocalizedTextMeshPro>().TranslationName
                = olay.olayCevaplariPhraseListesi[i].name;
            eventAnswersList.Add(Buton.gameObject);
        }

        eventAnswersList.ForEach(e => e.GetComponent<LeanButton>().OnClick.AddListener(() =>
         { GameEventManager.Instance.Answer = Answer(eventAnswersList.IndexOf(e)); }));
    }
    private OlaylarSO.EventAnswers Answer(int index)
    {
        if (index == 0)
            return OlaylarSO.EventAnswers.Answer1;
        else if (index == 1)
            return OlaylarSO.EventAnswers.Answer2;
        else if (index == 2)
            return OlaylarSO.EventAnswers.Answer3;
        else
            return OlaylarSO.EventAnswers.Empty;
    }
    private void DeleteEventAnswers()
    {
        for (int i = eventAnswersList.Count - 1; i >= 0; i--)
            Destroy(eventAnswersList[i]);
        eventAnswersList.Clear();
    }
    public void EventKill()
    {
        UIScript.Instance.ArkaPlanKarartmaLeanWindow.TurnOff();
        UIScript.Instance.EngellemeLeanWindow.TurnOff();
        GetComponent<LeanWindow>().TurnOff();
        GameEventManager.Instance.GameEventHandler = null;
        if (!DontDestroyAudio.Instance.GameAudioSource.isPlaying)
            DontDestroyAudio.Instance.SesDevamEt();
        Destroy(gameObject, .2f);
    }
    private void UretimMiktariFirlat(MerkezlerBase.ProductionType productionType, int? uretimMiktari, bool arttir)
    {
        if (productionType == MerkezlerBase.ProductionType.Empty)
            return;

        GameObject go = Instantiate(BuildManager.Instance.uretimMiktariPrefabObject);
        go.transform.localScale = Vector3.one * .8f;
        go.transform.position = BuildManager.Instance.KazanilanProdTransform.position;
        go.GetComponent<UretimFirlatmaObje>().UretimUIOlustur(productionType, uretimMiktari, arttir);
    }
}
