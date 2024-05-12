using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ReferenceItem : MonoBehaviour
{
    [SerializeField] private string referenceName;
    private Collider2D referencePos;

    private Animator animator;
    public Animator Animator { set => animator = value; }
    private SpriteRenderer sprite;
    public SpriteRenderer Sprite { set => sprite = value; }

    private Collider2D ReferencePos 
    { 
        get
        {
            if (referencePos == null) referencePos = GetComponent<Collider2D>();
            return referencePos; 
        }
    }

    public void OnMouseDown()
    {
        if(!EasterEggsForDummies.isLookingForReferences)
            return;
        float speed = animator.speed;
        animator.speed = 0;
        Color color = Color.white;
        if(sprite != null)
        {
            color = sprite.color;
            sprite.color = new(0,61f,1,0.98f);
        }
        Transform glass = EasterEggsForDummies.Glass;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        EasterEggsForDummies.SetGlassPos(sprite == null? Input.mousePosition : (Vector3)pos, sprite==null);
        CoroutineRunner.instance.StartCoroutine(EasterEggsForDummies.MoveGlassToPos(sprite==null ? ReferencePos.bounds.center : Camera.main.WorldToScreenPoint(ReferencePos.bounds.center),1.2f,false,sprite==null));
        StartCoroutine(textTyper(("Это отсылка на " + referenceName).ToString()));
        IEnumerator textTyper(string text)
        {
            EasterEggsForDummies.hintText.text = "Э";
            EasterEggsForDummies.hintText2.text = EasterEggsForDummies.hintText.text;
            while(EasterEggsForDummies.hintText.text != text)
            {
                EasterEggsForDummies.hintText.text += text.Replace(EasterEggsForDummies.hintText.text,"")[0];
                EasterEggsForDummies.hintText2.text = EasterEggsForDummies.hintText.text;
                yield return new WaitForSecondsRealtime(0.04f);
            }
            yield return new WaitForSecondsRealtime(1.5f);
            animator.speed = speed;
            if(sprite != null)
            {
                sprite.color = color;
            }
        }
    }
}
