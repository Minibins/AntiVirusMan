using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int Damage;
    [SerializeField] private TypeGun typeGun;

    public enum TypeGun
    {
        Laser,
        Player,  
    }
    private void FixedUpdate()
    {
        if (typeGun == TypeGun.Laser)
        {
            gameObject.transform.Translate(speed, 0, 0);
        }
        else if (typeGun == TypeGun.Player)
        {
            gameObject.transform.Translate(0, speed, 0);
        }
    }
}