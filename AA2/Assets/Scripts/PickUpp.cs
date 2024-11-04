using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpp : MonoBehaviour
{
    public float pickupDistance = 2f;
    public float minPickupDistance = 1f;
    public float maxPickupDistance = 3f;
    public LayerMask pickupLay;

    public Camera m_camera;
    private GameObject pickedObject;
    private Rigidbody pick_rb;
    private float currentDistance;
    private Vector3 prevPos;

    void Start()
    {
        currentDistance = pickupDistance;
    }

    void Update()
    {
        if (pickedObject == null)
        {
            TryPickup();
        }
        else
        {
            HoldPickedObj();

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            currentDistance = Mathf.Clamp(currentDistance - scroll, minPickupDistance, maxPickupDistance);
        }

        if (Input.GetMouseButtonUp(1) && pickedObject != null)
        {
            Stop();
        }
    }

    void TryPickup()
    {
        Ray ray = new Ray(m_camera.transform.position, m_camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance, pickupLay))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);

            if (Input.GetMouseButtonDown(1))
            {
                pickedObject = hit.transform.gameObject;
                pick_rb = pickedObject.GetComponent<Rigidbody>();

                if (pick_rb != null)
                {
                    currentDistance = hit.distance;
                    prevPos = pickedObject.transform.position;

                    pick_rb.useGravity = false;
                    pick_rb.isKinematic = true;
                }
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * pickupDistance, Color.red);
        }
    }

    void HoldPickedObj()
    {
        if (pick_rb != null)
        {
            Vector3 targetPosition = m_camera.transform.position + m_camera.transform.forward * currentDistance;
            pick_rb.MovePosition(targetPosition);

            prevPos = pickedObject.transform.position;
        }
    }

    void Stop()
    {
        if (pick_rb != null)
        {
            pick_rb.useGravity = true;
            pick_rb.isKinematic = false;

            pickedObject = null;
            pick_rb = null;
        }
    }
}
