using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Localization;
using Unity.VisualScripting;

[CreateAssetMenu]
public class SpriteReference : SpriteContainingSO
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private LocalizedSprite localizedSprite;
    [SerializeField] private ReferenceItem[] _referencePrefabs;
    private Dictionary<CustomRendererSpriteChanger, List<ReferenceItem>> _references = new();
    public override Sprite Sprite
    {
        get => !localizedSprite.IsEmpty ? localizedSprite.LoadAsset() : _sprite; 
    }

    public override void OnSpriteApplied(SpriteRenderer renderer,Image image,CustomRendererSpriteChanger changer)
    {
        if(!LevelUP.IsItemTaken(31)) return;
        _references.Add(changer,new());
        foreach (var _referencePrefab in _referencePrefabs) _references[changer].Add(Instantiate(_referencePrefab,changer.transform));
        Animator animator;
        if(changer.TryGetComponent<Animator>(out animator))
            foreach(var reference in _references[changer]) reference.Animator = animator;
        if(renderer!=null)
            foreach(var reference in _references[changer])  reference.Sprite = renderer;
    }
    public override void OnSpriteRemoved(SpriteRenderer renderer,Image image,CustomRendererSpriteChanger changer)
    {
        if(!LevelUP.IsItemTaken(31)) return;
        try
        {
            foreach(var reference in _references[changer]) if(!reference.IsDestroyed()) Destroy(reference.gameObject);
            _references.Remove(changer);
        }
        catch
        {

        }
    }
}