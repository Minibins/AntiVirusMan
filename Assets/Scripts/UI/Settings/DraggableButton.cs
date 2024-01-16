using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableButton : MonoBehaviour, IDragHandler
{
    [SerializeField] public string playerPrefsKey;


    private void Start()
    {
        if (PlayerPrefs.HasKey(playerPrefsKey))
        {
            Vector3 savedPosition = StringToVector3(PlayerPrefs.GetString(playerPrefsKey));
            transform.position = savedPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(DraggButtonController.Draggble ) transform.position = Input.mousePosition;
    }

    public void ResetPosition()
    {
        PlayerPrefs.SetString(playerPrefsKey,
            Vector3ToString(new Vector3(transform.position.x, transform.position.y, 0)));
        PlayerPrefs.Save();
    }

    private string Vector3ToString(Vector3 vector)
    {
        return vector.x + ":" + vector.y;
    }

    private Vector3 StringToVector3(string stringValue)
    {
        string[] components = stringValue.Split(':');
        float x = float.Parse(components[0]);
        float y = float.Parse(components[1]);
        float z = 0;
        return new Vector3(x, y, z);
    }
}