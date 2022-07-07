using Lean.Gui;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MerkezlerBase : MonoBehaviour
{
    public Transform CatSpawnTransform;
    public Transform UretimMiktariSpawnerTransform;
    public Transform EnerjiGostergePaneliTransform;
    public Button LevelUpButon;
    public List<Cat> InsideCatList;
    public List<GameObject> BarObjeList;
    public PolygonCollider2D PolygonCollider;

    private const int MAXBASELEVEL = 8;
    public int MaxBaseLevel { get { return MAXBASELEVEL; } }
    public float MerkezUretimHizi { get; set; }
    public float SonZaman { get; set; }
    public int MerkezSeviyesi { get; set; }
    public int MerkezKapasitesi { get; set; }
    public bool IsClosed { get; set; }
    public enum MerkezType
    {
        Castle,
        Rest,
        Mating,
        Military,
        LightHouse
    }
    public enum ProductionType
    {
        Empty,
        CastlePoint,
        Fish,
        Cat
    }
    public MerkezType MyMerkezType;
    public ProductionType MyProductionType;

    protected void UISetup()
    {
        LevelUpButon.onClick.AddListener(OpenLevelUpPanel);
    }

    [SerializeField] protected TextMeshProUGUI SeviyeGostergeText;
    [SerializeField] protected TextMeshProUGUI KapasiteGostergeText;


    public void LayoutUpdate(bool enerjisiAzalan)
    {
        if (enerjisiAzalan)
        {
            var newList = InsideCatList.OrderBy(x => x.EnerjiMiktari).ToList();
            InsideCatList = newList;
        }
        else
        {
            var newList = InsideCatList.OrderByDescending(x => x.EnerjiMiktari).ToList();
            InsideCatList = newList;
        }

        for (int i = 0; i < EnerjiGostergePaneliTransform.childCount; i++)
        {
            var child = EnerjiGostergePaneliTransform.GetChild(i).gameObject;
            if (!BarObjeList.Contains(child))
                BarObjeList.Add(child);
        }
        foreach (var item in BarObjeList.ToList())
        {
            if (item != null)
            {
                if (!item.transform.IsChildOf(EnerjiGostergePaneliTransform))
                {
                    BarObjeList.Remove(item);
                }
            }
            else
            {
                BarObjeList.Remove(item);
            }
        }

        if (enerjisiAzalan)
        {
            var newBarList = BarObjeList.OrderBy(x => x.GetComponent<HealthBar>().health).ToList();
            BarObjeList = newBarList;
        }
        else
        {
            var newBarList = BarObjeList.OrderByDescending(x => x.GetComponent<HealthBar>().health).ToList();
            BarObjeList = newBarList;
        }

        for (int i = 0; i < BarObjeList.Count; i++)
        {
            BarObjeList[i].transform.SetAsLastSibling();
        }

    }
    public void MerkezKapamaGuncelleme(GameObject closedObje, MerkezType merkezType)
    {
        closedObje.SetActive(IsClosed);
        if (IsClosed)
        {
            for (int i = 0; i < InsideCatList.Count; i++)
            {
                CatTransportManager.Instance.KediTasi(this, false);
            }
            MerkezEtkilesiminiKapat(merkezType);
        }
    }
    private void MerkezEtkilesiminiKapat(MerkezType merkezType)
    {
        switch (merkezType)
        {
            case MerkezType.Castle:
                InputScript.Instance.layerMask = LayerMask.GetMask("Dinlendirici", "Ureme", "Gol", "Mezarlik");
                break;
            case MerkezType.Mating:
                InputScript.Instance.layerMask = LayerMask.GetMask("Dinlendirici", "Sato", "Gol", "Mezarlik");
                break;
            case MerkezType.Military:
                InputScript.Instance.layerMask = LayerMask.GetMask("Dinlendirici", "Ureme", "Gol", "Sato");
                break;
            case MerkezType.LightHouse:
                InputScript.Instance.layerMask = LayerMask.GetMask("Dinlendirici", "Ureme", "Sato", "Mezarlik");
                break;
        }
    }
    public void UretimYap(Image uretimBarImage, int eklenecekUretimMiktari, ProductionType productionType)
    {
        if (UretimIcinBosYerVarMi(productionType))
        {
            SonZaman += Time.deltaTime;
            uretimBarImage.fillAmount = SonZaman / MerkezUretimHizi;

            if (SonZaman >= MerkezUretimHizi)
            {
                SonZaman = 0;
                MerkezUretimHizi = TimerHizi();
                uretimBarImage.transform.GetComponent<LeanPulse>().enabled = false;
                UretimMiktariEkle(productionType, eklenecekUretimMiktari);
                UretimMiktariFirlat(productionType, eklenecekUretimMiktari);
            }
        }
        else
        {
            uretimBarImage.transform.GetComponent<LeanPulse>().enabled = true;
            uretimBarImage.fillAmount = 1f;
        }
    }
    protected bool UretimIcinBosYerVarMi(ProductionType productionType)
    {
        switch (productionType)
        {
            case ProductionType.CastlePoint:
                if (BuildManager.Instance.ToplamSatoPuani < BuildManager.Instance.SatoPuaniKapasitesi) return true;
                break;
            case ProductionType.Fish:
                if (BuildManager.Instance.ToplamYiyecekMiktari < BuildManager.Instance.YiyecekKapasitesi) return true;
                break;
            case ProductionType.Cat:
                if (BuildManager.Instance.NufusKapasitesi > BuildManager.Instance.allCatList.Count) return true;
                    break;
        }
        return false;
    }
    public List<Cat> UretimeBaslamisKedileriGetir(ProductionType uretimType)
    {
        return InsideCatList.Where(c => c.UretimType == uretimType).ToList();
    }
    public virtual float TimerHizi()
    {
        return 0f;
    }
    protected void UretimMiktariEkle(ProductionType productionType, int eklenecekUretimMiktari)
    {
        switch (productionType)
        {
            case ProductionType.CastlePoint:
                if (BuildManager.Instance.SatoPuaniKapasitesi - BuildManager.Instance.ToplamSatoPuani >= eklenecekUretimMiktari)
                    BuildManager.Instance.ToplamSatoPuani += eklenecekUretimMiktari;
                else
                    BuildManager.Instance.ToplamSatoPuani = BuildManager.Instance.SatoPuaniKapasitesi;
                break;
            case ProductionType.Fish:
                if (BuildManager.Instance.YiyecekKapasitesi - BuildManager.Instance.ToplamYiyecekMiktari >= eklenecekUretimMiktari)
                    BuildManager.Instance.ToplamYiyecekMiktari += eklenecekUretimMiktari;
                else
                    BuildManager.Instance.ToplamYiyecekMiktari = BuildManager.Instance.YiyecekKapasitesi;
                break;
            case ProductionType.Cat:
                BuildManager.Instance.YeniKediOlustur();
                break;
        }
    }
    protected void UretimMiktariFirlat(ProductionType productionType, int uretimMiktari)
    {
        GameObject go = Instantiate(BuildManager.Instance.uretimMiktariPrefabObject);
        go.transform.position = UretimMiktariSpawnerTransform.position;
        go.GetComponent<UretimFirlatmaObje>().UretimUIOlustur(productionType, uretimMiktari);
    }

    public void BaseLevelUpButtonMethod()
    {
        if (IsBaseLevelUp())
            BaseLevelUp();
    }

    public virtual void BaseLevelUp()
    {
        BuildManager.Instance.ToplamSatoPuani -= GameBalanceValues.BaseForLevelUpAmount(MerkezSeviyesi);
        MerkezKapasitesi += GameBalanceValues.BaseCapacityIncreaseAmount(MyMerkezType, MerkezSeviyesi);
        DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.UpgradeEffectSource);
        MerkezSeviyesi++;
    }

    public bool IsBaseLevelUp()
    {
        if (BuildManager.Instance.ToplamSatoPuani >= GameBalanceValues.BaseForLevelUpAmount(MerkezSeviyesi) 
            && MerkezSeviyesi < MAXBASELEVEL)
            return true;
        else
            return false;
    }

    public void PrintUI()
    {
        SeviyeGostergeText.text = MerkezSeviyesi.ToString();
        if (MerkezKapasitesi == 0)
            KapasiteGostergeText.text = InsideCatList.Count.ToString();
        else
            KapasiteGostergeText.text = InsideCatList.Count.ToString() + "/" + MerkezKapasitesi.ToString();
        
    }
    public void OpenLevelUpPanel()
    {
        if (UIScript.Instance.UILevelPanel != null)
            return;
        UILevelPanel.PanelCreate(this);
        GameManager.Instance.ChangeState(GameState.Pause);
    }
}
