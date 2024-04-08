using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PC : Follower
{
    [SerializeField] private int radius;
    [SerializeField] private Transform pc;
    [SerializeField] private Sprite[] carmaSprites;
    private GameObject _player;
    private Health health;
    private Animator animator;
    private Transform rozetka;
    private Animator rozetkaAnim;
    private bool lowchrge;
    public bool OnlyBehind;
    private static float carma;
    public static bool IsFollowing;

    public static float Carma
    {
        get => carma;
        set
        {
            carma = value;
            GameObject.Find("PC").GetComponentInChildren<PC>().UpdateCarma();
        }
    }

    public Animator Animator { get => animator;}
    public Health Health { get => health;}

    private protected override void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = _player.transform;
        health = GetComponentInParent<Health>();
        animator = GetComponentInParent<Animator>();
        rozetka = GameObject.Find("Rozetka").transform;
        rozetkaAnim = rozetka.GetComponent<Animator>();
        StartCoroutine(LowCharge());
        rb = GetComponentInParent<Rigidbody2D>();
        PC.carma = 7;
        try
        {
            UpdateCarma();
        }
        catch
        { 
            UiElementsList.instance = FindObjectOfType<UiElementsList>();
        }
    
            IsFollowing = false;
    }

    private protected void FixedUpdate()
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
    }

    private protected override void Update()
    {
        if (IsFollowing)
        {
            Following(
                new Vector3(
                    playerPosition.position.x + (Convert.ToInt16(_player.GetComponent<SpriteRenderer>().flipX) - 0.5f) *
                    distanceFromPlayer,
                    pc.transform.position.y,
                    0)
                ,
                !lowchrge, pc);
            animator.SetBool("IsRunning",
                !lowchrge && (Mathf.Abs(playerPosition.position.x - pc.position.x) > distanceFromPlayer / 2) ||
                OnlyBehind);
        }
    }

    private protected override void Move(Vector3 startPos, Transform transforme)
    {
        if (Mathf.Abs(playerPosition.position.x - transforme.position.x) > distanceFromPlayer / 2)
        {
            transforme.position =
                Vector2.MoveTowards(transforme.position, startPos, speed * Time.deltaTime);
        }
        else if (OnlyBehind)
        {
            transforme.position =
                Vector2.MoveTowards(transforme.position, startPos, speed * Time.deltaTime * 2);
            if (Vector2.Distance(transforme.position, startPos) < 0.1f)
            {
                OnlyBehind = false;
            }
        }
    }

    private void UpdateCarma()
    {
        
        var carmaImage = UiElementsList.instance.Counters.Carma;
            switch(Convert.ToInt16(Carma))
            {
                default:
                if(Carma < 0) carmaImage.sprite = carmaSprites[0];
                else carmaImage.sprite = carmaSprites[6];
                break;
                case 1:
                carmaImage.sprite = carmaSprites[1];
                break;
                case 2:
                carmaImage.sprite = carmaSprites[2];
                break;
                case 3:
                carmaImage.sprite = carmaSprites[3];
                break;
                case 4:
                carmaImage.sprite = carmaSprites[4];
                break;
                case 5:
                carmaImage.sprite = carmaSprites[4];
                break;
                case 6:
                carmaImage.sprite = carmaSprites[5];
                break;
                case 7:
                carmaImage.sprite = carmaSprites[6];
                break;
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
        Carma -= 1f;
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