using System.Linq;

using UnityEngine;

public class StepaHedgehog : MonoBehaviour
{
    private void Start()
    {
        Enemy me = GetComponent<Enemy>();
        me._PC = Enemy.Enemies.Where(e => e.WhoAmI != EnemyTypes.Toocha && e.WhoAmI != EnemyTypes.Stepa).FirstOrDefault().gameObject;
        if (me._PC != null)
        {
            me.ResetPC();
        }
    }
}
