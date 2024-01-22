using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class UnityEvents : MonoBehaviour
{
    private static IPlayer _player;
    private static InstantiateWall _wall;
    private static PlayerAttack _playerAttack;
    private static Settings _settings;

    private static readonly int[] CheatToFly = {3, 3, 4, 4, 0, 1, 0, 1, 6, 5};
    private static Queue<int> CheatToFlyQueue;
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _player = player.GetComponent<Player>();
        _playerAttack = player.GetComponent<PlayerAttack>();
        _wall = player.GetComponent<InstantiateWall>();
        _settings = FindAnyObjectByType<Settings>();
        restartFlyCheat();
    }

    private static void restartFlyCheat()
    {
        CheatToFlyQueue = new Queue<int>(CheatToFly.ToList());
    }

    public static void UE_ButtonLeft()
    {
        _player.MoveHorizontally(-1);
        if(CheatToFlyQueue.Peek() != 0) restartFlyCheat();
        else CheatToFlyQueue.Dequeue();
    }

    public static void UE_ButtonRigth()
    {
        _player.MoveHorizontally(1);
        if(CheatToFlyQueue.Peek() != 1) restartFlyCheat();
        else CheatToFlyQueue.Dequeue();
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
        if(CheatToFlyQueue.Peek() != 6) restartFlyCheat();
        else CheatToFlyQueue.Dequeue();

    }

    public static void UE_ButtonStop()
    {
        _player.MoveHorizontally(0);
    }

    public static void UE_ButtonJump()
    {
        _player.Jump();
        _wall.OnJump();
        if(CheatToFlyQueue.Peek() != 3) restartFlyCheat();
        else CheatToFlyQueue.Dequeue();
    }

    public static void UE_ButtonDown()
    {
        _player.Down();
        if(CheatToFlyQueue.Peek() != 4) restartFlyCheat();
        else CheatToFlyQueue.Dequeue();
    }

    public static void UE_ButtonShot()
    {
        _playerAttack.Shot();
        if(CheatToFlyQueue.Peek() != 5) restartFlyCheat();
        else
        { LevelUP.isTaken[15] = true; print("Я читер"); }
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