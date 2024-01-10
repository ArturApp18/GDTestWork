using UnityEngine;
using UnityEngine.AI;

public class AnimateAlongAgent : MonoBehaviour
{
	private const float MinimalVelocity = 0.1f;

	private static readonly int IsMoving = Animator.StringToHash("IsMoving");
	private static readonly int Speed = Animator.StringToHash("Speed");
	
	[SerializeField] private NavMeshAgent _agent;
	[SerializeField] private Animator _animator;

	private void Update()
	{
		if (ShouldMove())
			Move(_agent.velocity.magnitude);
		else
			StopMoving();
	}

	private bool ShouldMove() =>
		_agent.velocity.magnitude > MinimalVelocity;

	private void Move(float speed)
	{
		_animator.SetBool(IsMoving, true);
		_animator.SetFloat(Speed, speed);
	}

	private void StopMoving()
		=> _animator.SetBool(IsMoving, false);

}