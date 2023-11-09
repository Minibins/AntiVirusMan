using System.Collections;

using UnityEngine;

public class starfall : MonoBehaviour
{
    [SerializeField] private float[] range;
    [SerializeField] private Sprite[] stars;
    [SerializeField] private GameObject starasset;
    [SerializeField] private float zlayer;
    private GameObject star;
    
    
    private void spawnstar(Sprite starsprite, Vector3 moveto)
    {
        star = Instantiate(starasset, new Vector3(Random.Range(range[0], range[1]), transform.position.y, zlayer), transform.localRotation);
        star.GetComponent<Fallingstar>().fallvector = moveto;
    }
    public IEnumerator spawnStars()
    {
        while(true)
        {
            spawnstar(stars[Random.Range(0,stars.Length)],new Vector3(Random.Range(0,0.4f),Random.Range(-1,-3),0));
            yield return new WaitForSeconds(0.3f);
        }
            
            
    }
}