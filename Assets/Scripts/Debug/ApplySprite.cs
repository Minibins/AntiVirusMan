using UnityEngine;

public class ApplySprite : MonoBehaviour
{
    [SerializeField] ISprite spriteSo;
    [SerializeField] Sprite sprite;
    void Start()
    {
        if(spriteSo != null) GetComponent<CustomRendererSpriteChanger>().SetSpriteSo(spriteSo);
        if(sprite!=null) GetComponent<CustomRendererSpriteChanger>().SetSprite(sprite);
    }
}
