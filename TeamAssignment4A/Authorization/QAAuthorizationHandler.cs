using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using TeamAssignment4A.Models;
using Microsoft.AspNetCore.Identity;

namespace TeamAssignment4A.Authorization {

    public class QAAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, IdentityUser> {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, IdentityUser resource) {
            if (context.User == null) {
                return Task.CompletedTask;
            }


            if (context.User.IsInRole(Constants.ContactQARole)) {
                if (requirement.Name == "View") {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}

