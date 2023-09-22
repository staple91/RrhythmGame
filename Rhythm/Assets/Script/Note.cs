using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField]
    Vector3 dir;
    [SerializeField]
    float speed;

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * dir);
    }
}
