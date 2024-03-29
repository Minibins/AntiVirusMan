
using Unity.VisualScripting;

using UnityEngine;

public class Transformer : MonoBehaviour, IDamageble
{
    LaserAttack laserAttack;
    PlayerAttack attack;
    Animator Anim;
    private void Start()
    {
        laserAttack = GameObject.FindObjectOfType<LaserAttack>();
        GameObject player = GameObject.FindWithTag("Player");
        attack = player.GetComponent<PlayerAttack>();
        Anim= player.GetComponent<Animator>();
    }
    public void OnDamageGet(float damage,IDamageble.DamageType type)
    {
        attack.slowdown();
        attack.AddTemporaryAttackSubstitute( laserAttack);
        Anim.SetBool("IsChad",true);
    }
}
