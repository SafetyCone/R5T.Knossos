using System;
using System.Text;


namespace R5T.Knossos
{
    public static class Utilities
    {
        /// <summary>
        /// Default uses <see cref="Utilities.GetDatabaseInformationWithVerification(DatabaseSpecification, DatabaseServerSpecification, DatabaseServerLocation, DatabaseServerAuthentication)"/>.
        /// </summary>
        public static DatabaseInformation GetDatabaseInformation(
            DatabaseSpecification databaseSpecification,
            DatabaseServerSpecification serverSpecification,
            DatabaseServerLocation serverLocation,
            DatabaseServerAuthentication serverAuthentication)
        {
            var databaseInformation = Utilities.GetDatabaseInformationWithVerification(databaseSpecification, serverSpecification, serverLocation, serverAuthentication);
            return databaseInformation;
        }

        public static DatabaseInformation GetDatabaseInformationWithVerification(
            DatabaseSpecification databaseSpecification,
            DatabaseServerSpecification serverSpecification,
            DatabaseServerLocation serverLocation,
            DatabaseServerAuthentication serverAuthentication)
        {
            Utilities.VerifyDatabaseInformation(databaseSpecification, serverSpecification, serverLocation, serverAuthentication);

            var databaseInformation = Utilities.GetDatabaseInformationWithoutVerification(databaseSpecification, serverSpecification, serverLocation, serverAuthentication);
            return databaseInformation;
        }

        public static DatabaseInformation GetDatabaseInformationWithoutVerification(
            DatabaseSpecification databaseSpecification,
            DatabaseServerSpecification serverSpecification,
            DatabaseServerLocation serverLocation,
            DatabaseServerAuthentication serverAuthentication)
        {
            var databaseInformation = new DatabaseInformation()
            {
                DatabaseName = databaseSpecification.DatabaseName,
                ServerName = serverSpecification.ServerName,
                DataSource = serverLocation.DataSource,
                AuthenticationName = serverAuthentication.Name,
                Username = serverAuthentication.Username,
                Password = serverAuthentication.Password,
            };

            return databaseInformation;
        }

        public static bool TryVerifyDatabaseInformation(
            DatabaseSpecification databaseSpecification,
            DatabaseServerSpecification serverSpecification,
            DatabaseServerLocation serverLocation,
            DatabaseServerAuthentication serverAuthentication,
            out string failureMessage)
        {
            var success = true;

            var failureMessageBuilder = new StringBuilder("Database information verification failed for the following reason(s):\n");

            if(databaseSpecification.ServerName != serverSpecification.ServerName)
            {
                success = false;

                failureMessageBuilder.Append($"Server name mismatch between database specification ('{databaseSpecification.ServerName}') and server specification ('{serverSpecification.ServerName}').");
            }

            if(databaseSpecification.ServerName != serverLocation.ServerName)
            {
                success = false;

                failureMessageBuilder.Append($"Server name mismatch between database specification ('{databaseSpecification.ServerName}') and server location ('{serverLocation.ServerName}').");
            }

            if(serverSpecification.AuthenticationName != serverAuthentication.Name)
            {
                success = false;

                failureMessageBuilder.Append($"Authentication name mismatch between server specification ('{serverSpecification.AuthenticationName}') and server authentication ('{serverAuthentication.Name}').");
            }

            failureMessage = success ? String.Empty : failureMessageBuilder.ToString();

            return success;
        }

        public static void VerifyDatabaseInformation(
            DatabaseSpecification databaseSpecification,
            DatabaseServerSpecification serverSpecification,
            DatabaseServerLocation serverLocation,
            DatabaseServerAuthentication serverAuthentication)
        {
            if(!Utilities.TryVerifyDatabaseInformation(databaseSpecification, serverSpecification, serverLocation, serverAuthentication, out var failureMessage))
            {
                throw new Exception(failureMessage);
            }
        }
    }
}
