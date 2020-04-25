using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iPractice.Models;
using iPractice.Repository;
using System.Data.Entity;
using AutoMapper;
using iPractice.Models.DTO;
using iPractice.Service;

namespace iPractice.Repository.Test
{
	class ServcieTest
	{
		static string connectionString = "PracticeContext";
		private static MainRepository rp = null;

		public static void MainTest()
		{
			//UpdateWorkSheetDesc();
			//TestSubtopic(1331);
			//TestSubtopic(1332); 
			//Console.ReadLine();
			var context = GetDbContext();
			//AutoMapperConfiguration.Configure();
			//Mapper.AssertConfigurationIsValid();

			//context.UserAnswerHistory.Add(new UserAnswerHistory
			//{
			//	QuestionId = 1111,
			//	IsCorrect = true,
			//	UserId = 12222,
			//	AnswerDateTime = DateTime.Now,
			//});

			//int r = context.SaveChanges();

			TestServices();

		}


		private static void TestServices()
		{
			TestTopicService();
			//TestGradeService();

		}

		private static void TestGradeService()
		{
			Mapper.CreateMap<Grade, GradeDto>();
			Mapper.CreateMap<GradeSubTopic, GradeSubTopicDto>();
			Mapper.CreateMap<SubTopic, SubTopicDto>();

			GradeService serv = new GradeService();
			var dto = serv.GetSubTopicsByGradeId(2);

			Console.WriteLine("testing titleServcie");
		}


		private static void TestTopicService()
		{
			//Mapper.CreateMap<Topic, TopicDto>();
			//Mapper.CreateMap<SubTopic, SubTopicDto>();
			AutoMapperConfigurator.Configure();

			TopicService serv = new TopicService();

			Console.WriteLine("without Cachinh");
			for (int i = 0; i < 5; i++)
			{
				DateTime dtStart = DateTime.Now;
				var dto = serv.GetSubTopicCategoryDto("TAdd");

				Console.WriteLine("{0} : {1}", i, DateTime.Now.Subtract(dtStart).TotalMilliseconds);

			}

			Console.WriteLine("with Caching");

			for (int i = 0; i < 5; i++)
			{
				DateTime dtStart = DateTime.Now;
				var dto = serv.GetSubTopicsbyTopicId("TAdd");

				Console.WriteLine("{0} : {1}", i, DateTime.Now.Subtract(dtStart).TotalMilliseconds);
			}

			for (int i = 0; i < 5; i++)
			{
				DateTime dtStart = DateTime.Now;
				var dto = serv.GetSubTopicCategoryDto("TAdd");

				Console.WriteLine("{0} : {1}", i, DateTime.Now.Subtract(dtStart).TotalMilliseconds);
			}



			Console.WriteLine("testing titleServcie");
		}

		private static void TestSubtopic(int subTopicId)
		{
			try
			{
				DateTime t = DateTime.Now;
				//string subTopicId = "1333";
				rp = rp = new MainRepository();
				Question q = rp.GetQuestionsBySubTopicId(subTopicId);

				Console.WriteLine(DateTime.Now.Subtract(t).ToString());

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		private static PracticeContext GetDbContext()
		{
			PracticeContext context = null;
			try
			{
				//Database.SetInitializer<PracticeContext>(new CreateDatabaseIfNotExists<PracticeContext>());

				context = new PracticeContext(connectionString);

				//AddTopics(context);
				//AddSubTopics();
				//AddCompleteQuestions();
				int i = context.Topics.Count();

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

			return context;
		}

		private static void AddTopics(PracticeContext context)
		{
			var topicList = new List<Topic>                                                            
            {
                new Topic { TopicId="TAdd", TopicDesc="Addition" },
                new Topic { TopicId="TSub", TopicDesc="Subtraction" },
                new Topic { TopicId="TMul", TopicDesc="Multiplication" },
                new Topic { TopicId="TDiv", TopicDesc="Division" },  
                new Topic { TopicId="TAlg", TopicDesc="Algebra" },     
                
				new Topic { TopicId="TDec", TopicDesc="Decimal" }, 
                new Topic { TopicId="TFra", TopicDesc="Fraction" },

             
                 new Topic { TopicId="TNum", TopicDesc="Number Sense" },
                 new Topic  { TopicId="TInt", TopicDesc="Integers" },
             };

			topicList.ForEach(s => context.Topics.Add(s));
			context.SaveChanges();
			Console.WriteLine("updated topics");
		}

		private static void UpdateWorkSheetDesc()
		{
			var dbContext = GetDbContext();
			try
			{
				dbContext.Sheet.Include("SubTopic").ToList().ForEach(m =>
				{
					string subString = m.SheetId.Substring(m.SheetId.Length - 2);
					m.Description = (m.SubTopic.SubTopicDesc + " Worksheet # " + subString);
				});
				dbContext.SaveChanges();
			}
			finally
			{
				dbContext.Dispose();
			}

		}




	}
}
