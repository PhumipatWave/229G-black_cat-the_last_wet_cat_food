using UnityEngine;

public class RotateHDRI : MonoBehaviour
{
    public float targetRotate;

    private void Start()
    {
        RenderSettings.skybox.SetFloat("_Rotation", targetRotate);
    }
}
