using UnityEngine;

public class Baaka : AboveDeath
{

    [SerializeField] private int numberOfObjectsToSpawn;
    [SerializeField]private GameObject objectToSpawn;
    [SerializeField] private GameObject starfallProjectile;
    private Collider2D spawnArea;
    protected override void Awake()
    {
        base.Awake();
        spawnArea = GameObject.Find("SpawnBaakaArea").GetComponent<Collider2D>();
    }
    protected override void OnPlayerStep()
    {
        Player.velocity = new Vector2 (0, ForcerePulsive);
        LoseGame.instance.Antivirus();
        Player.GetComponent<MoveBase>().SetSpeedMultiplierTemporary(3,6.34f);
        Player.position = new Vector2(transform.position.x,15.44f);
        Bounds bounds = spawnArea.bounds;
        for(int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            Vector2 randomPoint = new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
            GameObject spawnedObject = Instantiate(objectToSpawn, randomPoint, Quaternion.identity);
            
            Destroy(spawnedObject,6.34f);
        }
        if(LevelUP.Items[2].IsTaken)
        {
            for(int i2 = 0; i2 < numberOfObjectsToSpawn / 2; i2++)
            {
                Vector2 randomPoin = new Vector2(
                        Random.Range(bounds.min.x, bounds.max.x),
                        Random.Range(bounds.min.y, bounds.max.y)
                    );
                GameObject spawnedObject = Instantiate(starfallProjectile, randomPoin, Quaternion.identity);
                Destroy(spawnedObject,6.34f);
            }
        }
        Destroy(gameObject);
    }
}
