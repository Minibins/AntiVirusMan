using UnityEngine;

public class UnityEvents : MonoBehaviour
{

    private static Player _player;
    private static PlayerAttack _playerAttack;
    private static GameManager _gameManager;
    private void Awake()
    {

    }
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        _gameManager = FindAnyObjectByType<GameManager>();
    }
    public static void UE_ButtonLeft()
    {
        _player.Left();
    }
    public static void UE_ButtonRigth()
    {
        _player.Rigth();
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
        _playerAttack.shot();
    }
    public static void UE_ButtonSettings(bool open)
    {
        _gameManager.OpenSettings(open);
    }


}
