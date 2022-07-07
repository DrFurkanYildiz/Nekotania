using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : MerkezlerBase
{
    public static Rest Instance;
    public Rest()
    {
        MerkezSeviyesi = 1;
        InsideCatList = new List<Cat>();
        BarObjeList = new List<GameObject>();
        MyMerkezType = MerkezType.Rest;
        MyProductionType = ProductionType.Empty;
    }
    void Awake()
    {
        Instance = this;
        UISetup();
    }

    void Update()
    {
        LayoutUpdate(false);
        PrintUI();
    }

    public override void BaseLevelUp()
    {
        BuildManager.Instance.ToplamSatoPuani -= GameBalanceValues.BaseForLevelUpAmount(MerkezSeviyesi);
        BuildManager.Instance.NufusKapasitesi += GameBalanceValues.BaseCapacityIncreaseAmount(MyMerkezType, MerkezSeviyesi);
        BuildManager.Instance.DayaniklilikMiktari += GameBalanceValues.StaminaIncreaseAmount(MerkezSeviyesi);
        DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.UpgradeEffectSource);
        MerkezSeviyesi++;
    }

    public void SetSaveObject(SaveObject saveObject)
    {
        MerkezSeviyesi = saveObject.MerkezSeviyesi;
    }
    public SaveObject GetSaveObject()
    {
        return new SaveObject
        {
            MerkezSeviyesi = MerkezSeviyesi
        };
    }

    [System.Serializable]
    public class SaveObject
    {
        public int MerkezSeviyesi;
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
