using UnityEngine;

public class CellAnimator : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
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

    private void AfterDisableObj()
    {
        _gameObject.SetActive(true);
    }
}
