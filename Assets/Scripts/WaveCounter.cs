using TMPro;
using UnityEngine;

public class WaveCounter : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _waveCounter;
	[SerializeField] private LevelConfig Config;

	private void Start()
	{
		SceneManager.Instance.OnWaveChanged += WaveChanged;
		UpdateCounter(0, Config.Waves.Length);
	}

	private void OnDestroy() =>
		SceneManager.Instance.OnWaveChanged -= WaveChanged;

	private void WaveChanged(float currentWave) =>
		UpdateCounter(currentWave, Config.Waves.Length);

	private void UpdateCounter(float currentWave, float maxWave) =>
		_waveCounter.text = $"Current wave: {currentWave} / Maximum wave: {maxWave}";
}