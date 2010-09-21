/*~ README.txt
.---------------------------------------------------------------------------.
|  Software: Esimple Studios Unity3D Anaglyph-izer Pack                     |
|   Version: 1.0                                                            |
|   Compatibility: requires Unity3D Pro (using rendertexture), compatible   |
|                  with both Unity 2.6.1 and Unity 3.0f1 (separate shaders) |
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


Everything started from this forum post: http://forum.unity3d.com/viewtopic.php?t=12525&highlight=anaglyph

We took the script posted by aNTeNNa trEE of the Unity Demo Team.
We added the modification posted by Monark implementing the projection matrix method, to simplify vision for the eyes.
Monark also posted a modified shader from the original one, which we included by default.
Then, we cooked all our modifications:

- created 3 external shaders for the three anaglyph modes (Red/Cyan, Red/Blue, Red/Green)
- automatic linking of the shader to the camera
- added automatic support for GUITexture and GUIText
- converted the original JS script into C#

And we finally released it under GPL for everyone to enjoy.
If you are even remotely like us, you'll spend hours firing at stuff in the bootcamp demo like idiots.

We included shaders for both 2.6.1 and 3.0f1 versions and two packages for each version, if you want it the easy way.

Installation

1) Link the AnaglyphizerJ.Js (Javascript) or AnaglyphizerC.Cs (C#) to the camera.

Known issues

The red/green and red/blue shaders are not fully implemented. We are still trying to make them work properly, that's also why we released everything on GitHub, hoping for involvement and cooperation of more Unity3D developers.

Inspector variables to change script parameters:

Enable Keys -> Flag 
Allows you to use keys to change values

Down Eye Distance -> Key
Lowers the eyes distance

Up Eye Distance -> Key
Raises the eyes distance

Down Focal Distance -> Key
Lowers the focal distance

Up Focal Distance -> Key
Raises the focal distance

renderMode -> Enum
Sets the anaglyph mode: Red_Cyan, Red_Blue, Red_Green