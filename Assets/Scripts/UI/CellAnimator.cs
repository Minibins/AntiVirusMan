using UnityEngine;

public class CellAnimator : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Enable()
    {
        anim.SetBool("IsEnabled", true);
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        anim.SetBool("IsEnabled",false);
    }

    private void AfterDisable()
    {
        gameObject.SetActive(false);
    }
}
