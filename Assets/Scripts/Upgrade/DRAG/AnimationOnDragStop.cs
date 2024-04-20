using UnityEngine;

public class AnimationOnDragStop : MonoBehaviour, iDraggable
{
    [SerializeField] string animationName;
    public void OnDrag(){}

    public void OnDragEnd()
    {
        GetComponent<Animator>().Play(animationName);
    }
}
