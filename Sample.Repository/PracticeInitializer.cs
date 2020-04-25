using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Sample.Models;
using System.Data.Entity.Migrations;

namespace Sample.Repository
{
    //CreateDatabaseIfNotExists
    //DropCreateDatabaseAlways 
    //DropCreateDatabaseIfModelChanges
    //Custom
    //MigrateDatabaseToLatestVersion
    public class PracticeInitializer : CreateDatabaseIfNotExists<PracticeContext>  //CreateDatabaseIfNotExists<PracticeContext>
    {

        
        public PracticeInitializer()
        {
            
        }
        // good article about 
        // http://blogs.msdn.com/b/adonet/archive/2011/01/29/using-dbcontext-in-ef-feature-ctp5-part-4-add-attach-and-entity-states.aspx
        protected override void Seed(PracticeContext context)
        {
            
        }        
    }
    public class Configuration : DbMigrationsConfiguration<PracticeContext>
    {

    }
}