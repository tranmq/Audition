using System.Web;
using Audition.Data.Service;

namespace Audition
{
    public class DatabaseFileInAppDataReader : IDatabaseReader
    {
        private readonly HttpContext _httpContext;

        public DatabaseFileInAppDataReader(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public string ReadAll()
        {
            var databasePath = _httpContext.Server.MapPath("~/App_Data/Database.json");
            return System.IO.File.ReadAllText(databasePath);
        }
    }
}