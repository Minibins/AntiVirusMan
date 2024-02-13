using UnityEngine;
public class Portals : AbstractPortal
{
    private Animator anim;
    public bool IsExploding;
    [SerializeField] int ExplosionSize;
    Vector2Int Explosion;
    private void Start()
    {
        anim = GetComponent<Animator>();
        if (!secondPortal) {
            secondPortal = GameObject.FindGameObjectWithTag("SecondPortal");
                }
        Explosion = new Vector2Int(ExplosionSize,ExplosionSize);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsExploding) {if (other.tag == "Player") 
        { 
             gameObject.GetComponent<BoxCollider2D>().size = Explosion;
            secondPortal.GetComponent<BoxCollider2D>().size = Explosion; 
        }
        secondPortal.GetComponent<Portals>().IsExploding = true;
        Teleport(other.transform);
        secondPortal.GetComponent<Animator>().SetTrigger("Exit");
        anim.SetTrigger("Exit");}
        IsExploding = true;    
        
        
    }
    
}