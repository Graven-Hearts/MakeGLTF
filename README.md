# MakeGLTF
MakeGLTF for Linux/Windows 10/11.  Create PBR materials for Second Life.<br>
<br>
Contact Gabriele Graves in Second Life for more information about MakeGLTF.<br>
<br>
Based on GLTF Packer for Windows by Extrude Ragu<br>
<br>
https://aiaicapta.in/gltf-packer/<br>
<br>
<b>DISCLAIMER: MakeGLTF and source code is supplied as-is under the MIT License with no warranties or support expressed or implied.</b><br>

### Development and build environment
Ubuntu 22.04 or later, Windows 10/11. 64-Bit only<br>
DotNet (Core) SDK v8<br>
VSCode with "C#" and "Avalonia for VSCode" extensions installed.<br>

### Installation
Builds are in the repo under the "Builds" folder.<br>
The builds are in tar format.<br>
<br>
Download and extract the files from the build file.<br>
CD into the "makeglft" folder.<br>
Installation on Ubuntu v22.04 or later the app doesn't require any further installation and should run from the command line.<br>
Installation on Linux variants the requires GLIBC v2.35 or later installed.<br>
Installation on Windows 10 or later is just the same with no special requirements.

### Usage and differences from GLTF Packer
The app works slightly different to GLTF Packer on Windows.  The toolkit used to build the app doesn't support drag and drop.
To set an image just click on any image placeholder and a file selection dialog will appear for that image.
The only other difference of note is that the output folder is given the name of the material you type in and all files are placed into it including the .gltf file.
Pretty much everything else works the same.

### Builds
v1.0:<br>
Linux: [makeglft-linux-v1.0.tar.xz](https://github.com/Graven-Hearts/MakeGLTF/blob/429eb106269036d37907d17cc1a2de469019e71b/Builds/makegltf-linux-v1.0.tar.xz)<br>
Windows 10/11: [makeglft-win64-v1.0.zip](https://github.com/Graven-Hearts/MakeGLTF/blob/429eb106269036d37907d17cc1a2de469019e71b/Builds/makefltf-win64-v1.0.zip)<br>
