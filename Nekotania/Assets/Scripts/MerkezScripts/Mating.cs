using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mating : MerkezlerBase
{
    public static Mating Instance;
    private const int PRODUCTİON_VALUE = 1;
    private const int PRODUCTİON_SPEED = 18;
    [SerializeField] private Image uretimBarImage;
    [SerializeField] private GameObject closedObje;

    public Mating()
    {
        MerkezSeviyesi = 1;
        MerkezKapasitesi = 4;
        MerkezUretimHizi = PRODUCTİON_SPEED;
        InsideCatList = new List<Cat>();
        BarObjeList = new List<GameObject>();
        MyMerkezType = MerkezType.Mating;
        MyProductionType = ProductionType.Cat;
    }
    void Awake()
    {
        Instance = this;
        UISetup();
    }

    void Update()
    {
        ProductionStateControl();
        MerkezKapamaGuncelleme(closedObje, MyMerkezType);
        LayoutUpdate(true);
        PrintUI();
    }

    private void ProductionStateControl()
    {
        if (UretimeBaslamisKedileriGetir(MyProductionType).Count > 1)
        {
            UretimYap(uretimBarImage, PRODUCTİON_VALUE, MyProductionType);
        }
        else
        {
            uretimBarImage.fillAmount = 0f;
            SonZaman = 0f;
        }
    }
    public override float TimerHizi()
    {
        if (UretimeBaslamisKedileriGetir(MyProductionType).Count % 2 == 0)
            return (float)(PRODUCTİON_SPEED * 2) / (float)UretimeBaslamisKedileriGetir(MyProductionType).Count;
        else
            return (float)(PRODUCTİON_SPEED * 2) / (float)(UretimeBaslamisKedileriGetir(MyProductionType).Count - 1f);
    }

    public void SetSaveObject(SaveObject saveObject)
    {
        MerkezSeviyesi = saveObject.MerkezSeviyesi;
        MerkezKapasitesi = saveObject.MerkezKapasitesi;
        IsClosed = saveObject.IsClosed;
        MerkezUretimHizi = saveObject.MerkezUretimHizi;
    }
    public SaveObject GetSaveObject()
    {
        return new SaveObject
        {
            MerkezSeviyesi = MerkezSeviyesi,
            MerkezKapasitesi = MerkezKapasitesi,
            IsClosed = IsClosed,
            MerkezUretimHizi = MerkezUretimHizi
        };
    }

    [System.Serializable]
    public class SaveObject
    {
        public int MerkezSeviyesi;
        public int MerkezKapasitesi;
        public bool IsClosed;
        public float MerkezUretimHizi;
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
