using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightHand;

        public void TakeItem(bool toLeftHand, Transform item)
		{
            var parent = toLeftHand ? _leftHand : _rightHand;
            item.SetParent(parent);
            item.SetPositionAndRotation(parent.position, parent.rotation);
        }
    }
}
