using UnityEngine;
using System.Collections;

public class DRAG : MonoBehaviour
{
    bool isdrgging;
    private Vector2 offset;
    private Rigidbody2D rb;
    private PlayerAttack pa;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pa = GameObject.Find("Player").GetComponent<PlayerAttack>();
    }

    private void OnMouseDown()
    {
        isdrgging = true;
        StaminaConchaeca();
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }

    private void OnMouseUp()
    {
        isdrgging = false;
      
    }

    private void OnMouseDrag()
    {
        if (pa.Ammo > 0&&isdrgging)
        {
            Vector2 curPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            rb.velocity = (curPosition - rb.position) * 10f;
        }
    }

    private void Update()
    {
        if (isdrgging) { rb.freezeRotation = false; }
        else { rb.freezeRotation = true; }
        if (Input.touchCount > 0 && pa.Ammo > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isdrgging = true;
                    StaminaConchaeca();
                    offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                    break;

                case TouchPhase.Moved:
                    Vector2 curPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                    rb.velocity = (curPosition - rb.position) * 10f; ;
                    break;

                case TouchPhase.Ended:
                    isdrgging = false;
                    
                    break;
            }
        }
    }
    private void StaminaConchaeca()
    {
        if (isdrgging) {pa.Ammo--;
        
        Invoke(nameof(StaminaConchaeca), 0.5f); }
        if(pa.Ammo == 0)
        {
            isdrgging = false;
        }
    }
}
