using UnityEngine;

public class Explosive : MonoBehaviour, IDamageble
{
    [SerializeField] GameObject Explosion;

    public void OnDamageGet(int Damage)
    {
        Instantiate(Explosion,transform.position,Quaternion.identity);
        Level.EXP += 3;
        Destroy(gameObject);
    }
}