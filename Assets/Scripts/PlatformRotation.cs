using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotation : MonoBehaviour
{

    public float currentRotation;
    public float rotationAmount;
    
    public float degreesPerSec = 360f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotationAmount = degreesPerSec * Time.deltaTime;
        float currentRotation = transform.localRotation.eulerAngles.z;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, currentRotation + rotationAmount));
    }
}
