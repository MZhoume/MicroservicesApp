namespace UserService.SignUp
{
    using Shared.Authentication;
    using Shared.Interface;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using UserService.Model;

    /// <summary>
    /// Command for SignUp operation
    /// </summary>
    public class SignUpCommand : ICommand
    {
        /// <summary>
        /// Invoke this command
        /// </summary>
        /// <param name="request"> Request used for invoke </param>
        /// <returns> The response </returns>
        public Response Invoke(Request request)
        {
            var response = new Response();

            var payload = request.Payload.ToObject<SignUpPayload>();
            payload.Validate();
            var emailToken = AuthHelper.GenerateCustomAuthToken(payload);

            response.Payload = new
            {
                Email = payload.Email,
                Token = emailToken
            };
            return response;
        }
    }
}
