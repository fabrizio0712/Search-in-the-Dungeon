using System.Collections;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float cameraSensitivity = 1f;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float cameraShakeDuration = 0f;
    [SerializeField] private AnimationCurve cameraShakeCurve;
    [SerializeField] private float cameraShakeAngle = 0f;

    private float xRotation = 0;
    private float yRotation = 0;

    public bool CanMoveCamera = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMoveCamera)
        {
            transform.position = cameraPosition.position;

            float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity * Time.timeScale;
            float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity * Time.timeScale;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
    public void PlayCameraShake() 
    {
        StartCoroutine(Shake());
    }
    private IEnumerator Shake()
    {
        if (CanMoveCamera)
        {
            float currentTime = 0;
            while (currentTime < cameraShakeDuration)
            {
                transform.position = cameraPosition.position + (transform.right - transform.up) * cameraShakeCurve.Evaluate(currentTime / cameraShakeDuration);
                transform.rotation = Quaternion.Euler(xRotation, yRotation, cameraShakeAngle * cameraShakeCurve.Evaluate(currentTime / cameraShakeDuration));
                currentTime += Time.deltaTime;
                yield return null;
            }
            transform.position = cameraPosition.position;
        }
    }
}
