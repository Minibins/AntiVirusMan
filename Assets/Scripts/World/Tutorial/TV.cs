using UnityEngine;

public class TV : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        #region
        OnID(19);
        OnID(5);
        OnID(20);
        #endregion
    }
    private void OnID(int Id)
    {
        UpgradeButton.UpgradeActions.Add(Id,(HelloWorld));
    }
    void HelloWorld()
    {
        anim.SetFloat("Speed",1);
    }
}
