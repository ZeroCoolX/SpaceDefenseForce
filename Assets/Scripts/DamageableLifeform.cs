using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageableLifeform : MonoBehaviour {

    public int Health = 3;
    public Vector2 RotationSpeedRange = new Vector2(5f, 15f);
    private float _rotationX, _rotationY, _rotationZ;

	// Use this for initialization
	void Start () {
        Random.InitState((int)Time.time);
        _rotationX = Random.Range(RotationSpeedRange.x, RotationSpeedRange.y);
        _rotationY = Random.Range(RotationSpeedRange.x, RotationSpeedRange.y);
        _rotationZ = Random.Range(RotationSpeedRange.x, RotationSpeedRange.y);
    }

    // Update is called once per frame
    void Update () {
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x + _rotationX * Time.deltaTime, 
            transform.localEulerAngles.y + _rotationY * Time.deltaTime,
            transform.localEulerAngles.z + _rotationZ * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "PlayerBullet") {
            --Health;

            if (Health <= 0) {
                Destroy(gameObject);
            }

            Destroy(other.gameObject);
        }else if(other.tag == "Player") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
