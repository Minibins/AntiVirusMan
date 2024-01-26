using UnityEngine;

public class BrowserButton : MonoBehaviour
{
    public void OpenWebBrowser(string URL)
    {
        Application.OpenURL(URL);
    }
}
