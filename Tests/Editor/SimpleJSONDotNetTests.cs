using NUnit.Framework;
using SimpleJSON;
using UnityEngine;
using System.Collections.Generic;

namespace Tests
{
    public class SimpleJSONDotNetTests
    {
        private static System.Random rng;
        private static double RandomDouble
        {
            get
            {
                return rng.NextDouble();
            }
        }

        private static int RandomInt
        {
            get
            {
                return rng.Next();
            }
        }

        private static Dictionary<string, object> dictionary;
        private JSONObject jsonObject;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            rng = new System.Random();

            dictionary = new Dictionary<string, object>();
            dictionary["decimal"] = decimal.MaxValue * (decimal)RandomDouble;
            dictionary["char"] = (char)(char.MaxValue * RandomDouble);
            dictionary["uint"] = (uint)(uint.MaxValue * RandomDouble);
            dictionary["byte"] = (byte)(byte.MaxValue * RandomDouble);
            dictionary["sbyte"] = (sbyte)(sbyte.MaxValue * RandomDouble);
            dictionary["short"] = (short)(short.MaxValue * RandomDouble);
            dictionary["ushort"] = (ushort)(ushort.MaxValue * RandomDouble);
            dictionary["utcdatetime"] = System.DateTime.UtcNow;
            dictionary["datetime"] = System.DateTime.Now;
            dictionary["timespan"] = new System.TimeSpan((long)(long.MaxValue * RandomDouble));
            dictionary["guid"] = System.Guid.NewGuid();

            var bytes = new byte[64];
            rng.NextBytes(bytes);

            dictionary["bytearray"] = bytes;
            dictionary["bytelist"] = new List<byte>(bytes);

            var str = "abcdefghijklmnopqrstuvwxyz0123456789";

            var stringArray = new string[str.Length];
            var stringList = new List<string>();

            for (int i = 0; i < str.Length; i++)
            {
                var character = str[i].ToString();
                stringArray[i] = character;
                stringList.Add(character);
            }

            dictionary["stringarray"] = stringArray;
            dictionary["stringlist"] = stringList;

            dictionary["int?"] = (int?)RandomInt;
            dictionary["float?"] = (float?)(float.MaxValue * RandomDouble);
            dictionary["double?"] = (double?)(double.MaxValue * RandomDouble);
            dictionary["bool?"] = (bool?)(RandomDouble > 0.5 ? true : false);
            dictionary["long?"] = (long?)(long.MaxValue * RandomDouble);
            dictionary["short?"] = (short?)(short.MaxValue * RandomDouble);
        }

        [SetUp]
        public void SetUp()
        {
            jsonObject = new JSONObject();
        }

        [Test]
        public void DecimalTest()
        {
            var key = "decimal";
            var val = (decimal)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (decimal)deserializedObject[key]);
        }

        [Test]
        public void CharTest()
        {
            var key = "char";
            var val = (char)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (char)deserializedObject[key]);
        }

        [Test]
        public void UIntTest()
        {
            var key = "uint";
            var val = (uint)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (uint)deserializedObject[key]);
        }

        [Test]
        public void ByteTest()
        {
            var key = "byte";
            var val = (byte)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (byte)deserializedObject[key]);
        }

        [Test]
        public void SByteTest()
        {
            var key = "sbyte";
            var val = (sbyte)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (sbyte)deserializedObject[key]);
        }

        [Test]
        public void ShortTest()
        {
            var key = "short";
            var val = (short)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (short)deserializedObject[key]);
        }

        [Test]
        public void UShortTest()
        {
            var key = "ushort";
            var val = (ushort)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (ushort)deserializedObject[key]);
        }

        [Test]
        public void UTCDateTimeTest()
        {
            var key = "utcdatetime";
            var val = (System.DateTime)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (System.DateTime)deserializedObject[key]);
        }

        [Test]
        public void DateTimeTest()
        {
            var key = "datetime";
            var val = (System.DateTime)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (System.DateTime)deserializedObject[key]);
        }

        [Test]
        public void TimeSpanTest()
        {
            var key = "timespan";
            var val = (System.TimeSpan)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (System.TimeSpan)deserializedObject[key]);
        }

        [Test]
        public void GuidTest()
        {
            var key = "guid";
            var val = (System.Guid)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (System.Guid)deserializedObject[key]);
        }

        [Test]
        public void ByteArrayTest()
        {
            var key = "bytearray";
            var val = (byte[])dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (byte[])deserializedObject[key]);
        }

        [Test]
        public void ByteListTest()
        {
            var key = "bytelist";
            var val = (List<byte>)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (List<byte>)deserializedObject[key]);
        }

        [Test]
        public void StringArrayTest()
        {
            var key = "stringarray";
            var val = (string[])dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (string[])deserializedObject[key]);
        }

        [Test]
        public void StringListTest()
        {
            var key = "stringlist";
            var val = (List<string>)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (List<string>)deserializedObject[key]);
        }

        [Test]
        public void NullableIntTest()
        {
            var key = "int?";
            var val = (int?)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (int?)deserializedObject[key]);

            deserializedObject[key] = null;

            Assert.AreEqual(null, (int?)deserializedObject[key]);
        }

        [Test]
        public void NullableFloatTest()
        {
            var key = "float?";
            var val = (float?)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (float?)deserializedObject[key]);

            deserializedObject[key] = null;

            Assert.AreEqual(null, (float?)deserializedObject[key]);
        }

        [Test]
        public void NullableDoubleTest()
        {
            var key = "double?";
            var val = (double?)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (double?)deserializedObject[key]);

            deserializedObject[key] = null;

            Assert.AreEqual(null, (double?)deserializedObject[key]);
        }

        [Test]
        public void NullableBoolTest()
        {
            var key = "bool?";
            var val = (bool?)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (bool?)deserializedObject[key]);

            deserializedObject[key] = null;

            Assert.AreEqual(null, (bool?)deserializedObject[key]);
        }

        [Test]
        public void NullableLongTest()
        {
            var key = "long?";
            var val = (long?)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (long?)deserializedObject[key]);

            deserializedObject[key] = null;

            Assert.AreEqual(null, (long?)deserializedObject[key]);
        }

        [Test]
        public void NullableShortTest()
        {
            var key = "short?";
            var val = (short?)dictionary[key];

            jsonObject[key] = val;

            var jsonObjectString = jsonObject.ToString();

            Debug.Log(jsonObjectString);

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(val, (short?)deserializedObject[key]);

            deserializedObject[key] = null;

            Assert.AreEqual(null, (short?)deserializedObject[key]);
        }
    }
}
