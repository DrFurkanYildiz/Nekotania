using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleManager : MonoBehaviour
{
    public static CycleManager Instance;
    private float sonZaman;
    private const int BASE_TİME = 30;
    private const int END_GAME = 24;
    private const int CYCLE_AMOUNT = 4;
    private int baseTime;
    private int dayTime;
    private int donguMiktari;

    public int DonguMiktari
    {
        get { return donguMiktari; }
        set { donguMiktari = value; }
    }
    public int DayTime
    {
        get { return dayTime; }
        set { dayTime = value; }
    }
    public int BaseTime
    {
        get { return baseTime; }
        set { baseTime = value; }
    }


    private void Awake()
    {
        Instance = this;
        baseTime = BASE_TİME;
        dayTime = 1;
        donguMiktari = 0;
    }

    private void Update() => CycleUpdate();

    private void CycleUpdate()
    {
        if (Time.time - sonZaman >= .9999f)
        {
            sonZaman = Time.time;

            if (donguMiktari < CYCLE_AMOUNT)
            {
                if (baseTime > 0)
                {
                    baseTime--;
                    return;
                }
                else
                {
                    DonguSonuKalePuanKesintisi();
                    DonguSonuBalikKesintisi();

                    if (IsGameOver())
                        GameManager.Instance.ChangeState(GameState.Lose);
                    else
                        DonguSesiCal();

                    baseTime = BASE_TİME;
                    donguMiktari++;
                }
            }
            else
            {
                if (baseTime > 0)
                {
                    baseTime--;
                    return;
                }
                else
                {
                    DonguSonuKalePuanKesintisi();
                    DonguSonuBalikKesintisi();

                    if (!IsGameOver())
                    {
                        EventControl();
                        //DonguSesiCal();
                    }
                    else
                    {
                        GameManager.Instance.ChangeState(GameState.Lose);
                    }
                }
            }
        }
    }

    private bool IsGameOver()
    {
        return BuildManager.Instance.ToplamSatoPuani == 0;
    }
    private void EventControl()
    {
        if (dayTime == END_GAME)
        {
            GameManager.Instance.ChangeState(GameState.Lose);
            return;
        }

        while (true)
        {
            OlaylarSO Olay = GameEventManager.Instance.YeniOlay();
            if(Olay != null)
            {
                GameEventHandler.EventCreate(Olay);
                break;
            }
        }

        DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.NewEventEffectSource);
        GameManager.Instance.ChangeState(GameState.Pause);
    }
    public void EventSelected()
    {
        GameManager.Instance.ChangeState(GameState.Continue);

        baseTime = BASE_TİME;
        donguMiktari = 0;
        dayTime++;

        SaveManager.Instance.OnSave();

    }


    private void DonguSesiCal()
    {
        DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.HerTurKesmeEffectSource);
    }
    private void DonguSonuKalePuanKesintisi()
    {
        BuildManager.Instance.ToplamSatoPuani -= GameBalanceValues.SatoPuaniEksiltmeMiktari(dayTime);
    }
    private void DonguSonuBalikKesintisi()
    {
        BuildManager.Instance.KillCats(BuildManager.Instance.TotalCatAmount() - BuildManager.Instance.ToplamYiyecekMiktari);
        BuildManager.Instance.ToplamYiyecekMiktari -= BuildManager.Instance.TotalCatAmount();
    }


    public void SetSaveObject(SaveObject saveObject)
    {
        baseTime = saveObject.baseTime;
        dayTime = saveObject.dayTime;
        donguMiktari = saveObject.donguMiktari;
    }
    public SaveObject GetSaveObject()
    {
        return new SaveObject
        {
            baseTime = baseTime,
            dayTime = dayTime,
            donguMiktari = donguMiktari
        };
    }

    [System.Serializable]
    public class SaveObject
    {
        public int baseTime;
        public int dayTime;
        public int donguMiktari;
    }

}
