using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Greet;
using Grpc.Core;

namespace BillsMonster.WebApi
{
    public class GreeterService : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }

    public class XRequest : IMessage<XRequest>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static readonly MessageParser<XRequest> Parser = new MessageParser<XRequest>(() => new XRequest());

        public MessageDescriptor Descriptor
        {
            get
            {                                
                byte[] descriptorData = GetBytes();
                var descriptor = FileDescriptor.FromGeneratedCode(descriptorData, 
                    new FileDescriptor[] { }, 
                    new GeneratedClrTypeInfo(null, new GeneratedClrTypeInfo[] {new GeneratedClrTypeInfo(typeof(XRequest), Parser, new[]{ "Id","Name" }, null, null, null),
                    }));
                return descriptor.MessageTypes[0];
            }
        }

        public int CalculateSize()
        {
            return sizeof(int) + ((8 + 4 + 2 + (2 * Name.Length) + 4 - 1) / 4 * 4);
        }

        public XRequest Clone()
        {
            return new XRequest() { Id = this.Id, Name = this.Name };
        }

        public bool Equals(XRequest other)
        {
            if (other == null || ReferenceEquals(other, null))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Id == other.Id && this.Name.Equals(other.Name);
        }

        public void MergeFrom(XRequest message)
        {
            this.Id = message.Id;
            this.Name = message.Name;
        }

        public void MergeFrom(CodedInputStream input)
        {
            using var ms = new MemoryStream();
            var buffer = input.ReadBytes().ToByteArray();
            ms.Read(buffer, 0, buffer.Length);
            var formatter = new BinaryFormatter();
            var copy = (XRequest)formatter.Deserialize(ms);
            this.Id = copy.Id;
            this.Name = copy.Name;
        }

        public void WriteTo(CodedOutputStream output)
        {
            using var ms = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, this);
            output.WriteBytes(ByteString.FromStream(ms));
        }
                
        private byte[] GetBytes()
        {
            using var ms = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, this);
            return ms.ToArray();
        }
    }
}