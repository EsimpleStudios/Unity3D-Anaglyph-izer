/*~ StereoC.cs
.---------------------------------------------------------------------------.
|  Software: Esimple Studios Unity3D Anaglyph-izer - C# version             |
|   Version: 1.1                                                            |
|   Contact: info ( a t ) esimplestudios . com                              |
|      Info: http://blog.esimplestudios.com                                 |
|   Support: http://blog.esimplestudios.com                                 |
| ------------------------------------------------------------------------- |
|     Admin: Francesco Gallorini (project administrator)                    |
|	  Developers: Francesco Tozzi, Francesco Marcantoni                     |
|     Packaging: Gabriele Maidecchi                                         |
| Copyright (c) 2010, Esimple Studios All Rights Reserved.                  |
| ------------------------------------------------------------------------- |
|   License: Distributed under the GNU GENERAL PUBLIC LICENSE (GPL)         |
|            http://www.gnu.org/licenses/gpl.html                           |
| This program is distributed in the hope that it will be useful - WITHOUT  |
| ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or     |
| FITNESS FOR A PARTICULAR PURPOSE.                                         |
| ------------------------------------------------------------------------- |
| We offer innovative marketing and advertising solutions:                  |
| - Web3D and Unity3D development finalized to advergames                   |
|   and 3D configurators                                                    |
| - Marketing and Social Media Marketing consulting                         |
| - Web development finalized to dynamic ecommerce solution with 3D support |
'---------------------------------------------------------------------------'
*/

using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
[AddComponentMenu ("Anaglyphizer/Anaglyph-izer Cs Version")]

public class AnaglyphizerC : MonoBehaviour {

	
private		RenderTexture 		leftEyeRT;   
private 	RenderTexture		rightEyeRT;
private		GameObject	 		leftEye;
private		GameObject	 		rightEye;
internal 	Material	 		anaglyphMat;

internal	float		 		zvalue		= 0.0f; // original: 1.0


public		bool 		enableKeys			= true;
public		KeyCode 	downEyeDistance		= KeyCode.O;
public		KeyCode 	upEyeDistance		= KeyCode.P;
public		KeyCode 	downFocalDistance	= KeyCode.K;
public		KeyCode 	upFocalDistance		= KeyCode.L;

public		Shader		shader;




public class S3DV {
   internal static	float	 eyeDistance = 0.02f;
   internal static	float	focalDistance = 10f;
};

void Start () {
	// Code Added by esimple adding Various render mode
	
	if ( shader == null ) {
		Debug.LogError("No Shader Found Please Drag The shader in the appropriate Field");
		this.enabled = false;
		return;
	}
	anaglyphMat = new Material(shader);
	// end of code added by esimple
	
	leftEye = new GameObject ("leftEye", typeof ( Camera ) );
	rightEye = new GameObject ("rightEye", typeof ( Camera ) );
	
	leftEye.camera.CopyFrom (camera);
	rightEye.camera.CopyFrom (camera);
	
	// Code Added by esimple adding GUI LAYER to dynamic camera
	leftEye.AddComponent<GUILayer>();
	rightEye.AddComponent<GUILayer>();
	
	leftEyeRT = new RenderTexture (Screen.width, Screen.height, 24);
	rightEyeRT = new RenderTexture (Screen.width, Screen.height, 24);
	
	leftEye.camera.targetTexture = leftEyeRT;
	rightEye.camera.targetTexture = rightEyeRT;
	  
	anaglyphMat.SetTexture ("_LeftTex", leftEyeRT);
	anaglyphMat.SetTexture ("_RightTex", rightEyeRT);
	  
	leftEye.camera.depth = camera.depth -2;
	rightEye.camera.depth = camera.depth -1;
	
	leftEye.transform.position = transform.position + transform.TransformDirection(-S3DV.eyeDistance, 0f, 0f);
	rightEye.transform.position = transform.position + transform.TransformDirection(S3DV.eyeDistance, 0f, 0f);
	
	leftEye.camera.projectionMatrix = projectionMatrix(true);
	rightEye.camera.projectionMatrix = projectionMatrix(false);

	leftEye.transform.LookAt (transform.position + (transform.TransformDirection (Vector3.forward) * S3DV.focalDistance));
	rightEye.transform.LookAt (transform.position + (transform.TransformDirection (Vector3.forward) * S3DV.focalDistance));
   
	leftEye.transform.parent = transform;
	rightEye.transform.parent = transform;
	
	camera.cullingMask = 0;
	camera.backgroundColor = new Color (0f,0f,0f,0f);
	camera.clearFlags = CameraClearFlags.Nothing;
}

void Stop () {
}

void UpdateView() {
	leftEye.camera.depth = camera.depth -2;
	rightEye.camera.depth = camera.depth -1;
	
	leftEye.transform.position = transform.position + transform.TransformDirection(-S3DV.eyeDistance, 0f, 0f);
	rightEye.transform.position = transform.position + transform.TransformDirection(S3DV.eyeDistance, 0f, 0f);
	
	leftEye.camera.projectionMatrix = projectionMatrix(true);
	rightEye.camera.projectionMatrix = projectionMatrix(false);
	
	leftEye.transform.LookAt (transform.position + (transform.TransformDirection (Vector3.forward) * S3DV.focalDistance));
	rightEye.transform.LookAt (transform.position + (transform.TransformDirection (Vector3.forward) * S3DV.focalDistance));
	
	leftEye.transform.parent = transform;
	rightEye.transform.parent = transform;
}

void LateUpdate() {
   UpdateView();
   
   if (enableKeys) {
      // o and p
      float eyeDistanceAdjust	= 	0.01f;
      if (Input.GetKeyDown(upEyeDistance)) {
         S3DV.eyeDistance += eyeDistanceAdjust;
      } else if (Input.GetKeyDown(downEyeDistance)) {
         S3DV.eyeDistance -= eyeDistanceAdjust;
      }
      
      // k and l
      float focalDistanceAdjust	= 0.5f;
      if (Input.GetKeyDown(upFocalDistance)) {
         //Debug.Log("focal up");
         S3DV.focalDistance += focalDistanceAdjust;
      } else if (Input.GetKeyDown(downFocalDistance)) {
         S3DV.focalDistance -= focalDistanceAdjust;
      }
   }
}

void OnRenderImage ( RenderTexture source, RenderTexture destination ) {
   RenderTexture.active = destination;
   GL.PushMatrix();
   GL.LoadOrtho();
   for(int i = 0; i < anaglyphMat.passCount; i++) {
      anaglyphMat.SetPass(i);
      DrawQuad();
   }
   GL.PopMatrix();
}
 
private void DrawQuad() {
   GL.Begin (GL.QUADS);      
      GL.TexCoord2( 0.0f, 0.0f ); GL.Vertex3( 0.0f, 0.0f, zvalue );
      GL.TexCoord2( 1.0f, 0.0f ); GL.Vertex3( 1.0f, 0.0f, zvalue );
      GL.TexCoord2( 1.0f, 1.0f ); GL.Vertex3( 1.0f, 1.0f, zvalue );
      GL.TexCoord2( 0.0f, 1.0f ); GL.Vertex3( 0.0f, 1.0f, zvalue );
   GL.End();
} 

Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far )
{       
    float x =  (2.0f * near) / (right - left);
    float y =  (2.0f * near) / (top - bottom);
    float a =  (right + left) / (right - left);
    float b =  (top + bottom) / (top - bottom);
    float c = -(far + near) / (far - near);
    float d = -(2.0f * far * near) / (far - near);
    float e = -1.0f;

    Matrix4x4 m = new Matrix4x4();
    m[0,0] = x;  m[0,1] = 0f;  m[0,2] = a;  m[0,3] = 0f;
    m[1,0] = 0f;  m[1,1] = y;  m[1,2] = b;  m[1,3] = 0f;
    m[2,0] = 0f;  m[2,1] = 0f;  m[2,2] = c;  m[2,3] = d;
    m[3,0] = 0f;  m[3,1] = 0f;  m[3,2] = e;  m[3,3] = 0f;
    return m;
}

Matrix4x4 projectionMatrix(bool isLeftEye ) {
   float 	left;
   float 	right;
   float 	a;
   float 	b;
   float 	fov;
   
   fov = camera.fieldOfView / 180.0f * Mathf.PI;  // convert FOV to radians
 
   float aspect = camera.aspect;
 
   a = camera.nearClipPlane * Mathf.Tan(fov * 0.5f);
   b = camera.nearClipPlane / S3DV.focalDistance;
   
   if (isLeftEye)      	// left camera
   {
      left  = - aspect * a + (S3DV.eyeDistance) * b;
      right =   aspect * a + (S3DV.eyeDistance) * b;
   }
   else         		// right camera
   {
      left  = - aspect * a - (S3DV.eyeDistance) * b;
      right =   aspect * a - (S3DV.eyeDistance) * b;
   }

    return PerspectiveOffCenter(left, right, -a, a, camera.nearClipPlane, camera.farClipPlane);
    
} 
}


