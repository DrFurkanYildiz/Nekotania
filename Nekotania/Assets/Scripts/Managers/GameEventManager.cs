using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance;

    public GameEventHandler GameEventHandler = null;
    public OlaylarSO.EventAnswers Answer;

    public Transform EventPanelPrefab;

    [SerializeField] private List<OlaylarSO> araOlaylarSOList = new List<OlaylarSO>();
    [SerializeField] private List<OlaylarSO> anaOlaylarSOList = new List<OlaylarSO>();
    public OlaylarSO TutorialEvent;


    private Queue<OlaylarSO> OlayTekrariKuyrugu = new Queue<OlaylarSO>();
    [SerializeField] private List<OlaylarSO> araOlaylar = new List<OlaylarSO>();

    private const int EKSİLTME_MİKTARİ = 20;
    private const int ARTTİRMA_MİKTARİ = 20;
    private const int DAYAN_DİN_MİKTARİ = 20;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if(GameEventHandler != null)
        {
            YeniIslem();
        }
    }

    private void YeniIslem()
    {
        switch (GameEventHandler.MyEventOlaylarSO.MyEventName)
        {
            case OlaylarSO.EventName.Tutorial:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        GameEventHandler.EventKill();
                        Answer = OlaylarSO.EventAnswers.Empty;

                        TutorialScript.Instance.tutorialState = TutorialScript.TutorialState.SatoPuaniniGoster;

                        GameManager.Instance.ChangeState(GameState.Continue);
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay1:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1);
                        OlayIsleyiciScript.Instance.MerkeziKapat(MerkezlerBase.MerkezType.Military);
                        break;
                    case OlaylarSO.EventAnswers.Answer2:
                        CevapIsleMetot(2);
                        OlayIsleyiciScript.Instance.DayaniklilikArttir(DAYAN_DİN_MİKTARİ);
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay2:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1);
                        OlayIsleyiciScript.Instance.DinlenmeMiktariniArttir(DAYAN_DİN_MİKTARİ);
                        break;
                    case OlaylarSO.EventAnswers.Answer2:
                        CevapIsleMetot(2);
                        OlayIsleyiciScript.Instance.DinlenmeMiktariniAzalt(DAYAN_DİN_MİKTARİ);
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay3:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1, "kedi", OlayIsleyiciScript.Instance.KediOldur(EKSİLTME_MİKTARİ));
                        break;
                    case OlaylarSO.EventAnswers.Answer2:
                        CevapIsleMetot(2, "balik", OlayIsleyiciScript.Instance.BalikEksilt(EKSİLTME_MİKTARİ));
                        break;
                    case OlaylarSO.EventAnswers.Answer3:
                        CevapIsleMetot(3, "puan", OlayIsleyiciScript.Instance.PuanEksilt(EKSİLTME_MİKTARİ));
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay4:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1);
                        OlayIsleyiciScript.Instance.MerkeziKapat(MerkezlerBase.MerkezType.LightHouse);
                        break;
                    case OlaylarSO.EventAnswers.Answer2:
                        CevapIsleMetot(2);
                        OlayIsleyiciScript.Instance.DayaniklilikAzalt(DAYAN_DİN_MİKTARİ);
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay5:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1, "puan", OlayIsleyiciScript.Instance.PuanArttir(ARTTİRMA_MİKTARİ), null, null, true, true);
                        break;
                    case OlaylarSO.EventAnswers.Answer2:
                        CevapIsleMetot(2, "puan", OlayIsleyiciScript.Instance.PuanArttir(ARTTİRMA_MİKTARİ), null, null, true, true);
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay6:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1);
                        OlayIsleyiciScript.Instance.PuzzleParcasiHediyeEt();
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay7:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1, "balik", OlayIsleyiciScript.Instance.BalikEksilt(EKSİLTME_MİKTARİ));
                        break;
                    case OlaylarSO.EventAnswers.Answer2:
                        CevapIsleMetot(2);
                        OlayIsleyiciScript.Instance.DinlenmeMiktariniArttir(ARTTİRMA_MİKTARİ);
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay8:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1, "puan", OlayIsleyiciScript.Instance.PuanArttir(ARTTİRMA_MİKTARİ), null, null, true, true);
                        break;
                    case OlaylarSO.EventAnswers.Answer2:
                        CevapIsleMetot(2);
                        OlayIsleyiciScript.Instance.MerkeziKapat(MerkezlerBase.MerkezType.Castle);
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay9:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1, "puan", OlayIsleyiciScript.Instance.PuanEksilt(EKSİLTME_MİKTARİ));
                        CevapIsleMetot(1, null, null, "balik", OlayIsleyiciScript.Instance.BalikArttir(ARTTİRMA_MİKTARİ), false, true);
                        break;
                    case OlaylarSO.EventAnswers.Answer2:
                        CevapIsleMetot(2);
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay10:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1, "kedi", OlayIsleyiciScript.Instance.KediOldur(EKSİLTME_MİKTARİ));
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay11:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1);
                        OlayIsleyiciScript.Instance.MerkeziKapat(MerkezlerBase.MerkezType.Mating);
                        break;
                    case OlaylarSO.EventAnswers.Answer2:
                        CevapIsleMetot(2, "kedi", OlayIsleyiciScript.Instance.KediOldur(EKSİLTME_MİKTARİ));
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay12:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        OlayIsleyiciScript.Instance.KarYagdir();
                        CevapIsleMetot(1, "balik", OlayIsleyiciScript.Instance.BalikEksilt(EKSİLTME_MİKTARİ));
                        break;
                    case OlaylarSO.EventAnswers.Answer2:
                        OlayIsleyiciScript.Instance.KarYagdir();
                        CevapIsleMetot(2);
                        OlayIsleyiciScript.Instance.DayaniklilikAzalt(DAYAN_DİN_MİKTARİ);
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay13:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1, "kedi", OlayIsleyiciScript.Instance.KediOldur(EKSİLTME_MİKTARİ));
                        break;
                    case OlaylarSO.EventAnswers.Answer2:
                        CevapIsleMetot(2, "puan", OlayIsleyiciScript.Instance.PuanEksilt(EKSİLTME_MİKTARİ));
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay14:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1, "kedi", OlayIsleyiciScript.Instance.KediEkle(ARTTİRMA_MİKTARİ), null, null, true, true);
                        CevapIsleMetot(1, null, null, "puan", OlayIsleyiciScript.Instance.PuanArttir(ARTTİRMA_MİKTARİ), false, true);
                        break;
                    case OlaylarSO.EventAnswers.Answer2:
                        CevapIsleMetot(2, "kedi", OlayIsleyiciScript.Instance.KediEkle(ARTTİRMA_MİKTARİ), null, null, true, true);
                        CevapIsleMetot(2, null, null, "balik", OlayIsleyiciScript.Instance.BalikArttir(ARTTİRMA_MİKTARİ), false, true);
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay15:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1);
                        break;
                    case OlaylarSO.EventAnswers.Answer2:
                        CevapIsleMetot(2, "kedi", OlayIsleyiciScript.Instance.KediOldur(EKSİLTME_MİKTARİ));
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay16:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1, "kedi", OlayIsleyiciScript.Instance.KediOldur(EKSİLTME_MİKTARİ));
                        break;
                    case OlaylarSO.EventAnswers.Answer2:
                        CevapIsleMetot(2, "puan", OlayIsleyiciScript.Instance.PuanArttir(ARTTİRMA_MİKTARİ), null, null, true, true);
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay17:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1);
                        OlayIsleyiciScript.Instance.DayaniklilikAzalt(EKSİLTME_MİKTARİ);
                        break;
                    case OlaylarSO.EventAnswers.Answer2:
                        CevapIsleMetot(2, "kedi", OlayIsleyiciScript.Instance.KediOldur(EKSİLTME_MİKTARİ));
                        break;
                }
                break;
                // Ana Olaylar Başlangıç.
            case OlaylarSO.EventName.Olay18:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1, "puan", OlayIsleyiciScript.Instance.AnaOlayPuanArttir(75), null, null, true, true);
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay19:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1, "puan", OlayIsleyiciScript.Instance.AnaOlayPuanArttir(250), null, null, true, true);
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay20:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1, "puan", OlayIsleyiciScript.Instance.AnaOlayPuanArttir(500), null, null, true, true);
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay21:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1, "puan", OlayIsleyiciScript.Instance.AnaOlayPuanArttir(750), null, null, true, true);
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay22:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1, "puan", OlayIsleyiciScript.Instance.AnaOlayPuanArttir(1000), null, null, true, true);
                        break;
                }
                break;
            case OlaylarSO.EventName.Olay23:
                switch (Answer)
                {
                    case OlaylarSO.EventAnswers.Empty:
                        break;
                    case OlaylarSO.EventAnswers.Answer1:
                        CevapIsleMetot(1, "puan", OlayIsleyiciScript.Instance.AnaOlayPuanArttir(1250), null, null, true, true);
                        break;
                }
                break;
        }
    }


    public void CevapIsleMetot(int cevapIndex, string tokenName = null, int? value = null, string tokenName1 = null, int? value1 = null, bool isCreateButton = true, bool arttir = false)
    {
        if (GameEventHandler == null)
            return;

        GameEventHandler.ProductionType1 = SetProductionType(tokenName);
        GameEventHandler.ProductionType2 = SetProductionType(tokenName1);
        GameEventHandler.ProductionAmount1 = value;
        GameEventHandler.ProductionAmount2 = value1;
        GameEventHandler.ArttirdiMi = arttir;

        GameEventHandler.ContinueEvent(cevapIndex - 1, tokenName, value.ToString(), tokenName1, value1.ToString(), isCreateButton);
        Answer = OlaylarSO.EventAnswers.Empty;
    }

    private MerkezlerBase.ProductionType SetProductionType(string tokenName)
    {
        switch (tokenName)
        {
            case "kedi":
                return MerkezlerBase.ProductionType.Cat;
            case "balik":
                return MerkezlerBase.ProductionType.Fish;
            case "puan":
                return MerkezlerBase.ProductionType.CastlePoint;
            default:
                return MerkezlerBase.ProductionType.Empty;
        }
    }


    public OlaylarSO YeniOlay()
    {
        if (CycleManager.Instance.DayTime > 23)
            return OlayTekrariKuyrugu.Dequeue();


        foreach (OlaylarSO anaOlay in anaOlaylarSOList)
        {
            if (CycleManager.Instance.DayTime == anaOlay.KacinciGunAnaOlayi)
                return anaOlay;
        }
        
        OlaylarSO newEvent = araOlaylarSOList[Random.Range(0, araOlaylarSOList.Count)];
        if (!araOlaylar.Contains(newEvent))
        {
            araOlaylar.Add(newEvent);
            OlayTekrariKuyrugu.Enqueue(newEvent);
            return newEvent;
        }
        else
            return null;
    }


    public void SetSaveObject(SaveObject saveObject)
    {
        foreach (OlaylarSO olay in saveObject.OlayTekrariKuyrugu)
        {
            OlayTekrariKuyrugu.Enqueue(olay);
        }

        araOlaylar = saveObject.AraOlaylar;
    }
    public SaveObject GetSaveObject()
    {
        return new SaveObject
        {
            OlayTekrariKuyrugu = OlayTekrariKuyrugu.ToList(),
            AraOlaylar = araOlaylar
        };
    }

    [System.Serializable]
    public class SaveObject
    {
        public List<OlaylarSO> OlayTekrariKuyrugu = new List<OlaylarSO>();
        public List<OlaylarSO> AraOlaylar = new List<OlaylarSO>();
    }

}

/*

#CE583C    -----
#59CE3C    +++++

*/