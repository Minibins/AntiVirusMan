using System;
using System.Collections.Generic;
using System.Linq;

using Unity.VisualScripting;

using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    //��������
    protected Action
        EnterAction,
        StayAction,
        ExitAction;

    //��, ��� ����� � �������
    private List<GameObject> enteredThings = new List<GameObject>();

    protected List<GameObject> EnteredThings
    {
        get
        {
            enteredThings = enteredThings.Where(tning => !tning.IsDestroyed()).ToList();
            return enteredThings;
        }
    }

    //������� ��� ������� ����� ��������
    protected virtual bool EnterCondition(Collider2D other) => true;

    protected virtual bool StayCondition(Collider2D other) => true;

    protected virtual bool ExitCondition(Collider2D other) => true;

    //�������� �� ������ OnCollision(X)2D
    protected virtual void OnSomethingEnter2D(Collider2D other)
    {
        if (EnterCondition(other))
        {
            if (!enteredThings.Contains(other.gameObject))
            {
                enteredThings.Add(other.gameObject);
            }

            if (EnterAction != null)
                EnterAction.Invoke();
        }
    }

    protected virtual void OnSomethingStay2D(Collider2D other)
    {
        if (StayCondition(other) && StayAction != null)
        {
            StayAction.Invoke();
        }
    }

    protected virtual void OnSomethingExit2D(Collider2D other)
    {
        if (ExitCondition(other))
        {
            if (enteredThings.Contains(other.gameObject))
            {
                enteredThings.Remove(other.gameObject);
            }

            if (ExitAction != null)
                ExitAction.Invoke();
        }
    }

    //���������������
    private void OnCollisionEnter2D(Collision2D collision) => OnSomethingEnter2D(collision.collider);
    private void OnTriggerEnter2D(Collider2D collision) => OnSomethingEnter2D(collision);

    private void OnCollisionStay2D(Collision2D collision) => OnSomethingStay2D(collision.collider);
    private void OnTriggerStay2D(Collider2D collision) => OnSomethingStay2D(collision);

    private void OnCollisionExit2D(Collision2D collision) => OnSomethingExit2D(collision.collider);
    private void OnTriggerExit2D(Collider2D collision) => OnSomethingExit2D(collision);
}

public class PlayersCollisionChecker : CollisionChecker
{
    protected override bool EnterCondition(Collider2D other) => IsPlayer(other);
    protected override bool ExitCondition(Collider2D other) => IsPlayer(other);

    private bool IsPlayer(Collider2D other)
    {
        return other.CompareTag("Player");
    }
}

public class BookaCollisionChecker : CollisionChecker
{
    [SerializeField] private float _correctionY = 0.5f;

    protected override bool EnterCondition(Collider2D other)
    {
        return transform.position.y <= other.transform.position.y - _correctionY && !other.isTrigger;
    }

    protected override bool StayCondition(Collider2D other)
    {
        return transform.position.y <= other.transform.position.y - _correctionY && !other.isTrigger;
    }

    protected override bool ExitCondition(Collider2D other)
    {
        return false;
    }
}

public class TagCollisionChecker : CollisionChecker
{
    [SerializeField] private LayerMask layerMask;
    protected override bool EnterCondition(Collider2D other) => CompareMask(other);
    protected override bool ExitCondition(Collider2D other) => CompareMask(other);
    protected override bool StayCondition(Collider2D other) => CompareMask(other);
    private bool CompareMask(Collider2D other) => (layerMask.value & (1 << other.gameObject.layer)) != 0;
}