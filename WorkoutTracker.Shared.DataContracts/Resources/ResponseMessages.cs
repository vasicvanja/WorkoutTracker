namespace WorkoutTracker.Shared.DataContracts.Resources
{
    public class ResponseMessages
    {
        public const string InvalidModelState = "Invalid parameters.";
        public const string NonExistingRole = "The role does not exist!";
        public const string SuccessfulUserCreation = "User successfully registered!";
        public const string UnsuccessfulUserCreation = "User can't be registered!";
        public const string UserDoesNotExist = "User does not exist, please register in order to login!";
        public const string UserExists = "User already exists!";
        public const string UsernameAlreadyTaken = "Username {0} is already taken!";
        public const string UsernameAlreadyTakenAsEmailFromOtherUser = "Username {0} is already taken from other user as email!";
        public const string EmailAlreadyExists = "Email {0} already exists!";
        public const string EmailTakenAsUsernameFromOtherUser = "Email {0} is already taken as a username from other user!";
        public const string UnsuccessfulCreationOfPasswordResetToken = "Unsuccessful creation of Password Reset Token.";
        public const string UnsuccessfulPasswordReset = "Unsuccessful password reset.";
        public const string InvalidLoginPassword = "Wrong password, please try again!";
        public const string SuccessfulLogin = "Welcome to WorkoutTracker!";
        public const string InvalidToken = "Invalid Token!";
        public const string UnsuccessfulEmailSend = "Sending Mail failed!";
        public const string SmtpSettingsNotDefined = "SMTP Settings are not defined. Mails cannot be sent!";
        public const string SmtpSettingsDisabled = "SMTP Settings are currently disabled. Mails cannot be sent!";
        public const string AccountDisabledDueToMultipleFailedLoginAttempts = "Your account has been disabled untill {0} due to multiple failed login attempts.";
        public const string AccountDisabledByAdministrator = "Your account has been disabled. Please contact the administrator.";
        public const string SuccessfulUserLogout = "Successfully logged out!";
        public const string NoDataFoundForKey = "No data found for entity of type {0} with key {1}!";
        public const string NoDataFound = "No data found!";
        public const string GetEntityFailed = "Getting entity of type {0} by its key {1} failed!";
        public const string GettingEntitiesFailed = "Getting entites of type {0} failed!";
        public const string DeletionFailed = "Deletion of entity of type {0} by its key {1} failed!";
        public const string UnsuccessfulCreationOfEntity = "Unuccessful creation of entity of type {0}.";
        public const string EntityAlreadyExists = "Entity of type {0} already exists with key {1}.";
        public const string InvalidInputParameter = "Enter valid parameters for entity of type {0}.";
        public const string UnsuccessfulUpdateOfEntity = "Unsuccessful update of entity of type {0} with Id {1}.";
        public const string FailedToAssignRoleToUser = "Failed to assign role {0} to user {1} with id {2}.";
        public const string FailedToAssignRoleToUserId = "Failed to assign role {0} to user with id {1}.";
        public const string The_record_you_are_working_on_has_been_modified_by_another_user_Changes_you_have_made_have_not_been_saved_please_resubmit = "The record you are working on has been modified by another user. Changes you have made have not been saved please resubmit.";
        public const string UserNotUpdatedEmailAlreadyExist = "User {0} not updated, email {1} already exists.";
        public const string UserNotUpdatedEmailAlreadyUsedAsUsernameFromOtherUser = "User {0} not updated email {1} already used as username from other user.";
        public const string RoleMustBeAdminOrUser = "The role must be of type Admin or User!";
        public const string UserAlreadyHasRole = "User {0} already has the role {1}.";
        public const string UserDoesNotHaveRole = "User {0} does not have the role {1}.";
        public const string FailedToRemoveRoleFromUser = "Failed to remove role {0} from user {1} with id {2}.";
        public const string FailedToRemoveRoleFromUserId = "Failed to remove role {0} from user with id {1}.";
        public const string FailedToAddExercisesToWorkout = "Failed to add Exercises to Workout with Id {0}";
        public const string ExercisesMustBeProvided = "At least one exercise must be provided.";
        public const string NoValidExercisesProvided = "No valid exercises found for provided IDs.";
        public const string JWTSecurityKeyMissing = "JWT Security Key is missing from configuration.";
    }
}
