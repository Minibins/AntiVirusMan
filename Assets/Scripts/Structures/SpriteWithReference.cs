using System.Collections.Generic;

using UnityEngine;
[CreateAssetMenu]
public class SpriteReference : ScriptableObject
{
   [SerializeField] public Dictionary<Sprite, Vector3> sprite;
    public Sprite spritse;
}
