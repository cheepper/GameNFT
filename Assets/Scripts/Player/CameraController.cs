using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Transform target;
	public float angleY = 35;
	public float rotationSmoothing = 10;
	public float rotationSensitivity = 7;
	public float distance = 10;

	private Vector3 _angle = new Vector3();
	private Quaternion _oldRotation = new Quaternion();

	private Transform _t;

	public Vector2 CurrentRotation { get { return _angle; } }

	void Start()
	{
		_t = transform;
		_oldRotation = Quaternion.identity;
		_angle.y = angleY;
		_angle.x = -45f;

		_t.position = new Vector3(17f, 6f, -30f);
		
		CameraLootAt(_oldRotation, _angle.x);
	}

	void Update()
	{
		if (target && Input.GetMouseButton(1))
		{
			_angle.x += Input.GetAxis("Mouse X") * rotationSensitivity;
		}
	}

	void LateUpdate()
	{
		CameraLootAt(_oldRotation, _angle.x);

	}
	void CameraLootAt(Quaternion rotation, float angleX)
	{
		if (target)
		{
			Quaternion angleRotation = Quaternion.Euler(_angle.y, angleX, 0);
			Quaternion currentRotation = Quaternion.Lerp(rotation, angleRotation, Time.deltaTime * rotationSmoothing);

			_oldRotation = currentRotation;

			_t.position = target.position - currentRotation * Vector3.forward * distance;
			_t.LookAt(target.position, Vector3.up);
		}
	}
}
