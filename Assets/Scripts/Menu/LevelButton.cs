using UnityEngine;
using UnityEngine.UI;

public class LevelButton : Button
{

    [SerializeField] int ID;
    [SerializeField] Sprite Open, Close;
    [SerializeField]Image stateIndicator;
    protected override void Start()
    {
        base.Start();
        onClick.AddListener(()=>MainMenu.instance.LocationMove(ID));
        
        if(Save.WinLocation>=ID)    stateIndicator.sprite = Open;
        else                        stateIndicator.sprite = Close;
    }
}
