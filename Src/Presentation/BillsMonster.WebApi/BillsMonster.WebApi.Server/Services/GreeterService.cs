using System.Threading.Tasks;
using BillsMonster.WebApi.Models;
using Greet;
using Grpc.Core;

namespace BillsMonster.WebApi
{
    public class GreeterService //: Greeter.GreeterBase
    {
        public Task<XReply> SayHello2(XRequest request, ServerCallContext context)
        {
            return Task.FromResult(new XReply()
            {
                Id = 1,
                Content = "Hello " + request.Name
            });
        }

        public Task<XReply> SayHello(XRequest request, ServerCallContext context)
        {
            return Task.FromResult(new XReply()
            {
                Id = 1,
                Content = "Hello " + request.Name
            });
        }
    }
}