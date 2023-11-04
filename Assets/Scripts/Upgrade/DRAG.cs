using UnityEngine;
using System.Collections;

public class DRAG : MonoBehaviour,Draggable
{
    bool isdrgging;
    private Vector2 offset;
    private Rigidbody2D rb;
    private PlayerAttack pa;
    private Draggable MyScript;
    private enum Type
    {
        PC,
        Cannon,
        Lasergun,
        Tower,
        Enemy
    }
    private Type type;
    private void Start()
    {
        if(name == "PC")
        {
            type = Type.PC;
            MyScript = GetComponent<Health>();
        }
        else if(name =="DONBAS_Gun")
        {
            type = Type.Cannon;
            MyScript = GetComponent<PUSHKA>();
        }
        else if(name =="LaserGun")
        {
            type = Type.Lasergun;
            MyScript=GetComponent<LaserGun>();
        }
        else if (name== "Movable Wall(Clone)")
        {

            type = Type.Tower;
            MyScript = this;
        }

        if (CompareTag("Enemy"))
        {
            type = Type.Enemy;
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

    private void FixedUpdate()
    {
        if (isdrgging) { rb.freezeRotation = false;
            

        }
        else { rb.freezeRotation = true; }
        if (Input.touchCount > 0 && pa.Ammo > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                if(type == Type.Cannon)
                {
                    MyScript.OnDrag();
                 
                }
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
                MyScript.OnDragEnd();
                break;
            }
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
}
