using UnityEngine;

public class TagDoor : AbstractConduitionDoor
{
    [SerializeField] private string tagName;
    protected override bool EnterCondition(Collider2D other) => Comparetag(other);
    protected override bool ExitCondition(Collider2D other) => Comparetag(other);

    private bool Comparetag(Collider2D other)
    {
        return true; //other.CompareTag(tagName);
    }
}