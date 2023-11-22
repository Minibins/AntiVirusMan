using UnityEngine;

public class SaveButtons : MonoBehaviour
{
    [SerializeField] private DraggableButton[] _buttons;
    [SerializeField] private GameObject _buttonsList;

    public void SaveButtonsPosition()
    {
        foreach (DraggableButton button in _buttons)
        {
            button.ResetPosition();
        }
    }
}
