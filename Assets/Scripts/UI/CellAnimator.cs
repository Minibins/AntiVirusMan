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
        anim.SetTrigger("Heal");
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        anim.SetTrigger("Disable");
    }

    private void AfterDisable()
    {
        gameObject.SetActive(false);
    }
}
