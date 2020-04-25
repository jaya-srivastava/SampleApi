using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Sample.Models;

namespace Sample.Repository
{
    //CreateDatabaseIfNotExists
    //DropCreateDatabaseAlways 
    //DropCreateDatabaseIfModelChanges
    //Custom
    public class PracticeInitializerG : DropCreateDatabaseAlways<PracticeContext> //CreateDatabaseIfNotExists<PracticeContext>
    {
       
		public bool IsSeedTopicGrade { get; set; }

        public PracticeInitializerG()
        {
			IsSeedTopicGrade = true; 
        }
       
        protected override void Seed(PracticeContext context)
        {
			
          
        }        

    }

}
