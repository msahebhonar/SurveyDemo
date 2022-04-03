namespace Survey.Common
{
    public class ErrorMessages
    {
        public static string InvalidInput => "Invalid input parameters!";

        public static string InternalServerError => "Internal server error!";

        public static string InvalidUserInfo => "Invalid email or password!";

        public static string UnexpectedError => "Something went wrong when submit the survey!";

        public static string InvalidSurveyInfo => "Invalid user account or survey detail!";

        public static string InvalidOperation => "Survey has already submitted!";

        public static string IncompleteForm => "All questions are required!";
    }
}