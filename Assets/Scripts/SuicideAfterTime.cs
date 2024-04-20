using UnityEngine;

public class SuicideAfterTime : MonoBehaviour
{
    [SerializeField] private float Time;
    private void Start()
    {
        Destroy(gameObject, Time);
    }
}
