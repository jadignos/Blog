public static class Constant
{
    public class Name
    {
        public const string ADMIN = "Admin";
    }

    public class Variable
    {
        public const int PAGE_SIZE = 5;
    }

    public class MailMessage
    {
        public const string CONFIRM_EMAIL_SUBJECT = "Confirm your email address";
        public const string CONFIRM_EMAIL_BODY_PLAINTEXT = "Hi {0}, Good day! Please click the link to confirm your email address. {1}";
        public const string CONFIRM_EMAIL_BODY_HTML =
            @"<!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8' />
                <title>Confirm Email Address</title>
            </head>
            <body>
                <p>Hi {0},</p>
                <p>Good day!</p>
                <p>Please click <a href='{1}' target='_blank'>HERE</a> to confirm your email.</p>
                <p>Or you can copy-paste the following URL to your browser.</p>
                <p>{1}</p>
                <p>Thank you</p>
            </body>
            </html>";
    }

    public class MessageError
    {
        public const string ERROR = "Error";
        public const string EMAIL_NOT_CONFIRMED = "Email not confirmed";
        public const string INVALID_LOGIN_ATTEMPT = "Invalid login attempt";
        public const string UNKNOWN_ERROR = "Unknown error occured";
    }

    public class MessageInfo
    {
        public const string SUCCESS = "Success";
        public const string EMAIL_CONFIRMED = "Email confirmed";
        public const string ACCOUNT_HAS_BEEN_CREATED = "You account has been created";
        public const string LOGIN_TO_CONTINUE ="Login to continue";
        public const string CHECK_YOUR_MAIL = "Check your email to confirm";
        public const string MESSAGE_SENT = "Message Sent.";
        public const string WELL_GET_BACK_SHORTLY = "We'll get back to you shortly.";
    }
}
