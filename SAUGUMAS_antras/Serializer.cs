using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace SAUGUMAS_antras
{
    public static class Serializer
    {
        public static byte[] Serialize(object anySerializableObject)
        {
            MemoryStream ms = new MemoryStream();
            using (BsonWriter writer = new BsonWriter(ms))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, anySerializableObject);
            }

            return ms.ToArray();
        }

        public static SerializedData Deserialize(byte[] message)
        {
            MemoryStream ms = new MemoryStream(message);
            using (BsonReader reader = new BsonReader(ms))
            {
                JsonSerializer serializer = new JsonSerializer();

                return serializer.Deserialize<SerializedData>(reader);
            }
        }
    }
}
