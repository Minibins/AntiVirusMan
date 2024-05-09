using UnityEngine;

public class ApplySprite : MonoBehaviour
{
    [SerializeField] SpriteContainingSO sprite;
    void Start()
    {
        GetComponent<CustomRendererSpriteChanger>().Sprite = sprite;
    }
}
