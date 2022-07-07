using UnityEngine;
using System.Linq;

public class OlayIsleyiciScript : MonoBehaviour
{
    public static OlayIsleyiciScript Instance;

    InputScript InputScript;
    private bool dayaniklilikDegistir;
    private bool dayaniklilikTekrarDegisti;
    private int dayaniklilikMikDegisimi;
    private int oncekiDayaniklilikDegisimi;
    private int dayaniklilikKacinciGun;

    private bool dinlenmeDegistir;
    private bool dinlenmeTekrarDegisti;
    private int dinlenmeMikDegisimi;
    private int oncekiDinlenmeDegisimi;
    private int dinlenmeKacinciGun;

    private bool merkezKapandi;
    private int merkezKapanmaKacinciGun;

    private bool karYagdirdiMi;
    private bool karYagdirTekrarla;
    private int karYagdirKacinciGun;
    private void Awake()
    {
        Instance = this;
        InputScript = GetComponent<InputScript>();
    }
    private void Update()
    {
        DayaniklilikGuncelle();
        DinlenmeGuncelle();
        MerkezKapamaGuncelle();
        ParticleGuncelle();
    }
    public int PuanEksilt(int yuzdeKac)
    {
        //int kadar = BuildManager.Instance.ToplamSatoPuani * yuzdeKac / 100;
        int kadar = BuildManager.Instance.SatoPuaniKapasitesi * yuzdeKac / 100;
        BuildManager.Instance.ToplamSatoPuani -= kadar;
        return kadar;
    }
    public int PuanArttir(int yuzdeKac)
    {
        //int kadar = BuildManager.Instance.ToplamSatoPuani * yuzdeKac / 100;
        int kadar = BuildManager.Instance.SatoPuaniKapasitesi * yuzdeKac / 100;
        BuildManager.Instance.ToplamSatoPuani += kadar;
        return kadar;
    }
    public int AnaOlayPuanArttir(int value)
    {
        BuildManager.Instance.ToplamSatoPuani += value;
        return value;
    }
    public int BalikEksilt(int yuzdeKac)
    {
        //int kadar = BuildManager.Instance.ToplamYiyecekMiktari * yuzdeKac / 100;
        int kadar = BuildManager.Instance.YiyecekKapasitesi * yuzdeKac / 100;
        BuildManager.Instance.ToplamYiyecekMiktari -= kadar;
        return kadar;
    }
    public int BalikArttir(int yuzdeKac)
    {
        //int kadar = BuildManager.Instance.ToplamYiyecekMiktari * yuzdeKac / 100;
        int kadar = BuildManager.Instance.YiyecekKapasitesi * yuzdeKac / 100;
        BuildManager.Instance.ToplamYiyecekMiktari += kadar;
        return kadar;
    }
    public int KediOldur(int yuzdeKac)
    {
        //int kadar = BuildManager.Instance.TotalCatAmount() * yuzdeKac / 100;
        int kadar = BuildManager.Instance.NufusKapasitesi * yuzdeKac / 100;
        if (kadar == 0)
            if (BuildManager.Instance.TotalCatAmount() > 0)
                kadar = 1;
        BuildManager.Instance.KillCats(kadar);
        return kadar;
    }
    public int KediEkle(int yuzdeKac)
    {
        //int kadar = BuildManager.Instance.TotalCatAmount() * yuzdeKac / 100;
        int kadar = BuildManager.Instance.NufusKapasitesi * yuzdeKac / 100;
        if (kadar < 1) kadar = 1;
        BuildManager.Instance.StartedCatGenerate(kadar);
        return kadar;
    }
    public void PuzzleParcasiHediyeEt()
    {
        var puzzles = PuzzleManager.Instance.AllPuzzleList.Where(p => p.State == PuzzleHandler.PuzzleState.IsLoked).ToList();

        var sirali = puzzles.OrderBy(p => p.priceAmount).ToList();
        sirali[0].State = PuzzleHandler.PuzzleState.UnLoked;
    }
    private void ParticleGuncelle()
    {
        if (karYagdirdiMi)
        {
            if (CycleManager.Instance.DayTime - karYagdirKacinciGun == 2)
            {
                karYagdirdiMi = false;
                ParticleManager.Instance.SnowParticle.Stop();
            }
            else
            {
                if (!karYagdirTekrarla)
                {
                    ParticleManager.Instance.SnowParticle.Play();
                    karYagdirTekrarla = true;
                }
            }
        }
    }
    public void KarYagdir()
    {
        karYagdirdiMi = true;
        karYagdirKacinciGun = CycleManager.Instance.DayTime;
        ParticleManager.Instance.SnowParticle.Play();
    }
    private void DayaniklilikGuncelle()
    {
        if (dayaniklilikDegistir)
        {
            if (!dayaniklilikTekrarDegisti)
            {
                if (CycleManager.Instance.DayTime - dayaniklilikKacinciGun == 2)
                {
                    dayaniklilikDegistir = false;
                    BuildManager.Instance.DayaniklilikMiktari += dayaniklilikMikDegisimi;
                    dayaniklilikKacinciGun = 0;
                    dayaniklilikMikDegisimi = 0;
                }
            }
            else
            {
                BuildManager.Instance.DayaniklilikMiktari += oncekiDayaniklilikDegisimi;
                oncekiDayaniklilikDegisimi = 0;
                dayaniklilikTekrarDegisti = false;
            }
        }
    }
    public int DayaniklilikAzalt(int yuzdeKac)
    {
        if (!dayaniklilikDegistir)
            dayaniklilikDegistir = true;
        else
        {
            dayaniklilikTekrarDegisti = true;
            oncekiDayaniklilikDegisimi = dayaniklilikMikDegisimi;
        }
        int kadar = BuildManager.Instance.DayaniklilikMiktari * yuzdeKac / 100;
        BuildManager.Instance.DayaniklilikMiktari -= kadar;
        dayaniklilikMikDegisimi = kadar;
        dayaniklilikKacinciGun = CycleManager.Instance.DayTime;
        return kadar;
    }
    public int DayaniklilikArttir(int yuzdeKac)
    {
        if (!dayaniklilikDegistir)
            dayaniklilikDegistir = true;
        else
        {
            dayaniklilikTekrarDegisti = true;
            oncekiDayaniklilikDegisimi = dayaniklilikMikDegisimi;
        }
        int kadar = BuildManager.Instance.DayaniklilikMiktari * yuzdeKac / 100;
        BuildManager.Instance.DayaniklilikMiktari += kadar;
        dayaniklilikMikDegisimi = -kadar;
        dayaniklilikKacinciGun = CycleManager.Instance.DayTime;
        return kadar;
    }


    private void DinlenmeGuncelle()
    {
        if (dinlenmeDegistir)
        {
            if (!dinlenmeTekrarDegisti)
            {
                if (CycleManager.Instance.DayTime - dinlenmeKacinciGun == 2)
                {
                    dinlenmeDegistir = false;
                    BuildManager.Instance.DinlenmeMiktari += dinlenmeMikDegisimi;
                    dinlenmeKacinciGun = 0;
                    dinlenmeMikDegisimi = 0;
                }
            }
            else
            {
                BuildManager.Instance.DinlenmeMiktari += oncekiDinlenmeDegisimi;
                oncekiDinlenmeDegisimi = 0;
                dinlenmeTekrarDegisti = false;
            }
        }
    }
    public void DinlenmeMiktariniAzalt(int neKadar)
    {
        if (!dinlenmeDegistir)
            dinlenmeDegistir = true;
        else
        {
            dinlenmeTekrarDegisti = true;
            oncekiDinlenmeDegisimi = dinlenmeMikDegisimi;
        }
        BuildManager.Instance.DinlenmeMiktari -= neKadar;
        dinlenmeMikDegisimi = neKadar;
        dinlenmeKacinciGun = CycleManager.Instance.DayTime;
    }
    public void DinlenmeMiktariniArttir(int neKadar)
    {
        if (!dinlenmeDegistir)
            dinlenmeDegistir = true;
        else
        {
            dinlenmeTekrarDegisti = true;
            oncekiDinlenmeDegisimi = dinlenmeMikDegisimi;
        }
        BuildManager.Instance.DinlenmeMiktari += neKadar;
        dinlenmeMikDegisimi = -neKadar;
        dinlenmeKacinciGun = CycleManager.Instance.DayTime;
    }
    private void MerkezKapamaGuncelle()
    {
        if (merkezKapandi)
        {
            if (CycleManager.Instance.DayTime - merkezKapanmaKacinciGun == 2)
            {
                merkezKapandi = false;
                InputScript.layerMask = LayerMask.GetMask("Dinlendirici", "Sato", "Ureme", "Gol", "Mezarlik");
                merkezKapanmaKacinciGun = 0;
                Castle.Instance.IsClosed = false;
                Mating.Instance.IsClosed = false;
                LightHouse.Instance.IsClosed = false;
                Military.Instance.IsClosed = false;
            }
        }
    }
    public void MerkeziKapat(MerkezlerBase.MerkezType merkezType)
    {
        merkezKapandi = true;
        merkezKapanmaKacinciGun = CycleManager.Instance.DayTime;
        Castle.Instance.IsClosed = false;
        Mating.Instance.IsClosed = false;
        LightHouse.Instance.IsClosed = false;
        Military.Instance.IsClosed = false;
        switch (merkezType)
        {
            case MerkezlerBase.MerkezType.Castle: Castle.Instance.IsClosed = true; break;
            case MerkezlerBase.MerkezType.Mating: Mating.Instance.IsClosed = true; break;
            case MerkezlerBase.MerkezType.Military: Military.Instance.IsClosed = true; break;
            case MerkezlerBase.MerkezType.LightHouse: LightHouse.Instance.IsClosed = true; break;
        }
    }

    public void SetSaveObject(SaveObject saveObject)
    {
        dayaniklilikDegistir = saveObject.dayaniklilikDegistir;
        dayaniklilikTekrarDegisti = saveObject.dayaniklilikTekrarDegisti;
        dayaniklilikMikDegisimi = saveObject.dayaniklilikMikDegisimi;
        oncekiDayaniklilikDegisimi = saveObject.oncekiDayaniklilikDegisimi;
        dayaniklilikKacinciGun = saveObject.dayaniklilikKacinciGun;

        dinlenmeDegistir = saveObject.dinlenmeDegistir;
        dinlenmeTekrarDegisti = saveObject.dinlenmeTekrarDegisti;
        dinlenmeMikDegisimi = saveObject.dinlenmeMikDegisimi;
        oncekiDinlenmeDegisimi = saveObject.oncekiDinlenmeDegisimi;
        dinlenmeKacinciGun = saveObject.dinlenmeKacinciGun;

        merkezKapandi = saveObject.merkezKapandi;
        merkezKapanmaKacinciGun = saveObject.merkezKapanmaKacinciGun;

        karYagdirdiMi = saveObject.karYagdirdiMi;
        karYagdirKacinciGun = saveObject.karYagdirKacinciGun;
    }
    public SaveObject GetSaveObject()
    {
        return new SaveObject
        {
            dayaniklilikDegistir = dayaniklilikDegistir,
            dayaniklilikTekrarDegisti = dayaniklilikTekrarDegisti,
            dayaniklilikMikDegisimi = dayaniklilikMikDegisimi,
            oncekiDayaniklilikDegisimi = oncekiDayaniklilikDegisimi,
            dayaniklilikKacinciGun = dayaniklilikKacinciGun,

            dinlenmeDegistir = dinlenmeDegistir,
            dinlenmeTekrarDegisti = dinlenmeTekrarDegisti,
            dinlenmeMikDegisimi = dinlenmeMikDegisimi,
            oncekiDinlenmeDegisimi = oncekiDinlenmeDegisimi,
            dinlenmeKacinciGun = dinlenmeKacinciGun,

            merkezKapandi = merkezKapandi,
            merkezKapanmaKacinciGun = merkezKapanmaKacinciGun,

            karYagdirdiMi = karYagdirdiMi,
            karYagdirKacinciGun = karYagdirKacinciGun
        };
    }


    [System.Serializable]
    public class SaveObject
    {
        public bool dayaniklilikDegistir;
        public bool dayaniklilikTekrarDegisti;
        public int dayaniklilikMikDegisimi;
        public int oncekiDayaniklilikDegisimi;
        public int dayaniklilikKacinciGun;

        public bool dinlenmeDegistir;
        public bool dinlenmeTekrarDegisti;
        public int dinlenmeMikDegisimi;
        public int oncekiDinlenmeDegisimi;
        public int dinlenmeKacinciGun;

        public bool merkezKapandi;
        public int merkezKapanmaKacinciGun;

        public bool karYagdirdiMi;
        public int karYagdirKacinciGun;
    }
}
