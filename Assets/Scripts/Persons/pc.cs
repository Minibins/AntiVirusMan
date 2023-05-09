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
    private void OnEnable()
    {
        _health.OnDeath += GameOver;
    }
    private void GameOver()
    {
        GetComponent<Animator>().SetBool("Lose", true);
        FindFirstObjectByType<GameManager>().TakeDamage(-10f);
    }
    private void OnDisable()
    {
        _health.OnDeath -= GameOver;
    }

}
