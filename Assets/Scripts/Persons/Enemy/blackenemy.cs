using UnityEngine;

[RequireComponent(typeof(Health)),
 RequireComponent(typeof(Rigidbody2D)),
 RequireComponent(typeof(RotateToGameobject))]
public class blackenemy : Enemy
{
    [SerializeField] private float otklonenieOtklonenia, SkorostOtklonenia, Speed;
    private float otklonenie;

    protected new void Awake()
    {
        _PC = GameObject.FindGameObjectWithTag("PC");
        GetComponent<RotateToGameobject>().Gameobject = _PC.transform;
        if (isElite)
        {
            PC.Carma = 7.5f;
        }

        _move = GetComponent<MoveBase>();
        _health = GetComponent<Health>();
        _animator = GetComponent<Animator>();
        if (LevelUP.Items[14].IsTaken)
        {
            gameObject.AddComponent<DRAG>();
        }

        if (LevelUP.Items[16].IsTaken)
        {
            AddBookaComponent();
        }
    }

    private new void OnEnable()
    {
        _health.OnDeath += () => gameObject.GetComponent<Rigidbody2D>().simulated = true;
        onComputerReach += () => gameObject.GetComponent<Rigidbody2D>().simulated = true;
        base.OnEnable();
        if (isElite)
        {
            _health.OnDeath += () => PC.Carma = 0;
        }
    }

    private new void OnDisable()
    {
        _health.OnDeath -= () => gameObject.GetComponent<Rigidbody2D>().simulated = true;
        onComputerReach -= () => gameObject.GetComponent<Rigidbody2D>().simulated = true;
        base.OnDisable();
        if (isElite)
        {
            _health.OnDeath -= () => PC.Carma = 0;
        }
    }

    private protected override void EnemyMove()
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