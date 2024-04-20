using System.Linq;
using UnityEngine;

public class physics2D
{
    public static bool OverlapCircleWithoutTrigger(Vector2 pos, float radius, LayerMask layerMask)
    {
        return Physics2D.OverlapCircleAll(pos, radius, layerMask).Where(c => c.isTrigger == false).Count() > 0;
    }
}