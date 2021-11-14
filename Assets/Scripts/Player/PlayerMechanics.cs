using UnityEngine;
using Utilities;
using Weapons;

namespace Player
{
    public class PlayerMechanics : MonoBehaviour
    {
        [SerializeField] private PlayerView _view;

		[Header("Keys")]
		[SerializeField] private KeyCode _leftWeaponFireKey;
		[SerializeField] private KeyCode _rightWeaponFireKey;
		[SerializeField] private KeyCode _leftHandInteractionKey;
		[SerializeField] private KeyCode _rightHandInteractionKey;

		private WeaponBase _leftWeapon;
        private WeaponBase _rightWeapon;

		private void Update()
		{
			if (_leftWeapon != null)
				_leftWeapon.Interact(_leftWeaponFireKey);

			if (_rightWeapon != null)
				_rightWeapon.Interact(_rightWeaponFireKey);

			if (Input.GetKeyDown(_leftHandInteractionKey))
			{
				if (_leftWeapon != null)
					DropItem(true);
				else
					TryTakeItem(true);
			}

			if (Input.GetKeyDown(_rightHandInteractionKey))
			{
				if (_rightWeapon != null)
					DropItem(false);
				else
					TryTakeItem(false);
			}
		}

		private void DropItem(bool toLeftHand)
		{
			if (toLeftHand)
			{
				_leftWeapon.transform.SetParent(null);
				_leftWeapon = null;
			}
			else
			{
				_rightWeapon.transform.SetParent(null);
				_rightWeapon = null;
			}
		}

		private void TryTakeItem(bool toLeftHand)
		{
			if (!Physics.Raycast(CameraRayHelper.GetCenterCameraRay(), out RaycastHit raycastHit))
				return;

			if (raycastHit.collider.TryGetComponent(out WeaponBase item))
			{
				if (toLeftHand)
					_leftWeapon = item;
				else
					_rightWeapon = item;

				_view.TakeItem(toLeftHand, item.transform);
				item.PickUp();
				return;
			}
		}
	}
}
