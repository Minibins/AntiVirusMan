using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class DRAG : MonoBehaviour,iDraggable
{
    bool isdrgging;
    private Vector2 offset;
    private Rigidbody2D rb;
    private PlayerAttack pa;
    private iDraggable MyScript;
    private void Start()
    {
        if(name == "PC")
        {
            MyScript = GetComponentInChildren<PC>();
        }
        else if(name =="DONBAS_Gun")
        {
            MyScript = GetComponent<PUSHKA>();
        }
        else if(name =="LaserGun")
        {
            MyScript=GetComponent<LaserGun>();
        }
        else if (name== "Movable Wall(Clone)")
        {
            MyScript = this;
        }

        if (CompareTag("Enemy"))
        {
            MyScript = GetComponent<Move>();
        }

        rb = GetComponent<Rigidbody2D>();
        pa = GameObject.Find("Player").GetComponent<PlayerAttack>();
    }

    private void OnMouseDown()
    {
        isdrgging = true;
        StaminaConchaeca();
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        MyScript.OnDrag();
    }

    private void OnMouseUp()
    {
        isdrgging = false;
        MyScript.OnDragEnd();
    }

    private void OnMouseDrag()
    {
        if (pa.Ammo > 0&&isdrgging)
        {
            Vector2 curPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            rb.velocity = (curPosition - rb.position) * 10f;
            
        }

    }

    private void StaminaConchaeca()
    {
        if (isdrgging) {pa.Ammo--;
        
        Invoke(nameof(StaminaConchaeca), 0.75f); }
        if(pa.Ammo == 0)
        {
            isdrgging = false;
        }
    }
    public void OnDrag() { }
    public void OnDragEnd() { }
}
