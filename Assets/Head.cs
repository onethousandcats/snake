using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public float rotationSpeed = 3.0f;
    public float movementSpeed = 0.2f;

    public float segmentOffset = 1.0f;
    public int segments = 5;

    private GameObject[] _segments;

    private int _rotation = 0;
    private int _direction = 0;

    void Start()
    {
        _segments = new GameObject[segments];

        var currentPosition = this.transform.position;

        for (int i = 0; i < segments; i++)
        {
            _segments[i] = (GameObject)Instantiate(Resources.Load("Prefabs/Body"));

            currentPosition -= Vector3.forward * segmentOffset;
            _segments[i].transform.position = currentPosition;
        }
    }

    void Update()
    {
        MoveHead();
    }

    public void MoveHead()
    {
        Rotate();
        Move();
        
        if (IsMoving())
            MoveBody();
    }

    public void SetRotation()
    {
        if (Input.GetKeyDown(KeyCode.A))
            _rotation = -1;        
        
        if (Input.GetKeyDown(KeyCode.D))
            _rotation = 1;

        if ((Input.GetKeyUp(KeyCode.A) && _rotation == -1) || (Input.GetKeyUp(KeyCode.D) && _rotation == 1))
            _rotation = 0;        
    }

    public void SetDirection()
    {
        if (Input.GetKeyDown(KeyCode.W))
            _direction = 1;

        if (Input.GetKeyDown(KeyCode.S))
            _direction = -1;

        if ((Input.GetKeyUp(KeyCode.W) && _direction == 1) || (Input.GetKeyUp(KeyCode.S) && _direction == -1))
            _direction = 0;
    }

    public void Rotate()
    {
        SetRotation();
        this.transform.Rotate(new Vector3(0, _rotation, 0) * rotationSpeed);
    }

    public void Move()
    {
        SetDirection();

        this.transform.position += this.transform.forward * _direction * movementSpeed;
    }

    public void MoveBody()
    {
        var currentPart = gameObject;

        for (int i = 0; i < _segments.Length; i++)
        {
            _segments[i].transform.LookAt(currentPart.transform.position);

            _segments[i].transform.position = Vector3.MoveTowards(_segments[i].transform.position, currentPart.transform.position - (currentPart.transform.forward * segmentOffset), 0.8f);

            currentPart = _segments[i];
        }
    }

    public bool IsMoving()
    {
        return _direction != 0;
    }
}
