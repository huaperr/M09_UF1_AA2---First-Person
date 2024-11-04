using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [Header("Configuración de sensibilidad")]
    public float mouseSensitivity = 100f;
    public float minYAngle = -60f;       
    public float maxYAngle = 60f;        

    private float rotationX = 0f;

    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minYAngle, maxYAngle);
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
