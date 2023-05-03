using UnityEngine;

public class UnityEvents : MonoBehaviour
{

    private Player _player;
    private PlayerAttack _playerAttack;
    private GameManager _gameManager;
    private void Awake()
    {

    }
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        _gameManager = FindAnyObjectByType<GameManager>();
    }
    public void UE_ButtonLeft()
    {
        _player.Left();
    }
    public void UE_ButtonRigth()
    {
        _player.Rigth();
    }
    public void UE_ButtonStop()
    {
        _player.Stop();
    }
    public void UE_ButtonJump()
    {
        _player.Jump();
    }
    public void UE_ButtonDown()
    {
        _player.Down();
    }
    public void UE_ButtonShot()
    {
        _playerAttack.shot();
    }
    public void UE_ButtonSettings(bool open)
    {
        _gameManager.OpenSettings(open);
    }
}
