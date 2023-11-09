using System;
using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PC : MonoBehaviour
{
    [SerializeField] private int radius;
    [SerializeField] private float speed;
    [SerializeField] private float distanceFromPlayer;
    private Transform playerPosition;
    private GameObject pc;
    private GameObject _player;
    private Health health;
    private Animator animator;
    private Transform rozetka;
    private Animator rozetkaAnim;
    private bool lowchrge;
    public static bool IsFollowing;
    public static bool OnlyBehind;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        playerPosition=_player.transform;
        pc = transform.parent.gameObject;
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

        if (IsFollowing) Following();
    }

    private void Following()
    {
        float playerX = _player.transform.position.x;
        float pcX = pc.transform.position.x;

        Vector3 followToPosition = playerPosition.position;
        followToPosition += ((Vector3.right*Convert.ToInt16(_player.GetComponent<SpriteRenderer>().flipX))-(Vector3.right/2))*distanceFromPlayer;
        Vector3 startPos = new Vector3(2, -2.3f, 0);

        if (lowchrge)
        {
            Move(startPos);
        }
        else
        {
            Move(new Vector3(followToPosition.x, pc.transform.position.y, 0));
        }
    }

    private void Move(Vector3 startPos)
    {
        if(MathF.Abs(playerPosition.position.x-transform.position.x) > distanceFromPlayer/2)
        {
            pc.transform.position =
                Vector2.MoveTowards(pc.transform.position,startPos,speed * Time.deltaTime);
        }
        else if(OnlyBehind)
        {
            pc.transform.position =
                    Vector2.MoveTowards(pc.transform.position,startPos,speed * Time.deltaTime*2);
            if(Vector2.Distance( transform.position,startPos)<0.1f)
            {
                OnlyBehind = false;
            }
        }
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