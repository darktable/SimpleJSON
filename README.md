A simple JSON Parser / builder
------------------------------

SimpleJSON mainly has been written as a simple JSON parser. It can build a JSON string
from the node-tree, or generate a node tree from any valid JSON string.

Written by Bunny83
2012-06-09

Add the following to your package.json "dependencies" section to import SimpleJSON:
    "com.github.bunny83.simplejson": "https://github.com/darktable/SimpleJSON.git#unity-package",

SimpleJSONBinary is an extension of the SimpleJSON framework to provide methods to
serialize a JSON object tree into a compact binary format. Optionally the
binary stream can be compressed with the SharpZipLib when using the define
"USE_SharpZipLib"

Those methods where originally part of the framework but since it's rarely
used I've extracted this part into this seperate module file.

You can use the define "SimpleJSON_ExcludeBinary" to selectively disable
this extension without the need to remove the file from the project.

If you want to use compression when saving to file / stream / B64 you have to include
SharpZipLib ( http://www.icsharpcode.net/opensource/sharpziplib/ ) in your project and
define "USE_SharpZipLib" at the top of the file

SimpleJSONUnity is a Unity extension for the SimpleJSON framework. It does
only work together with the SimpleJSON.cs
It provides several helpers and conversion operators to serialize/deserialize
common Unity types such as Vector2/3/4, Rect, RectOffset, Quaternion and
Matrix4x4 as JSONObject or JSONArray.
This extension will add 3 static settings to the JSONNode class:
( VectorContainerType, QuaternionContainerType, RectContainerType ) which
control what node type should be used for serializing the given type. So a
Vector3 as array would look like [12,32,24] and {"x":12, "y":32, "z":24} as
object.
