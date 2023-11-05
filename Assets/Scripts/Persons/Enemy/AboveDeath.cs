using System.Collections;

using UnityEngine;
[RequireComponent(typeof(Health))]
public class AboveDeath : MonoBehaviour
{
	
	private Rigidbody2D Player;
	[SerializeField] private float _correctionY = 0.5f;
	[SerializeField] public float ForcerePulsive;
	[SerializeField] private bool Elite;
	[SerializeField] private int numberOfObjectsToSpawn;
	[SerializeField]private GameObject objectToSpawn;
	private Collider2D spawnArea;
	private Health _health;
	private void Awake()
	{
		_health = GetComponent<Health>();
		Player=GameObject.Find("Player").GetComponent<Rigidbody2D>();
		spawnArea = GameObject.Find("BaakaSpawnArea").GetComponent<Collider2D>();
		
	}
    private void FixedUpdate()
    {
		if(Player.position.y > transform.position.y&&Player.velocity.y<0.5f)
		{
            gameObject.layer = 12;
        }
		else
		{
			gameObject.layer = 11;
		}
    }
    private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.transform==Player.transform && other.transform.position.y > transform.position.y + _correctionY)
		{
			_health.ApplyDamage(_health.CurrentHealth);

			if(!Elite)
			{
				other.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
				other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * ForcerePulsive,ForceMode2D.Impulse);
			}
			else
			{
				_health.gameManager.Antivirus();
                other.gameObject.GetComponent<Move>().SetSpeedMultiplierTemporary(3,6.34f);
				other.transform.position = new Vector3(transform.position.x,15.44f,other.transform.position.z);
                other.gameObject.GetComponent<Rigidbody2D>().velocity=new Vector2(0,ForcerePulsive);
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
                Destroy(gameObject);
            }
		}
	}
	
}