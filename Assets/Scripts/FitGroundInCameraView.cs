using UnityEngine;

public class FitGroundInCameraView : MonoBehaviour
{
    public Camera mainCamera;
    public float scaleMultiplier = 0.1f; // Scale Multiplier

    private void Start()
    {
        mainCamera = Camera.main;

        FitGroundCameraView();
    }

    private void FitGroundCameraView()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera reference not set in the inspector!");
            return;
        }

        // Get the screen height in world coordinates
        float screenHeight = 2f * mainCamera.orthographicSize;

        // Get the screen width in world coordinates
        float screenWidth = screenHeight * mainCamera.aspect;

        // Adjust the scale of the ground plane based on screen size
        transform.localScale = new Vector3(screenWidth * scaleMultiplier, 1f, screenHeight * scaleMultiplier);
    }
}
