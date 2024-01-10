using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float CurrentHp;
	public float MaxHp = 10;

	private float lastAttackTime = 0;
	private bool isDead = false;
	public Animator AnimatorController;

	private void Start() =>
		CurrentHp = MaxHp;

	private void Update()
	{
		if (isDead)
		{
			return;
		}

		if (CurrentHp <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		isDead = true;
		AnimatorController.SetTrigger("Die");

		SceneManager.Instance.GameOver();
	}


}