using UnityEngine.SceneManagement;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    public Transform Player;
    public Transform AsteroidPrefab;
    public TextMesh InfoText;

    public int AsteroidNumber = 10;
    public float SpawnDistanceFromPlayer = 20f;

    private float _gameTimer;
    private float _gameOverTimer;

	// Use this for initialization
	void Start () {
        Random.InitState((int)Time.time);
        SpawnAsteroids();
    }

    private void SpawnAsteroids() {
        for(var i = 0; i < AsteroidNumber; ++i) {

            // random angle in circle
            float randAngle = Random.Range(0, 2 * Mathf.PI);
            float randHeight = Random.Range(0, 2 * Mathf.PI);

            Vector3 asteroidPosition = new Vector3(
                Mathf.Cos(randAngle)* SpawnDistanceFromPlayer, 
                Mathf.Cos(randHeight) * SpawnDistanceFromPlayer,
                Mathf.Sin(randAngle) * SpawnDistanceFromPlayer
            );

            var clone = Instantiate(AsteroidPrefab) as Transform;
            clone.parent = transform;
            clone.transform.position = asteroidPosition;
        }
    }
	
	// Update is called once per frame
	void Update () {
        int remainingAsteroids = transform.GetComponentsInChildren<DamageableLifeform>().Length;
        bool isGameOver = remainingAsteroids == 0;

        if(isGameOver) {
            InfoText.text = "You Successfully defended space! \n GameTime: (" + Mathf.Floor(_gameTimer) + ") seconds";
            _gameOverTimer -= Time.deltaTime;
            if(_gameOverTimer <= 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        } else {
            _gameTimer += Time.deltaTime;
            InfoText.text = remainingAsteroids+" Asteroids left! [" + Mathf.Floor(_gameTimer) + "s]";
        }
    }
}
