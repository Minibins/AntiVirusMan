using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health)),
    RequireComponent(typeof(Animator))]
public class PC : MonoBehaviour
{
    [SerializeField] private Text Health_Text;
    private Health _health;
    private void Awake()
    {
        _health = GetComponent<Health>();
    }
    private void Start()
    {
        _health.SetActionDeath(GameOver);
    }
    private void GameOver()
    {
        GetComponent<Animator>().SetBool("Lose", true);
        FindFirstObjectByType<GameManager>().TakeDamage(-10f);
    }

}
