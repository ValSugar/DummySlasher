using Interfaces;
using UnityEngine;

namespace Weapons
{
	public class FireStone : ParticleWeaponBase
	{
		protected override void OnParticleCollision(GameObject go)
		{
			if (!go.TryGetComponent(out IFlammable flammable))
				return;

			flammable.TakeFireHit(_damage);
		}
	}
}
