using UnityEngine;

public class SpawnAndDie : MonoBehaviour,IScannable
{
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private bool DestroyHimself = true, rotateObject = true;
    [SerializeField] private float chargeTime = 2f;
    public void Action() => Invoke(nameof(Spawn),chargeTime);

    protected virtual GameObject Spawn()
    {
        Destroy();
        GameObject gameObject = Instantiate(spawnObject,transform.position,Quaternion.identity);
        if(rotateObject)
        {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            Transform spawnTransform = gameObject.transform;
            spawnTransform.localScale = new(
                spawnTransform.localScale.x * (mySprite.flipX ? -1 : 1),
                spawnTransform.localScale.y * (mySprite.flipY ? -1 : 1),
                spawnTransform.localScale.z);
        }
        return gameObject;
    }

    protected void Destroy()
    {
        if(DestroyHimself)
            Destroy(gameObject);
    }


    [SerializeField] private bool OnScan = true;
    public void EndScan()
    {
        if(OnScan)
            Spawn();
    }
    public void StartScan(){}
    public void StopScan(){}
}