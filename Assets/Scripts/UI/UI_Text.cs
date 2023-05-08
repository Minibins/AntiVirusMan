using TMPro;
using UnityEngine;

public class UI_Text : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI _attackText;
    [SerializeField] private PlayerAttack _attackCount;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _enemyKillsText;
    //[SerializeField] private int _enemyKills;
    private void OnEnable()
    {
        _health.SetActionApplyDamage(RefreshHealthText);
        RefreshHealthText();
        _attackCount.SetActionOnRefreshAmmo(RefreshAttackText);
        RefreshAttackText();
        _gameManager.OnTimer += RefreshTimerText;
        RefreshTimerText();
        _gameManager.OnEnemyDie += RefreshEnemyKillsText;
        RefreshEnemyKillsText();
    }
    private void RefreshHealthText()
    {
        _healthText.text = "Health: " + _health.CurrentHealth;
    }
    private void RefreshAttackText()
    {
        _attackText.text = "Attack: " + _attackCount.Ammo + "/" + _attackCount.MaxAmmo;
    }
    private void RefreshTimerText()
    {
        _timerText.text = _gameManager.TimerText;
    }
    private void RefreshEnemyKillsText()
    {
        _enemyKillsText.text = _gameManager.EnemyDieText;
    }
}
