using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Sample.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Metadata.Edm;


namespace Sample.Repository
{
    public class PracticeContext : DbContext
    {
       
        public DbSet<Topic> Topics { get; set; }

        public DbSet<SubTopic> SubTopics { get; set; }

        
        public DbSet<Grade> Grades { get; set; }

        public DbSet<GradeSubTopic> GradeSubTopics { get; set; }

       
        public DbSet<Group> Group { get; set; }

        public DbSet<SubTopicGroup> SubTopicGroup { get; set; }

        
        public DbSet<HtmlMetaData> HtmlMetaData { get; set; }

        public DbSet<HtmlMetaRelation> HtmlMetaRelation { get; set; }
        

        ////old flunet mapping article : http://blogs.msdn.com/b/adonet/archive/2010/12/06/ef-feature-ctp5-fluent-api-samples.aspx
        //EF5 CTP http://blogs.microsoft.co.il/blogs/gilf/archive/2010/12/08/ef-feature-ctp5-code-first-fluent-api.aspx
        //http://msdn.microsoft.com/en-US/data/jj591617
        public PracticeContext()
        {
            //   Database.SetInitializer<PracticeContext>(null);
            //this.Configuration.ProxyCreationEnabled = false;            
        }

        public PracticeContext(string connectionString)
            : base(connectionString)
        {
            //  Database.SetInitializer<PracticeContext>(null);
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();                       
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //to make sure that Gen Db - don't use Identity 
            //modelBuilder.Conventions.Remove<StoreGeneratedIdentityKeyConvention>();

            //modelBuilder.Entity<UserAnswer>().Ignore(ua => ua.AnswerDateTime);
            //modelBuilder.Entity<UserAnswer>().Property(t => t.AnswerDateTime).HasColumnName("AnswerDateTime");
            //modelBuilder.Conventions.Remove<>();
            // modelBuilder.Entity<SubTopic>()
            // .HasKey(p => p.SubTopicId)
            // .Property(p => p.SubTopicId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Database.SetInitializer<PracticeContext>(null); // <--- This is what i needed

            base.OnModelCreating(modelBuilder);

            

        }

        public ObjectContext ObjectContext()
        {
            return (this as IObjectContextAdapter).ObjectContext;
        }

        
    }


}