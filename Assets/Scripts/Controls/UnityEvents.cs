using System;
using UnityEngine;

public class UnityEvents : MonoBehaviour
{
    private static Player _player;
    private static PlayerAttack _playerAttack;
    private static Settings _settings;
    private static FixedJoystick _joystick;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        _settings = FindAnyObjectByType<Settings>();
    }

    private void FixedUpdate()
    {
        if (Settings.isUsingJoystick)
        {
            if (_joystick == null)
            {
                _joystick = GameObject.FindGameObjectWithTag("FixedJoystick").GetComponent<FixedJoystick>();
            }

            UE_joystick();
        }
    }

    public static void UE_ButtonLeft()
    {
        _player.Left();
    }

    public static void UE_ButtonRigth()
    {
        _player.Rigth();
    }

    public static void UE_joystick()
    {
        if (_joystick.Horizontal < 0)
        {
            _player.Left();
        }
        else if (_joystick.Horizontal > 0)
        {
            _player.Rigth();
        }
        else if (_joystick.Horizontal == 0)
        {
            _player.Stop();
        }
    }

    public static void UE_ButtonDash()
    {
        _player.Dash(0);
    }

    public static void UE_ButtonStop()
    {
        _player.Stop();
    }

    public static void UE_ButtonJump()
    {
        _player.Jump();
    }

    public static void UE_ButtonDown()
    {
        _player.Down();
    }

    public static void UE_ButtonShot()
    {
        _playerAttack.Shot();
    }

    public static void UE_ButtonSettings(bool open)
    {
        _settings.OpenSettings(open);
    }

    public static void UE_StopFly()
    {
        _player.StopJump(true);
    }
}