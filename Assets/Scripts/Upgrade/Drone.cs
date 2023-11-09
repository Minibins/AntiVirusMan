using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Follower
{
    private SpriteRenderer sprite;
    [SerializeField] float howMuchUp;
    override private protected void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        rb=GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    override private protected void Update()
    {
        if(!FarPlayer(playerPosition.position + Vector3.up * howMuchUp,transform))
        {
            if(rb.velocity.x < 0)
            {
                sprite.sortingOrder = 1001;
            }
            else
            {
                sprite.sortingOrder = 998;
            }
        }
        
        Following(playerPosition.position+Vector3.up*howMuchUp,true,transform);
    }
}
