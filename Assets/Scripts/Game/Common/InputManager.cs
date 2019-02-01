using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Game.Common
{
	public class InputManager: MonoBehaviour, IInputManager
	{
		[SerializeField]
		private Camera _camera;

		public event Action<RaycastHit> OnClick; 

		private void Update()
		{
			if (IsOverGUI())
				return;

			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					OnClick?.Invoke(hit);
				}
			}
		}

		private bool IsOverGUI()
		{
			if (EventSystem.current == null)
				return false;
			if (EventSystem.current.IsPointerOverGameObject())
				return true;

			if (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended)
			{
				if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
					return true;
			}

			return false;
		}
	}
}
