using UnityEngine;

public class AnimationOnScan : MonoBehaviour, IScannable
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public virtual void EndScan()
    {
        
    }

    public virtual void StartScan()
    {
        anim.SetBool("IsScan",true);
    }

    public virtual void StopScan()
    {
        anim.SetBool("IsScan",false);
    }
}
