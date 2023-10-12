using UnityEngine;

public class starfall : MonoBehaviour
{
    [SerializeField] private float[] range;
    [SerializeField] private Sprite[] stars;
    [SerializeField] private GameObject starasset;
    public bool IsSpawn = true;
    public GameObject star;
    private int wait = 20;
    private void spawnstar(Sprite starsprite, float Damage, Vector3 moveto)
    {
        star = Instantiate(starasset, new Vector3(Random.Range(range[0], range[1]), transform.position.y, 0), transform.localRotation);
        star.transform.localScale = new Vector3(8, 7, 7);
        star.GetComponent<SpriteRenderer>().sprite = starsprite;
        star.GetComponent<Fallingstar>().fallvector = moveto;
        //star.GetComponent<AtackProjectile>().power = Damage;
    }
    private void FixedUpdate()
    {
        if (wait <= 0 && IsSpawn)
        {
            spawnstar(stars[Random.Range(0, stars.Length)], Random.Range(0, 1f), new Vector3(Random.Range(0, 0.4f), Random.Range(-1, -3), 0));
            wait = 20;
        }
        else
        {
            wait--;
        }
    }
}