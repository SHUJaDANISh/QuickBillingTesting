using Microsoft.Data.SqlClient;
using System.Data;

namespace QuickBiilingTesting.Utilities
{
    public static class SqlHelper
    {
        public static SqlParameter CreateParam(string name, object value, SqlDbType dbType, int? size = null)
        {
            var param = new SqlParameter(name, dbType);
            param.Value = value ?? DBNull.Value;

            if (size.HasValue)
            {
                param.Size = size.Value;
            }

            return param;
        }
    }
}
