using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    public Player Player;
    public List<Enemy> Enemies;
    public GameObject Lose;
    public GameObject Win;

    private int currWave = 0;
    [SerializeField] private LevelConfig Config;

    public Action<float> OnWaveChanged;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnWave();
    }

    public void AddEnemie(Enemy enemy)
    {
        Enemies.Add(enemy);
        enemy.DeathHappened += HealPlayer;
    }

    public void RemoveEnemie(Enemy enemy)
    {
        Enemies.Remove(enemy);
        if(Enemies.Count == 0)
        {
            SpawnWave();
        }
        enemy.DeathHappened -= HealPlayer;
    }

    public void GameOver()
    {
        Lose.SetActive(true);
    }

    private void HealPlayer()
    {
        Player.CurrentHp += Player.MaxHp * 0.1f;
    }

    private void SpawnWave()
    {
        if (currWave >= Config.Waves.Length)
        {
            Win.SetActive(true);
            return;
        }

        Wave wave = Config.Waves[currWave];
        foreach (GameObject character in wave.Characters)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Instantiate(character, pos, Quaternion.identity).TryGetComponent(out AgentMoveToPlayer goblinMove);
            goblinMove.Construct(Player.transform);
        }
        currWave++;
        OnWaveChanged?.Invoke(currWave);
    }

    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    

}
