using Missiles;
using PoolObjects;
using System.Collections;
using UnityEngine;
using Utilities;

namespace Weapons
{
	public class SimpleWeapon : WeaponBase
	{
		public const float DISTANCE_CHECK = 100f;

		[SerializeField] protected MissileBase _missilePrefab;
		[SerializeField] protected Transform _muzzle;
		[SerializeField] private float _spreadRate;
		[SerializeField] private float _fireCooldown;
		[SerializeField] private float _reloadTime;
		[SerializeField] private int _maxShotsCount;
		[SerializeField] private int _missileCountPerShot;
		[SerializeField] private bool _isAutoWeapon;

		private int _currentShotsCount;

		private bool _isReadyToFire;

		private Coroutine _reloadCoroutine;

		private void Awake()
		{
			_isReadyToFire = true;
			_currentShotsCount = _maxShotsCount;
		}

		public override void Drop()
		{
			base.Drop();
			if (_reloadCoroutine != null)
				StopCoroutine(_reloadCoroutine);
		}

		public override void PickUp()
		{
			if (!_isReadyToFire)
				_reloadCoroutine = StartCoroutine(ReloadCoroutine());

			var ray = CameraRayHelper.GetCenterCameraRay();
			var lookPoint = ray.origin + (ray.direction * DISTANCE_CHECK);
			_muzzle.LookAt(lookPoint);
		}

		public override void Interact(KeyCode interactKey)
		{
			var hasInput = (_isAutoWeapon && Input.GetKey(interactKey)) || (!_isAutoWeapon && Input.GetKeyDown(interactKey));
			if (!hasInput)
				return;

			if (!_isReadyToFire)
				return;

			for (var i = 0; i < _missileCountPerShot; i++)
				Fire();

			--_currentShotsCount;

			_isReadyToFire = false;

			_reloadCoroutine = StartCoroutine(ReloadCoroutine());
		}

		protected override void Fire()
		{
			var rotation = _muzzle.rotation.eulerAngles;
			rotation.x += Random.Range(-_spreadRate, _spreadRate);
			rotation.y += Random.Range(-_spreadRate, _spreadRate);
			Pool.SpawnObject(_missilePrefab, _muzzle.position, Quaternion.Euler(rotation));
		}

		private IEnumerator ReloadCoroutine()
		{
			var delay = _currentShotsCount <= 0 ? _reloadTime : _fireCooldown;

			yield return new WaitForSeconds(delay);

			_isReadyToFire = true;

			if (_currentShotsCount > 0)
				yield break;

			_currentShotsCount = _maxShotsCount;
		}
	}
}