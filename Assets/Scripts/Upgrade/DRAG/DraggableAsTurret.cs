using UnityEngine;
public class DraggableAsTurret : MonoBehaviour
{
    void Start()
    {
        if(LevelUP.Items[1].IsTaken)
        {
            gameObject.AddComponent<DRAG>();
        }
    }
}
