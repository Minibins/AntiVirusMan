using UnityEngine;
[RequireComponent(typeof(Health)),
    RequireComponent(typeof(Move)),
    RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask _maskWhoKills;
    private GameObject PC;
    private Health _health;
    private Move _move;
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _health = GetComponent<Health>();
        _move = GetComponent<Move>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        PC = GameObject.FindGameObjectWithTag("PC");
        EnemyMove();
    }
    private void EnemyMove()
    {
        if (PC.transform.position.x < transform.position.x)
        {
            _move.MoveHorizontally(-1f);
            _spriteRenderer.flipX = false;
        }
        else
        {
            _move.MoveHorizontally(1f);
            _spriteRenderer.flipX = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((_maskWhoKills.value & (1 << collision.gameObject.layer)) != 0)
        {
            Destroy(gameObject);
        }
    }
}