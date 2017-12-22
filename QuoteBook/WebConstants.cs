using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteBook
{
    public class WebConstants
    {
        public const string AdministratorRole = "Administrator";
        public const string AuthorRole = "Author";
        public const string ViewerRole = "Viewer";

        public const string TempDataSuccessMessageKey = "SuccessMessage";
        public const string TempDataErrorMessageKey = "ErrorMessage";
        public const string TempDataUserRoleSuccessfullChange = "User {0}'s role has been set to {1} successfully.";

        public const string InvalidIdentityDetailsOrUser = "Invalid identity details /role or user/.";
        public const string UserAccountLockedOut = "User account locked out.";
        public const string UserLoggedIn = "User logged in.";
        public const string InvalidLogInAttempt = "Invalid login attempt.";
        public const string TwoFactorAuthenticationFailure = "Unable to load two-factor authentication user.";
        public const string UserLoggedInWithTwoFactor = "User with ID {UserId} logged in with 2fa.";
        public const string UserAccountWithIdLockedOut = "User with ID {UserId} account locked out.";
        public const string InvalidAuthenticationCodeEnteredForUser = "Invalid authenticator code entered for user with ID {UserId}.";
        public const string InvalidAuthenticatorCode = "Invalid authenticator code.";
        public const string TwoFactorAuthenticationUserLoadFail = "Unable to load two-factor authentication user.";
        public const string UserLoggedInWithRecoveryCode = "User with ID {UserId} logged in with a recovery code.";
        public const string InvalidRecoveryCodeEnteredForUser = "Invalid recovery code entered for user with ID {UserId}";
        public const string InvalidRecoveryCodeEntered = "Invalid recovery code entered.";
        public const string UserCreatedNewAccount = "User created a new account with password.";
        public const string UserLoggedOut = "User logged out.";
        public const string UserLoggedInWithProvider = "User logged in with {Name} provider.";
        public const string ErrorLoadingExternalLoginInformation = "Error loading external login information during confirmation.";
        public const string UserCreatedAccountWithProvider = "User created an account using {Name} provider.";
        public const string AdminArea = "Admin";
    }
}
