using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public float Speed;
    public Transform BulletPrefab;

    private Transform _parentMover;
    private float _fireTimer;
    private float _fireCooldown = 0.5f;

    private Vector2 DirectionalInput;

    // Use this for initialization
    void Start () {
        _parentMover = transform.parent;
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
        print("Player Input : " + DirectionalInput);
    }

    private void Move() {
        GetPlayerInput();

        var adjustedSpeed = Speed * (DirectionalInput.y > 0 ? 2f : DirectionalInput.y < 0 ? 0.5f : 1f);
        _parentMover.position += transform.forward * adjustedSpeed * Time.deltaTime;

        print("adjustedSpeed: " + adjustedSpeed);
    }

    private void CheckForFire() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            FireBullet();
        }
    }

    private void FireBullet() {
        var clone = Instantiate(BulletPrefab, transform.position, transform.parent.rotation) as Transform;
        var bulletScript = clone.GetComponent<Bullet>();
        if(bulletScript == null) {
            throw new MissingComponentException("Missing bullet Script on bullet");
        }

        bulletScript.BulletSpeed = 10;
        bulletScript.Direction = transform.forward;
    }
}
