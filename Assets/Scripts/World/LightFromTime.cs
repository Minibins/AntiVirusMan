using System.Collections;
using System.Collections.Generic;

using MathAVM;

using UnityEngine;
using UnityEngine.Tilemaps;

public class LightFromTime : MonoBehaviour
{
    [SerializeField] Color colorStart,colorEnd;
    [SerializeField] int transformationStart,transformationEnd;
    new object renderer;
    void Start()
    {
        
        if(TryGetComponent<SpriteRenderer>(out SpriteRenderer render))
        {
            renderer = render;
        }

        if(TryGetComponent <Tilemap>(out Tilemap tile))
        { 
            renderer=tile;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(renderer is SpriteRenderer)
        {
            SpriteRenderer spriteRenderer = (SpriteRenderer)renderer;
            spriteRenderer.color = MathA.ColorBetweenTwoOther(colorStart,colorEnd,transformationStart,transformationEnd,Timer.time);
        }
        else if(renderer is Tilemap) {
            Tilemap spriteRenderer = (Tilemap)renderer;
            spriteRenderer.color = MathA.ColorBetweenTwoOther(colorStart,colorEnd,transformationStart,transformationEnd,Timer.time);
        }
    }
}
