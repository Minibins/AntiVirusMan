using DustyStudios;

using UnityEngine;

public class Explosive : MonoBehaviour, IDamageble
{
    [SerializeField] GameObject Explosion;
    [SerializeField] string ppname = "EnemyToochaTutorial";
    [SerializeField] int exp = 3;
    public void OnDamageGet(float Damage,IDamageble.DamageType type)
    {
        Explode();
    }
    public void Explode()
    {
        Instantiate(Explosion,transform.position,Quaternion.identity);
        Level.EXP += exp;
        Destroy(gameObject);
        if(ppname != ""&&!DustyConsoleInGame.UsedConsoleInSession)
                PlayerPrefs.SetInt(ppname,PlayerPrefs.GetInt(ppname,0) + 1);
    }
}
