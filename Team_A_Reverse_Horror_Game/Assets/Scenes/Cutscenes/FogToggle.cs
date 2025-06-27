using UnityEngine;

public class FogToggle : MonoBehaviour
{
    public void EnableFog()
    {
        RenderSettings.fog = true;
    }

    public void DisableFog()
    {
        RenderSettings.fog = false;
    }
}
