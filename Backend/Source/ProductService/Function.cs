using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "ProductService.Test"
)]

namespace ProductService
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Amazon.Lambda.Core;
    using Shared;
    using Shared.Container;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Http;
    using Dapper;
    using Dommel;
    using MySql.Data.MySqlClient;
    using ProductService.Read;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// Lambda function handler for product service
        /// </summary>
        /// <param name="request"> Input for lambda handler </param>
        /// <param name="context"> Context info for lambda handler </param>
        /// <returns> Value send to clients </returns>
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public Response FunctionHandler(Request request, ILambdaContext context)
        {
            // if (request.Operation != Operation.Read)
            // {
            //     throw new LambdaException(HttpCode.BadRequest, "Operation not supported");
            // }

            // MySql.Data.MySqlClient.MySqlConnection conn;

            // try
            // {
            //     conn = new MySql.Data.MySqlClient.MySqlConnection();
            //     conn.ConnectionString = Helper.DbConnString;
            //     conn.Open();
            // }
            // catch (MySql.Data.MySqlClient.MySqlException ex)
            // {
            //     //MessageBox.Show(ex.Message);
            //     throw new LambdaException(HttpCode.InternalServerError, ex.Message);
            // }

            // var Products = conn.GetAll<Shared.Model.Product>();
            // //IEnumerable<Shared.Model.Product> ProductsFilted;
            // foreach (SearchTerm p in request.SearchTerm)
            // {
            //     if (p.Field == "Name")
            //     {
            //         if (p.Operator == Shared.Request.SearchOperator.EQ)
            //         {
            //             Products = Products.Where(c => c.Name == p.Value);
            //         }
            //         else if (p.Operator == Shared.Request.SearchOperator.LIKE)
            //         {
            //             //To Do: regex match
            //         }
            //     }
            //     else if (p.Field == "Price")
            //     {
            //         switch (p.Operator)
            //         {
            //             case Shared.Request.SearchOperator.EQ:
            //                 Products = Products.Where(c => c.Price == decimal.Parse(p.Value));
            //                 break;

            //             case Shared.Request.SearchOperator.GE:
            //                 Products = Products.Where(c => c.Price >= decimal.Parse(p.Value));
            //                 break;

            //             case Shared.Request.SearchOperator.GT:
            //                 Products = Products.Where(c => c.Price > decimal.Parse(p.Value));
            //                 break;

            //             case Shared.Request.SearchOperator.LE:
            //                 Products = Products.Where(c => c.Price <= decimal.Parse(p.Value));
            //                 break;

            //             case Shared.Request.SearchOperator.LT:
            //                 Products = Products.Where(c => c.Price < decimal.Parse(p.Value));
            //                 break;

            //             default:
            //                 break;
            //         }
            //     }
            //     else
            //     {
            //         throw new LambdaException(HttpCode.BadRequest, "Operation not supported");
            //     }
            // }
            // int skip = request.PagingInfo.Start < 0 ? 0 : request.PagingInfo.Start;
            // int count = request.PagingInfo.Count < 0 ? 0 : request.PagingInfo.Count;
            // Products = Products.Skip(skip).Take(count);
            // // To Do: throw exception if out of bound
            // return (Response)Products;

            var container = new CommandContainer();

            container.Register<ReadCommand>(Operation.Read);

            try
            {
                return container[request.Operation].Invoke(request);
            }
            catch (Exception ex)
            {
                throw new LambdaException(HttpCode.BadRequest, ex.Message);
            }
        }
    }
}
