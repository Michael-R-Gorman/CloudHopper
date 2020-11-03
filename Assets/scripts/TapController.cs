using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //  Any obj importing this script will require a RigidBody2D
public class TapController : MonoBehaviour {

    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;

    public float tapForce = 10;
    public float tiltness = 5;
    public Vector3 startPos;

    Rigidbody2D rigidBody;
    Quaternion downRotation;  //  What player is slowly moving towards
    Quaternion forwardRotation;    // Everytime you tap, it will snap up to that forward rotation

    GameManager gameManager;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, 90);
        forwardRotation = Quaternion.Euler(0, 0, 40);
        gameManager = GameManager.Instance;
        rigidBody.simulated = false;
    }

    void OnEnable() {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable() {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameStarted() {
        rigidBody.velocity = Vector3.zero;
        rigidBody.simulated = true;     // Allow player to move again if game starts
    }

    void OnGameOverConfirmed() {
        transform.localPosition = startPos;     // Put character back at beginning position
        transform.rotation = Quaternion.identity;   // No rotation
    }

    // Update is called once per frame
    void Update() {
        if (gameManager.getGameOver()) return;
        if (Input.GetMouseButtonDown(0)) { //  This capturing the tap notification on mobile devices
            rigidBody.velocity = Vector3.zero;  // We don't want to add our new force with the current force
            transform.rotation = forwardRotation;
            rigidBody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltness * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "ScoreZone") {
            //register score events
            OnPlayerScored();   // event sent to GameManager
            //play sound
            // etc
        } 
        if (collider.gameObject.tag == "DeadZone") {
            rigidBody.simulated = false;    //  Freeze player if dead
            // register a dead event
            OnPlayerDied(); // event sent to GameManager
            // play sound
            //etc
        }
    }
}
