using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowEffect : MonoBehaviour
{
    private GameObject shadow;
    private SpriteMask shadowRenderer;
    private SpriteRenderer renderere;
    private static Vector3 shadowOffset=new Vector3(0.3F,-0.17f,0);
    void Start()
    {
        shadow = new GameObject();
        shadow.transform.parent = transform;
        shadowRenderer = shadow.AddComponent< SpriteMask>();
        
        if(transform.localScale.x >= 0)
        {
            shadow.transform.localPosition = shadowOffset;
        }
        else
        {
            shadow.transform.localPosition = -shadowOffset;
        }
        renderere=GetComponent<SpriteRenderer>();
        shadow.transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        shadowRenderer.sprite = renderere.sprite;
        shadow.transform.localScale = new Vector3(renderere.flipX ? -1 : 1,1,1);
        shadow.transform.position = transform.position + shadowOffset;
    }
}
