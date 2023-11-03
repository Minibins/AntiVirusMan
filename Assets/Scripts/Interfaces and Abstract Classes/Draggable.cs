using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Draggable
{
    public void OnDrag()
    {

    }
    public void OnDragEnd()
    {
        OnDrag();
    }
}
