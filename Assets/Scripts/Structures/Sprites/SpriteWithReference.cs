using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu]
public class SpriteReference : SpriteContainingSO
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private LocalizedSprite localizedSprite;
    [SerializeField] private ReferenceItem[] _referencePrefabs;
    private Dictionary<CustomRendererSpriteChanger, ReferenceItem> _references = new();
    public override Sprite Sprite
    {
        get => !localizedSprite.IsEmpty ? localizedSprite.LoadAsset() : _sprite; 
    }

    public override void OnSpriteApplied(SpriteRenderer renderer,Image image,CustomRendererSpriteChanger changer)
    {
        foreach (var _referencePrefab in _referencePrefabs) _references.Add(changer, Instantiate(_referencePrefab,changer.transform));
        _references[changer].Animator = changer.GetComponent<Animator>();
        _references[changer].Sprite = renderer;
    }
    public override void OnSpriteRemoved(SpriteRenderer renderer,Image image,CustomRendererSpriteChanger changer)
    {   
        Destroy(_references[changer].gameObject);
        _references.Remove(changer);
    }
}