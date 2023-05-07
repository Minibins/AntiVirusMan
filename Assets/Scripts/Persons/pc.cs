using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health)),
    RequireComponent(typeof(Animator))]
public class pc : MonoBehaviour
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
    private void Update()
    {
        Health_Text.text = "Health:" + _health.CurrentHealth;
    }
    private void GameOver()
    {
        GetComponent<Animator>().SetBool("Lose", true);
        FindFirstObjectByType<GameManager>().TakeDamage(-10f);
    }

}
