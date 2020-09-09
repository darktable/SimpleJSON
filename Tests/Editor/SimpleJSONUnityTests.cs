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

            for (int i=0; i < 2; i++)
            {
                Assert.AreEqual(vec2[i], deserializedObject[i]);
                Assert.AreEqual(vec2[i], deserializedArray[i]);
            }
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

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(vec3[i], deserializedObject[i]);
                Assert.AreEqual(vec3[i], deserializedArray[i]);
            }
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

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(vec4[i], deserializedObject[i]);
                Assert.AreEqual(vec4[i], deserializedArray[i]);
            }
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

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(quat[i], deserializedObject[i]);
                Assert.AreEqual(quat[i], deserializedArray[i]);
            }
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

            Assert.AreEqual(rect.x, deserializedObject.x);
            Assert.AreEqual(rect.y, deserializedObject.y);
            Assert.AreEqual(rect.width, deserializedObject.width);
            Assert.AreEqual(rect.height, deserializedObject.height);

            Assert.AreEqual(rect.x, deserializedArray.x);
            Assert.AreEqual(rect.y, deserializedArray.y);
            Assert.AreEqual(rect.width, deserializedArray.width);
            Assert.AreEqual(rect.height, deserializedArray.height);
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
    }
}
