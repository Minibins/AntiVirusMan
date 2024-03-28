using UnityEngine;

public class ScannerDebugger : MonoBehaviour, IScannable
{
    public void EndScan()
    {
        print(nameof(EndScan));
    }

    public void StartScan()
    {
        print(nameof(StartScan));
    }

    public void StopScan()
    {
        print(nameof(StopScan));
    }
}
