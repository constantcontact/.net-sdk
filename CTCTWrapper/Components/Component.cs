using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Runtime.Serialization;

namespace CTCT.Components
{
    /// <summary>
    /// Base class for components.
    /// </summary>
    [Serializable]
    [DataContract]
    public abstract class Component
    {
        /// <summary>
        /// Get the object from JSON.
        /// </summary>
        /// <typeparam name="T">The class type to be deserialized.</typeparam>
        /// <param name="json">The serialization string.</param>
        /// <returns>Returns the object deserialized from the JSON string.</returns>
        public static T FromJSON<T>(string json) where T : class
        {
            T obj = null;
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                ms.Write(buffer, 0, buffer.Length);
                ms.Position = 0;
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                obj = ser.ReadObject(ms) as T;
            }

            return obj;
        }

        /// <summary>
        /// Serialize an object to JSON.
        /// </summary>
        /// <returns>Returns a string representing the serialized object.</returns>
        public virtual string ToJSON()
        {
            string json = null;
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(this.GetType());
                ser.WriteObject(ms, this);
                json = Encoding.UTF8.GetString(ms.ToArray());
            }

            return json;
        }
    }
}
