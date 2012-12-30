using UnityEngine;
using System.Collections;

public class FXManager : MonoBehaviour {
	
	public Main main;
	
	public CameraMotionBlur MotionBlur_Low;
	public CameraMotionBlur MotionBlur_High;
	
	public Vignetting Vignetting_Low;
	public Vignetting Vignetting_High;
	
	public Vignetting ChromaticAbberation_Low;
	public Vignetting ChromaticAbberation_High;
	
	public Crease Creasing_Low;
	public Crease Creasing_High;
	
	public SSAOEffect AmbientOcclusion_Low;
	public SSAOEffect AmbientOcclusion_High;
	
	public AntialiasingAsPostEffect PostProcessedAntiAliasing_Low;
	public AntialiasingAsPostEffect PostProcessedAntiAliasing_High;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		switch(main.Options_MotionBlur)
		{
			
			case(Option.Off):
				MotionBlur_Low.enabled = false;
				MotionBlur_High.enabled = false;
				break;
				
			case(Option.Low):
				MotionBlur_Low.enabled = true;
				MotionBlur_High.enabled = false;
				break;
				
			case(Option.High):
				MotionBlur_Low.enabled = false;
				MotionBlur_High.enabled = true;
				break;
			
		}
		
		
		
		switch(main.Options_Vignetting)
		{
			
			case(Option.Off):
				Vignetting_Low.enabled = false;
				Vignetting_High.enabled = false;
				break;
				
			case(Option.Low):
				Vignetting_Low.enabled = true;
				Vignetting_High.enabled = false;
				break;
				
			case(Option.High):
				Vignetting_Low.enabled = false;
				Vignetting_High.enabled = true;
				break;
			
		}
		
		
		
		switch(main.Options_ChromaticAbberation)
		{
			
			case(Option.Off):
				ChromaticAbberation_Low.enabled = false;
				ChromaticAbberation_High.enabled = false;
				break;
				
			case(Option.Low):
				ChromaticAbberation_Low.enabled = true;
				ChromaticAbberation_High.enabled = false;
				break;
				
			case(Option.High):
				ChromaticAbberation_Low.enabled = false;
				ChromaticAbberation_High.enabled = true;
				break;
			
		}
		
		
		
		switch(main.Options_Creasing)
		{
			
			case(Option.Off):
				Creasing_Low.enabled = false;
				Creasing_High.enabled = false;
				break;
				
			case(Option.Low):
				Creasing_Low.enabled = true;
				Creasing_High.enabled = false;
				break;
				
			case(Option.High):
				Creasing_Low.enabled = false;
				Creasing_High.enabled = true;
				break;
			
		}
		
		
		
		switch(main.Options_AmbientOcclusion)
		{
			
			case(Option.Off):
				AmbientOcclusion_Low.enabled = false;
				AmbientOcclusion_High.enabled = false;
				break;
				
			case(Option.Low):
				AmbientOcclusion_Low.enabled = true;
				AmbientOcclusion_High.enabled = false;
				break;
				
			case(Option.High):
				AmbientOcclusion_Low.enabled = false;
				AmbientOcclusion_High.enabled = true;
				break;
			
		}
		
		
		
		switch(main.Options_PostProcessedAntiAliasing)
		{
			
			case(Option.Off):
				PostProcessedAntiAliasing_Low.enabled = false;
				PostProcessedAntiAliasing_High.enabled = false;
				break;
				
			case(Option.Low):
				PostProcessedAntiAliasing_Low.enabled = true;
				PostProcessedAntiAliasing_High.enabled = false;
				break;
				
			case(Option.High):
				PostProcessedAntiAliasing_Low.enabled = false;
				PostProcessedAntiAliasing_High.enabled = true;
				break;
			
		}

	}
}
