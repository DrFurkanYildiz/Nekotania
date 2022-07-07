using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Gui;

public class PuzzleHandler : MonoBehaviour
{
    public enum PuzzleState
    {
        IsLoked,
        UnLoked,
        Draging,
        Drop,
        Wait
    }
    public PuzzleState State;
    [SerializeField] private LeanDrag drag;
    [SerializeField] private LeanSnap snap;
    [SerializeField] private LeanConstrainToParent leanConstrainToParent;
    [SerializeField] private LeanButton lokedButton;
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private Vector2 targetPosition;
    [SerializeField] private Vector2 startSize;
    public int priceAmount;
    public int PuzzleIndex;
    private Image puzzleImage;
    void Start()
    {
        startPosition = transform.localPosition;
        puzzleImage = GetComponent<Image>();
    }
    void Update()
    {
        switch (State)
        {
            case PuzzleState.IsLoked:
                PieceLoced();
                break;
            case PuzzleState.UnLoked:
                PieceUnloked();
                break;
            case PuzzleState.Draging:
                Draging();
                break;
            case PuzzleState.Drop:
                Drop();
                break;
            case PuzzleState.Wait:
                Wait();
                lokedButton.enabled = false;
                break;
        }
    }
    private void PieceLoced()
    {
        drag.enabled = false;
        snap.enabled = false;
        puzzleImage.color = new Color32(255, 255, 255, 80);

        if (PuzzleManager.Instance.PuzzleHandler == this)
            puzzleImage.material = PuzzleManager.Instance.ImageOutSideMaterial;
        else
            puzzleImage.material = null;
    }
    private void PieceUnloked()
    {
        lokedButton.enabled = false;
        drag.enabled = true;
        snap.enabled = false;
        puzzleImage.color = new Color(255, 255, 255, 255);
        transform.GetComponent<RectTransform>().sizeDelta = startSize;
        puzzleImage.material = null;
    }

    private void Draging()
    {
        snap.enabled = true;
        transform.GetComponent<RectTransform>().sizeDelta = startSize * 2f;
    }
    private void Drop()
    {
        Wait();
        puzzleImage.material = null;

        DontDestroyAudio.Instance.SesEfectiCal(DontDestroyAudio.EffectType.GemiParcaYerlestirmeEffectSource);

        State = PuzzleState.Wait;
    }
    private void Wait()
    {

        if (!PuzzleManager.Instance.PuzzleList.Contains(this))
            PuzzleManager.Instance.PuzzleList.Add(this);

        drag.enabled = false;
        snap.enabled = false;
        transform.localPosition = targetPosition;
        transform.GetComponent<RectTransform>().sizeDelta = startSize * 2f;
        GetComponent<LeanMoveToTop>().enabled = false;
    }


    public void SetSaveObject(SaveObject saveObject)
    {
        State = saveObject.State;
    }
    public SaveObject GetSaveObject()
    {
        return new SaveObject
        {
            State = State,
            PuzzleIndex = PuzzleIndex
        };
    }

    [System.Serializable]
    public class SaveObject
    {
        public PuzzleState State;
        public int PuzzleIndex;
    }

    #region Buton Control Method

    public void DragingEndDropControl()
    {
        if (Vector2.Distance(transform.localPosition, targetPosition) < 60f)
        {
            State = PuzzleState.Drop;
        }
        else
        {
            transform.localPosition = startPosition;
            State = PuzzleState.UnLoked;
        }
    }
    public void PieceDrag()
    {
        State = PuzzleState.Draging;
    }

    public void PuzzleSelected()
    {
        PuzzleManager.Instance.PuzzleHandler = this;
        PuzzleManager.Instance.PuzzleParcaSecmeSesiCal();
        PuzzleManager.Instance.priceAmount.text = priceAmount.ToString() + " <sprite=2>";
    }
    #endregion
}
