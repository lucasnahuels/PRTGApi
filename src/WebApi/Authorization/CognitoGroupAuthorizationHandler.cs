using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace WebApi.Authorization
{
    public class CognitoGroupAuthorizationHandler : AuthorizationHandler<CognitoGroupAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CognitoGroupAuthorizationRequirement requirement)
        {
            if(context.User.HasClaim(c => c.Type == "cognito:groups" && c.Value == requirement.CognitoGroup))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
