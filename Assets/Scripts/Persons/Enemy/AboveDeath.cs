using System.Collections;

using UnityEngine;
[RequireComponent(typeof(Health))]
public class AboveDeath : MonoBehaviour
{
	[SerializeField] LayerMask _deathFromLayers;
	[SerializeField] float _correctionY = 0.5f;
	[SerializeField] private float ForcerePulsive;
	[SerializeField] private bool Elite;
	[SerializeField] private int numberOfObjectsToSpawn;
	[SerializeField]private GameObject objectToSpawn;
	private Collider2D spawnArea;
	private Health _health;
	private void Awake()
	{
		_health = GetComponent<Health>();
		spawnArea = GameObject.Find("BaakaSpawnArea").GetComponent<Collider2D>();
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(((_deathFromLayers.value & (1 << other.gameObject.layer)) != 0) && other.transform.position.y > transform.position.y + _correctionY)
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
				other.gameObject.GetComponent<Move>().SetSpeedMultiplierTemporary(3,6.36f);
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
					Destroy(spawnedObject,6.36f);
                }
                Destroy(gameObject);
            }
		}
	}
	
}