using DDD.API.Application.Commands.Create;
using DDD.API.Application.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// bạn có thể đặt namespace trùng với csharp_namespace trong file .proto
namespace GrpcProduct
{
    public class ProductService : ProductGrpc.ProductGrpcBase
    {
        private readonly IMediator _mediat;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IMediator mediat, ILogger<ProductService> logger)
        {
            _mediat = mediat;
            _logger = logger;
        }

        public override async Task<GrpcResult> CreateProductDraftFromUser(GrpcCreateProductsCommand createProductsCommand, ServerCallContext context)
        {
            //_logger.LogInformation("Begin grpc call from method {Method} for ordering get order draft {CreateProductDraftFromUser}", context.Method, createProductsCommand);
            //_logger.LogTrace(
            //    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            //    createProductsCommand.Name,
            //    nameof(createProductsCommand.Id),
            //    createProductsCommand.Id,
            //    createProductsCommand);
            var model = new ProductsCommand();
            model.Image = createProductsCommand.Image;
            model.Name = createProductsCommand.Name;
            model.Id = 0;
            model.Price = (decimal)createProductsCommand.Price;
            var data = await _mediat.Send(new CreateProductsCommand() { ProductsCommand = model });

            if (data != null)
            {
                context.Status = new Status(StatusCode.OK, $" Products get order draft do exist");
                return new GrpcResult() { Result = true };
            }
            else
            {
                context.Status = new Status(StatusCode.NotFound, $" Products get order draft do not exist");
            }

            return new GrpcResult() { Result = false };
        }





    }
}
