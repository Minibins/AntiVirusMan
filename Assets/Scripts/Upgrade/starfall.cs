using System.Collections;
using UnityEngine;

public class starfall : MonoBehaviour
{
    [SerializeField] private float[] range;
    [SerializeField] private GameObject starasset;
    [SerializeField] private int zlayer;
    private GameObject star;
    
    
    private void spawnstar(Vector3 moveto)
    {
        star = Instantiate(starasset, new Vector3(Random.Range(range[0], range[1]), transform.position.y, 0), transform.localRotation);
        star.GetComponent<Fallingstar>().fallvector = Vector3.Scale(star.GetComponent<Fallingstar>().fallvector, moveto/Mathf.Abs(zlayer/200));
       SpriteRenderer starRenderer= star.GetComponent<SpriteRenderer>();
        starRenderer.sortingOrder = -zlayer;
        TrailRenderer starTrail = star.GetComponentInChildren<TrailRenderer>();
        starTrail.sortingOrder = -1-zlayer;
    }
    public IEnumerator spawnStars()
    {
        while(true)
        {
            spawnstar(new Vector3(Random.Range(0,0.4f),Random.Range(-1,-3),0));
            yield return new WaitForSeconds(0.3f);
        }
            
            
    }
}