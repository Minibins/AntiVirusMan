using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower:MonoBehaviour,Draggable
{

    [SerializeField] private protected float speed;
    [SerializeField] private protected float distanceFromPlayer;
    private protected Transform playerPosition;
    private bool isDrag;
    private protected void Following(Vector3 followToPosition,bool MoveToPlayer,Transform transforme)
    {
        Debug.Log("Проверяю IsDarg");
        if(!isDrag) { 
            Vector3 startPos = new Vector3(2, -2.3f, 0);
            Debug.Log("Куда идти");
            if (!MoveToPlayer)
        {
            Move(startPos,transforme);
        }
        else
        {
                Debug.Log("Говорю идти");
                Move(new Vector3(followToPosition.x, transforme.position.y, 0),transforme);
        }
        }
        
    }

    virtual private protected void Move(Vector3 startPos,Transform transforme)
    {
        Debug.Log("Проверяю надо ли");
        if(Mathf.Abs(playerPosition.position.x - transforme.position.x) > distanceFromPlayer / 2)
        {
            Debug.Log("Иду");
            transforme.position =
                Vector2.MoveTowards(transforme.position,startPos,speed * Time.deltaTime);
        }
        
    }
    public void OnDrag()
    {
        isDrag = true;
    }
    public void OnDragEnd()
    {
        isDrag = false;
    }
}
