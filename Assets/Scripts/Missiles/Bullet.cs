using Interfaces;
using UnityEngine;
using Utilities;

namespace Missiles
{
	public class Bullet : MissileBase
	{
		protected override void OnHit(Collider collider)
		{
			if (collider.TryGetComponent(out ITakeDamage damageTaker))
				damageTaker.TakeDamageByBullet(_damage);
		}
	}
}