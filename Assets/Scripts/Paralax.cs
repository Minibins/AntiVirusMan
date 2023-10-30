using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Paralax : MonoBehaviour
{
    [SerializeField] private Transform followingTarget;
    [SerializeField, Range(0f, 1f)] private float parallaxStrenght = 0.1f;
    private float actualY;
    [SerializeField] private bool disableVerticalParallax;
    [SerializeField, Range(-1f, 1f)] float IsSkyComponent;
    private Vector3 targetpreviousPosition;
    private GameManager gameManager;
    private void Start()
    {
        actualY = transform.position.y;
        if (!followingTarget)
            followingTarget = Camera.main.transform;

        targetpreviousPosition = followingTarget.position;
        if(IsSkyComponent!=0 ) gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, actualY, transform.position.z);
        var delta = followingTarget.position - targetpreviousPosition;
        
        if (disableVerticalParallax)
            delta.y = 0;

        targetpreviousPosition = followingTarget.position;

        transform.position += delta * parallaxStrenght;
        actualY = transform.position.y;
        switch(IsSkyComponent){
        case 0:
            return;
        }
        transform.position += Vector3.up * (gameManager.min * 60 + gameManager.sec)*IsSkyComponent;
    }
}
