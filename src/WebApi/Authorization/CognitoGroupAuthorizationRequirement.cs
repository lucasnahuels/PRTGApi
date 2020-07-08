using Microsoft.AspNetCore.Authorization;

namespace WebApi.Authorization
{
    public class CognitoGroupAuthorizationRequirement : IAuthorizationRequirement
    {
        public string CognitoGroup { get; private set; }
        public CognitoGroupAuthorizationRequirement(string cognitoGroup)
        {
            CognitoGroup = cognitoGroup;
        }
    }
}
