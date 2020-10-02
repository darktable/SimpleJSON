using System.Collections.Generic;
using NUnit.Framework;
using SimpleJSON;
using UnityEngine;

namespace Tests
{
    public class SimpleJSONTests
    {
        private const long MAX_SAFE_INTEGER = (long)1 << 53;
        private const long MIN_SAFE_INTEGER = -(long)1 << 53;

        private const string jsonString = "{ \"array\": [1.44,2,3], " +
                                          "\"object\": {\"key1\":\"value1\", \"key2\":256}, " +
                                          "\"string\": \"The quick brown fox \\\"jumps\\\" over the lazy dog \", " +
                                          "\"unicode\": \"\\u3041 Men\u00fa sesi\u00f3n\", " +
                                          "\"int\": 65536, " +
                                          "\"double\": 3.1415926, " +
                                          "\"bool\": true, " +
                                          "\"null\": null }";

        private const string arrayString = "[0,1,2,3,4,5]";
        private const string objectString = "{ \"zero\":0, \"one\":1, \"two\":2, \"three\":3, \"four\":4, \"five\":5 }";

        private JSONNode parsedJSON;

        private double[] doubleArray;
        private Dictionary<string, object> objectDictionary;
        private string stringValue;
        private string unicodeValue;
        private int intValue;
        private double doubleValue;
        private bool boolValue;
        private object nullValue;

        [SetUp]
        public void SetUp()
        {
            parsedJSON = JSON.Parse(jsonString);

            var jsonArray = parsedJSON["array"];

            doubleArray = new double[jsonArray.Count];
            for (int i = 0; i < jsonArray.Count; i++)
            {
                doubleArray[i] = jsonArray[i].AsDouble;
            }

            var jsonObject = parsedJSON["object"];

            objectDictionary = new Dictionary<string, object>();

            foreach (var key in jsonObject.Keys)
            {
                var value = jsonObject[key];

                objectDictionary[key] = value.IsString ? (object)value.Value : (object)value.AsDouble;
            }

            stringValue = parsedJSON["string"].Value;
            unicodeValue = parsedJSON["unicode"].Value;
            intValue = parsedJSON["int"].AsInt;
            doubleValue = parsedJSON["double"].AsDouble;
            boolValue = parsedJSON["bool"].AsBool;
            nullValue = parsedJSON["null"].AsObject;
        }

        [Test]
        public void ArrayTest()
        {
            var jsonArray = JSON.ToJSONNode(doubleArray);

            for (int i = 0; i < jsonArray.Count; i++)
            {
                Assert.AreEqual(jsonArray[i], parsedJSON["array"][i]);

                Assert.AreEqual(jsonArray[i].AsDouble, doubleArray[i]);
            }
        }

        [Test]
        public void DictionaryTest()
        {
            var jsonObject = JSON.ToJSONNode(objectDictionary);

            foreach (var key in jsonObject.Keys)
            {
                Assert.AreEqual(jsonObject[key], parsedJSON["object"][key]);
            }
        }

        [Test]
        public void StringTest()
        {
            var jsonString = JSON.ToJSONNode(stringValue);

            Assert.AreEqual(jsonString, parsedJSON["string"]);

            Assert.AreEqual(stringValue, parsedJSON["string"].Value);

            var emptyJsonString = new JSONString(string.Empty);

            Assert.False(emptyJsonString.IsNull);
            Assert.AreEqual(emptyJsonString.Value, string.Empty);

            var nullJsonString = new JSONString(null);

            Assert.True(nullJsonString.IsNull);

            Assert.AreEqual(nullJsonString.Value, null);

            Assert.True(nullJsonString == JSONNull.CreateOrGet());
            Assert.False(nullJsonString == null);

            Assert.AreEqual(nullJsonString.ToString(), "null");
        }

        [Test]
        public void UnicodeTest()
        {
            var jsonString = JSON.ToJSONNode(unicodeValue);

            Assert.AreEqual(jsonString, parsedJSON["unicode"]);

            Assert.AreEqual(unicodeValue, parsedJSON["unicode"].Value);
        }

        [Test]
        public void IntegerTest()
        {
            var jsonNumber = JSON.ToJSONNode(intValue);

            Assert.AreEqual(jsonNumber, parsedJSON["int"]);

            Assert.AreEqual(parsedJSON["int"].AsInt, intValue);
        }

        [Test]
        public void DoubleTest()
        {
            var jsonNumber = JSON.ToJSONNode(doubleValue);

            Assert.AreEqual(jsonNumber, parsedJSON["double"]);

            Assert.AreEqual(parsedJSON["double"].AsDouble, doubleValue);
        }

        [Test]
        public void NumberTest()
        {
            var numberA = new JSONNumber(7.5);
            var numberB = JSON.Parse("7.5");

            Assert.AreEqual(numberA, numberB);

            Assert.AreEqual(numberA.AsDouble, 7.5);

            Assert.AreEqual(numberB, 7.5);

            Assert.AreEqual(numberB.AsFloat, 7.5f);
        }

        [Test]
        public void BooleanTest()
        {
            var jsonBool = JSON.ToJSONNode(boolValue);

            Assert.AreEqual(jsonBool, parsedJSON["bool"]);

            Assert.AreEqual(parsedJSON["bool"].AsBool, boolValue);
        }

        [Test]
        public void NullTest()
        {
            var jsonNull = JSON.ToJSONNode(nullValue);

            Assert.AreEqual(jsonNull, parsedJSON["null"]);

            Assert.IsNull(parsedJSON["null"].AsObject);

            Assert.True(parsedJSON["null"].IsNull);

            Assert.AreEqual(parsedJSON["null"], JSONNull.CreateOrGet());

            Assert.AreEqual(parsedJSON["null"].Value, null);

            Assert.True(parsedJSON["null"] == null);
        }

        [Test]
        public void ArrayGet()
        {
            var jsonArray = JSON.Parse(arrayString);

            Assert.AreEqual(jsonArray[2].AsInt, 2);
        }

        [Test]
        public void ArraySet()
        {
            var jsonArray = JSON.Parse(arrayString);

            jsonArray[5] = int.MaxValue;

            Assert.AreEqual(jsonArray[5].AsInt, int.MaxValue);
        }

        [Test]
        public void ArrayAdd()
        {
            var jsonArray = JSON.Parse(arrayString);

            jsonArray.Add(int.MinValue);

            Assert.AreEqual(jsonArray.Count, 7);

            Assert.AreEqual(jsonArray[6].AsInt, int.MinValue);

            Assert.Catch(delegate { jsonArray[8] = int.MaxValue; });

            Assert.Catch(delegate { jsonArray.Add("fail", 8); });
        }

        [Test]
        public void ArrayRemove()
        {
            var jsonArray = JSON.Parse(arrayString);

            var removed = jsonArray.Remove(1);

            Assert.AreEqual(removed.AsInt, 1);

            Assert.AreEqual(jsonArray.Count, 5);

            Assert.AreEqual(jsonArray[1].AsInt, 2);

            Assert.Catch(delegate { var fail = jsonArray.Remove("key"); });
        }

        [Test]
        public void DictionaryGet()
        {
            var jsonObject = JSON.Parse(objectString);

            Assert.AreEqual(jsonObject["two"].AsInt, 2);
        }

        [Test]
        public void DictionarySet()
        {
            var jsonObject = JSON.Parse(objectString);

            jsonObject["three"] = int.MaxValue;

            Assert.AreEqual(jsonObject["three"].AsInt, int.MaxValue);
        }

        [Test]
        public void DictionaryAdd()
        {
            var jsonObject = JSON.Parse(objectString);

            jsonObject.Add("six", new JSONNumber(6));

            Assert.AreEqual(jsonObject["six"].AsInt, 6);

            jsonObject["ninetynine"] = 99;

            Assert.AreEqual(jsonObject["ninetynine"].AsInt, 99);

            Assert.Catch(delegate { jsonObject.Add(new JSONString("fail")); });

            jsonObject["tier1"] = new JSONObject();

            Assert.Catch(delegate { jsonObject["tier1"].Add("failure"); });

            jsonObject["tier1"].Add("success", true);
            Assert.AreEqual(jsonObject["tier1"]["success"], true);
        }

        [Test]
        public void DictionaryRemove()
        {
            var jsonObject = JSON.Parse(objectString);

            Assert.Catch(delegate { var fail = jsonObject.Remove(3); });

            var removed = jsonObject.Remove("zero");

            Assert.AreEqual(removed.AsInt, 0);

            Assert.AreEqual(jsonObject.Count, 5);

            Assert.True(jsonObject["zero"].IsNull);

            Assert.AreEqual(jsonObject["zero"], JSONNull.CreateOrGet());

            Assert.AreEqual(jsonObject["zero"].Value, null);

            Assert.True(jsonObject["zero"] == null);
        }

        [Test]
        public void LazyCreatorTest()
        {
            var jsonObject = new JSONObject();

            jsonObject["one"] = 1;

            jsonObject["tier1"]["tier2"] = "second tier";

            Assert.True(jsonObject["tier1"].IsObject);
            Assert.AreEqual(jsonObject["tier1"]["tier2"], "second tier");

            jsonObject["array"].Add(0);
            jsonObject["array"].Add(1);
            jsonObject["array"].Add(99);

            Assert.True(jsonObject["array"].IsArray);
            Assert.AreEqual(jsonObject["array"][2], 99);

            Assert.Catch(delegate { jsonObject["one"]["two"] = "failure"; });

            Assert.Catch(delegate { jsonObject["tier1"][0] = true; });

            jsonObject["tier1"]["tier2"] = true;

            Assert.AreEqual(jsonObject["tier1"]["tier2"], true);

            jsonObject["array"][0] = "replaced";
            Assert.AreEqual(jsonObject["array"][0], "replaced");
        }

        [Test]
        public void SerializeTest()
        {
            var objectWrapper = new Dictionary<string, object>();
            objectWrapper["array"] = doubleArray;
            objectWrapper["object"] = objectDictionary;
            objectWrapper["string"] = stringValue;
            objectWrapper["unicode"] = unicodeValue;
            objectWrapper["int"] = intValue;
            objectWrapper["double"] = doubleValue;
            objectWrapper["bool"] = boolValue;
            objectWrapper["null"] = nullValue;

            var jsonString = JSON.Serialize(objectWrapper.ToJSONNode(), 4);

            Assert.AreEqual(jsonString, parsedJSON.ToString(4));

            jsonString = JSON.Serialize(objectWrapper, 0);

            Assert.AreEqual(jsonString, parsedJSON.ToString(0));

            Debug.LogFormat("Serialized result:\n{0}", jsonString);
        }

        [Test]
        public void NumericTest()
        {
            Assert.True(JSONNumber.IsNumeric(0));
            Assert.True(JSONNumber.IsNumeric(1L));
            Assert.True(JSONNumber.IsNumeric(2.0f));
            Assert.True(JSONNumber.IsNumeric(3.0));

            Assert.False(JSONNumber.IsNumeric('2'));
            Assert.False(JSONNumber.IsNumeric("two"));
        }

        [Test]
        public void MinMaxTest()
        {
            Debug.Log($"max safe int64: {MAX_SAFE_INTEGER} min safe int64: {MIN_SAFE_INTEGER}");

            var jsonObject = new JSONObject();
            jsonObject["maxLong"] = long.MaxValue;
            jsonObject["minLong"] = long.MinValue;
            jsonObject["maxULong"] = ulong.MaxValue;
            jsonObject["minULong"] = ulong.MinValue;
            jsonObject["maxDecimal"] = decimal.MaxValue;
            jsonObject["minDecimal"] = decimal.MinValue;

            jsonObject["maxSafeInt"] = MAX_SAFE_INTEGER;
            jsonObject["maxSafeInt+1"] = MAX_SAFE_INTEGER + 1;

            jsonObject["minSafeInt"] = MIN_SAFE_INTEGER;
            jsonObject["minSafeInt-1"] = MIN_SAFE_INTEGER - 1;


            var jsonObjectString = jsonObject.ToString();

            Debug.Log($"minmax values: {jsonObjectString}");

            var deserializedObject = JSON.Parse(jsonObjectString);

            Assert.AreEqual(deserializedObject["maxLong"].AsLong, long.MaxValue);
            Assert.AreEqual(deserializedObject["minLong"].AsLong, long.MinValue);
            Assert.AreEqual(deserializedObject["maxULong"].AsULong, ulong.MaxValue);
            Assert.AreEqual(deserializedObject["minULong"].AsULong, ulong.MinValue);
            Assert.AreEqual(deserializedObject["maxDecimal"].AsDecimal, decimal.MaxValue);
            Assert.AreEqual(deserializedObject["minDecimal"].AsDecimal, decimal.MinValue);

            Assert.AreNotEqual(jsonObject["maxSafeInt"].AsLong, jsonObject["maxSafeInt+1"].AsLong);
            Assert.AreEqual(jsonObject["maxSafeInt"].AsDouble, jsonObject["maxSafeInt+1"].AsDouble);

            Assert.AreNotEqual(jsonObject["minSafeInt"].AsLong, jsonObject["minSafeInt-1"].AsLong);
            Assert.AreEqual(jsonObject["minSafeInt"].AsDouble, jsonObject["minSafeInt-1"].AsDouble);

            var bigInt64 = new JSONNumber(0);
            var smallInt64 = new JSONNumber(0);

            bigInt64.AsULong = MAX_SAFE_INTEGER;
            smallInt64.AsLong = MIN_SAFE_INTEGER;

            Assert.Throws<System.ArgumentException>(() =>
            {
                bigInt64.AsLong = (long)(MAX_SAFE_INTEGER + 1);
            });

            Assert.Throws<System.ArgumentException>(() =>
            {
                smallInt64.AsLong = (long)(MIN_SAFE_INTEGER - 1);
            });

            Assert.Throws<System.ArgumentException>(() =>
            {
                bigInt64.AsULong = (long)(MAX_SAFE_INTEGER + 1);
            });
        }
    }
}
