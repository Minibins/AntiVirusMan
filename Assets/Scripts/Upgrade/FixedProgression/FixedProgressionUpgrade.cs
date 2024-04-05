using UnityEngine.UI;
using UnityEngine;

public class FixedProgressionUpgrade : MonoBehaviour
{
    public int Level;
    public Sprite SelectedSprite,notSelectedSprite;
    public Image Image;
    public virtual void Pick()
    {
        Image.sprite = SelectedSprite;
    }
    private void Start()
    {
        LevelUP.AddFixedProgressionItem(this);
    }
}
