using UnityEngine;
using UnityEngine.UI;

public class StatsItem : MonoBehaviour
{
    [SerializeField] private string _key;
    [SerializeField] protected Sprite _sprite;
    public int _count;
    protected Image _itemSprite;

    private void Start()
    {
        SetSprite();
        _count = PlayerPrefs.GetInt("Database."+_key, 0);
        transform.GetChild(1).gameObject.GetComponent<Text>().text = _count.ToString();

    }

    protected virtual void SetSprite()
    {
        _itemSprite = transform.GetChild(0).gameObject.GetComponent<Image>();
        _itemSprite.sprite = _sprite;
        _itemSprite.color = _count>0 ? Color.white : Color.black;
    }
}