using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] private Transform followingTarget;
    [SerializeField, Range(0f, 1f)] private float parallaxStrenght = 0.1f;
    [SerializeField] private bool disableVerticalParallax;
    Vector3 targetpreviousPosition;
    private void Start()
    {
        if (!followingTarget)
            followingTarget = Camera.main.transform;

        targetpreviousPosition = followingTarget.position;
    }

    private void Update()
    {
        var delta = followingTarget.position - targetpreviousPosition;

        if (disableVerticalParallax)
            delta.y = 0;

        targetpreviousPosition = followingTarget.position;

        transform.position += delta * parallaxStrenght;
    }
}
