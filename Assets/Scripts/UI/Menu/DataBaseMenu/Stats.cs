using System;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] private StatsItem[] _items;

    [SerializeField] private GameObject Grid;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private bool _isUpgradeStats;
    [SerializeField] private Sprite _unknownSprite;


    private void Start()
    {
        foreach (StatsItem item in _items)
        {
            var _item = Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity, Grid.transform);

            Image itemSprite = _item.transform.GetChild(0).GetComponent<Image>();
            itemSprite.sprite = item.Sprite;

            
            
            if (item.Count > 0)
                itemSprite.color = new Color(255,255,255);
            else if (_isUpgradeStats)
                itemSprite.sprite = _unknownSprite;
            else
                itemSprite.color = new Color(0,0,0);
            
            
            if(_isUpgradeStats)
                _item.transform.GetChild(1).GetComponent<Text>().text = "Killed:" + item.Count;
                
            else
                _item.transform.GetChild(1).GetComponent<Text>().text = "Taken:" + item.Count;
        }
    }
}

[Serializable]
public class StatsItem
{
    public Sprite Sprite;
    public int Count;
}