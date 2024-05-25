using System.Collections;
using System.Collections.Generic;

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
    public static List<string> foundReferenceNames = new List<string>();
    private Collider2D ReferencePos 
    { 
        get
        {
            if (referencePos == null && TryGetComponent<Collider2D>(out referencePos)) return referencePos;
            else return null;
        }
    }
    public void OnMouseDown()
    {
        if(!EasterEggsForDummies.isLookingForReferences)
            return;
        float speed = 0;
        if(animator != null)
        {
            speed = animator.speed;
            animator.speed = 0;
        }
        Color color = Color.white;
        if(sprite != null)
        {
            color = sprite.color;
            sprite.color = new(0,61f,1,0.98f);
        }
        Transform glass = EasterEggsForDummies.Glass;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        EasterEggsForDummies.SetGlassPos(sprite == null ? Input.mousePosition : (Vector3)pos,sprite == null);
        CoroutineRunner.instance.StartCoroutine(EasterEggsForDummies.MoveGlassToPos(sprite == null ? GetCenter() : Camera.main.WorldToScreenPoint(GetCenter()),1.2f,false,sprite == null));
        const string HereIsReference = "Here is reference to a ",FoundReference = "You already found that reference to a ";
        if(!foundReferenceNames.Contains(referenceName))
        {
            CoroutineRunner.instance.StartCoroutine(textTyper((HereIsReference + referenceName).ToString()));
            GameObject.FindObjectOfType<PlayerAttack>().Damage.additions.Add(0.5f);
            foundReferenceNames.Add(referenceName);
        }
        else CoroutineRunner.instance.StartCoroutine(textTyper((FoundReference + referenceName).ToString()));
        gameObject.SetActive(false);
        IEnumerator textTyper(string text)
        {
            EasterEggsForDummies.hintText.text = text[0].ToString();
            EasterEggsForDummies.hintText2.text = EasterEggsForDummies.hintText.text;
            while(EasterEggsForDummies.hintText.text != text)
            {
                EasterEggsForDummies.hintText.text += text.Replace(EasterEggsForDummies.hintText.text,"")[0];
                EasterEggsForDummies.hintText2.text = EasterEggsForDummies.hintText.text;
                yield return new WaitForSecondsRealtime(0.04f);
            }
            yield return new WaitForSecondsRealtime(1.5f);
            if(animator != null) animator.speed = speed;
            if(sprite != null) sprite.color = color;
        }

        Vector3 GetCenter()
        {
            if(referencePos!=null) return ReferencePos.bounds.center;
            else return transform.position;
        }
    }
}
