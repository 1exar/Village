using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Vector3 cameraOffsetPosition;
    [SerializeField] Vector3 cameraStartPosition;
    [Header("Sensivity")]
    [SerializeField] float xSensivity = 1;
    [SerializeField] float zSensivity = 1;
    [Header("Borders")]
    [SerializeField] Vector2 maxDistance;
    [SerializeField] Vector2 minDistance;

    public bool Draged;

    public bool canDrag = true;
    
    public static CameraController I;

    private void Awake()
    {
        I = this;
    }

    void Start() => cameraTransform.position = cameraStartPosition + cameraOffsetPosition;

    public void OnDrag(PointerEventData eventData)
    {
        if(!canDrag) return;
        float xPos = cameraTransform.position.x;
        float zPos = cameraTransform.position.y;

        if ((xPos - (eventData.delta.x * Time.deltaTime * xSensivity)) < maxDistance.x && (xPos - (eventData.delta.x * Time.deltaTime * xSensivity)) > minDistance.x)
            xPos -= eventData.delta.x * Time.deltaTime * xSensivity;
        else if ((xPos - (eventData.delta.x * Time.deltaTime * xSensivity)) >= maxDistance.x)
            xPos = maxDistance.x;
        else if ((xPos - (eventData.delta.x * Time.deltaTime * xSensivity)) <= minDistance.x)
            xPos = minDistance.x;

        if ((zPos - (eventData.delta.y * Time.deltaTime * zSensivity)) < maxDistance.y && (zPos - (eventData.delta.y * Time.deltaTime * zSensivity)) > minDistance.y)
            zPos -= eventData.delta.y * Time.deltaTime * zSensivity;
        else if ((zPos - (eventData.delta.y * Time.deltaTime * zSensivity)) >= maxDistance.y)
            zPos = maxDistance.y;
        else if ((zPos - (eventData.delta.y * Time.deltaTime * zSensivity)) <= minDistance.y)
            zPos = minDistance.y;

        cameraTransform.position = Vector3.Lerp(transform.position, new Vector3(xPos, zPos, cameraTransform.position.z), 1);
        
        Draged = true;
        //  print(Draged);
    }
    

    public void OnEndDrag(PointerEventData eventData)
    {
        Draged = false;
    }
}