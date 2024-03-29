using UnityEngine;
public class BookaShampoo : Collectible
{
    public override void Pick(GameObject picker)
    {
        picker.GetComponent<Animator>().Play("BookaHold");
        picker.GetComponent<PlayerAttack>().AddTemporaryAttackSubstitute(picker.GetComponentInChildren<BookaAttack>());
        base.Pick(picker);
    }
}
