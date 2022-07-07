using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Military : MerkezlerBase
{
    public static Military Instance;
    [SerializeField] private GameObject closedObje;
    public int KediArtigiSatoPuani { get; set; }
    public int KediArtigiYiyecekPuani { get; set; }

    public Military()
    {
        MerkezSeviyesi = 1;
        KediArtigiSatoPuani = 10;
        KediArtigiYiyecekPuani = 1;
        InsideCatList = new List<Cat>();
        BarObjeList = new List<GameObject>();
        MyMerkezType = MerkezType.Military;
        MyProductionType = ProductionType.Empty;
    }

    void Awake()
    {
        Instance = this;
        UISetup();
    }
    void Update()
    {
        MerkezKapamaGuncelleme(closedObje, MyMerkezType);
        PrintUI();
    }
    public override void BaseLevelUp()
    {
        BuildManager.Instance.ToplamSatoPuani -= GameBalanceValues.BaseForLevelUpAmount(MerkezSeviyesi);
        KediArtigiSatoPuani += GameBalanceValues.KediArtigiArtisMiktari(MerkezSeviyesi);
        KediArtigiYiyecekPuani += GameBalanceValues.KediArtigiYiyecekArtisMiktari(MerkezSeviyesi);
        DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.UpgradeEffectSource);
        MerkezSeviyesi++;
    }
    public void KediArtigiEkle()
    {
        BuildManager.Instance.ToplamSatoPuani += KediArtigiSatoPuani;
        BuildManager.Instance.ToplamYiyecekMiktari += KediArtigiYiyecekPuani;

        UretimMiktariFirlat(ProductionType.CastlePoint, KediArtigiSatoPuani);
        FunctionTimer.Create(() => { UretimMiktariFirlat(ProductionType.Fish, KediArtigiYiyecekPuani); }, .5f);
    }

    public void SetSaveObject(SaveObject saveObject)
    {
        MerkezSeviyesi = saveObject.MerkezSeviyesi;
        KediArtigiSatoPuani = saveObject.KediArtigiSatoPuani;
        KediArtigiYiyecekPuani = saveObject.KediArtigiYiyecekPuani;
        IsClosed = saveObject.IsClosed;
    }
    public SaveObject GetSaveObject()
    {
        return new SaveObject
        {
            MerkezSeviyesi = MerkezSeviyesi,
            KediArtigiSatoPuani = KediArtigiSatoPuani,
            KediArtigiYiyecekPuani = KediArtigiYiyecekPuani,
            IsClosed = IsClosed,
        };
    }

    [System.Serializable]
    public class SaveObject
    {
        public int MerkezSeviyesi;
        public int KediArtigiSatoPuani;
        public int KediArtigiYiyecekPuani;
        public bool IsClosed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Cat>(out Cat cat))
        {
            InsideCatList.Add(cat);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (InsideCatList.Count != 0)
        {
            if (collision.TryGetComponent<Cat>(out Cat cat))
            {
                InsideCatList.Remove(cat);
            }
        }
    }
}
