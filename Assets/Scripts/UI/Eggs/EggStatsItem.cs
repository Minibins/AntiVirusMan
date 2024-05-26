using System;
using UnityEngine;
using UnityEngine.UI;

public class EggStatsItem : MonoBehaviour
{
    [SerializeField] private int _key;
    public int _count;
    protected Image _itemSprite;

    public void Start()
    {
        UpdateStats();
    }
    public void UpdateStats()
    {
        if(Save.EggStates.TryGetValue(_key,out _count));
        else
        {
            _count = 0;
            Save.EggStates[_key] = _count;
            Save.SaveField();
        }
        SetInfromation();
    }
    public virtual void SetInfromation()
    {
        _itemSprite = transform.GetChild(0).gameObject.GetComponent<Image>();
        _itemSprite.color = _count > 0 ? Color.white : Color.black;

        transform.GetChild(1).gameObject.GetComponent<Text>().text = _count.ToString();
    }
}
