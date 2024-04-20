using UnityEngine;

public class AcidCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IAcidable target;
        if(collision.TryGetComponent<IAcidable>(out target))
            target.OblitCislotoy();
    }
}
