using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UretimFirlatmaObje : MonoBehaviour
{
    [SerializeField] private Image[] _uretimTypeImage = new Image[3];
    [SerializeField] private TextMeshProUGUI _uretimMiktariText;
    [SerializeField] private Color arttiColor;
    [SerializeField] private Color azaldiColor;
    private void Start()
    {
        Destroy(gameObject, 1f);
    }
    public void UretimUIOlustur(MerkezlerBase.ProductionType productionType, int? miktar, bool arttir = true)
    {
        _uretimMiktariText.text = miktar.ToString();
        _uretimTypeImage[(int)productionType - 1].enabled = true;

        if (arttir)
            _uretimMiktariText.color = arttiColor;
        else
            _uretimMiktariText.color = azaldiColor;
    }
}
