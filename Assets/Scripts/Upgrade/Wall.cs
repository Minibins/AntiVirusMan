using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private float Health;

    public void TakeDamageWall(float Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
