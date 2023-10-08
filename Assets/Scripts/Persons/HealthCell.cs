using UnityEngine;

public class HealthCell : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Enable()
    {
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
