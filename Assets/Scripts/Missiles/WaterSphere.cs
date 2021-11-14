using Interfaces;
using UnityEngine;

namespace Missiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class WaterSphere : MissileBase
    {
		[SerializeField] private Rigidbody _rigidBody;

		private void OnEnable()
		{
			_rigidBody.velocity = Vector3.zero;
		}

		protected override void OnHit(Collider collider)
		{
			if (collider.TryGetComponent(out IGettingWet component))
                component.TakeWaterHit(_damage);
		}
    }
}
