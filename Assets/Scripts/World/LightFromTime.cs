using DustyStudios.MathAVM;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LightFromTime : MonoBehaviour
{
    [SerializeField] private Color colorStart, colorEnd;
    [SerializeField] private int transformationStart, transformationEnd;
    private new object renderer;

    private void Start()
    {
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer render))
        {
            renderer = render;
        }

        if (TryGetComponent<Tilemap>(out Tilemap tile))
        {
            renderer = tile;
        }
    }
    
    private void FixedUpdate()
    {
        if (renderer is SpriteRenderer)
        {
            SpriteRenderer spriteRenderer = (SpriteRenderer) renderer;
            spriteRenderer.color = MathA.ColorBetweenTwoOther(colorStart, colorEnd, transformationStart,
                transformationEnd, Timer.time);
        }
        else if (renderer is Tilemap)
        {
            Tilemap spriteRenderer = (Tilemap) renderer;
            spriteRenderer.color = MathA.ColorBetweenTwoOther(colorStart, colorEnd, transformationStart,
                transformationEnd, Timer.time);
        }
    }
}