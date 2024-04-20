using UnityEngine;

public class SecretGuy : Upgrade
{
    private TalkingPerson dialogue;

    private void Awake()
    {
        dialogue = GetComponent<TalkingPerson>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) dialogue.Talk();
    }

    protected override void OnTake()
    {
        base.OnTake();
        Destroy(gameObject);
    }
}