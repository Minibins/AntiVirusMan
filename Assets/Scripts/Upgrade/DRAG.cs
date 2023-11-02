using UnityEngine;
using System.Collections;

public class DRAG : MonoBehaviour
{
    bool isdrgging;
    private Vector2 offset;
    private Rigidbody2D rb;
    private PlayerAttack pa;
    private PUSHKA cannonScript;
    private enum Type
    {
        PC,
        Cannon,
        Lasergun,
        Tower
    }
    private Type type;
    private void Start()
    {
        if(name == "PC")
        {
            type = Type.PC;
        }
        else if(name =="DONBAS_Gun")
        {
            type = Type.Cannon;
            cannonScript = GetComponent<PUSHKA>();
        }
        else if(name =="LaserGun")
        {
            type = Type.Lasergun;
        }
        else if (name== "Movable Wall(Clone)")
        {

            type = Type.Tower;
        }

        rb = GetComponent<Rigidbody2D>();
        pa = GameObject.Find("Player").GetComponent<PlayerAttack>();
    }

    private void OnMouseDown()
    {
        isdrgging = true;
        StaminaConchaeca();
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        if(type == Type.Cannon)
        {
            cannonScript.StopAllCoroutines();
            cannonScript.StartCoroutine(cannonScript.Shoot());
            cannonScript.istemporaryboost = true;
            cannonScript.TimeReload = 0.2f;

        }
    }

    private void OnMouseUp()
    {
        isdrgging = false;
      if(type == Type.Cannon)
            {
                cannonScript.TimeReload = 3f;
            cannonScript.StopAllCoroutines();
            cannonScript.StartCoroutine(cannonScript.Shoot());
        }
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
                    cannonScript.istemporaryboost = true;
                    cannonScript.TimeReload = 0.2f;
                    cannonScript.StopAllCoroutines();
                    cannonScript.StartCoroutine(cannonScript.Shoot());
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
                if(type == Type.Cannon)
                {
                    cannonScript.TimeReload = 3f;
                    cannonScript.StopAllCoroutines();
                    cannonScript.StartCoroutine(cannonScript.Shoot());
                }
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
