using UnityEngine;

public class SuicideAfterTime : MonoBehaviour
{
    [SerializeField] private float Time;
    void Start()
    {
        Destroy(gameObject, Time);
    }
}
