using UnityEngine;

public class AcidCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryToAcid(collision.collider);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryToAcid(collision);
    }

    private static void TryToAcid(Collider2D collision)
    {
        IAcidable target;
        if(collision.TryGetComponent<IAcidable>(out target))
            target.OblitCislotoy();
    }
}
