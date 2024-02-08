using UnityEngine;

[RequireComponent(typeof(Health)),
 RequireComponent(typeof(Rigidbody2D))]
public class blackenemy : Enemy
{
    [SerializeField] private float otklonenieOtklonenia;
    [SerializeField] private float SkorostOtklonenia;
    [SerializeField] private float Speed;
    private float otklonenie;
    

    new protected private void Awake()
    {
        if(isElite)
        {
            PC.Carma = 7.5f;
        }
        _PC = GameObject.FindGameObjectWithTag("PC");
        _move = GetComponent<MoveBase>();
        _health = GetComponent<Health>();
        _animator = GetComponent<Animator>();
        if(LevelUP.Items[14].IsTaken)
        {
            gameObject.AddComponent<DRAG>();
        }
        if(LevelUP.Items[16].IsTaken)
        {
            AddBookaComponent();
        }
    }

    new private void OnEnable()
    {
        _health.OnDeath += () => gameObject.GetComponent<Rigidbody2D>().simulated = true;
        onComputerReach += () => gameObject.GetComponent<Rigidbody2D>().simulated = true;
        base.OnEnable();
        if(isElite)
        {
            _health.OnDeath += CarmaSetZero;
        }
    }

    new private void OnDisable()
    {
        _health.OnDeath -= () => gameObject.GetComponent<Rigidbody2D>().simulated = true;
        onComputerReach -= () => gameObject.GetComponent<Rigidbody2D>().simulated = true;
        base.OnDisable();
        if(isElite)
        {
            _health.OnDeath -= CarmaSetZero;
        }
    }
    private void CarmaSetZero()
    {
        PC.Carma = 0;
    }

    override protected private void EnemyMove()
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