using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Work.Domain.Shared
{
    public static class APIWorkDomainCode
    {
        // Authentication error codes
        public const string AuthDefault = "APIWork_Auth_";

        public const string UserNotFound = AuthDefault + "User_Not_Found_404";
        public const string InvalidCredential = AuthDefault + "Invalid_Credential_403";
        public const string UserInactive = AuthDefault + "User_Inactive_403";
        public const string AccountLocked = AuthDefault + "Account_Locked_403";
        public const string InternalError = AuthDefault + "Internal_Error_500";
        public const string RefreshTokenNotFound = AuthDefault + "Refresh_Token_Not_Found_404";
        public const string RefreshTokenRevoked = AuthDefault + "Refresh_Token_Revoked_403";
        public const string RefreshTokenExpired = AuthDefault + "Refresh_Token_Expired_401";

        // User-related error codes
        public const string UserDefault = "APIWork_User_";

        public const string UserCreationFailed = UserDefault + "Creation_Failed_400";
        public const string UserSuccessfullyCreated = UserDefault + "Successfully_Created_201";
        public const string UserUpdateFailed = UserDefault + "Update_Failed_400";
        public const string UserSuccessfullyUpdated = UserDefault + "Successfully_Updated_200";
        public const string UserRetrievedSuccessfully = UserDefault + "Retrieved_Successfully_200";
        public const string UserDeletionFailed = UserDefault + "Deletion_Failed_400";
        public const string UserDeletedSuccessfully = UserDefault + "Deleted_Successfully_200";

        // Role-related error codes
        public const string RoleDefault = "APIWork_Role_";

        public const string RoleNotFound = RoleDefault + "Not_Found_404";
        public const string RoleCreationFailed = RoleDefault + "Creation_Failed_400";
        public const string RoleSuccessfullyCreated = RoleDefault + "Successfully_Created_201";
        public const string RoleUpdateFailed = RoleDefault + "Update_Failed_400";
        public const string RoleSuccessfullyUpdated = RoleDefault + "Successfully_Updated_200";
        public const string RoleRetrievedSuccessfully = RoleDefault + "Retrieved_Successfully_200";
        public const string RoleDeletionFailed = RoleDefault + "Deletion_Failed_400";
        public const string RoleDeletedSuccessfully = RoleDefault + "Deleted_Successfully_200";
   



}
}
