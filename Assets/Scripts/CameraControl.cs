using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    public float xOffset;
    public float yOffset;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (followTarget.transform.position.x - xOffset, followTarget.transform.position.y - yOffset, followTarget.position.z);
    }
}
