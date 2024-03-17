using UnityEngine;

public class SonicRing : Collectible
{
    [SerializeField] float multipler, startSpeed;
    private void Awake()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized*startSpeed;
    }
    public override void Pick(GameObject picker)
    {
        MoveBase move;
        if(picker.TryGetComponent<MoveBase>(out move))
            move.SetSpeedMultiplierTemporary(multipler,2);
        base.Pick(picker);
    }
}
