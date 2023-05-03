using UnityEngine;

public class Controller : MonoBehaviour
{
    private Player _player;
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
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            _player.Left();
        }
        else if (_moveValue.x > 0.1f)
        {
            _player.Rigth();
        }
        else
        {
            StopMove();
        }
    }
    private void StopMove()
    {
        _player.Stop();
    }
    private void Jump()
    {
        _player.Jump();
    }
    private void Attack()
    {
        _playerAttack.shot();
    }
}
