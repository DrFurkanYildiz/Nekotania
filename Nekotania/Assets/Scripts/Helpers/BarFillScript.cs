using UnityEngine;

public class BarFillScript : MonoBehaviour
{
    public const int Bar_Max = 100;

    public float BarAmont;
    public float BarRegenAmont;

    public BarFillScript()
    {
        BarAmont = 0f;
        BarRegenAmont = 0f;
    }

    public void Update()
    {
        BarAmont += BarRegenAmont * Time.deltaTime;
    }
    public void TrySpendBar(int amont)
    {
        if (BarAmont >= amont)
        {
            BarAmont -= amont;
        }
    }
    public float GetBarNormalized()
    {
        return BarAmont / Bar_Max;
    }
}
