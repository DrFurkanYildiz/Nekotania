using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using Lean.Gui;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public List<PuzzleHandler> PuzzleList = new List<PuzzleHandler>();
    public PuzzleHandler PuzzleHandler = null;
    [SerializeField] private Transform tumParcalarTransform;
    [SerializeField] private Transform backImageTransform;
    [SerializeField] private Transform finalBackImageTransform;
    public TextMeshProUGUI priceAmount;
    [SerializeField] private LeanButton buyButton;
    public List<PuzzleHandler> AllPuzzleList = new List<PuzzleHandler>();
    public Material ImageOutSideMaterial;
    [SerializeField] private LeanWindow finalGemiPanelLeanWindow;
    public Transform FinalGemiSprite;
    [SerializeField] private bool isVictory;
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        ParcalariSirala();
        PiecesFinishedUpdate();
        PricePanelUpdate();
    }

    public void PuzzleBuying()
    {
        if (PuzzleHandler != null)
        {
            if (IsPriceable(PuzzleHandler))
            {
                Buy(PuzzleHandler);
                PuzzleHandler.State = PuzzleHandler.PuzzleState.UnLoked;
                PuzzleUnSelected();
            }
        }
    }
    private bool IsPriceable(PuzzleHandler puzzle)
    {
        return BuildManager.Instance.ToplamSatoPuani >= puzzle.priceAmount;
    }
    private void Buy(PuzzleHandler puzzle)
    {
        BuildManager.Instance.ToplamSatoPuani -= puzzle.priceAmount;
    }
    public void PuzzleUnSelected()
    {
        PuzzleHandler = null;
    }
    private void ParcalariSirala()
    {
        var newList = PuzzleList.OrderBy(x => x.PuzzleIndex).ToList();
        PuzzleList = newList;

        for (int i = 0; i < PuzzleList.Count; i++)
        {
            PuzzleList[i].transform.SetAsLastSibling();
        }
    }
    private void PiecesFinishedUpdate()
    {
        if (TumParcalarOturduMu() && !isVictory)
        {
            WinTransition();
        }
    }
    private bool TumParcalarOturduMu()
    {
        return PuzzleList.Count == 12;
    }
    private void PricePanelUpdate()
    {
        if (PuzzleHandler == null)
        {
            buyButton.interactable = false;
            priceAmount.text = 0.ToString() + " <sprite=2>";
        }
        else
        {
            if (PuzzleHandler.priceAmount > BuildManager.Instance.ToplamSatoPuani)
                buyButton.interactable = false;
            else
                buyButton.interactable = true;
        }
    }
    public void WinTransition()
    {
        isVictory = true;
        tumParcalarTransform.gameObject.SetActive(false);
        backImageTransform.gameObject.SetActive(false);
        finalBackImageTransform.gameObject.SetActive(true);
        UIScript.Instance.ArkaPlanKarartmaLeanWindow.TurnOff();
        GameManager.Instance.ChangeState(GameState.Continue);
        InputScript.Instance.layerMask = LayerMask.GetMask("Nothing");
        
        FunctionTimer.Create(() => { finalGemiPanelLeanWindow.TurnOff();  FinalGemiSprite.gameObject.SetActive(true); }, 2f);
        FinalGemiSprite.GetComponent<LeanWindow>().TurnOn();
        BuildManager.Instance.allCatList.ForEach(c => c.animator.SetBool("Win", true));
        GameManager.Instance.ChangeState(GameState.Win);
    }

    #region Panel Button Method
    public void PuzzleParcaSecmeSesiCal()
    {
        DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.GemiParcaSecmeEffectSource);
    }
    public void PuzzlePanelOpenMethod()
    {
        UIScript.Instance.EngellemeLeanWindow.TurnOn();
        UIScript.Instance.ArkaPlanKarartmaLeanWindow.TurnOn();
        GameManager.Instance.ChangeState(GameState.Pause);
    }
    public void PuzzlePanelClosedMethod()
    {
        UIScript.Instance.EngellemeLeanWindow.TurnOff();
        UIScript.Instance.ArkaPlanKarartmaLeanWindow.TurnOff();
        GameManager.Instance.ChangeState(GameState.Continue);
        DontDestroyAudio.Instance.SesDevamEt();
    }
    #endregion

}
