using System.Collections;
using UnityEngine;


    public class backStager : MonoBehaviour
    {
        public void StartCorutin()
        {
            StartCoroutine(Find());
        }

        IEnumerator Find()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    if(enemy != null)  enemy.GetComponent<Health>().backStager = true;
                }
                print("test");
            }
        }
    }