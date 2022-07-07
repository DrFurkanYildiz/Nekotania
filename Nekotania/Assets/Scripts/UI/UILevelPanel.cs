using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Gui;
using TMPro;
using Lean.Localization;

public class UILevelPanel : MonoBehaviour
{
    public static UILevelPanel PanelCreate(MerkezlerBase merkezlerBase)
    {
        Transform PanelObject = Instantiate(BuildManager.Instance.LevelPanelPrefab);
        PanelObject.SetParent(BuildManager.Instance.UIPanelSafeAreaTransform, false);
        PanelObject.GetComponent<LeanWindow>().TurnOn();
        UILevelPanel panel = PanelObject.GetComponent<UILevelPanel>();
        UIScript.Instance.UILevelPanel = panel;
        panel.MerkezAdiText.GetComponent<LeanLocalizedTextMeshPro>().TranslationName = panel.GetTranslationName(merkezlerBase);
        panel.MerkezGoreviText.GetComponent<LeanLocalizedTextMeshPro>().TranslationName = panel.GetTranslationMission(merkezlerBase);
        panel.GetLevelAndUpgradeLocalizedText();
        panel.BolumIcerikleriniAktiflestir(merkezlerBase);
        panel.LevelUpControl(merkezlerBase);
        DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.MerkezAcmaEffectSource);
        panel.UpgradeButton.OnClick.AddListener(()=> 
        {
            merkezlerBase.BaseLevelUpButtonMethod();
            panel.LevelUpControl(merkezlerBase);
        });
        TutorialScript.Instance.allButtons.ForEach(b => b.interactable = false);
        return panel;
    }

    [SerializeField] private TextMeshProUGUI MerkezAdiText;
    [SerializeField] private TextMeshProUGUI MerkezGoreviText;
    [SerializeField] private List<Transform> BolumIcerikleri = new List<Transform>();
    [SerializeField] private TextMeshProUGUI LevelLocalizedText;
    [SerializeField] private TextMeshProUGUI LevelValueText;
    [SerializeField] private TextMeshProUGUI CapacityValueText;
    [SerializeField] private TextMeshProUGUI StaminaValueText;
    [SerializeField] private TextMeshProUGUI RestingValueText;
    [SerializeField] private TextMeshProUGUI FishStorageAmountText;
    [SerializeField] private TextMeshProUGUI MilitaryLootCastlePointAmountText;
    [SerializeField] private TextMeshProUGUI MilitaryLootFishPointAmountText;
    [SerializeField] private TextMeshProUGUI UpgradeLocalizedTextText;
    [SerializeField] private TextMeshProUGUI UpgradeAmountText;
    [SerializeField] private LeanButton UpgradeButton;
    public Transform TutorialPatiTransform;
    void Update()
    {
        if (IsPanelTurnOff())
            PanelTurnOff();
    }

    private string GetTranslationName(MerkezlerBase merkezlerBase)
    {
        if (merkezlerBase is Rest)
            return "DinlenmeMerkezAdiText";
        else if (merkezlerBase is Castle)
            return "SatoMerkezAdiText";
        else if (merkezlerBase is Mating)
            return "UremeMerkezAdiText";
        else if (merkezlerBase is LightHouse)
            return "GolMerkezAdiText";
        else if (merkezlerBase is Military)
            return "MezarlikMerkezAdiText";
        else
            return null;
    }
    private string GetTranslationMission(MerkezlerBase merkezlerBase)
    {
        if (merkezlerBase is Rest)
            return "DinlenmeMerkezGorevText";
        else if (merkezlerBase is Castle)
            return "SatoMerkezGorevText";
        else if (merkezlerBase is Mating)
            return "UremeMerkezGorevText";
        else if (merkezlerBase is LightHouse)
            return "GolMerkezGorevText";
        else if (merkezlerBase is Military)
            return "MezarlikMerkezGorevText";
        else
            return null;
    }
    private void GetLevelAndUpgradeLocalizedText()
    {
        LevelLocalizedText.GetComponent<LeanLocalizedTextMeshPro>().TranslationName = "LevelText";
        UpgradeLocalizedTextText.GetComponent<LeanLocalizedTextMeshPro>().TranslationName = "UpgradeText";
    }
    private void BolumIcerikleriniAktiflestir(MerkezlerBase merkezlerBase)
    {
        if (merkezlerBase is Rest)
        {
            BolumIcerikleri[0].gameObject.SetActive(true);
            BolumIcerikleri[1].gameObject.SetActive(true);
            BolumIcerikleri[2].gameObject.SetActive(true);
        }
        else if (merkezlerBase is Castle)
        {
            BolumIcerikleri[2].gameObject.SetActive(true);
            BolumIcerikleri[3].gameObject.SetActive(true);
        }
        else if (merkezlerBase is Mating)
        {
            BolumIcerikleri[2].gameObject.SetActive(true);
            BolumIcerikleri[4].gameObject.SetActive(true);
        }
        else if (merkezlerBase is LightHouse)
        {
            BolumIcerikleri[2].gameObject.SetActive(true);
            BolumIcerikleri[5].gameObject.SetActive(true);
            BolumIcerikleri[6].gameObject.SetActive(true);
        }
        else if (merkezlerBase is Military)
        {
            BolumIcerikleri[7].gameObject.SetActive(true);
            BolumIcerikleri[8].gameObject.SetActive(true);
            BolumIcerikleri[9].gameObject.SetActive(true);
        }
    }
    private void LevelUpControl(MerkezlerBase merkezlerBase)
    {
        if(merkezlerBase.MerkezSeviyesi < merkezlerBase.MaxBaseLevel)
        {
            UpgradeButton.interactable = true;
            if(merkezlerBase is Rest)
            {
                CapacityValueText.text = BuildManager.Instance.NufusKapasitesi.ToString() + " + " + "<color=#CE583C>" +
                    GameBalanceValues.BaseCapacityIncreaseAmount(merkezlerBase.MyMerkezType, merkezlerBase.MerkezSeviyesi).ToString() + "</color>";
            }
            else
            {
                CapacityValueText.text = merkezlerBase.MerkezKapasitesi.ToString() + " + " + "<color=#CE583C>" +
                    GameBalanceValues.BaseCapacityIncreaseAmount(merkezlerBase.MyMerkezType, merkezlerBase.MerkezSeviyesi).ToString() + "</color>";
            }
            UpgradeAmountText.text = "<sprite=2> " + GameBalanceValues.BaseForLevelUpAmount(merkezlerBase.MerkezSeviyesi).ToString();
            StaminaValueText.text = BuildManager.Instance.DayaniklilikMiktari.ToString() + " + " + "<color=#CE583C>" +
                (GameBalanceValues.StaminaIncreaseAmount(merkezlerBase.MerkezSeviyesi)).ToString() + "sn" + "</color>";
            FishStorageAmountText.text = BuildManager.Instance.YiyecekKapasitesi.ToString() + " + " +
                "<color=#CE583C>" + GameBalanceValues.YiyecekKapasitesiArttirmaMiktari().ToString() + "</color>" + " <sprite=1>";

            if (merkezlerBase is Military)
            {
                if (merkezlerBase.MerkezSeviyesi < 4)
                {
                    MilitaryLootFishPointAmountText.text = Military.Instance.KediArtigiYiyecekPuani.ToString() +
                    " + " + "<color=#CE583C>" + GameBalanceValues.KediArtigiYiyecekArtisMiktari(merkezlerBase.MerkezSeviyesi).ToString() + "</color>" + " <sprite=1>";
                }
                else
                {
                    MilitaryLootFishPointAmountText.text = Military.Instance.KediArtigiYiyecekPuani.ToString() + " <sprite=1>";
                }
            }
            MilitaryLootCastlePointAmountText.text = Military.Instance.KediArtigiSatoPuani.ToString() +
                " + " + "<color=#CE583C>" + GameBalanceValues.KediArtigiArtisMiktari(merkezlerBase.MerkezSeviyesi).ToString() + "</color>" + " <sprite=2>";
            
        }
        else
        {
            if(merkezlerBase is Rest)
            {
                CapacityValueText.text = BuildManager.Instance.NufusKapasitesi.ToString();
            }
            else
            {
                CapacityValueText.text = merkezlerBase.MerkezKapasitesi.ToString();
            }
            StaminaValueText.text = BuildManager.Instance.DayaniklilikMiktari.ToString() + "sn";
            FishStorageAmountText.text = BuildManager.Instance.YiyecekKapasitesi.ToString();
            MilitaryLootCastlePointAmountText.text = Military.Instance.KediArtigiSatoPuani.ToString() + " <sprite=2>";
            MilitaryLootFishPointAmountText.text = Military.Instance.KediArtigiYiyecekPuani.ToString() + " <sprite=1>";
            UpgradeAmountText.text = "Max";
            UpgradeButton.interactable = false;
        }
        RestingValueText.text = BuildManager.Instance.DinlenmeMiktari.ToString() + "sn";
        LevelValueText.text = merkezlerBase.MerkezSeviyesi + "/" + merkezlerBase.MaxBaseLevel.ToString();
    }
    
    public void PanelTurnOff()
    {
        DontDestroyAudio.Instance.SesDevamEt();
        TutorialScript.Instance.allButtons.ForEach(b => b.interactable = true);
        GameManager.Instance.ChangeState(GameState.Continue);
        GetComponent<LeanWindow>().TurnOff();
        UIScript.Instance.UILevelPanel = null;
        Destroy(gameObject, .2f);
    }
    private bool IsPanelTurnOff()
    {
        return Input.GetMouseButtonDown(0) && !Helpers.IsOverUI() && TutorialScript.Instance.isTutorialFinished;
    }
}
