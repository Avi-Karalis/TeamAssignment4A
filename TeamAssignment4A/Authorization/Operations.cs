using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.VisualBasic;

namespace TeamAssignment4A.Authorization {
    public static class Operations {
        public static OperationAuthorizationRequirement Create = new OperationAuthorizationRequirement { Name = Constants.CreateOperationName }; 
        
        public static OperationAuthorizationRequirement Read = new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };

        public static OperationAuthorizationRequirement Update = new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

        public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };

        public static OperationAuthorizationRequirement Roles = new OperationAuthorizationRequirement { Name = Constants.RolesOperationName };
        public static OperationAuthorizationRequirement Mark = new OperationAuthorizationRequirement { Name = Constants.RolesOperationName };
    }

    public class Constants {
        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";
        public static readonly string RolesOperationName = "Roles";
        public static readonly string MarkOperationName = "Mark";
        public static readonly string BuyExamOperationName = "BuyExam";

        public static readonly string ContactAdministratorsRole = "Admin";
        public static readonly string ContactQARole = "QA";
        public static readonly string ContactMarkerRole = "Marker";
        public static readonly string ContactCandidateRole = "Candidate";
    }
}
