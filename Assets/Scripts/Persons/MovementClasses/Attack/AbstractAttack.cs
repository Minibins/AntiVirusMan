using UnityEngine;

public class AbstractAttack : MonoBehaviour
{
    [SerializeField] GameObject prefab, attackSound;
    protected PlayerAttack playerAttack;
    public float LoadTime;
    public int AmmoCost;
    public bool isUsingJoystick, allowOtherAttacks;
    protected virtual void Awake()
    {
        playerAttack = GameObject.FindObjectOfType<PlayerAttack>();
    }
    virtual protected Vector2 spawnPos
    {
        get => transform.position;
    }
    protected virtual GameObject attack()
    {
        if (attackSound != null) Instantiate(attackSound);
        return Instantiate(prefab, spawnPos, Quaternion.identity);
    }
    public void Attack(float time)
    {
        Invoke(nameof(attack), time);
    }
}