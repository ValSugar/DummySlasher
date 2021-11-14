using System;
using UnityEngine;

namespace Enemies
{
	public class EnemyModel : MonoBehaviour //TODO: remove MonoBeh, set params by config
	{
		[SerializeField] private float _maxHealth;
		[SerializeField] private float _maxWetnessValue;
		[SerializeField] private float _burningDamage;
		[SerializeField] private float _burningTime;
		[SerializeField] private float _additionalDamageIfBurn;
		[SerializeField] private float _additionalDamageIfWet;

		private float _currentHealth;
		private float _wetnessValue;

		private Action<float, float> _onChangeHP;
		private Action<float, float> _onChangeWetness;

		public float AdditionalDamageIfBurn => _additionalDamageIfBurn;
		public float AdditionalDamageIfWet => _additionalDamageIfWet;
		public float BurningTime => _burningTime;
		public bool IsWet => _wetnessValue > 0;
		public bool IsDead => _currentHealth <= 0;

		public void Init(Action<float, float> onChangeHPCallback, Action<float, float> onChangeWetnessCallback)
		{
			Restore();
			_onChangeHP = onChangeHPCallback;
			_onChangeWetness = onChangeWetnessCallback;
		}

		public void Restore()
		{
			_currentHealth = _maxHealth;
			_onChangeHP?.Invoke(_maxHealth, _currentHealth);

			_wetnessValue = 0f;
			_onChangeWetness?.Invoke(_maxWetnessValue, _wetnessValue);
		}

		public void TakeDamage(float damage)
		{
			_currentHealth -= damage;
			_onChangeHP?.Invoke(_maxHealth, _currentHealth);
		}

		public void WetnessChange(float value)
		{
			_wetnessValue = Mathf.Clamp(_wetnessValue + value, 0f, _maxWetnessValue);
			_onChangeWetness?.Invoke(_maxWetnessValue, _wetnessValue);
		}

		public void BurningHit() => TakeDamage(_burningDamage);
	}
}
