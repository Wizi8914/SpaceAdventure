using UnityEngine;

public class RotateSky : MonoBehaviour
{
    public float rotationSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
