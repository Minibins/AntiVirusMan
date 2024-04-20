using UnityEngine;

public class Transformer : MonoBehaviour, IDamageble
{
    private LaserAttack laserAttack;
    private PlayerAttack attack;
    private Animator Anim;

    private void Start()
    {
        laserAttack = FindObjectOfType<LaserAttack>();
        GameObject player = GameObject.FindWithTag("Player");
        attack = player.GetComponent<PlayerAttack>();
        Anim = player.GetComponent<Animator>();
    }

    public void OnDamageGet(float damage, IDamageble.DamageType type)
    {
        attack.slowdown();
        attack.AddTemporaryAttackSubstitute(laserAttack);
        attack.AddTemporaryAttackSubstitute(laserAttack);
        Anim.SetBool("IsChad", true);
    }
}