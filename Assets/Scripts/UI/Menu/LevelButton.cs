using UnityEngine;
using UnityEngine.UI;

public class LevelButton : Button
{
    [SerializeField] private int ID;
    [SerializeField] private Sprite Open, Close;
    [SerializeField] private Image stateIndicator;

    protected override void Start()
    {
        base.Start();
        onClick.AddListener(() => MainMenu.instance.LocationMove(ID));

        if (Save.WinLocation >= ID) stateIndicator.sprite = Open;
        else stateIndicator.sprite = Close;
    }
}