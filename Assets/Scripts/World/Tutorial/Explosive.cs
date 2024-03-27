using UnityEngine;

public class Explosive : MonoBehaviour, IDamageble
{
    [SerializeField] GameObject Explosion;
    public void OnDamageGet(float Damage,IDamageble.DamageType type)
    {
        Instantiate(Explosion,transform.position,Quaternion.identity);
        Level.EXP += 3;
        Destroy(gameObject);
        const string ppname = "EnemyToochaTutorial";
        PlayerPrefs.SetInt(ppname,PlayerPrefs.GetInt(ppname,0) + 1);
    }
}
