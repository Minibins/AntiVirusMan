using System;

using UnityEngine;
public class Wall : MonoBehaviour, IAcidable, IDamageble
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

    public float DestroyLevel { get => Animator.GetInteger("DestroyStage"); set => animator.SetInteger("DestroyStage", (int)value); }

    bool isDamagable;

    public void OblitCislotoy()
    {
        Animator.SetBool("IsAcid",true);
        isDamagable = true;
    }
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

    public void OnDamageGet(float Damage,IDamageble.DamageType type)
    {
        if(isDamagable)
            DestroyLevel += Damage;
    }
}