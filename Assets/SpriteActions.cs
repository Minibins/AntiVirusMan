using UnityEngine;

public class SpriteActions : MonoBehaviour
{
    SpriteRenderer sprite;
    Transform spritePos;

    public bool StickToFloor;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        spritePos = sprite.transform;
    }
    float lastYPos;
    private void Update()
    {
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down * 99,99,1 << 10);
            if(StickToFloor)
            {
                if(lastYPos == hit.point.y)
                {
                    spritePos.position = hit.point;
                }
            }
            else
            {
                lastYPos = hit.point.y;
                spritePos.localPosition = Vector3.zero;
            }
        }
    }
}
