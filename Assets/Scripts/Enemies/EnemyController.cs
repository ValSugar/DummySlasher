using Interfaces;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class EnemyController : MonoBehaviour, ITakeDamage, IFlammable, IGettingWet
    {
        [SerializeField] private EnemyView _view;
        [SerializeField] private EnemyModel _model;

		private Coroutine _fireCoroutine;

		private void Awake()
		{
			_view.Init();
			_model.Init(OnChangeHP, OnChangeWetnessValue);
		}

		public void Restore()
		{
			gameObject.SetActive(true);
			if (_fireCoroutine != null)
				StopCoroutine(_fireCoroutine);
			_model.Restore();
			_view.SetStandardView();
		}

		private void OnChangeHP(float max, float current)
		{
			var healthInOnePercent = 1f / max * current;
			_view.ChangeHPBar(healthInOnePercent);

			if (_model.IsDead)
				gameObject.SetActive(false);
		}

		private void OnChangeWetnessValue(float max, float current)
		{
			var wetnessInOnePercent = 1f / max * current;
			_view.ChangeWetnessBar(wetnessInOnePercent);
		}

		public void TakeDamageByBullet(float damage)
		{
			if (_model.IsWet)
				damage += _model.AdditionalDamageIfWet;
			else if (_fireCoroutine != null)
				damage += _model.AdditionalDamageIfBurn;
			
			_model.TakeDamage(damage);
		}

		public void TakeFireHit(float value)
		{
			if (_model.IsWet)
			{
				_model.WetnessChange(-value);
				return;
			}

			_model.TakeDamage(value);

			if (_fireCoroutine != null)
				StopCoroutine(_fireCoroutine);

			_fireCoroutine = StartCoroutine(Burning());
		}

		public void TakeWaterHit(float value)
		{
			if (_fireCoroutine != null)
				StopCoroutine(_fireCoroutine);

			_fireCoroutine = null;

			_view.SetWetView();
			_model.WetnessChange(value);
		}

		private IEnumerator Burning()
		{
			_view.SetBurnView();
			var waitForOneSecond = new WaitForSeconds(1f);
			for (var i = 0; i < _model.BurningTime; i++)
			{
				yield return waitForOneSecond;
				if (_model.IsDead)
					break;

				_model.BurningHit();
			}
			_view.SetStandardView();
			_fireCoroutine = null;
		}

		private void OnDisable()
		{
			if (_fireCoroutine != null)
				StopCoroutine(_fireCoroutine);
		}
	}
}
