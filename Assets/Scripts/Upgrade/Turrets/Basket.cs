using Unity.VisualScripting;
using UnityEngine;
public class Basket : TurretLikeUpgrade
{
    [SerializeField] private SpriteRenderer splash;
    [SerializeField] private Sprite[] splashSprites;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!IsTaken) return;
        PassedThoughMesh passedThough;
        if(!collision.TryGetComponent<PassedThoughMesh>(out passedThough))
            passedThough = collision.AddComponent<PassedThoughMesh>();
        Level.EXP += (float)passedThough.Exp/3f;
        Instantiate(splash,collision.transform.position,Quaternion.identity).sprite = splashSprites[passedThough.Exp];
        if(passedThough.Exp == 3)
        {
            passedThough.ReduceEXP();
            PassedThoughMesh.ObjectPassedMesh();
        }
        else
        {
            PassedThoughMesh.ObjectPassedMesh();
            passedThough.ReduceEXP();
        }
    }

    public override void AddDrag()
    {}
}