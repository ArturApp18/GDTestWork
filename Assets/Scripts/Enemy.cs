using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public float Hp;
	public float Damage;
	public float AtackSpeed;
	public float AttackRange = 2;
	
	public Animator AnimatorController;
	public NavMeshAgent Agent;

	private float lastAttackTime = 0;
	private bool isDead = false;

	public Action DeathHappened;
	
	private void Start()
	{
		SceneManager.Instance.AddEnemie(this);
	}

	private void Update()
	{
		if (isDead)
		{
			return;
		}

		if (Hp <= 0)
		{
			Die();
			Agent.isStopped = true;
			return;
		}

		float distance = Vector3.Distance(transform.position, SceneManager.Instance.Player.transform.position);

		if (distance <= AttackRange)
		{
			if (Time.time - lastAttackTime > AtackSpeed)
			{
				lastAttackTime = Time.time;
				SceneManager.Instance.Player.CurrentHp -= Damage;
				AnimatorController.SetTrigger("Attack");
			}
		}
	}

	public void TakeDamage(float damage) =>
		Hp -= damage;


	private void Die()
	{
		DeathHappened?.Invoke();
		SceneManager.Instance.RemoveEnemie(this);
		isDead = true;
		AnimatorController.SetTrigger("Die");
	}

}