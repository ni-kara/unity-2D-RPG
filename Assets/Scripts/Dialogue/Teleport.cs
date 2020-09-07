using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private float speed = 200f;
    void Update()=>    
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);    
}
