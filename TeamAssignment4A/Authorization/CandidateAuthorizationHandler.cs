using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Authorization {
    public class CandidateAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, IdentityUser> {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, IdentityUser resource) {
            if (context.User == null) {
                return Task.CompletedTask;
            }

            // Administrators can do anything.
            if (context.User.IsInRole(Constants.ContactCandidateRole)) {
                if (requirement.Name == "View" || requirement.Name == "Edit" || requirement.Name == "Buy") {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
