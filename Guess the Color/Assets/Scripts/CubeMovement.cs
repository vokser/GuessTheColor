using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _moveRange;
    [SerializeField] private float _rotateSpeed;

    private Vector3 startPosition;
    
    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * _moveSpeed) * _moveRange;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

}


