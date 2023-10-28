using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] images;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = images[Random.Range(0,images.Length)];
    }
}
