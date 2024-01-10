using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	private static readonly int Move = Animator.StringToHash("IsMove");

	[SerializeField] private CharacterController _characterController;
	[SerializeField] private Animator _animator;
	[SerializeField] private float _movementSpeed;

	private Camera _camera;

	private Vector2 _axis;
	private bool _canMove;
	public bool CanMove
	{
		get
		{
			return _canMove;
		}
		set
		{
			_canMove = value;
		}
	}

	private void Start()
	{
		_canMove = true;
		_camera = Camera.main;
	}

	private void Update()
	{
		if (!CanMove)
			return;

		_axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		Vector3 movementVector = Vector3.zero;

		_animator.SetBool(Move,_axis.sqrMagnitude > 0.001f);
		if (_axis.sqrMagnitude > 0.001f)
		{
			movementVector = _camera.transform.TransformDirection(_axis);
			movementVector.y = 0;
			movementVector.Normalize();
			transform.forward = movementVector;
		}

		movementVector += Physics.gravity;

		_characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
	}
}