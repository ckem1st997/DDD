using DDD.API.Application.Commands.Create;
using DDD.API.Application.Commands.Delete;
using DDD.API.Application.Commands.Update;
using DDD.API.Application.Models;
using DDD.API.Application.Queries.Paginated;
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



        public override async Task<GrpcResult> UpdateProductDraftFromUser(GrpcUpdateProductsCommand updateProductsCommand, ServerCallContext context)
        {
            var model = new ProductsCommand();
            model.Image = updateProductsCommand.Image;
            model.Name = updateProductsCommand.Name;
            model.Id = updateProductsCommand.Id;
            model.Price = (decimal)updateProductsCommand.Price;
            var data = await _mediat.Send(new UpdateProductsCommand() { ProductsCommand = model });
            if (data)
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


        public override async Task<GrpcResult> DeleteProductDraftFromUser(GrpcDeleteProductsCommand deleteProductsCommand, ServerCallContext context)
        {
            var data = await _mediat.Send(new DeleteProductsCommand() { Id = deleteProductsCommand.Id });
            if (data)
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


        public override async Task<GrpcPaginatedList> PaginatedListDraftFromUser(GrpcPaginatedListCommand paginatedList, ServerCallContext context)
        {
            var data = await _mediat.Send(new PaginatedListCommand() { KeySearch = paginatedList.KeySearch, PageNumber = paginatedList.PageNumber, fromPrice = (decimal?)paginatedList.FromPrice, toPrice = (decimal?)paginatedList.ToPrice, PageIndex = paginatedList.PageIndex });
            var list = new GrpcPaginatedList();
            foreach (var item in data.ProductsDTOs)
            {
                var tem = new GrpcProductsDTO();
                tem.Id = item.Id;
                tem.Name = item.Name;
                tem.Price = (double)item.Price;
                tem.Image = item.Image==null?"": item.Image;
                tem.CreateDate =Timestamp.FromDateTime(DateTime.SpecifyKind(item.CreateDate, DateTimeKind.Utc));
                tem.ModiDate = Timestamp.FromDateTime(DateTime.SpecifyKind(item.ModiDate, DateTimeKind.Utc));
                list.ProductsDTOs.Add(tem);
            }
            list.TotalCount = data.totalCount;
            return list;
        }

    }
}
