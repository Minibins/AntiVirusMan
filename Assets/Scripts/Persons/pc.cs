using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health)),
    RequireComponent(typeof(Animator))]
public class pc : MonoBehaviour
{
    [SerializeField] private Text Health_Text;
    private Health _health;
    private bool _gameOver = false;
    private void Awake()
    {
        _health = GetComponent<Health>();
    }
    private void Update()
    {
        Health_Text.text = "Health:" + _health.CurrentHealth;
        if (_health.CurrentHealth <= 0 && !_gameOver)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        _gameOver = true;
        GetComponent<Animator>().SetBool("Lose", true);
        FindFirstObjectByType<GameManager>().TakeDamage(-10f);
    }

}
