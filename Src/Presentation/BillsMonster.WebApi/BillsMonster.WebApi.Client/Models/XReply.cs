using Google.Protobuf;
using Google.Protobuf.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BillsMonster.WebApi.Models
{
    public class XReply : IMessage<XReply>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public static readonly MessageParser<XReply> Parser = new MessageParser<XReply>(() => new XReply());

        public MessageDescriptor Descriptor
        {
            get
            {
                byte[] descriptorData = GetBytes();
                var descriptor = FileDescriptor.FromGeneratedCode(descriptorData,
                    new FileDescriptor[] { },
                    new GeneratedClrTypeInfo(null, new GeneratedClrTypeInfo[] {new GeneratedClrTypeInfo(typeof(XReply), Parser, new[]{ "Id","Content" }, null, null, null),
                    }));
                return descriptor.MessageTypes[0];
            }
        }

        public int CalculateSize()
        {
            return sizeof(int) + ((8 + 4 + 2 + (2 * Content.Length) + 4 - 1) / 4 * 4);
        }

        public XReply Clone()
        {
            return new XReply() { Id = this.Id, Content = this.Content };
        }

        public bool Equals(XReply other)
        {
            if (other == null || ReferenceEquals(other, null))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Id == other.Id && this.Content.Equals(other.Content);
        }

        public void MergeFrom(XReply message)
        {
            this.Id = message.Id;
            this.Content = message.Content;
        }

        public void MergeFrom(CodedInputStream input)
        {
            using (var ms = new MemoryStream())
            {
                var buffer = input.ReadBytes().ToByteArray();
                ms.Read(buffer, 0, buffer.Length);
                var formatter = new BinaryFormatter();
                var copy = (XReply)formatter.Deserialize(ms);
                this.Id = copy.Id;
                this.Content = copy.Content;
            }
        }

        public void WriteTo(CodedOutputStream output)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                output.WriteBytes(ByteString.FromStream(ms));
            }
        }

        private byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                return ms.ToArray();
            }
        }
    }
}
