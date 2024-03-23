using System.Linq;

using UnityEngine;

public class FirstAid : Collectible
{
    [System.Serializable] struct EnemyPrefab
    {
        public EnemyTypes type;
        public GameObject prefab;
    }
    [SerializeField] EnemyPrefab[] enemyPrefabs;
    public override void Pick(GameObject picker)
    {
        picker.GetComponent<IHealable>().Heal(1);
        base.Pick(picker);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy;
        if(collision.gameObject.TryGetComponent<Enemy>(out enemy))
        {
            Instantiate(enemyPrefabs.Where(e => e.type == enemy.WhoAmI).FirstOrDefault().prefab,enemy.transform.position,Quaternion.identity).GetComponent<Animator>().Play("FAK");
            Pick(enemy.gameObject);
            Destroy(enemy.gameObject);
        }
    }
}
