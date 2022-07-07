using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Lean.Gui;

public class UIScript : MonoBehaviour
{
    public static UIScript Instance;
    public GameObject BaseUICanvas;
    public LeanWindow ArkaPlanKarartmaLeanWindow;
    public LeanWindow EngellemeLeanWindow;
    [SerializeField] private TextMeshProUGUI satoPuaniCurrentText;
    [SerializeField] private TextMeshProUGUI satoPuaniMaxText;

    [SerializeField] private TextMeshProUGUI yiyecekPuaniCurrentText;
    [SerializeField] private TextMeshProUGUI yiyecekPuaniMaxText;

    [SerializeField] private TextMeshProUGUI NufusCurrentText;
    [SerializeField] private TextMeshProUGUI NufusMaxText;

    [SerializeField] private TextMeshProUGUI zamanText;
    [SerializeField] private TextMeshProUGUI dayValueText;
    [SerializeField] private List<Image> checkImageList = new List<Image>();

    [SerializeField] private LeanWindow mainMenuModalPanel;
    public Color ButtonPressedColor;
    public Color ButtonDefaultColor;
    public Image PauseButtonImage;
    public Image PlayButtonImage;
    public Image SpeedButtonImage;

    public LeanPulse satoPointPulse;
    public LeanPulse yiyecekPointPulse;
    public LeanPulse nufusPointPulse;

    public TextMeshProUGUI eksilecekMiktarValueText;
    BuildManager manager;

    public UILevelPanel UILevelPanel = null;

    public GameObject FinalGemiExitPanelButton;
    public GameObject FinalGemiBuyButton;
    public GameObject FinalGemiBuyYesButton;
    public GameObject FinalGemiBuyNoButton;
    private void Awake()
    {
        Instance = this;
        manager = GetComponent<BuildManager>();
    }
    private void Update()
    {
        UIPrint();
        SatoPuaniUyariIsaretiUIGoster();
        YiyecekUyariIsaretiUIGoster();
        NufusUyariIsaretiUIGoster();

        for (int i = 0; i < CycleManager.Instance.DonguMiktari; i++)
        {
            checkImageList[i].transform.gameObject.SetActive(true);
        }
        if (CycleManager.Instance.DonguMiktari == 0)
        {
            checkImageList.ForEach(e => e.transform.gameObject.SetActive(false));
        }


        eksilecekMiktarValueText.text = "-" + GameBalanceValues.SatoPuaniEksiltmeMiktari(CycleManager.Instance.DayTime).ToString() + " <sprite=2>" +
            " -" + manager.allCatList.Count.ToString() + " <sprite=1>";

    }

    private void UIPrint()
    {
        satoPuaniCurrentText.text = manager.ToplamSatoPuani.ToString();
        satoPuaniMaxText.text = manager.SatoPuaniKapasitesi.ToString();

        yiyecekPuaniCurrentText.text = manager.ToplamYiyecekMiktari.ToString();
        yiyecekPuaniMaxText.text = manager.YiyecekKapasitesi.ToString();

        NufusCurrentText.text = manager.allCatList.Count.ToString();
        NufusMaxText.text = manager.NufusKapasitesi.ToString();

        zamanText.text = CycleManager.Instance.BaseTime.ToString();
        dayValueText.text = CycleManager.Instance.DayTime.ToString();

    }
    private void SatoPuaniUyariIsaretiUIGoster()
    {
        if (BuildManager.Instance.ToplamSatoPuani > GameBalanceValues.SatoPuaniEksiltmeMiktari(CycleManager.Instance.DayTime))
        {
            satoPointPulse.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().color = Color.white;
            satoPointPulse.RemainingTime = 0f;
            satoPointPulse.enabled = false;
        }
        else
        {
            satoPointPulse.enabled = true;
        }
    }
    private void YiyecekUyariIsaretiUIGoster()
    {

        if (BuildManager.Instance.ToplamYiyecekMiktari > BuildManager.Instance.allCatList.Count)
        {
            yiyecekPointPulse.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().color = Color.white;
            yiyecekPointPulse.RemainingTime = 0f;
            yiyecekPointPulse.enabled = false;

        }
        else
        {
            yiyecekPointPulse.enabled = true;
        }
    }
    private void NufusUyariIsaretiUIGoster()
    {
        if (BuildManager.Instance.NufusKapasitesi > BuildManager.Instance.allCatList.Count)
        {
            nufusPointPulse.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().color = Color.white;
            nufusPointPulse.RemainingTime = 0f;
            nufusPointPulse.enabled = false;
        }
        else
        {
            nufusPointPulse.enabled = true;
        }
    }



}
