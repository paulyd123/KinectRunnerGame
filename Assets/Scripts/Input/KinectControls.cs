using Assets.Scripts;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsPreview.Kinect;

public sealed partial class kinectControls : Page
{

	private KinectSensor kinectSensor = null;

	public kinectControls()
	{
		// one sensor is currently supported
		this.kinectSensor = KinectSensor.GetDefault();

		// open the sensor
		this.kinectSensor.Open();

		this.InitializeComponent();
	}
} u s i n g   U n i t y E n g i n 
 u s i n g   S y s t e m ; 
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ; 
 u s i n g   S y s t e m . I O ; 
 u s i n g   S y s t e m . L i n q ; 
 u s i n g   S y s t e m . R u n t i m e . I n t e r o p S e r v i c e s . W i n d o w s R u n t i m e ; 
 u s i n g   W i n d o w s . F o u n d a t i o n ; 
 u s i n g   W i n d o w s . F o u n d a t i o n . C o l l e c t i o n s ; 
 u s i n g   W i n d o w s . U I . X a m l ; 
 u s i n g   W i n d o w s . U I . X a m l . C o n t r o l s ; 
 u s i n g   W i n d o w s . U I . X a m l . C o n t r o l s . P r i m i t i v e s ; 
 u s i n g   W i n d o w s . U I . X a m l . D a t a ; 
 u s i n g   W i n d o w s . U I . X a m l . I n p u t ; 
 u s i n g   W i n d o w s . U I . X a m l . M e d i a ; 
 u s i n g   W i n d o w s . U I . X a m l . N a v i g a t i o n ; 
 u s i n g   W i n d o w s P r e v i e w . K i n e c t ; 
 
 p u b l i c   s e a l e d   p a r t i a l   c l a s s   K i n e c t C o n t r o l s   :   P a g e 
 { 
 
 	 p r i v a t e   K i n e c t S e n s o r   k i n e c t S e n s o r   =   n u l l ; 
 
 	 p u b l i c   K i n e c t C o n t r o l s ( ) 
 	 { 
 	 	 / /   o n e   s e n s o r   i s   c u r r e n t l y   s u p p o r t e d 
 	 	 t h i s . k i n e c t S e n s o r   =   K i n e c t S e n s o r . G e t D e f a u l t ( ) ; 
 
 	 	 / /   o p e n   t h e   s e n s o r 
 	 	 t h i s . k i n e c t S e n s o r . O p e n ( ) ; 
 
 	 	 t h i s . I n i t i a l i z e C o m p o n e n t ( ) ; 
 	 } 
 } 