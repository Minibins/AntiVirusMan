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
      //  _count = PlayerPrefs.GetInt(_key, 0);

        transform.GetChild(1).gameObject.GetComponent<Text>().text = _count.ToString();

        SetSprite();
    }

    protected virtual void SetSprite()
    {
        _itemSprite = transform.GetChild(0).gameObject.GetComponent<Image>();
        _itemSprite.sprite = _sprite;
    }
}