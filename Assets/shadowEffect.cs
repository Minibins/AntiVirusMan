using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowEffect : MonoBehaviour
{
    private GameObject shadow;
    private SpriteMask shadowRenderer;
    private SpriteRenderer renderer;
    private static Vector3 shadowOffset=new Vector3(0.08F,0,0);
    void Start()
    {
        shadow = new GameObject();
        shadow.transform.parent = transform;
        shadowRenderer = shadow.AddComponent< SpriteMask>();
        shadow.transform.localPosition = shadowOffset;
        renderer=GetComponent<SpriteRenderer>();
        shadow.transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        shadowRenderer.sprite = renderer.sprite;
        shadow.transform.localScale=new Vector3(renderer.flipX?-1: 1,1,1);
    }
}
