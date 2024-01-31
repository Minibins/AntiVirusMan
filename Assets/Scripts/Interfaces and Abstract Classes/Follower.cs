using TMPro;

using UnityEngine;
public class Follower:MonoBehaviour,iDraggable
{

    [SerializeField] private protected float speed;
    [SerializeField] private protected float distanceFromPlayer;
    private protected Transform playerPosition;
    private bool isDrag;
    private protected Rigidbody2D rb;
    virtual protected private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    virtual private protected void Update()
    {
        Following(playerPosition.position,true,transform);
    }
    private protected void Following(Vector3 followToPosition,bool MoveToPlayer,Transform transforme)
    {
        if(!isDrag) { 
            Vector3 startPos = new Vector3(2, -2.3f, 0);
            if (!MoveToPlayer)
        {
            Move(startPos,transforme);
        }
        else
        {
                Move(followToPosition,transforme);
        }
        }
        
    }

    virtual private protected void Move(Vector3 startPos,Transform transforme)
    {
        if(FarPlayer(startPos,transforme))
        {
            Vector3 direction = (startPos - transforme.position).normalized;
            rb.velocity = direction * speed;
        }
    }
    private protected bool FarPlayer(Vector3 startPos,Transform transforme)
    {
        return Mathf.Abs(startPos.x - transforme.position.x) > distanceFromPlayer / 2
            || Mathf.Abs(startPos.y - transforme.position.y) > distanceFromPlayer / 2;
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
