
using Unity.VisualScripting;

using UnityEngine;

public class Transformer : MonoBehaviour, IDamageble
{
    PlayerAttack attack;
    Animator Anim;
    private void Start()
    {

        GameObject player = GameObject.FindWithTag("Player");
        attack = player.GetComponent<PlayerAttack>();
        Anim= player.GetComponent<Animator>();
    }
    public void OnDamageGet(int damage)
    {
        attack.slowdown();
        attack.AttackType = PlayerAttack.attackTypes.Laser;
        Anim.SetBool("IsChad",true);
        UiElementsList.instance.Joysticks.Attack.gameObject.SetActive(true);
    }
}
