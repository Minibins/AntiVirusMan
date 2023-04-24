using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int Damage;
    [SerializeField] private bool IsLaser;
    private void FixedUpdate()
    {
        if (IsLaser == true)
        {
            gameObject.transform.Translate(speed, 0, 0);
        }
        else
        {
            gameObject.transform.Translate(0, speed, 0);
        }
    }
}