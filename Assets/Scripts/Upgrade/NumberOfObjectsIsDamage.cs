using UnityEngine;

public class NumberOfObjectsIsDamage : Upgrade
{
    private PlayerAttack playerAttack;
    private NewInputSystem inputActions;
    [SerializeField] private string[] objectsTag;
    [SerializeField] private float[] multiplerDamage;
    public static bool[] tagAlloved;
    private void Awake()
    {
        tagAlloved=new bool[objectsTag.Length];
        playerAttack=GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
    }
    protected override void OnTake()
    {
        base.OnTake();
        tagAlloved[0]=true;
        InvokeRepeating(nameof(ChangeDamage), 2,2);
        inputActions = new();
        inputActions.Basic.Attack.performed += context => ChangeDamage();
        inputActions.Enable();
    }
    public void ChangeDamage()
    {
        while(playerAttack.Damage.summingMultiplers.Count < 2)
        {
            playerAttack.Damage.summingMultiplers.Add(0f);
        }
        playerAttack.Damage.summingMultiplers[1] = 0f;
        sbyte iterateon=-1;
        foreach (var tag in objectsTag)
        {
            if(!tagAlloved[++iterateon]) {
                continue;
            }
            playerAttack.Damage.summingMultiplers[1]+= GameObject.FindGameObjectsWithTag(tag).Length * multiplerDamage[iterateon];
        }
        Debug.Log(playerAttack.Damage.summingMultiplers[1]);
    }
}