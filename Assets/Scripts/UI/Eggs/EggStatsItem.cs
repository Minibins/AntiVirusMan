using System;
using UnityEngine;
using UnityEngine.UI;

public class EggStatsItem : MonoBehaviour
{
    [SerializeField] private string _key;
    protected int _count;
    protected Image _itemSprite;
    
    private void Start()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        _count = PlayerPrefs.GetInt(_key, 0);
        _itemSprite = GetComponent<Image>();
        _itemSprite.color = _count > 0 ? Color.white : Color.black;
    }
    
    public virtual void SetInfromation()
    {
        transform.GetChild(1).gameObject.GetComponent<Text>().text = _count.ToString();
    }
}
