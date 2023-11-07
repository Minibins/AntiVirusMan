using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PC : MonoBehaviour
{
    [SerializeField] private int radius;
    [SerializeField] private float speed;
    [SerializeField] private Transform LeftfollowTo;
    [SerializeField] private Transform RightfollowTo;
    private GameObject pc;
    private GameObject _player;
    private Health health;
    private Animator animator;
    private Transform rozetka;
    private Animator rozetkaAnim;
    private bool lowchrge;
    public bool IsFollowing = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        pc = GameObject.FindGameObjectWithTag("PC");
        health = GetComponentInParent<Health>();
        animator = GetComponentInParent<Animator>();
        rozetka = GameObject.Find("Rozetka").transform;
        rozetkaAnim = rozetka.GetComponent<Animator>();
        StartCoroutine(LowCharge());
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(rozetka.position, transform.position) > radius)
        {
            rozetkaAnim.SetBool("Sad", true);
            lowchrge = true;
        }
        else
        {
            rozetkaAnim.SetBool("Sad", false);
            lowchrge = false;
        }

        if(IsFollowing)
            Following();
    }

    private void Following()
    {
        float playerX = _player.transform.position.x;
        float pcX = pc.transform.position.x;
        
        Vector3 followToPosition = _player.GetComponent<SpriteRenderer>().flipX ? RightfollowTo.position : LeftfollowTo.position;
        Vector3 startPos = new Vector3(2, -2.3f, 0);
        
        if (_player.transform.position.y >= 1f)
        {
            pc.transform.position =
                Vector2.MoveTowards(pc.transform.position, startPos, speed * Time.deltaTime);
            return;
        }

        if (playerX < -3.5f || playerX > 5.5f || pcX < -4.4f || pcX > 6.5f)
        {
            pc.transform.position =
                Vector2.MoveTowards(pc.transform.position, startPos, speed * Time.deltaTime);
            return;
        }
        
        Vector2 pos = Vector2.MoveTowards(pc.transform.position,
            new Vector2(followToPosition.x, pc.transform.position.y), speed * Time.deltaTime);
        pc.transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            animator.SetTrigger("NoCalm");
        }
    }

    public void EnemyKilled()
    {
        animator.SetTrigger("HeKilledEnemy");
    }


    IEnumerator LowCharge()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            while (lowchrge)
            {
                health.CurrentHealth--;
                health.ApplyDamage(0);
                yield return new WaitForSeconds(5);
            }
        }
    }
}