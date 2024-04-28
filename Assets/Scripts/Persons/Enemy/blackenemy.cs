using UnityEngine;

[RequireComponent(typeof(Health)),
 RequireComponent(typeof(Rigidbody2D)),
 RequireComponent(typeof(RotateToGameobject))]
public class blackenemy : AbstractEnemy
{
    [SerializeField] private float otklonenieOtklonenia, SkorostOtklonenia, Speed;
    private float otklonenie;

    protected override void Awake()
    {
        if (isElite)
            PC.Carma = 7.5f;

        _move = GetComponent<MoveBase>();
        _animator = GetComponent<Animator>();
        base.Awake();
        GetComponent<RotateToGameobject>().Gameobject = _PC.transform;
    }

    protected override void OnEnable()
    {
        _health.OnDeath += () => gameObject.GetComponent<Rigidbody2D>().simulated = true;
        onComputerReach += () => gameObject.GetComponent<Rigidbody2D>().simulated = true;
        base.OnEnable();
        if (isElite)
        {
            _health.OnDeath += () => PC.Carma = 0;
        }
    }

    protected override void OnDisable()
    {
        _health.OnDeath -= () => gameObject.GetComponent<Rigidbody2D>().simulated = true;
        onComputerReach -= () => gameObject.GetComponent<Rigidbody2D>().simulated = true;
        base.OnDisable();
        if (isElite)
        {
            _health.OnDeath -= () => PC.Carma = 0;
        }
    }

    protected override void EnemyMove()
    {
        Vector3 FlyVector = transform.position - _PC.transform.position;
        FlyVector.Normalize();
        FlyVector.x += otklonenieOtklonenia * Mathf.Sin(SkorostOtklonenia * otklonenie);
        ;
        FlyVector *= Speed;
        transform.position += FlyVector;
        otklonenie++;
    }
}