using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/GlitchEffect")]
[RequireComponent(typeof(Camera))]
public class GlitchEffect : MonoBehaviour
{
	[Header("Random Vector(can be null)")]
	public Texture2D randomVectorMap;

	[Header("Glitch Shader")]
	public Shader Shader;

	[Header("Glitch Time")]
	[Tooltip("Duration of Glitch")] public float glitchTime = 0.5f;

	[Header("Glitch Intensity")]
	[Range(0, 1)]
	public float intensity = 0.8f;

	[Range(0, 1)]
	public float flipIntensity = 0.5f;

	[Range(0, 1)]
	public float colorIntensity = 0.5f;

	private bool startGlitch;
	private float timer;
	private float _glitchup;
	private float _glitchdown;
	private float flicker;
	private float _glitchupTime = 0.05f;
	private float _glitchdownTime = 0.05f;
	private float _flickerTime = 0.5f;
	private Material _material;


	public static GlitchEffect Instance;

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			Instance = this;
		}
	}

	void Start()
	{
		_material = new Material(Shader);
		ResetGlitchEffect();
	}

    private void Update()
    {
		#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.G)) Glitch();
		#endif

		if(CountTimer())
        {
			startGlitch = false;
			ResetGlitchEffect();
        }
    }

    // Called by camera to apply image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!startGlitch)
		{
			Graphics.Blit(source, destination, _material);
			return;
		}

		_material.SetFloat("_Intensity", intensity);
		_material.SetFloat("_ColorIntensity", colorIntensity);
		_material.SetTexture("_DispTex", randomVectorMap);

		flicker += Time.deltaTime * colorIntensity;
		if (flicker > _flickerTime)
		{
			_material.SetFloat("filterRadius", Random.Range(-3f, 3f) * colorIntensity);
			_material.SetVector("direction", Quaternion.AngleAxis(Random.Range(0, 360) * colorIntensity, Vector3.forward) * Vector4.one);
			flicker = 0;
			_flickerTime = Random.value;
		}

		if (colorIntensity == 0)
			_material.SetFloat("filterRadius", 0);

		_glitchup += Time.deltaTime * flipIntensity;
		if (_glitchup > _glitchupTime)
		{
			if (Random.value < 0.8f * flipIntensity)
				_material.SetFloat("flip_up", Random.Range(0.5f, 1f) * flipIntensity);
			else
				_material.SetFloat("flip_up", 0);

			_glitchup = 0;
			_glitchupTime = Random.value / 10f;
		}

		if (flipIntensity == 0)
			_material.SetFloat("flip_up", 0);

		_glitchdown += Time.deltaTime * flipIntensity;
		if (_glitchdown > _glitchdownTime)
		{
			if (Random.value < 0.8f * flipIntensity)
				_material.SetFloat("flip_down", 1 - Random.Range(0.5f, 1f) * flipIntensity);
			else
				_material.SetFloat("flip_down", 1);

			_glitchdown = 0;
			_glitchdownTime = Random.value / 10f;
		}

		if (flipIntensity == 0)
			_material.SetFloat("flip_down", 1);

		if (Random.value < 0.1 * intensity)
		{
			_material.SetFloat("displace", Random.value * intensity);
			_material.SetFloat("scale", 1 - Random.value * intensity);
		}
		else
			_material.SetFloat("displace", 0);

		Graphics.Blit(source, destination, _material);
	}

	public void Glitch()
    {
		startGlitch = true;
		timer = 0;
	}

	private bool CountTimer()
    {
		timer += Time.deltaTime;
		if(timer>glitchTime)
        {
			timer = 0;
			return true;
        }
		return false;
    }

	private void ResetGlitchEffect()
    {
		_material.SetFloat("_Intensity", 0);
		_material.SetFloat("_ColorIntensity", 0);
		_material.SetTexture("_DispTex", null);
		_material.SetFloat("filterRadius", 0);
		_material.SetVector("direction", Vector4.zero);
		_material.SetFloat("flip_up", 0);
		_material.SetFloat("flip_down", 1);
		_material.SetFloat("displace", 0);
	}
}
