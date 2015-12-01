using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public	GameObject	hazard;
	public	GameObject	hazard2;
	public	GameObject	hazard3;
	public	Vector3		spawnValues;
	public	int			hazardCount;
	public	float		spawnWait;
	public	float		startWait;
	public	float		waveWait;

	public	GUIText		scoreText;
	public	GUIText		gameOverText;
	public	GUIText		restartText;
	private	int			score;

	private bool		gameOver;
	private	bool		restart;

	void	Start()
	{
		gameOver = false;
		restart = false;
		gameOverText.text = "";
		restartText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves());
	}

	void	Update()
	{
		if (restart == true)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator	SpawnWaves()
	{
		Vector3		spawnPosition;
		Quaternion	spawnRotation;
		int			asteroidKind;

		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				asteroidKind = Random.Range(1, 4);
				spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				spawnRotation = Quaternion.identity;
				if (asteroidKind == 1)
					Instantiate (hazard, spawnPosition, spawnRotation);
				else if (asteroidKind == 2)
					Instantiate (hazard2, spawnPosition, spawnRotation);
				else if (asteroidKind == 3)
					Instantiate (hazard3, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}

			yield return new WaitForSeconds (waveWait);
			if (gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public	void	AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void	UpdateScore ()
	{
		scoreText.text = "Score : " + score;
	}

	public	void	GameOver()
	{
		gameOverText.text = "Game Over !\t";
		gameOver = true;
	}
}