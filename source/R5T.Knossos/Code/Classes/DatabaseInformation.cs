using System;

using R5T.Thermopylae;


namespace R5T.Knossos
{
    public class DatabaseInformation
    {
        #region Static

        public static DatabaseInformation Get(
            DatabaseSpecification databaseSpecification,
            DatabaseServerSpecification serverSpecification,
            DatabaseServerLocation serverLocation,
            DatabaseServerAuthentication serverAuthentication)
        {
            var databaseInformation = Utilities.GetDatabaseInformation(databaseSpecification, serverSpecification, serverLocation, serverAuthentication);
            return databaseInformation;
        }

        #endregion


        public DatabaseName DatabaseName { get; set; }
        public DatabaseServerName ServerName { get; set; }
        public DataSource DataSource { get; set; }
        public AuthenticationName AuthenticationName { get; set; }
        public Username Username { get; set; }
        public Password Password { get; set; }
    }
}
