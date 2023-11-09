using System;
using System.Collections;
using UnityEngine;

public class PC : Follower
{
    [SerializeField] private int radius;
    [SerializeField] private Transform pc;
    private GameObject _player;
    private Health health;
    private Animator animator;
    private Transform rozetka;
    private Animator rozetkaAnim;
    private bool lowchrge;
    public static bool IsFollowing;
    public bool OnlyBehind;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        playerPosition=_player.transform;
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

        if (IsFollowing) Following(
            playerPosition.position+ (((Vector3.right * Convert.ToInt16(_player.GetComponent<SpriteRenderer>().flipX)) - (Vector3.right / 2)) * distanceFromPlayer),
            !lowchrge,pc);
    }
    override private protected void Move(Vector3 startPos,Transform transforme)
    {
        Debug.Log("Проверяю надо ли");
        if(Mathf.Abs(playerPosition.position.x - transforme.position.x) > distanceFromPlayer / 2)
        {
            Debug.Log("Иду");
            transforme.position =
                Vector2.MoveTowards(transforme.position,startPos,speed * Time.deltaTime);
        }
        else if(OnlyBehind)
        {
            transforme.position =
                    Vector2.MoveTowards(transforme.position,startPos,speed * Time.deltaTime * 2);
            if(Vector2.Distance(transforme.position,startPos) < 0.1f)
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