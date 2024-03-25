using UnityEngine;

public class Controller : UnityEvents
{
    private NewInputSystem _newInputSystem;
    private void Awake()
    {
        _newInputSystem = new NewInputSystem();
    }
    private void Start()
    {
        _newInputSystem.Basic.Jump.performed += context => UE_ButtonJump();
        _newInputSystem.Basic.Jump.canceled += context => UE_StopFly();
        _newInputSystem.Basic.Attack.performed += context => UE_ButtonShot();
        _newInputSystem.Basic.Attack.canceled += context => UE_ButtonStopShot();
        _newInputSystem.Basic.Move.performed += context => Move();
        _newInputSystem.Basic.Move.canceled += context => UE_ButtonStop(); 
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
        var _moveValue = _newInputSystem.Basic.Move.ReadValue<Vector2>();
        UE_JoystickHorizontal(_moveValue.x);
        if(_moveValue.y < -0.1f)
        {
            if(LevelUP.Items[4].IsTaken)
                UE_ButtonDown();
        }
    }
    private void Dash()
    {
        if(LevelUP.Items[10].IsTaken)
        UE_ButtonDash();
    }
}
