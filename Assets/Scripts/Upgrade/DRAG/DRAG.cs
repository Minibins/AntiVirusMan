using UnityEngine;
public class DRAG : MonoBehaviour,iDraggable
{
    bool isdrgging;
    private Vector2 offset;
    private Rigidbody2D rb;
    private PlayerAttack pa;
    private iDraggable MyScript;
    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        pa = GameObject.FindAnyObjectByType<PlayerAttack>();
        MyScript = GetComponent<iDraggable>();
    }
    public void SetDragging()
    {
        isdrgging = true;
        Invoke(nameof(StaminaConchaeca),0.75f);
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
        if (pa.Ammo > 1&&isdrgging)
        {
            Vector2 curPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            rb.velocity = (curPosition - rb.position) * 10f;
        }
    }

    private void StaminaConchaeca()
    {
        if(!isdrgging) return;
        if(pa.Ammo == 0)
        {
            StopDragging();
            return;
        }
        try
        {
            pa.Ammo--;
        }
        catch
        {
            Awake();
            pa.Ammo--;
        }

        Invoke(nameof(StaminaConchaeca),0.3f);
    }
    public void OnDrag() { }
    public void OnDragEnd() { }
}
