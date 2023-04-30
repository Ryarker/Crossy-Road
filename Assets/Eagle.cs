using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeReference, Range(0,100)] float speed;
    

    private void Update() {
            transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }
    
}
