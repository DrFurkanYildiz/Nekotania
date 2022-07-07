using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatTransportManager : MonoBehaviour
{
    public static CatTransportManager Instance;
    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;

    }

    private List<Cat> DinlenenKediListesiGetir()
    {
        return Rest.Instance.InsideCatList.Where(c => c.catState == Cat.CatState.Dinlen).ToList();
    }
    private List<Cat> TapinanKediListesiniGetir()
    {
        return Castle.Instance.InsideCatList.Where(c => c.catState == Cat.CatState.Tapin).ToList();
    }
    private List<Cat> BalikTutanKediListesiniGetir()
    {
        return LightHouse.Instance.InsideCatList.Where(c => c.catState == Cat.CatState.BalikTopla).ToList();
    }
    private List<Cat> UreyenKediListesiniGetir()
    {
        return Mating.Instance.InsideCatList.Where(c => c.catState == Cat.CatState.Sevis).ToList();
    }
    public int KaleyeGidenKediSayisi()
    {
        return BuildManager.Instance.allCatList.Where(c => c.isinlanacakBolge == Cat.CatArea.Sato).ToList().Count;
    }
    public int UremeyeGidenKediSayisi()
    {
        return BuildManager.Instance.allCatList.Where(c => c.isinlanacakBolge == Cat.CatArea.Ureme).ToList().Count;
    }
    public int BaligaGidenKediSayisi()
    {
        return BuildManager.Instance.allCatList.Where(c => c.isinlanacakBolge == Cat.CatArea.Gol).ToList().Count;
    }
    public int YagmayaGidenKediSayisi()
    {
        return BuildManager.Instance.allCatList.Where(c => c.isinlanacakBolge == Cat.CatArea.Asker).ToList().Count;
    }
    public void KediTasi(MerkezlerBase merkezlerBase, bool swipeDown)
    {
        if (!swipeDown)
        {
            List<Cat> catList = new List<Cat>();
            if (merkezlerBase is Mating)
            {
                catList = UreyenKediListesiniGetir();

                if (catList.Count > 0)
                {

                    if (catList.Count % 2 == 0)
                    {
                        Cat cat0 = catList[0];
                        cat0.catState = Cat.CatState.CikisYap;
                        cat0.isinlanacakBolge = Cat.CatArea.Dinlenme;
                        FunctionTimer.Create(() =>
                        { if (cat0 != null) cat0.GetBarObje().transform.SetParent(Rest.Instance.EnerjiGostergePaneliTransform); }, 1f);
                        
                        Cat cat1 = catList[1];
                        cat1.catState = Cat.CatState.CikisYap;
                        cat1.isinlanacakBolge = Cat.CatArea.Dinlenme;
                        FunctionTimer.Create(() =>
                        { if (cat1 != null) cat1?.GetBarObje().transform.SetParent(Rest.Instance.EnerjiGostergePaneliTransform); }, 1f);


                        DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.KediKaydirmaEffectSource);
                    }
                    else
                    {
                        Cat cat0 = catList[0];
                        cat0.catState = Cat.CatState.CikisYap;
                        cat0.isinlanacakBolge = Cat.CatArea.Dinlenme;
                        FunctionTimer.Create(() =>
                        { if (cat0 != null) cat0.GetBarObje().transform.SetParent(Rest.Instance.EnerjiGostergePaneliTransform); }, 1f);


                        DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.KediKaydirmaEffectSource);
                    }

                }
            }
            else
            {
                if (merkezlerBase is Castle)
                    catList = TapinanKediListesiniGetir();
                else if (merkezlerBase is LightHouse)
                    catList = BalikTutanKediListesiniGetir();

                if (catList.Count > 0)
                {
                    Cat cat = catList[0];
                    cat.catState = Cat.CatState.CikisYap;
                    cat.isinlanacakBolge = Cat.CatArea.Dinlenme;
                    FunctionTimer.Create(() =>
                    { if (cat != null) cat.GetBarObje().transform.SetParent(Rest.Instance.EnerjiGostergePaneliTransform); }, 1f);


                    DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.KediKaydirmaEffectSource);
                }
            }
        }
        else
        {
            List<Cat> cats = DinlenenKediListesiGetir();
            if (merkezlerBase is Castle)
            {
                if (cats.Count > 0 && Castle.Instance.MerkezKapasitesi > KaleyeGidenKediSayisi())
                {
                    cats[0].catState = Cat.CatState.CikisYap;
                    cats[0].isinlanacakBolge = Cat.CatArea.Sato;
                    FunctionTimer.Create(() =>
                    { if (cats[0] != null) cats[0].GetBarObje().transform.SetParent(Castle.Instance.EnerjiGostergePaneliTransform); }, 1f);


                    DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.KediKaydirmaEffectSource);
                }
            }
            else if (merkezlerBase is LightHouse)
            {
                if (cats.Count > 0 && LightHouse.Instance.MerkezKapasitesi > BaligaGidenKediSayisi())
                {
                    cats[0].catState = Cat.CatState.CikisYap;
                    cats[0].isinlanacakBolge = Cat.CatArea.Gol;
                    FunctionTimer.Create(() =>
                    { if (cats[0] != null) cats[0].GetBarObje().transform.SetParent(LightHouse.Instance.EnerjiGostergePaneliTransform); }, 1f);


                    DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.KediKaydirmaEffectSource);
                }
            }
            else if (merkezlerBase is Military)
            {
                if (cats.Count > 0)
                {
                    cats[0].catState = Cat.CatState.CikisYap;
                    cats[0].isinlanacakBolge = Cat.CatArea.Asker;

                    FunctionTimer.Create(() =>
                    { if (cats[0] != null) cats[0].GetBarObje().transform.SetParent(Military.Instance.EnerjiGostergePaneliTransform); }, 1f);


                    DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.KediKaydirmaEffectSource);
                }
            }
            else if (merkezlerBase is Mating)
            {
                if (cats.Count > 0 && Mating.Instance.MerkezKapasitesi > UremeyeGidenKediSayisi())
                {
                    if(cats.Count > 1 && UreyenKediListesiniGetir().Count % 2 == 0)
                    {
                        if (UremeyeGidenKediSayisi() % 2 == 0)
                        {
                            cats[0].catState = Cat.CatState.CikisYap;
                            cats[0].isinlanacakBolge = Cat.CatArea.Ureme;
                            FunctionTimer.Create(() =>
                            { if (cats[0] != null) cats[0].GetBarObje().transform.SetParent(Mating.Instance.EnerjiGostergePaneliTransform); }, 1f);

                            cats[1].catState = Cat.CatState.CikisYap;
                            cats[1].isinlanacakBolge = Cat.CatArea.Ureme;

                            FunctionTimer.Create(() =>
                            { if (cats[1] != null) cats[1].GetBarObje().transform.SetParent(Mating.Instance.EnerjiGostergePaneliTransform); }, 1f);


                            DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.KediKaydirmaEffectSource);
                        }
                    }
                    else
                    {
                        if (UremeyeGidenKediSayisi() % 2 == 1)
                        {
                            cats[0].catState = Cat.CatState.CikisYap;
                            cats[0].isinlanacakBolge = Cat.CatArea.Ureme;
                            FunctionTimer.Create(() =>
                            { if (cats[0] != null) cats[0].GetBarObje().transform.SetParent(Mating.Instance.EnerjiGostergePaneliTransform); }, 1f);

                            DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.KediKaydirmaEffectSource);
                        }
                    }

                    
                }
            }
        }
    }




}
