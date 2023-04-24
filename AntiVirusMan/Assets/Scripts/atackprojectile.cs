using UnityEngine;

public class atackprojectile : MonoBehaviour
{
    private GameObject PC;
    public float power;
    private void Start()
    {
        PC = GameObject.FindGameObjectWithTag("PC");
    }
    private void Update()
    {
        if (PC.transform.position.x < transform.position.x + 1 && PC.transform.position.x > transform.position.x - 1 && gameObject.CompareTag("ATACK EVERYBODY"))
        {
            GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gm.TakeDamage(power);
            power=0;
        }
    }
}
