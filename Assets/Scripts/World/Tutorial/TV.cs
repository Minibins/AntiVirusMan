using UnityEngine;

public class TV : MonoBehaviour
{
    void Start()
    {
        OnID(19);
        OnID(5);
        OnID(20);
    }

    private void OnID(int Id)
    {
        UpgradeButton.UpgradeActions.Add(Id,(HelloWorld));
    }

    // Update is called once per frame
    void HelloWorld()
    {
        print("HelloWorld");
    }
}
