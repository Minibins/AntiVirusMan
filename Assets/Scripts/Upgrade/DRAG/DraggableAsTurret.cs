using UnityEngine;
public class DraggableAsTurret : MonoBehaviour
{
    void Start()
    {
        if(LevelUP.IsItemTaken(1))
        {
            gameObject.AddComponent<DRAG>();
        }
    }
}
