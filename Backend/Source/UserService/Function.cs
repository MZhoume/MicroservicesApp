namespace UserService
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Amazon.Lambda.Core;
    using Amazon.SimpleNotificationService;
    using BCrypt.Net;
    using Dapper;
    using Dapper.Contrib.Extensions;
    using Shared;
    using Shared.Authentication;
    using Shared.DbAccess;
    using Shared.Email;
    using Shared.Http;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using UserService.Model;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// UserService lambda function handler
        /// </summary>
        /// <param name="request"> Request for lambda handler </param>
        /// <param name="context"> Context info for lambda handler </param>
        /// <returns> Lambda response </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public async Task<Response> FunctionHandler(Request request, ILambdaContext context)
        {
            var response = new Response();

            try
            {
                switch (request.Operation)
                {
                    case Operation.SignUp:
                        var signUpPayload = request.Payload.ToObject<User>();
                        signUpPayload.Validate();
                        var emailToken = AuthHelper.GenerateCustomAuthToken(signUpPayload);

                        await new AmazonSimpleNotificationServiceClient()
                                    .PublishAsync(EmailHelper.EmailTopicArn, emailToken);
                        break;
                    case Operation.LogIn:
                        var loginPayload = request.Payload.ToObject<LoginModel>();
                        loginPayload.Validate();

                        var loginRes = DbHelper.DbConnection.Query<User>($"SELECT Email = @Email", new { Email = loginPayload.Email }).First();

                        if (BCrypt.Verify(loginPayload.Password, loginRes.PwdHash))
                        {
                            var loginToken = AuthHelper.GenerateAuthToken(new AuthPayload()
                            {
                                UserId = loginRes.Id,
                                Email = loginRes.Email,
                                FirstName = loginRes.FirstName,
                                LastName = loginRes.LastName,
                                DateTime = DateTime.Now
                            });
                            response.Payload = loginToken;
                        }
                        else
                        {
                            throw new Exception("Email or Password is invalid!");
                        }

                        break;
                    case Operation.Read:
                        var readRes = DbHelper.DbConnection.Query<User>(
                            RequestHelper.ComposeSearchExp(request.SearchTerm, request.PagingInfo != null),
                            RequestHelper.GetSearchObject(request.SearchTerm, request.PagingInfo));
                        response.Payload = readRes.ToArray();
                        break;
                    case Operation.Update:
                        var updatePayload = request.Payload.ToObject<User>();
                        updatePayload.Validate();
                        DbHelper.DbConnection.Update<User>(updatePayload);
                        break;
                    case Operation.VerifyEmail:
                        var emailPayload = AuthHelper.GetCustomAuthPayload<User>(request.AuthToken);
                        emailPayload.Validate();
                        DbHelper.DbConnection.Insert<User>(emailPayload);
                        break;
                    default:
                        throw new Exception("Operation not supported");
                }
            }
            catch (Exception ex)
            {
                throw new LambdaException(HttpCode.BadRequest, ex.Message);
            }

            return response;
        }
    }
}
