using UnityEngine;


public class Portals : MonoBehaviour
{
    public GameObject secondPortal;
    private Animator anim;
    public bool IsExploding;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (!secondPortal)
        {
            secondPortal = GameObject.FindGameObjectWithTag("SecondPortal");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsExploding)
        {
            if (other.tag == "Player")
            {
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
                secondPortal.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
            }

            secondPortal.GetComponent<Portals>().IsExploding = true;
            other.transform.position = secondPortal.transform.position;
            secondPortal.GetComponent<Animator>().SetTrigger("Exit");
            anim.SetTrigger("Exit");
        }

        IsExploding = true;
    }
}