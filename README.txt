/*~ README.txt
.---------------------------------------------------------------------------.
|  Software: Esimple Studios Unity3D Anaglyph-izer Pack                     |
|   Version: 1.2                                                            |
|   Compatibility: requires Unity3D Pro (using rendertexture), compatible   |
|                  with both Unity 2.6.1 and Unity 3.0f2 (separate shaders) |
|   Contact: info ( a t ) esimplestudios . com                              |
|      Info: http://bit.ly/u3dack                                           |
|   Support: http://bit.ly/u3dack                                           |
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


Everything started from this forum post: http://forum.unity3d.com/viewtopic.php?t=12525&highlight=anaglyph

We took the script posted by aNTeNNa trEE of the Unity Demo Team.
We added the modification posted by Monark implementing the projection matrix method, to simplify vision for the eyes.
Monark also posted a modified shader from the original one, which we included by default.

Then, we cooked all our modifications, and we finally released it under GPL for everyone to enjoy.

If you are even remotely like us, you'll spend hours firing at stuff in the bootcamp demo like idiots.

WHATÕS NEW (1.2)

- thereÕs no need anymore to link the various shaders
- included a single parametric shader
- itÕs now possible to create an infinite number of materials adjustable with parameters (see below) included configurations for Magenta/Green and Red/Blue modes
- added a ÒUse Projection MatrixÓ flag to optimize the focal distance based on the cameraÕs Field of View
- changed the folder structure to be more ÒUnity-friendlyÓ
- added a fancy Demo (ONLY 3.x) that we promise will make you jump from your chair. ItÕs already packed in the ÒWith_DemoÓ package and available as a project in the Assets/Demo folder

INSTALLATION

ONLY FOR 2.6.1: replace the shader in the package with the correct version in the source/Shaders/2.6.1 folder

1) Link the AnaglyphizerJ.Js (Javascript) or AnaglyphizerC.Cs (C#) to the camera. Or Select Camera and Open Component -> Anaglyphizer -> Anaglyphizer(Cs or Js)
2) Link your material or one of those included in the prefab folder to the script

Inspector variables to change script parameters:

Use Projection Matrix -> Flag
Improve correct lens calculation based on Field Of View parameter