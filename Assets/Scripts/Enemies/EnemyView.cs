using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Image _hpBar;
        [SerializeField] private RectTransform _wetnessBar;

        [Header("Parameters")]
        [SerializeField] private float _redMaterialValue;
        [SerializeField] private float _blueMaterialValue;

        private Color _stantardColor;

		public void Init()
		{
            _stantardColor = _meshRenderer.material.color;
            SetStandardView();
        }

        public void ChangeHPBar(float value)
        {
            _hpBar.color = Color.Lerp(Color.red, Color.green, value);
            var temp = _hpBar.rectTransform.localScale;
            temp.x = value;
            _hpBar.rectTransform.localScale = temp;
        }

        public void ChangeWetnessBar(float value)
		{
            var temp = _wetnessBar.localScale;
            temp.x = value;
            _wetnessBar.localScale = temp;
        }

		public void SetStandardView()
        {
           _meshRenderer.material.color = _stantardColor;
        }

        public void SetWetView()
		{
            var wettedColor = _stantardColor;
            wettedColor.b += _blueMaterialValue;
            _meshRenderer.material.color = wettedColor;
        }

        public void SetBurnView()
        {
            var burnedColor = _stantardColor;
            burnedColor.r += _redMaterialValue;
            _meshRenderer.material.color = burnedColor;
        }
    }
}
