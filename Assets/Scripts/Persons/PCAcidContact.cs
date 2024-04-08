using UnityEngine;
public class PCAcidContact : MonoBehaviour,IAcidable
{
    PC PC;
    public void OblitCislotoy()
    {
        throw new System.NotImplementedException();
    }

    void Awake()
    {
        PC = GetComponentInChildren<PC>();
    }
    
}