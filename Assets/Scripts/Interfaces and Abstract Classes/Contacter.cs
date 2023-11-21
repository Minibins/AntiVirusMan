using UnityEngine;
using UnityEngine.Events;

public class Contacter : MonoBehaviour
{ private enum ContactType
    {
        Trigger,
        Collision,
        Both
    }
    [SerializeField] private ContactType contactType;
    [SerializeField] private UnityEvent onContact;
    [SerializeField] private LayerMask contacterLayer;
    [SerializeField] private string contacterTag;
    [SerializeField] private object methodArgument;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((contactType == ContactType.Collision | contactType == ContactType.Both) && (contacterLayer == collision.gameObject.layer | contacterTag == collision.gameObject.tag))
        {
            methodArgument = collision;
            onContact.Invoke();
                    methodArgument = null;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((contactType == ContactType.Trigger | contactType == ContactType.Both) && (contacterLayer == collision.gameObject.layer | contacterTag == collision.gameObject.tag))
        {
            methodArgument = collision;
            onContact.Invoke();
            methodArgument = null;
        }
    }
}
