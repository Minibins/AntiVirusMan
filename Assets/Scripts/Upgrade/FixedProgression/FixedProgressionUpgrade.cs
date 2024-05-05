using UnityEngine.UI;
using UnityEngine;

public class FixedProgressionUpgrade : MonoBehaviour
{
    public bool isTaken;
    public int Level;
    public Sprite SelectedSprite,notSelectedSprite;
    public Image Image;
    public virtual void Pick()
    {
        isTaken = true;
        Image.sprite = SelectedSprite;
    }
    private void Start() => LevelUP.AddFixedProgressionItem(this);
}
