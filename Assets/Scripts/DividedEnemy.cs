using UnityEngine;

public class DividedEnemy : MonoBehaviour
{
	[SerializeField] private Enemy _baseEnemy;
	[SerializeField] private GameObject[] _dividedEnemies;

	private SceneManager _sceneManager;

	private void Start()
	{
		_sceneManager = SceneManager.Instance;
		_baseEnemy.DeathHappened += Divide;
	}

	private void OnDestroy()
	{
		_baseEnemy.DeathHappened -= Divide;
	}

	private void Divide()
	{
		for (int i = 0; i < _dividedEnemies.Length; i++)
		{
			Instantiate(_dividedEnemies[i], transform.position, Quaternion.identity).TryGetComponent(out Enemy enemy);
			_sceneManager.AddEnemie(enemy);

			enemy.TryGetComponent(out AgentMoveToPlayer moveToPlayer);
			moveToPlayer.Construct(_sceneManager.Player.transform);
		}
	}
}