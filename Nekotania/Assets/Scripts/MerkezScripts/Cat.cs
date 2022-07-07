using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public static Cat CatCreate(Transform catObjectPrefab, Vector2 catPosition, float enerjiMiktari, GameObject enerjiBarPrefab, Transform enerjiBarSpawnTransform, Transform parentTransform)
    {
        Transform catObjectTransform = Instantiate(catObjectPrefab, catPosition, Quaternion.identity);
        catObjectTransform.SetParent(parentTransform, false);
        Cat cat = catObjectTransform.GetComponent<Cat>();

        GameObject bar = Instantiate(enerjiBarPrefab);
        bar.transform.SetParent(enerjiBarSpawnTransform, false);
        bar.GetComponent<HealthBar>().health = enerjiMiktari;
        cat.barObje = bar;

        cat.catPosition = catPosition;
        cat.enerjiMiktari = enerjiMiktari;
        return cat;
    }
    public enum CatState
    {
        GirisYap,
        CikisYap,
        Isinla,
        Dinlen,
        Tapin,
        Sevis,
        BalikTopla,
        ArkerOl
    }
    public enum CatArea
    {
        Dinlenme,
        Sato,
        Ureme,
        Gol,
        Asker
    }
    public MerkezlerBase.ProductionType UretimType;

    [SerializeField]private Vector2 catPosition;
    [SerializeField]private GameObject barObje;
    [SerializeField]private float enerjiMiktari;
    public Animator animator;
    private float sonZaman;
    public float EnerjiMiktari
    {
        get { return enerjiMiktari; }
        set { enerjiMiktari = value; }
    }

    [SerializeField] private SpriteRenderer catSprite;
    [SerializeField] private int spriteLayer = 2;
    private float elapsedTime;
    private float desiredDuration = 1f;
    public CatState catState = CatState.Dinlen;
    public CatArea catArea = CatArea.Dinlenme;
    public CatArea isinlanacakBolge;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 targetPosition;
    public Animation TapmaAnim;
    private bool askereAlindiMi = false;
    private bool frameAtlandiMi;
    private void Start()
    {
        startPosition = transform.position;
        catSprite.sortingOrder = spriteLayer;
    }
    private void Update()
    {
        catPosition = transform.position;
        CatStateChanger();
    }
    private void CatStateChanger()
    {
        switch (catArea)
        {
            case CatArea.Dinlenme:
                switch (catState)
                {
                    case CatState.GirisYap:

                        KedininLayeriniDegistir(this, 2);

                        KediyiKonumaHareketEttir(startPosition, targetPosition, CatState.Dinlen);
                        KediyiSagTarafaYurut(this);
                        animator.SetBool("Yuru", true);
                        animator.SetBool("Dinlen", false);
                        break;
                    case CatState.CikisYap:
                        KediyiKonumaHareketEttir(startPosition, targetPosition, CatState.Isinla);
                        KediyiSolTarafaYurut(this);
                        animator.SetBool("Yuru", true);
                        animator.SetBool("Dinlen", false);
                        break;
                    case CatState.Isinla:
                        switch (isinlanacakBolge)
                        {
                            case CatArea.Sato:
                                startPosition = Castle.Instance.CatSpawnTransform.position;
                                targetPosition = RasgeleKonumBelirle(Castle.Instance.PolygonCollider);
                                catArea = CatArea.Sato;
                                catState = CatState.GirisYap;
                                break;
                            case CatArea.Ureme:
                                startPosition = Mating.Instance.CatSpawnTransform.position;
                                targetPosition = RasgeleKonumBelirle(Mating.Instance.PolygonCollider);
                                catArea = CatArea.Ureme;
                                catState = CatState.GirisYap;
                                break;
                            case CatArea.Gol:
                                startPosition = LightHouse.Instance.CatSpawnTransform.position;
                                targetPosition = RasgeleKonumBelirle(LightHouse.Instance.PolygonCollider);
                                catArea = CatArea.Gol;
                                catState = CatState.GirisYap;
                                break;
                            case CatArea.Asker:
                                startPosition = Military.Instance.CatSpawnTransform.position;
                                targetPosition = RasgeleKonumBelirle(Military.Instance.PolygonCollider);
                                catArea = CatArea.Asker;
                                catState = CatState.GirisYap;
                                break;
                            default:
                                break;
                        }


                        break;
                    case CatState.Dinlen:
                        KediDinlendir();
                        UretimType = MerkezlerBase.ProductionType.Empty;
                        startPosition = transform.position;
                        targetPosition = Rest.Instance.CatSpawnTransform.position;
                        animator.SetBool("Yuru", false);
                        if (!frameAtlandiMi)
                        {
                            frameAtlandiMi = true;
                            animator.Play("UyumaAnim", 0, Random.Range(0f, 1f));
                        }
                        animator.SetBool("Dinlen", true);
                        break;
                    case CatState.Tapin:
                        break;
                    case CatState.Sevis:
                        break;
                    case CatState.BalikTopla:
                        break;
                    case CatState.ArkerOl:
                        break;
                    default:
                        break;
                }
                break;
            case CatArea.Sato:
                switch (catState)
                {
                    case CatState.GirisYap:


                        KedininLayeriniDegistir(this, 6);

                        if (targetPosition.x <= 0)
                            KediyiSolTarafaYurut(this);
                        else
                            KediyiSagTarafaYurut(this);

                        KediyiKonumaHareketEttir(startPosition, targetPosition, CatState.Tapin);

                        animator.SetBool("Yuru", true);

                        break;
                    case CatState.CikisYap:

                        if (startPosition.x >= 0)
                            KediyiSolTarafaYurut(this);
                        else
                            KediyiSagTarafaYurut(this);

                        KediyiKonumaHareketEttir(startPosition, targetPosition, CatState.Isinla);
                        animator.SetBool("Tapin", false);

                        break;
                    case CatState.Isinla:

                        startPosition = Rest.Instance.CatSpawnTransform.position;
                        targetPosition = RasgeleKonumBelirle(Rest.Instance.PolygonCollider);
                        catArea = CatArea.Dinlenme;
                        catState = CatState.GirisYap;

                        break;
                    case CatState.Dinlen:
                        break;
                    case CatState.Tapin:
                        UretimType = MerkezlerBase.ProductionType.CastlePoint;
                        KediCalistir(BuildManager.Instance.DayaniklilikMiktari);

                        startPosition = transform.position;
                        targetPosition = Castle.Instance.CatSpawnTransform.position;
                        if (!frameAtlandiMi)
                        {
                            frameAtlandiMi = true;
                            animator.Play("TapmaAnim", 0, Random.Range(0f,1f));
                        }
                        animator.SetBool("Tapin", true);
                        break;
                    case CatState.Sevis:
                        break;
                    case CatState.BalikTopla:
                        break;
                    case CatState.ArkerOl:
                        break;
                    default:
                        break;
                }
                break;
            case CatArea.Ureme:
                switch (catState)
                {
                    case CatState.GirisYap:
                        KedininLayeriniDegistir(this, 2);
                        KediyiSolTarafaYurut(this);

                        KediyiKonumaHareketEttir(startPosition, targetPosition, CatState.Sevis);

                        animator.SetBool("Yuru", true);
                        break;
                    case CatState.CikisYap:
                        KediyiSagTarafaYurut(this);
                        animator.SetBool("Ureme", false);

                        KediyiKonumaHareketEttir(startPosition, targetPosition, CatState.Isinla);
                        break;
                    case CatState.Isinla:
                        startPosition = Rest.Instance.CatSpawnTransform.position;
                        targetPosition = RasgeleKonumBelirle(Rest.Instance.PolygonCollider);
                        catArea = CatArea.Dinlenme;
                        catState = CatState.GirisYap;
                        break;
                    case CatState.Dinlen:
                        break;
                    case CatState.Tapin:
                        break;
                    case CatState.Sevis:
                        UretimType = MerkezlerBase.ProductionType.Cat;
                        KediCalistir(BuildManager.Instance.DayaniklilikMiktari);
                        startPosition = transform.position;
                        targetPosition = Mating.Instance.CatSpawnTransform.position;

                        animator.SetBool("Ureme", true);
                        break;
                    case CatState.BalikTopla:
                        break;
                    case CatState.ArkerOl:
                        break;
                    default:
                        break;
                }
                break;
            case CatArea.Gol:
                switch (catState)
                {
                    case CatState.GirisYap:
                        KedininLayeriniDegistir(this, 6);
                        KediyiSagTarafaYurut(this);

                        KediyiKonumaHareketEttir(startPosition, targetPosition, CatState.BalikTopla);

                        animator.SetBool("Yuru", true);
                        break;
                    case CatState.CikisYap:
                        KediyiSolTarafaYurut(this);

                        KediyiKonumaHareketEttir(startPosition, targetPosition, CatState.Isinla);
                        animator.SetBool("BalikTopla", false);

                        break;
                    case CatState.Isinla:
                        startPosition = Rest.Instance.CatSpawnTransform.position;
                        targetPosition = RasgeleKonumBelirle(Rest.Instance.PolygonCollider);
                        catArea = CatArea.Dinlenme;
                        catState = CatState.GirisYap;
                        break;
                    case CatState.Dinlen:
                        break;
                    case CatState.Tapin:
                        break;
                    case CatState.Sevis:
                        break;
                    case CatState.BalikTopla:
                        if (!frameAtlandiMi)
                        {
                            frameAtlandiMi = true;
                            animator.Play("BalikTutmaAnim", 0, Random.Range(0f, 1f));
                        }
                        UretimType = MerkezlerBase.ProductionType.Fish;
                        animator.SetBool("BalikTopla", true);
                        KediCalistir(BuildManager.Instance.DayaniklilikMiktari);
                        startPosition = transform.position;
                        targetPosition = LightHouse.Instance.CatSpawnTransform.position;

                        break;
                    case CatState.ArkerOl:
                        break;
                    default:
                        break;
                }
                break;
            case CatArea.Asker:
                switch (catState)
                {
                    case CatState.GirisYap:
                        if (!askereAlindiMi)
                        {
                            enerjiMiktari = 0f;
                            askereAlindiMi = true;
                            barObje.GetComponent<HealthBar>().health = 0f;
                            barObje.GetComponent<HealthBar>().healthBar.fillAmount = 0;
                        }
                        KedininLayeriniDegistir(this, 6);
                        KediyiSagTarafaYurut(this);

                        KediyiKonumaHareketEttir(startPosition, targetPosition, CatState.ArkerOl);

                        animator.SetBool("Yuru", true);
                        break;
                    case CatState.CikisYap:
                        break;
                    case CatState.Isinla:
                        break;
                    case CatState.Dinlen:
                        break;
                    case CatState.Tapin:
                        break;
                    case CatState.Sevis:
                        break;
                    case CatState.BalikTopla:
                        break;
                    case CatState.ArkerOl:
                        UretimType = MerkezlerBase.ProductionType.Empty;
                        if (!frameAtlandiMi)
                        {
                            frameAtlandiMi = true;
                            animator.Play("AskerOlmaAnim", 0, Random.Range(0f, 1f));
                        }
                        animator.SetBool("AskerOl", true);
                        AskereGonder();
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
    private void KediCalistir(float dayaniklilik, bool askerdeMi = false)
    {
        if(enerjiMiktari <= 0)
        {
            if (askerdeMi)
                KediOldur(true);
            else 
                KediOldur(false);
        }

        if (Time.time - sonZaman >= .9f)
        {
            sonZaman = Time.time;

            if(enerjiMiktari > 0)
            {
                enerjiMiktari -= (100f / dayaniklilik);
                barObje.GetComponent<HealthBar>().Damage(100f / dayaniklilik);
            }
        }
    }
    private void KediDinlendir()
    {
        if (Time.time - sonZaman >= .9f)
        {
            sonZaman = Time.time;

            if(enerjiMiktari < 100)
            {
                enerjiMiktari += (100f / BuildManager.Instance.DinlenmeMiktari);
                barObje.GetComponent<HealthBar>().Heal(100f / BuildManager.Instance.DinlenmeMiktari);
            }
            else
            {
                enerjiMiktari = 100;
            }
        }
    }
    public void KediOldur(bool artikEkle)
    {
        if (artikEkle)
        {
            GameObject kilic = Instantiate(BuildManager.Instance.kilicPrefabObject, transform.position, Quaternion.identity);
            if (!DontDestroyAudio.Instance.AskerOlmaEffectSource.isPlaying)
                DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.AskerOlmaEffectSource);
            Military.Instance.KediArtigiEkle();
        }
        else
        {
            GameObject hayalet = Instantiate(BuildManager.Instance.hayaletPrefabObject, transform.position, Quaternion.identity);
            DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.CatKillEffectSource);
        }
        DestroySelf();
    }
    
    private void AskereGonder()
    {
        if (enerjiMiktari >= 100)
            KediOldur(true);


        if (Time.time - sonZaman >= .9f)
        {
            sonZaman = Time.time;

            if (enerjiMiktari < 100)
            {
                enerjiMiktari += (100f / GameBalanceValues.MezarlikYokEtmeSuresi());
                barObje.GetComponent<HealthBar>().Heal(100f / GameBalanceValues.MezarlikYokEtmeSuresi());
            }
        }
    }
    private void KediyiKonumaHareketEttir(Vector3 startPosition, Vector3 targetPosition, CatState state)
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredDuration;

        if (transform.position == targetPosition || percentageComplete > 1)
        {
            elapsedTime = 0f;
            catState = state;
        }
        else
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.SmoothStep(0, 1, percentageComplete));
        }
    }
    private void KedininLayeriniDegistir(Cat cat, int layer)
    {
        cat.catSprite.sortingOrder = layer;
        spriteLayer = layer;
    }
    private void KediyiSagTarafaYurut(Cat cat)
    {
        cat.catSprite.flipX = false;
    }
    private void KediyiSolTarafaYurut(Cat cat)
    {
        cat.catSprite.flipX = true;
    }
    private Vector3 RasgeleKonumBelirle(PolygonCollider2D polygon)
    {
        float x = Random.Range(polygon.bounds.min.x, polygon.bounds.max.x);
        float y = Random.Range(polygon.bounds.min.y, polygon.bounds.max.y);
        return new Vector2(x, y);
    }
    public GameObject GetBarObje()
    {
        return barObje;
    }
    public void DestroySelf()
    {
        switch (catArea)
        {
            case CatArea.Dinlenme:
                Rest.Instance.BarObjeList.Remove(barObje);
                break;
            case CatArea.Sato:
                Castle.Instance.BarObjeList.Remove(barObje);
                break;
            case CatArea.Ureme:
                Mating.Instance.BarObjeList.Remove(barObje);
                break;
            case CatArea.Gol:
                LightHouse.Instance.BarObjeList.Remove(barObje);
                break;
            case CatArea.Asker:
                Military.Instance.BarObjeList.Remove(barObje);
                break;
        }
        Destroy(gameObject);
        Destroy(barObje);
        BuildManager.Instance.allCatList.Remove(this);
    }



    public void SetSaveObject(SaveObject saveObject)
    {
        elapsedTime = saveObject.elapsedTime;
        desiredDuration = saveObject.desiredDuration;
        catState = saveObject.catState;
        catArea = saveObject.catArea;
        isinlanacakBolge = saveObject.isinlanacakBolge;
        startPosition = saveObject.startPosition;
        targetPosition = saveObject.targetPosition;
        spriteLayer = saveObject.spriteLayer;
        askereAlindiMi = saveObject.askereAlindiMi;
    }
    public SaveObject GetSaveObject()
    {
        return new SaveObject
        {
            enerjiMiktari = enerjiMiktari,
            catPosition = catPosition,
            elapsedTime = elapsedTime,
            desiredDuration = desiredDuration,
            catState = catState,
            catArea = catArea,
            isinlanacakBolge = isinlanacakBolge,
            startPosition = startPosition,
            targetPosition = targetPosition,
            spriteLayer = spriteLayer,
            askereAlindiMi = askereAlindiMi
        };
    }

    [System.Serializable]
    public class SaveObject
    {
        public Vector2 catPosition;
        public float enerjiMiktari;
        public float elapsedTime;
        public float desiredDuration;
        public CatState catState;
        public CatArea catArea;
        public CatArea isinlanacakBolge;
        public Vector3 startPosition;
        public Vector3 targetPosition;
        public int spriteLayer;
        public bool askereAlindiMi;
    }
}
