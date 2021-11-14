using UnityEngine;

namespace Weapons
{
	public abstract class WeaponBase : MonoBehaviour
	{
		public virtual void Drop()
		{
			transform.SetParent(null);
		}

		public abstract void PickUp();

		public abstract void Interact(KeyCode interactKey);

		protected abstract void Fire();
	}
}