using DataExtensions;
using UnityEngine;
using UnityEngine.AI;

public class AgentMoveToPlayer : MonoBehaviour
{
	public NavMeshAgent Agent;

	private const float MinimalDistance = 1;
    
	[SerializeField] private Transform _heroTransform;

	public void Construct(Transform heroTransform) => 
		_heroTransform = heroTransform;

	private void Update()
	{
		if(_heroTransform && IsHeroNotReached())
			Agent.destination = _heroTransform.position;
	}

	private bool IsHeroNotReached() => 
		Agent.transform.position.SqrMagnitudeTo(_heroTransform.position) >= MinimalDistance;
}