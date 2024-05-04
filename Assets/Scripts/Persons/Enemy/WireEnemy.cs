using UnityEngine;
public class WireEnemy : Enemy
{
    private bool isFallFromWire = false;
    public bool isDefault = false;
    public GameObject MoveToPoint;
    protected override bool canEvolute => base.canEvolute&&isDefault;
    protected override void Awake()
    {
        base.Awake();
        if(_animator != null) _animator.Play("Wire");
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if(isDefault)
            return;
        if(other.CompareTag("WayPoint"))
        {
            MoveToPoint = other.gameObject.GetComponent<WayPoint>()
                .nextPoint[Random.Range(0,other.gameObject.GetComponent<WayPoint>().nextPoint.Length)];
        }
        else if(other.CompareTag("EndWire"))
            ExitWire();
    }
    protected override void EnemyMove()
    {
        if(isDefault) base.EnemyMove(); 
        else _move.MoveOnWire(MoveToPoint);
    }
    private void ExitWire()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        isDefault = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isFallFromWire)
            return;
        _animator.SetTrigger("Corpse");
        isFallFromWire = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(isDefault)
            return;
        if(collision.CompareTag("Way"))
        {
            ExitWire();
            _animator.SetTrigger("Fall");
            isFallFromWire = true;
        }
    }
}
