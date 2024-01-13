using System.Collections.Generic;

using UnityEngine;
using System;

public class CollisionChecker : MonoBehaviour
{   //��������
    protected Action
        CollisionEnterAction,
        CollisionStayAction,
        CollisionExitAction;
    //��, ��� ����� � �������
    protected List<GameObject> EnteredThings=new List<GameObject>();
    //������� ��� ������� ����� ��������
    protected virtual bool EnterCondition(Collider2D other)
    {
        return true;
    }
    protected virtual bool StayCondition(Collider2D other)
    {
        return true;
    }
    protected virtual bool ExitCondition(Collider2D other)
    {
        return true;
    }

    //�������� �� ������ OnCollision(X)2D
    protected virtual void OnSomethingEnter2D(Collider2D other)
    {
        if(EnterCondition(other))
        {
            if(CollisionEnterAction != null)
            CollisionEnterAction.Invoke();

            if(!EnteredThings.Contains(other.gameObject))
            {
                EnteredThings.Add(other.gameObject);
            }
        }
    }
    protected virtual void OnSomethingStay2D(Collider2D other)
    {
        if(StayCondition(other)&&CollisionStayAction!=null)
        {
            CollisionStayAction.Invoke();
        }
    }
    protected virtual void OnSomethingExit2D(Collider2D other)
    {
        if(ExitCondition(other))
        {
            if(CollisionExitAction != null)
                CollisionExitAction.Invoke();
            if(EnteredThings.Contains(other.gameObject))
            {
                EnteredThings.Remove(other.gameObject);
            }
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