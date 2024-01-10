using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
	private static readonly int AttackHash = Animator.StringToHash("Attack");
	private static readonly int HeavyAttackHash = Animator.StringToHash("HeavyAttack");

	public Animator Animator;
	public Transform AttackPoint;
	public float Damage;
	public float DamageRadius;
	public float HeavyAttackCooldown;
	public Image CooldownImage;
	public Button HeavyAttackButton;

	private static int _layerMask;
	private Collider[] _hits = new Collider[3];
	private bool _isAttacking;
	private bool _isHeavyAttacking;
	private bool _isEnemyInZone;
	private float _heavyAttackTimer;
	private List<GameObject> _enemyInZone = new List<GameObject>();

	private void Awake() =>
		_layerMask = 1 << LayerMask.NameToLayer("Enemy");

	private void Update()
	{
		if (Input.GetButtonUp("Fire1") && !_isAttacking)
		{
			Animator.SetTrigger(AttackHash);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!_enemyInZone.Contains(other.gameObject))
			_enemyInZone.Add(other.gameObject);

		if (_isEnemyInZone || _enemyInZone.Count <= 0)
			return;

		SwitchOnHeavyAttackButton();
		_isEnemyInZone = true;
	}

	private void OnTriggerExit(Collider other)
	{
		if (_enemyInZone.Contains(other.gameObject))
			_enemyInZone.Remove(other.gameObject);

		if (!_isEnemyInZone || _enemyInZone.Count >= 1)
			return;

		SwitchOffHeavyAttackButton();
		_isEnemyInZone = false;
	}

	private void SwitchOffHeavyAttackButton()
	{
		HeavyAttackButton.enabled = false;
		CooldownImage.fillAmount = 1;
	}

	private void SwitchOnHeavyAttackButton()
	{
		HeavyAttackButton.enabled = true;
		CooldownImage.fillAmount = 0;
	}

	public void HeavyAttack()
	{
		if (_isHeavyAttacking)
			return;

		_isHeavyAttacking = true;
		Animator.SetTrigger(HeavyAttackHash);
		StartCoroutine(CooldownAfterHeavyAttack());
	}

	private IEnumerator CooldownAfterHeavyAttack()
	{
		_heavyAttackTimer = HeavyAttackCooldown;
		while (_isHeavyAttacking)
		{
			_heavyAttackTimer -= Time.deltaTime;
			SetValue(_heavyAttackTimer, HeavyAttackCooldown);
			yield return null;

			if (_heavyAttackTimer <= 0)
				_isHeavyAttacking = false;
		}
	}

	private void OnAttackStart() =>
		_isAttacking = true;

	private void OnAttack()
	{
		for (int i = 0; i < Hit(); ++i)
		{
			Debug.Log(_hits[i].gameObject.name);
			_hits[i].transform.GetComponent<Enemy>().TakeDamage(Damage);
		}
	}
	
	private void OnHeavyAttack()
	{
		for (int i = 0; i < Hit(); ++i)
		{
			Debug.Log(_hits[i].gameObject.name);
			_hits[i].transform.GetComponent<Enemy>().TakeDamage(Damage * 2);
		}
	}

	private void OnAttackEnded() =>
		_isAttacking = false;

	private int Hit() =>
		Physics.OverlapSphereNonAlloc(AttackPoint.position, DamageRadius, _hits, _layerMask);

	public void SetValue(float current, float max)
	{
		CooldownImage.fillAmount = current / max;
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(AttackPoint.position, DamageRadius);
	}
}