using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Authorization {
    public class MarkerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, IdentityUser> {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, IdentityUser resource) {
            if (context.User == null) {
                return Task.CompletedTask;
            }

            // Administrators can do anything.
            if (context.User.IsInRole(Constants.ContactMarkerRole)) {
                if (requirement.Name == "View" || requirement.Name == "BuyExam") {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
