using UnityEngine;

public class pccontroller : MonoBehaviour
{
    [SerializeField] private KeyCode[] keys;
    private Move mov;
    private PlayerAttack atk;
    private GameManager gm;
    private void Start()
    {
        mov = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();
        atk = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        if (Input.GetKey(keys[0]))
        {
            mov.Left();
        }
        if (Input.GetKey(keys[1]))
        {
            mov.Rigth();
        }
        if (Input.GetKeyDown(keys[2]))
        {
            mov.Jump();
        }
        if (Input.GetKeyDown(keys[4]))
        {
            atk.shot();
        }
    }
}
