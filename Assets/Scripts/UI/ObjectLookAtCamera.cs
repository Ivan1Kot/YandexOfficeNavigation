using UnityEngine;

public class ObjectLookAtCamera : MonoBehaviour
{
    private Transform _cam;

    void Start()
    {
        _cam = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(_cam);
    }
}