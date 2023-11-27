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
    protected List<GameObject> EnteredThings;
    //������� ��� ������� ����� ��������
    protected virtual bool EnterCondition(Collision2D other)
    {
        return true;
    }
    protected virtual bool StayCondition(Collision2D other)
    {
        return true;
    }
    protected virtual bool ExitCondition(Collision2D other)
    {
        return true;
    }

    //�������� �� ������ OnCollision(X)2D
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if(EnterCondition(other))
        {
            CollisionEnterAction.Invoke();

            if(!EnteredThings.Contains(other.gameObject))
            {
                EnteredThings.Add(other.gameObject);
            }
        }
    }
    protected virtual void OnCollisionStay2D(Collision2D other)
    {
        if(StayCondition(other))
        {
            CollisionStayAction.Invoke();
        }
    }
    protected virtual void OnCollisionExit2D(Collision2D other)
    {
        if(ExitCondition(other))
        {
            CollisionExitAction.Invoke();
            if(EnteredThings.Contains(other.gameObject))
            {
                EnteredThings.Remove(other.gameObject);
            }
        }
    }
}




public class BookaCollisionChecker : CollisionChecker
{
    [SerializeField] private float _correctionY = 0.5f;
    protected override bool EnterCondition(Collision2D other)
    {
        return transform.position.y <= other.transform.position.y - _correctionY;
    }
    protected override bool StayCondition(Collision2D other)
    {
        return transform.position.y <= other.transform.position.y - _correctionY;
    }
    protected override bool ExitCondition(Collision2D other)
    {
        return false;
    }
}