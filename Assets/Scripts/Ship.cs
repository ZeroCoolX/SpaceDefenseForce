using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public float Speed;
    public Transform BulletPrefab;
    public Transform Camera;

    private float _bankSpeedAdditive = 3;
    private float _fireTimer;
    private float _fireCooldown = 0.5f;

    private Vector2 DirectionalInput;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        CheckForFire();
        //_fireTimer -= Time.deltaTime;
        //if(_fireTimer <= 0) {
        //    _fireTimer = _fireCooldown;

        //    //RaycastHit hit;
        //    if(Input.GetKeyDown(KeyCode.Space)) {//if (Physics.Raycast(transform.position, transform.forward, out hit)) {
        //        //print("Hit object : " + hit.transform.tag);
        //        //if (hit.transform.tag == "Damageable") {
        //            FireBullet();
        //        //}
        //    }
        //}
    }

    private void GetPlayerInput() {
        DirectionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private float _refOut;

    private float _shipTopSpeed = 5f;
    private float _shipSpeed = 3;

    private float _xRotation;
    private float _yRotation;

    private void Move() {
        GetPlayerInput();

        var adjustedSpeed = Speed * (Input.GetKey("w") ? 2f : Input.GetKey("s") ? 0.5f : 1f);
        var horizontalMovement = (Input.GetKey("d") ? Camera.right : Input.GetKey("a") ? Camera.right * -1 : Vector3.zero) * Time.deltaTime * _bankSpeedAdditive;

        var adjustedForwardDirection = transform.position + horizontalMovement + (Camera.forward * adjustedSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, adjustedForwardDirection, 1);

        // NOT WORKING YET
        //// partial
        //var yaw = (Input.GetKey("d") ? 15 : Input.GetKey("a") ? -15 : 0) * -1;
        //Vector3 eulerAngles = Camera.localEulerAngles;
        //float adjustedAngle = Mathf.LerpAngle(eulerAngles.z, yaw, Time.deltaTime);
        //Camera.localEulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, adjustedAngle);
    }

    private void CheckForFire() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            FireBullet();
        }
    }

    private void FireBullet() {
        var clone = Instantiate(BulletPrefab, transform.position, Camera.rotation) as Transform;
        var bulletScript = clone.GetComponent<Bullet>();
        if(bulletScript == null) {
            throw new MissingComponentException("Missing bullet Script on bullet");
        }

        bulletScript.BulletSpeed = 15;
        bulletScript.Direction = Camera.forward;
    }
}
