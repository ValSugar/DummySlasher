using UnityEngine;

namespace Weapons
{
    public abstract class ParticleWeaponBase : WeaponBase
    {
		[SerializeField] private ParticleSystem _particleSystem;
		[SerializeField] protected float _damage;

		public override void Drop()
		{
			base.Drop();
			_particleSystem.Stop();
		}

		public override void Interact(KeyCode interactKey)
		{
			if (Input.GetKeyDown(interactKey))
				_particleSystem.Play();

			if (Input.GetKeyUp(interactKey))
				_particleSystem.Stop();
		}

		protected override void Fire() {}

		public override void PickUp() { }

		protected abstract void OnParticleCollision(GameObject go);
	}
}
