using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class DRAG : MonoBehaviour,iDraggable
{
    bool isdrgging;
    private Vector2 offset;
    private Rigidbody2D rb;
    private PlayerAttack pa;
    private iDraggable MyScript;
    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        pa = GameObject.FindAnyObjectByType<PlayerAttack>();
        MyScript = GetComponent<iDraggable>();
    }
    public void SetDragging()
    {
        isdrgging = true;
        StaminaConchaeca();
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MyScript.OnDrag();
    }
    public void StopDragging()
    {
        isdrgging = false;
        MyScript.OnDragEnd();
    }
    private void OnMouseDown() => SetDragging();
    private void OnMouseUp() => StopDragging();

    private void FixedUpdate()
    {
        if (pa.Ammo > 0&&isdrgging)
        {
            Vector2 curPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            rb.velocity = (curPosition - rb.position) * 10f;
            
        }

    }

    private void StaminaConchaeca()
    {
        if (isdrgging) {
            try
            {
                pa.Ammo--;
            }
            catch
            {
                Start();
                pa.Ammo--;
            }
        
        Invoke(nameof(StaminaConchaeca), 0.75f); }
        if(pa.Ammo == 0)
        {
            StopDragging();
        }
    }
    public void OnDrag() { }
    public void OnDragEnd() { }
}
