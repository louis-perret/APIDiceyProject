using System;
using Grpc.Core;
using ApiGRPCDiceyProject;

namespace ApiGRPCDiceyProject.Services
{
	public class ServiceThrow : ThrowService.ThrowServiceBase
	{

        public override Task<Throw> GetThrowById(Request request, ServerCallContext context)
        {
            return Task.FromResult(new Throw() { ThrowId = "1", Result = 3 });
        }
    }
}

