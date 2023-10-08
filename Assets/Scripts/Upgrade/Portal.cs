using UnityEngine;


public class Portals : MonoBehaviour
{
    [SerializeField] GameObject secondPortal;
    [SerializeField] bool IsSecondPortal;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (!secondPortal) {
            secondPortal = GameObject.FindGameObjectWithTag("SecondPortal");
                }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!IsSecondPortal)
            {
                other.transform.position = secondPortal.transform.position;
                IsSecondPortal = true;
            }

            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
            secondPortal.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);

            anim.SetTrigger("Exit");
        }
    }

    public void Destroyu()
    {
        Destroy(gameObject);
    }
}