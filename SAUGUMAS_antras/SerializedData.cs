using System.Security.Cryptography;

namespace SAUGUMAS_antras
{
    [Serializable]
    public class SerializedData
    {
        public byte[] OriginalData { get; set; }
        public byte[] SignedData { get; set; }
        public RSAParameters Key { get; set; }

        public SerializedData(byte[] originalData, byte[] signedData, RSAParameters key)
        {
            OriginalData = originalData;
            SignedData = signedData;
            Key = key;
        }
    }
}
