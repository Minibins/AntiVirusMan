using System;
using UnityEngine;

public class UnityEvents : MonoBehaviour
{
    private static IPlayer _player;
    private static InstantiateWall _wall;
    private static PlayerAttack _playerAttack;
    private static Settings _settings;
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _player = player.GetComponent<Player>();
        _playerAttack = player.GetComponent<PlayerAttack>();
        _wall = player.GetComponent<InstantiateWall>();
        _settings = FindAnyObjectByType<Settings>();
    }

    public static void UE_ButtonLeft()
    {
        _player.MoveHorizontally(-1);
    }

    public static void UE_ButtonRigth()
    {
        _player.MoveHorizontally(1);
    }

    public void UE_JoystickHorizontal(float input)
    {
        _player.MoveHorizontally(input);
    }
    public void UE_JoystickHorizontal()
    {
        _player.MoveHorizontally(UiElementsList.instance.Joysticks.Walk.Horizontal);
    }
    public static void UE_ButtonDash()
    {
        _player.Dash();
    }

    public static void UE_ButtonStop()
    {
        _player.MoveHorizontally(0);
    }

    public static void UE_ButtonJump()
    {
        _player.Jump();
        _wall.OnJump();
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
        _player.StopJump();
    }
}