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
			_oldRotation = _t.rotation;
			_angle.y = angleY;

			//Debug.Log("(Start)Old rotation: " + _oldRotation);
			//Debug.Log("(Start)position: " + _t.position);
		//_t.position = new Vector3(16.5f, 5.8f, -28.8f);
	}

		void Update()
		{
			if(target && Input.GetMouseButton(1))
			{
				_angle.x += Input.GetAxis("Mouse X") * rotationSensitivity;
			//Debug.Log("(Update)Old rotation: " + _oldRotation);
			//Debug.Log("(Update)position: " + _t.position);
			//Debug.Log("(Update)current rotation: " + _oldRotation);
		}
		}

		void LateUpdate()
		{
			if(target)
			{
				Quaternion angleRotation = Quaternion.Euler(_angle.y, _angle.x, 0);
				Quaternion currentRotation = Quaternion.Lerp(_oldRotation, angleRotation, Time.deltaTime * rotationSmoothing);

				_oldRotation = currentRotation;

				_t.position = target.position - currentRotation * Vector3.forward * distance;
				_t.LookAt(target.position, Vector3.up);
			}
		}
	}
