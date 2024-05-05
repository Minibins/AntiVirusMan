using System.Collections;

using UnityEngine;

public class Collectible : MonoBehaviour, IDamageble
{
    public bool canPick = true, canAttract = true;

    public void OnDamageGet(float Damage,IDamageble.DamageType type)
    {
        StartCoroutine(flyToPlayer());
        IEnumerator flyToPlayer()
        {
            Transform player = GameObject.FindObjectOfType<Player>().transform;
            while (canAttract) 
            {
                transform.position = Vector3.MoveTowards(transform.position,player.position,
            Time.fixedDeltaTime*(transform.position-player.position).magnitude*2.5f);
                yield return new WaitForFixedUpdate();
            }
        }
    }

    public virtual void Pick(GameObject picker)
    {
        canPick = false;
        GetComponent<Animator>().SetTrigger("Pick");
    }
}