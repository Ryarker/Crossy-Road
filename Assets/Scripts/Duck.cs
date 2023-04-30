using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Duck : MonoBehaviour
{

    [SerializeField, Range(0,1)] float moveDuration; 
    [SerializeField, Range(0,1)] float jumpHeight; 
    // Start is called before the first frame update
    
    [SerializeField] int leftMoveLimit;
    [SerializeField] int rightMoveLimit;
    [SerializeField] int backMoveLimit;
    public UnityEvent<Vector3> onJumped;
    public UnityEvent<int> onGetCoin;
    public UnityEvent OnDie;
    private bool isMoveable = false;
    // Update is called once per frame
    void Update()
    {
        if(isMoveable)
        return;
        

        if (DOTween.IsTweening(transform))
            return;

        Vector3 direction = Vector3.zero;
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            direction += Vector3.forward;
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            direction += Vector3.left;
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            direction += Vector3.back;
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            direction += Vector3.right;
        }

        if (direction == Vector3.zero)
        return;
        Move(direction);

    }

    public void Move(Vector3 direction) {
        var targetPosition = transform.position + direction;
        if(targetPosition.x < leftMoveLimit || 
        targetPosition.x > rightMoveLimit || 
        targetPosition.z < backMoveLimit ||
        Tree.AllPositions.Contains(targetPosition)){
         
            targetPosition = transform.position;
        }

    transform.DOJump(
    targetPosition,
    jumpHeight,
    1,
    moveDuration)
    .onComplete = BroadcastPositionOnJumped;

    transform.forward = direction;
    }

    public void SetMoveable (bool value) {
        isMoveable = value;
    }
public void UpdateMoveLimit (int horizontalLimit, int backLimit) {
    leftMoveLimit = -horizontalLimit/2;
    rightMoveLimit = horizontalLimit/2;

    backMoveLimit = backLimit;
}
    private void BroadcastPositionOnJumped(){
        onJumped.Invoke(transform.position);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Car")){
        if(isMoveable == true) {
         return;   
        }
        transform.DOScaleY(0.1f,0.2f);
        isMoveable = true;
        Invoke("Die", 3);
        }
        else if(other.CompareTag("Coin")){

            var coin = other.GetComponent<Coin>();
            onGetCoin.Invoke(coin.Value);
            coin.Collected();
        }
        else if(other.CompareTag("Eagle"))
        {
            if(this.transform != other.transform){
            this.transform.SetParent(other.transform);
            Invoke("Die", 3);
            }
        }
    }
    private void Die () {
        OnDie.Invoke();
    }
}
