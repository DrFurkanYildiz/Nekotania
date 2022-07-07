using Lean.Gui;
using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;


    private int toplamSatoPuani;
    private int toplamYiyecekMiktari;
    private int satoPuaniKapasitesi;
    private int nufusKapasitesi;
    private int yiyecekKapasitesi;

    private int dinlenmeMiktari;
    private int dayaniklilikMiktari;



    public int DayaniklilikMiktari
    {
        get { return dayaniklilikMiktari; }
        set { dayaniklilikMiktari = value; }
    }
    public int DinlenmeMiktari
    {
        get { return dinlenmeMiktari; }
        set { dinlenmeMiktari = value; }
    }
    
    public int YiyecekKapasitesi
    {
        get { return yiyecekKapasitesi; }
        set { yiyecekKapasitesi = value; }
    }
    public int NufusKapasitesi
    {
        get { return nufusKapasitesi; }
        set { nufusKapasitesi = value; }
    }
    public int SatoPuaniKapasitesi
    {
        get { return satoPuaniKapasitesi; }
        set { satoPuaniKapasitesi = value; }
    }

    public int ToplamYiyecekMiktari
    {
        get { return toplamYiyecekMiktari; }
        set { toplamYiyecekMiktari = value.FixedValue(0, yiyecekKapasitesi); }
    }
    public int ToplamSatoPuani
    {
        get { return toplamSatoPuani; }
        set { toplamSatoPuani = value.FixedValue(0, satoPuaniKapasitesi); }
    }


    public Transform catObjectPrefab;
    public Transform kedilerParentTransform;
    public GameObject catEnerjiBarPrefab;
    public GameObject hayaletPrefabObject;
    public GameObject kilicPrefabObject;
    public GameObject uretimMiktariPrefabObject;
    public Transform UIPanelSafeAreaTransform;
    public Transform LevelPanelPrefab;

    public Transform KazanilanProdTransform;

    public List<Cat> allCatList = new List<Cat>();


    private void Awake()
    {
        Init();
        Instance = this;
    }

    private void Update()
    {
        GameBalanceValues.SatoPuanKapasitesiniGuncelle();
    }
    private void Init()
    {
        toplamSatoPuani = 120;
        toplamYiyecekMiktari = 20;

        nufusKapasitesi = 15;
        yiyecekKapasitesi = 50;
        dinlenmeMiktari = 60;
        dayaniklilikMiktari = 50;

    }


    public Transform EnerjiBarTransformBul(Cat.CatArea catArea)
    {
        Transform barTransform = null;
        switch (catArea)
        {
            case Cat.CatArea.Dinlenme:
                barTransform = Rest.Instance.EnerjiGostergePaneliTransform;
                break;
            case Cat.CatArea.Sato:
                barTransform = Castle.Instance.EnerjiGostergePaneliTransform;
                break;
            case Cat.CatArea.Ureme:
                barTransform = Mating.Instance.EnerjiGostergePaneliTransform;
                break;
            case Cat.CatArea.Gol:
                barTransform = LightHouse.Instance.EnerjiGostergePaneliTransform;
                break;
            case Cat.CatArea.Asker:
                barTransform = Military.Instance.EnerjiGostergePaneliTransform;
                break;
        }
        return barTransform;
    }
    
    public void StartedCatGenerate(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            YeniKediOlustur();
        }
    }
    public void YeniKediOlustur()
    {
        Cat go = Cat.CatCreate(catObjectPrefab, RasgeleKonumBelirle(Rest.Instance.PolygonCollider), 100f, catEnerjiBarPrefab, Rest.Instance.EnerjiGostergePaneliTransform, kedilerParentTransform);
        allCatList.Add(go);
    }
    private Vector3 RasgeleKonumBelirle(PolygonCollider2D polygon)
    {
        float x = Random.Range(polygon.bounds.min.x, polygon.bounds.max.x);
        float y = Random.Range(polygon.bounds.min.y, polygon.bounds.max.y);
        return new Vector2(x, y);
    }



    public void KillCats(int killedAmount)
    {
        if (killedAmount <= 0)
            return;

        int amount = TotalCatAmount() >= killedAmount ? killedAmount : TotalCatAmount();

        for (int i = amount - 1; i >= 0; i--)
            NonMilitaryAllCatList()[i].KediOldur(false);
    }
    public int TotalCatAmount()
    {
        return allCatList.Count;
    }
    public List<Cat> NonMilitaryAllCatList()
    {
        return allCatList.Where(c => c.catArea != Cat.CatArea.Asker).ToList();
    }




    public void ButtonSoundEffectPlay()
    {
        DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.ButtonClickEffectSource);
    }
    public void FinalGemiPanelAcilisSesi()
    {
        DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.GemiPanelAcmaEffectSource);
    }
    public void SetSaveObject(SaveObject saveObject)
    {
        toplamSatoPuani = saveObject.toplamSatoPuani;
        toplamYiyecekMiktari = saveObject.toplamYiyecekMiktari;
        satoPuaniKapasitesi = saveObject.satoPuaniKapasitesi;
        nufusKapasitesi = saveObject.nufusKapasitesi;
        yiyecekKapasitesi = saveObject.yiyecekKapasitesi;
        dinlenmeMiktari = saveObject.dinlenmeMiktari;
        dayaniklilikMiktari = saveObject.dayaniklilikMiktari;
    }
    public SaveObject GetSaveObject()
    {
        return new SaveObject
        {
            toplamSatoPuani = toplamSatoPuani,
            toplamYiyecekMiktari = toplamYiyecekMiktari,
            satoPuaniKapasitesi = satoPuaniKapasitesi,
            nufusKapasitesi = nufusKapasitesi,
            yiyecekKapasitesi = yiyecekKapasitesi,
            dinlenmeMiktari = dinlenmeMiktari,
            dayaniklilikMiktari = dayaniklilikMiktari
        };
    }

    [System.Serializable]
    public class SaveObject
    {
        public int toplamSatoPuani;
        public int toplamYiyecekMiktari;
        public int satoPuaniKapasitesi;
        public int nufusKapasitesi;
        public int yiyecekKapasitesi;
        public int dinlenmeMiktari;
        public int dayaniklilikMiktari;
    }
}