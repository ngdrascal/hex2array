namespace Hex2Array
{
    internal record HexRecord
    {
        public char StartCode { get; set; }
        public int ByteCount { get; set; }
        public int Address { get; set; }
        public byte RecordType { get; set; }
        public byte[] Data { get; set; }
        public byte CheckSum { get; set; }
    }
}
