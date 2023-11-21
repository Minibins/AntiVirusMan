using UnityEngine;

public class Controller : MonoBehaviour
{
    private Player _player;
    private PlayerAttack _playerAttack;
    private InstantiateWall _wall;
    private NewInputSystem _newInputSystem;
    private Vector2 _moveValue;
    private void Awake()
    {
        _newInputSystem = new NewInputSystem();
    }
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        _wall = GameObject.FindGameObjectWithTag("Player").GetComponent<InstantiateWall>();
        _newInputSystem.Basic.Jump.performed += context => Jump();
        _newInputSystem.Basic.Jump.canceled += context => StopFly();
        _newInputSystem.Basic.Attack.performed += context => Attack();
        _newInputSystem.Basic.Move.performed += context => Move();
        _newInputSystem.Basic.Move.canceled += context => StopMove();
        _newInputSystem.Basic.Dash.performed += context => Dash();
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
        if(_moveValue.y < -0.1f)
        {
            _player.Down();
        }
    }
    private void StopMove()
    {
        _player.Stop();
    }
    private void StopFly()
    {
        _player.StopJump(true);
    }
    private void Jump()
    {
        _player.Jump();
        _wall.OnJump();
    }
    private void Attack()
    {
        _playerAttack.Shot();
    }
    private void Dash()
    {
        _player.Dash(0);
    }
}
