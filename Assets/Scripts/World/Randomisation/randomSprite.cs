using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] images;
    private void Start()
    {
        int spriteID = Random.Range(0,images.Length);
        GetComponent<SpriteRenderer>().sprite = images[spriteID];
       
        if(GetComponentInChildren<TrailRenderer>() != null)
        {
            GetComponentInChildren<TrailRenderer>().startWidth = images[spriteID].rect.width/25;
        }
    }
}
