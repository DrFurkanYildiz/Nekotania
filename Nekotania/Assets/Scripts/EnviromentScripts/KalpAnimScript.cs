using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KalpAnimScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer kalpSprite;
    [SerializeField] private List<Cat> catList = new List<Cat>();

    private void Update()
    {
        if (catList.Count > 1)
            kalpSprite.enabled = true;
        else
            kalpSprite.enabled = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Cat>(out Cat cat))
        {
            catList.Add(cat);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (catList.Count != 0)
        {
            if (collision.TryGetComponent<Cat>(out Cat cat))
            {
                catList.Remove(cat);
            }
        }
    }
}
