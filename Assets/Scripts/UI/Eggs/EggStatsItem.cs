using System;
using UnityEngine;
using UnityEngine.UI;

public class EggStatsItem : MonoBehaviour
{
    [SerializeField] private string _key;
    public int _count;
    protected Image _itemSprite;
    
    public void Start()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        _count = PlayerPrefs.GetInt(_key, 0); 
        SetInfromation();
    }
    
    public virtual void SetInfromation()
    {
        _itemSprite = transform.GetChild(0).gameObject.GetComponent<Image>();
        _itemSprite.color = _count > 0 ? Color.white : Color.black;
        
        transform.GetChild(1).gameObject.GetComponent<Text>().text = _count.ToString();
    }
}
