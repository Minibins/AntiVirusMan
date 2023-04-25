using UnityEngine;

public class Controller : MonoBehaviour
{
    private Move _move;
    private PlayerAttack _playerAttack;
    private GameManager _gameManager;
    private NewInputSystem _newInputSystem;
    private Vector2 _moveValue;
    private void Awake()
    {
        _newInputSystem = new();
    }
    private void Start()
    {
        _move = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();
        _playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        _newInputSystem.Basic.Jump.performed += context => Jump();
        _newInputSystem.Basic.Attack.performed += context => Attack();
        _newInputSystem.Basic.Move.performed += context => Move();
        _newInputSystem.Basic.Move.canceled += context => StopMove();
    }
    private void OnEnable()
    {
        _newInputSystem.Enable();
    }
    private void OnDisable()
    {
        _newInputSystem.Disable();
    }
    private void Move()
    {
        _moveValue = _newInputSystem.Basic.Move.ReadValue<Vector2>();
        if (_moveValue.x < -0.1f)
        {
            _move.Left();
        }
        else if (_moveValue.x > 0.1f)
        {
            _move.Rigth();
        }
        else
        {
            StopMove();
        }
    }
    private void StopMove()
    {
        _move.Stop();
    }
    private void Jump()
    {
        _move.Jump();
    }
    private void Attack()
    {
        _playerAttack.shot();
    }
}
