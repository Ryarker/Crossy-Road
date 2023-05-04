using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Car : MonoBehaviour
{
    [SerializeField, Range(0,10)] float speed = 1 ;
    [SerializeField] List<AudioClip> carNoise;
    [SerializeField] AudioSource carSoundEffect;
    Vector3 initialPosition;
    float distanceLimit = float.MaxValue;
    public void setupDistanceLimit (float distance) {
        this.distanceLimit = distance;
    }
    private void Start() {
        initialPosition = this.transform.position;
    }
    private void Update () {
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
        if(Vector3.Distance(initialPosition, this.transform.position) > this.distanceLimit) {
            Destroy(this.gameObject);
            var index = Random.Range(0, carNoise.Count);
        carSoundEffect.PlayOneShot(carNoise[index]);
        }
    }
}
