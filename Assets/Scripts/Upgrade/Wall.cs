using UnityEngine;
public class Wall : MonoBehaviour, IAcidable
{
    public float TowerHeight = 0;
    [SerializeField] new Collider2D collider;
    private Animator animator;
    public Animator Animator
    {
        get
        {
            if(animator==null) animator =GetComponent<Animator>();
            return animator;
        }
    }
    public void OblitCislotoy()
    {
        Animator.SetBool("IsAcid",true);
        health = gameObject.AddComponent<Health>();
        health.AddMaxHealth(3);
        health.OnApplyDamage = test; //animator.SetInteger("DestroyStage",3 - (int)health.CurrentHealth);
        health.OnDeath += () => animator.SetTrigger("Destroy");
    }
    void test() { print("И так тоже"); }
    Health health;
    private void Start()
    {
        {
            RaycastHit2D hit = Physics2D.Raycast(collider.bounds.min.y * Vector3.up - Vector3.up + transform.position.x * Vector3.right,Vector2.down);
            Wall w;
            if(hit.collider.TryGetComponent<Wall>(out w))
                TowerHeight = w.TowerHeight++;
        }
        TowerHeight += 1;
        if(TowerHeight >= 5)
            InstantiateWall.ClearWalls();
    }
}