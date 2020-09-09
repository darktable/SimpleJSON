using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleJSON
{
    public interface ISimpleJSONSerializable
    {
        JSONNode ToJSONNode();
    }

    public static partial class JSON
    {
        public static string Serialize(object value, int aIndent = 0)
        {
            return ToJSONNode(value).ToString(aIndent);
        }

        public static JSONNode ToJSONNode(object value)
        {
            switch (value)
            {
                case null:
                    return JSONNull.CreateOrGet();
                case JSONNode jsonValue:
                    return jsonValue;
                case string strValue:
                    return new JSONString(strValue);
                case char charValue:
                    return new JSONString(new string(charValue, 1));
                case bool boolValue:
                    return new JSONBool(boolValue);
                case IList listValue:
                    return ToJSONNode(listValue);
                case IDictionary dictValue:
                    return ToJSONNode(dictValue);
                case ISimpleJSONSerializable serializableValue:
                    return serializableValue.ToJSONNode();
#if UNITY_5_3_OR_NEWER
                case Vector2 v2Value:
                    return v2Value.ToJSONNode();
                case Vector3 v3Value:
                    return v3Value.ToJSONNode();
                case Vector4 v4Value:
                    return v4Value.ToJSONNode();
                case Quaternion quatValue:
                    return quatValue.ToJSONNode();
                case Rect rectValue:
                    return rectValue.ToJSONNode();
                case RectOffset rectOffsetValue:
                    return rectOffsetValue.ToJSONNode();
                case Matrix4x4 matrixValue:
                    return matrixValue.ToJSONNode();
#endif

                default:
                    if (JSONNumber.IsNumeric(value))
                    {
                        return new JSONNumber(System.Convert.ToDouble(value));
                    }

                    return new JSONString(value.ToString());
            }
        }

        private static JSONArray ToJSONNode(IList list)
        {
            var jsonArray = new JSONArray();

            for (int i = 0; i < list.Count; i++)
            {
                jsonArray.Add(ToJSONNode(list[i]));
            }

            return jsonArray;
        }

        private static JSONObject ToJSONNode(IDictionary dict)
        {
            var jsonObject = new JSONObject();

            foreach (object key in dict.Keys)
            {
                jsonObject.Add(key.ToString(), ToJSONNode(dict[key]));
            }

            return jsonObject;
        }

        #region Extension methods
#if UNITY_5_3_OR_NEWER
        public static JSONNode ToJSONNode(this Vector2 vector2, bool asArray = false)
        {
            if (asArray)
            {
                return new JSONArray().WriteVector2(vector2);
            }
            else
            {
                return new JSONObject().WriteVector2(vector2);
            }
        }

        public static JSONNode ToJSONNode(this Vector3 vector3, bool asArray = false)
        {
            if (asArray)
            {
                return new JSONArray().WriteVector3(vector3);
            }
            else
            {
                return new JSONObject().WriteVector3(vector3);
            }
        }

        public static JSONNode ToJSONNode(this Vector4 vector4, bool asArray = false)
        {
            if (asArray)
            {
                return new JSONArray().WriteVector4(vector4);
            }
            else
            {
                return new JSONObject().WriteVector4(vector4);
            }
        }

        public static JSONNode ToJSONNode(this Quaternion quaternion, bool asArray = false)
        {
            if (asArray)
            {
                return new JSONArray().WriteQuaternion(quaternion);
            }
            else
            {
                return new JSONObject().WriteQuaternion(quaternion);
            }
        }

        public static JSONNode ToJSONNode(this Rect rect, bool asArray = false)
        {
            if (asArray)
            {
                return new JSONArray().WriteRect(rect);
            }
            else
            {
                return new JSONObject().WriteRect(rect);
            }
        }

        public static JSONNode ToJSONNode(this RectOffset rectOffset, bool asArray = false)
        {
            if (asArray)
            {
                return new JSONArray().WriteRectOffset(rectOffset);
            }
            else
            {
                return new JSONObject().WriteRectOffset(rectOffset);
            }
        }

        public static JSONNode ToJSONNode(this Matrix4x4 matrix)
        {
            return new JSONArray().WriteMatrix(matrix);
        }
#endif

        public static JSONNode ToJSONNode<T>(this List<T> list)
        {
            return ToJSONNode((IList)list);
        }

        public static JSONNode ToJSONNode<T>(this Dictionary<string, T> dict)
        {
            return ToJSONNode((IDictionary)dict);
        }

        #endregion
    }
}
