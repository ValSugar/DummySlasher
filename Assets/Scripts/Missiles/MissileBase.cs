using PoolObjects;
using UnityEngine;

namespace Missiles
{
	public abstract class MissileBase : MonoBehaviour
	{
		[SerializeField] protected int _damage;
		[SerializeField] protected float _speed;

		protected Transform _transform; //Cached for micro-optimization

		private void Awake()
		{
			_transform = transform;
		}

		private void FixedUpdate()
		{
			_transform.position += _transform.forward * _speed * Time.fixedDeltaTime;
		}

		protected void Disable()
		{
			gameObject.SetActive(false);
		}

		protected virtual void OnDisable()
		{
			Pool.ReturnToPool(gameObject);
		}

		private void OnTriggerEnter(Collider collider)
		{
			OnHit(collider);
			Disable();
		}

		protected abstract void OnHit(Collider collider);
	}
}