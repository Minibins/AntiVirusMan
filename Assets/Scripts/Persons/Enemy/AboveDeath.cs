using UnityEngine;

[RequireComponent(typeof(Health))]
public class AboveDeath : BookaCollisionChecker
{
    protected Rigidbody2D Player;
    public float ForcerePulsive;
    public bool IsPlatform;

    private Health _health;

    protected virtual void Awake()
    {
        EnterAction += OnPlayerStep;
        _health = GetComponent<Health>();
        Player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Player.position.y < transform.position.y && Player.velocity.y > -0.5f && IsPlatform)
        {
            gameObject.layer = 11;
        }
        else
        {
            gameObject.layer = 12;
        }
    }

    protected override bool EnterCondition(Collider2D other)
    {
        return other.transform == Player.transform && base.EnterCondition(other);
    }

    protected virtual void OnPlayerStep()
    {
        _health.ApplyDamage(_health.CurrentHealth);
        Player.velocity = Vector2.zero;
        Player.AddForce(Vector2.up * ForcerePulsive, ForceMode2D.Impulse);
    }
}