using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
[CreateAssetMenu]
public class SpriteReference : ScriptableObject, ISprite
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private ReferenceItem _referencePrefab;
    private Dictionary<CustomRendererSpriteChanger, ReferenceItem> _references = new();
    public Sprite Sprite => _sprite;

    public void OnSpriteApplied(SpriteRenderer renderer,Image image,CustomRendererSpriteChanger changer)
    {
        _references.Add(changer, Instantiate(_referencePrefab,changer.transform));
        _references[changer].Animator = changer.GetComponent<Animator>();
        _references[changer].Sprite = renderer;
    }
    public void OnSpriteRemoved(SpriteRenderer renderer,Image image,CustomRendererSpriteChanger changer)
    {   
        Destroy(_references[changer].gameObject);
        _references.Remove(changer);
    }
}