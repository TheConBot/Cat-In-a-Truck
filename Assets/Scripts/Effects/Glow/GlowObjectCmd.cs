using UnityEngine;
using System.Collections.Generic;

public class GlowObjectCmd : MonoBehaviour
{
	public Color GlowColor;
    public Color NeutralColor = Color.black;
	public float LerpFactor = 10;
    public bool doNotDisableInUpdate = false;

    private Color _neutralColor;

	public Renderer[] Renderers
	{
		get;
		private set;
	}

	public Color CurrentColor
	{
		get { return _currentColor; }
	}

	private Color _currentColor;
	private Color _targetColor;

	void Start()
	{
		Renderers = GetComponentsInChildren<Renderer>();
		GlowController.RegisterObject(this);
        _neutralColor = NeutralColor;
        _targetColor = _neutralColor;
    }

    private void OnMouseEnter()
	{
		_targetColor = GlowColor;
		enabled = true;
	}

    private void OnDisable() {
        _currentColor = Color.black;
    }

    private void OnMouseExit()
	{
		_targetColor = _neutralColor;
		enabled = true;
	}

    public void SetVisible(bool setState) {
        if (setState) {
            _neutralColor = NeutralColor;
        }
        else { 
            _neutralColor = Color.black;
        }
        _targetColor = _neutralColor;
    }

	/// <summary>
	/// Update color, disable self if we reach our target color.
	/// </summary>
	private void Update()
	{
		_currentColor = Color.Lerp(_currentColor, _targetColor, Time.deltaTime * LerpFactor);

		if (_currentColor.Equals(_targetColor) && !doNotDisableInUpdate)
		{
			enabled = false;
		}
	}
}
