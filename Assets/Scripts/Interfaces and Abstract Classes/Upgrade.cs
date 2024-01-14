using UnityEngine;
[System.Serializable] public class Upgrade : MonoBehaviour
{
    public Sprite Sprite;
    public int ID;
    private void Start()
    {
        var Actions = UpgradeButton.UpgradeActions;
        if(Actions.ContainsKey(ID)) throw new System.Exception("��� ���� ���������� )=");
        UpgradeButton.UpgradeActions.Add(ID,new System.Action(OnTake));
    }
    protected virtual void OnTake()
    {
        //������� �����
    }
}
