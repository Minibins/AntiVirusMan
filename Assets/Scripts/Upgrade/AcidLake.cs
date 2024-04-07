using UnityEngine;
public class AcidLake : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DebuffBank bank;
        if(collision.TryGetComponent<DebuffBank>(out bank))
        {
            bank.AddDebuff(new AcidDebuff());
        }
    }
}
