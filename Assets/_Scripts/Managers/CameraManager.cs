using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed;
    [SerializeField] private float _cameraAcceleration;

    public void StopCamera()
    {
        _cameraSpeed = 0;
        _cameraAcceleration = 0;
    }

    void Update()
    {
        transform.position += new Vector3(_cameraSpeed * Time.deltaTime, 0, 0);
        _cameraSpeed += _cameraAcceleration;
    }
}
