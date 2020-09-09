using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SimpleJSON;

namespace Tests
{
    public class SimpleJSONUnityTests
    {
        [Test]
        public void Vector2Test()
        {
            var vec2 = Random.insideUnitCircle;

            var jsonObject = new JSONObject().WriteVector2(vec2);
            var jsonArray = new JSONArray().WriteVector2(vec2);

            var jsonObjectString = jsonObject.ToString();
            var jsonArrayString = jsonArray.ToString();

            Debug.Log($"{vec2.GetType().Name} object: {jsonObjectString} array: {jsonArrayString}");

            var deserializedObject = JSON.Parse(jsonObjectString).ReadVector2();
            var deserializedArray = JSON.Parse(jsonArrayString).ReadVector2();

            Assert.AreEqual(vec2, deserializedObject);
            Assert.AreEqual(vec2, deserializedArray);
        }

        [Test]
        public void Vector3Test()
        {
            var vec3 = Random.insideUnitSphere;

            var jsonObject = new JSONObject().WriteVector3(vec3);
            var jsonArray = new JSONArray().WriteVector3(vec3);

            var jsonObjectString = jsonObject.ToString();
            var jsonArrayString = jsonArray.ToString();

            Debug.Log($"{vec3.GetType().Name} object: {jsonObjectString} array: {jsonArrayString}");

            var deserializedObject = JSON.Parse(jsonObjectString).ReadVector3();
            var deserializedArray = JSON.Parse(jsonArrayString).ReadVector3();

            Assert.AreEqual(vec3, deserializedObject);
            Assert.AreEqual(vec3, deserializedArray);
        }

        [Test]
        public void Vector4Test()
        {
            Vector4 vec4 = Random.insideUnitSphere;
            vec4.w = Random.value;

            var jsonObject = new JSONObject().WriteVector4(vec4);
            var jsonArray = new JSONArray().WriteVector4(vec4);

            var jsonObjectString = jsonObject.ToString();
            var jsonArrayString = jsonArray.ToString();

            Debug.Log($"{vec4.GetType().Name} object: {jsonObjectString} array: {jsonArrayString}");

            var deserializedObject = JSON.Parse(jsonObjectString).ReadVector4();
            var deserializedArray = JSON.Parse(jsonArrayString).ReadVector4();

            Assert.AreEqual(vec4, deserializedObject);
            Assert.AreEqual(vec4, deserializedArray);
        }

        [Test]
        public void QuaternionTest()
        {
            Quaternion quat = Random.rotation;

            var jsonObject = new JSONObject().WriteQuaternion(quat);
            var jsonArray = new JSONArray().WriteQuaternion(quat);

            var jsonObjectString = jsonObject.ToString();
            var jsonArrayString = jsonArray.ToString();

            Debug.Log($"{quat.GetType().Name} object: {jsonObjectString} array: {jsonArrayString}");

            var deserializedObject = JSON.Parse(jsonObjectString).ReadQuaternion();
            var deserializedArray = JSON.Parse(jsonArrayString).ReadQuaternion();

            Assert.AreEqual(quat, deserializedObject);
            Assert.AreEqual(quat, deserializedArray);
        }

        [Test]
        public void RectTest()
        {
            Rect rect = new Rect(Random.insideUnitCircle * 10.0f, Random.insideUnitCircle * 10.0f);

            var jsonObject = new JSONObject().WriteRect(rect);
            var jsonArray = new JSONArray().WriteRect(rect);

            var jsonObjectString = jsonObject.ToString();
            var jsonArrayString = jsonArray.ToString();

            Debug.Log($"{rect.GetType().Name} object: {jsonObjectString} array: {jsonArrayString}");

            var deserializedObject = JSON.Parse(jsonObjectString).ReadRect();
            var deserializedArray = JSON.Parse(jsonArrayString).ReadRect();

            Assert.AreEqual(rect, deserializedObject);
            Assert.AreEqual(rect, deserializedArray);
        }

        [Test]
        public void RectOffsetTest()
        {
            RectOffset rectOffset = new RectOffset(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));

            var jsonObject = new JSONObject().WriteRectOffset(rectOffset);
            var jsonArray = new JSONArray().WriteRectOffset(rectOffset);

            var jsonObjectString = jsonObject.ToString();
            var jsonArrayString = jsonArray.ToString();

            Debug.Log($"{rectOffset.GetType().Name} object: {jsonObjectString} array: {jsonArrayString}");

            var deserializedObject = JSON.Parse(jsonObjectString).ReadRectOffset();
            var deserializedArray = JSON.Parse(jsonArrayString).ReadRectOffset();

			Assert.AreEqual(rectOffset.left, deserializedObject.left);
			Assert.AreEqual(rectOffset.right, deserializedObject.right);
			Assert.AreEqual(rectOffset.top, deserializedObject.top);
			Assert.AreEqual(rectOffset.bottom, deserializedObject.bottom);

			Assert.AreEqual(rectOffset.left, deserializedArray.left);
			Assert.AreEqual(rectOffset.right, deserializedArray.right);
			Assert.AreEqual(rectOffset.top, deserializedArray.top);
			Assert.AreEqual(rectOffset.bottom, deserializedArray.bottom);
		}

        [Test]
        public void MatrixTest()
        {
            Matrix4x4 matrix = new Matrix4x4();
            for (int i=0; i < 16; i++)
			{
                matrix[i] = Random.Range(-1.0f, 1.0f);
			}

            var jsonArray = new JSONArray().WriteMatrix(matrix);

            var jsonArrayString = jsonArray.ToString();

            Debug.Log($"{matrix.GetType().Name} array: {jsonArrayString}");

            var deserializedArray = JSON.Parse(jsonArrayString).ReadMatrix();

            Assert.AreEqual(matrix, deserializedArray);
        }

        [Test]
        public void ColorTest()
        {           
            Color color = new Color(Random.value, Random.value, Random.value, Random.value);

			var jsonObject = new JSONObject().WriteColor(color);
			var jsonArray = new JSONArray().WriteColor(color);
            var jsonString = new JSONString().WriteColor(color);

            var jsonObjectString = jsonObject.ToString();
			var jsonArrayString = jsonArray.ToString();
            var jsonStringString = jsonString.ToString();

			Debug.Log($"{color.GetType().Name} object: {jsonObjectString} array: {jsonArrayString} string: {jsonStringString}");

			var deserializedObject = JSON.Parse(jsonObjectString).ReadColor();
			var deserializedArray = JSON.Parse(jsonArrayString).ReadColor();

            Assert.AreEqual(color, deserializedObject);
            Assert.AreEqual(color, deserializedArray);
        }

        [Test]
        public void Color32Test()
        {
            Color32 color32 = new Color(Random.value, Random.value, Random.value, Random.value);

            var jsonObject = new JSONObject().WriteColor32(color32);
            var jsonArray = new JSONArray().WriteColor32(color32);
            var jsonString = new JSONString().WriteColor32(color32);

            var jsonObjectString = jsonObject.ToString();
            var jsonArrayString = jsonArray.ToString();
            var jsonStringString = jsonString.ToString();

            Debug.Log($"{color32.GetType().Name} object: {jsonObjectString} array: {jsonArrayString} string: {jsonStringString}");

            var deserializedObject = JSON.Parse(jsonObjectString).ReadColor32();
            var deserializedArray = JSON.Parse(jsonArrayString).ReadColor32();
            var deserializedString = JSON.Parse(jsonStringString).ReadColor32();

            Assert.AreEqual(color32, deserializedObject);
            Assert.AreEqual(color32, deserializedArray);
            Assert.AreEqual(color32, deserializedString);
		}
    }
}