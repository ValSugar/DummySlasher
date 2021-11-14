using Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class EnemiesManager : MonoBehaviour
    {
        [SerializeField] private EnemyController _dummyPrefab;

		private List<EnemyController> _spawnedDummies;

		private void Start()
		{
			_spawnedDummies = new List<EnemyController>();
			_spawnedDummies.Add(Instantiate(_dummyPrefab, transform.position, Quaternion.identity));
		}

		private void Update()
		{
			if (!Input.GetKeyDown(KeyCode.R))
				return;

			foreach (var dummy in _spawnedDummies)
				dummy.Restore();
		}
	}
}
