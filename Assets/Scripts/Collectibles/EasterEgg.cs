using DustyStudios;
using UnityEngine;
public class EasterEgg : Collectible
{
    [SerializeField] private int ID;
    private int i;
    public static bool isEaster = false;
    protected Color color =>new Color(1f,1f,1f,i <= 0 ? 1f : 0.5f);
    private void Start()
    {
        gameObject.SetActive(isEaster);
        if(Save.EggStates.ContainsKey(ID))
            i = Save.EggStates[ID];
        else
        {
            i = 0;
            Save.EggStates[ID] = i;
        }

        foreach(var renderer in GetComponentsInChildren<SpriteRenderer>())
            renderer.color = color;
    }
    public override void Pick(GameObject picker)
    {
        base.Pick(picker);
        Save.EggStates[ID] = ++i;
    }
    public void DestoryEgg()
    {
        Destroy(gameObject);
    }
    [DustyConsoleCommand("easter","restart your current run to play easter event")]
    public static string Easter()
    {
        isEaster = true;
        return "Restart your current run to play easter event";
    }
}