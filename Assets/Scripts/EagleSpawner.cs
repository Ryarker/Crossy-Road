using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour
{
    [SerializeField] Eagle eagle;
    [SerializeField] Duck duck;
    [SerializeField] float initialTimer;
    [SerializeField] AudioSource eagleSoundEffect;
    [SerializeField] AudioClip eagleSound;
    private bool isPlayed;
    

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = initialTimer;
        eagle.gameObject.SetActive(false);
        duck.SetMoveable(false);
        isPlayed = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0 && eagle.gameObject.activeInHierarchy == false)
        {
            eagle.gameObject.SetActive(true);
            eagle.transform.position = duck.transform.position + new Vector3(0,0, 20);
            if(!isPlayed){
                playEagleSound();
                isPlayed=true;
            }

        }
        timer -= Time.deltaTime;
    }
    public void ResetTimer () {
        timer = initialTimer;
    }
    private void playEagleSound () {
        eagleSoundEffect.PlayOneShot(eagleSound);
    }
}
