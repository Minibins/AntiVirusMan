using System.Collections;
using System.Linq;

using UnityEngine;

public class ReferenceItem : MonoBehaviour
{
    [SerializeField] private string referenceName;

    private Collider2D referencePos;
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
        Transform glass = EasterEggsForDummies.Glass;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        glass.position = new(pos.x,pos.y,glass.position.z);
        EasterEggsForDummies.isLookingForReferences = false;
        StartCoroutine(EasterEggsForDummies.MoveGlassToPos(ReferencePos.bounds.center,1.2f,false));
        StartCoroutine(textTyper(("Это отсылка на " + referenceName).ToString()));
        IEnumerator textTyper(string text)
        {
            EasterEggsForDummies.hintText.text = "Э";
            while(EasterEggsForDummies.hintText.text != text)
            {
                EasterEggsForDummies.hintText.text += text.Replace(EasterEggsForDummies.hintText.text,"")[0];
                yield return new WaitForSeconds(0.04f);
            }
        }
    }
}
