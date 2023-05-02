using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject SpawnPoinBullet;
    [SerializeField] private GameObject shieldspawnpoint;
    [SerializeField] private GameObject shield;
    [SerializeField] public int Ammo;
    [SerializeField] private int MaxAmmo;
    [SerializeField] private float TimeReload;
    [SerializeField] private float attackRange;
    [SerializeField] private Text AttackText;
    public bool IsSelectedBullet;
    public float Damage;
    private GameObject shields;
    private Animator anim;
    private int rotation;

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(reload());
        AttackText.text = "Attack:" + Ammo.ToString() + "/" + MaxAmmo.ToString();
    }
    public void OnAttack()
    {
        shields = Instantiate(shield, shieldspawnpoint.transform.position, Quaternion.identity);
        shields.GetComponent<Rigidbody2D>().velocity = new Vector2((transform.rotation.y * 2) + 1, 0);
        shields.GetComponent<atackprojectile>().power = Damage;
    }
    public void OnFullAttack()
    {
        Instantiate(Bullet, SpawnPoinBullet.transform.position, Quaternion.identity);
        shields = Instantiate(shield, shieldspawnpoint.transform.position, Quaternion.identity);
        shields.GetComponent<Rigidbody2D>().velocity = new Vector2((transform.rotation.y * 2) + 1, 0);
        shields.GetComponent<atackprojectile>().power = Damage;
    }
    public void shot()
    {
        if (Ammo > 0 && IsSelectedBullet == false)
        {
            anim.SetTrigger("Attack");
            Ammo--;
            AttackText.text = "Attack:" + Ammo.ToString() + "/" + MaxAmmo.ToString();
        }
        else if (Ammo > 0 && IsSelectedBullet == true)
        {
            anim.SetTrigger("FullAttack");
            Ammo--;
            AttackText.text = "Attack:" + Ammo.ToString() + "/" + MaxAmmo.ToString();
        }
    }
    IEnumerator reload()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeReload);
            if (Ammo < MaxAmmo)
            {
                Ammo++;
                AttackText.text = "Attack:" + Ammo.ToString() + "/" + MaxAmmo.ToString();
            }
        }
    }
}