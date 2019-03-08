using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6
{
    #region snippet_IDbContextFactory
    public class SchoolContextFactory : IDbContextFactory<SchoolContext>
    {
        public SchoolContext Create()
        {
            return new EF6.SchoolContext("Server=.\\;Database=School;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
    #endregion
}
