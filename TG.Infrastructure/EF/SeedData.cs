using System;
using System.Collections.Generic;
using System.Linq;
using TG.Domain.Entities;

namespace TG.Infrastructure.EF
{
	public static class SeedData
	{
		static List<CategoryEntity> Categories = new List<CategoryEntity>();
		static List<QuestionEntity> Questions = new List<QuestionEntity>();

		public static void Initialize(ApplicationDbContext context)
		{
			context.Database.EnsureCreated();

			if (!context.Categories.Any())
			{
				GreateCategories();
				context.Categories.AddRange(Categories);
				context.SaveChanges();
			}
			if (!context.Players.Any())
			{
				context.Players.AddRange(GreatePlayers());
				context.SaveChanges();
			}
			
			if (!context.Questions.Any())
			{
				GreateQuestions();
				context.Questions.AddRange(Questions);
				context.SaveChanges();
			}
			if (!context.Answers.Any())
			{
				context.Answers.AddRange(GreateAnswers());
				context.SaveChanges();
			}
		}
		private static void GreateCategories()
		{
			
			for (int i = 1; i <= 10; i++)
			{
				var category = new CategoryEntity 
				{
					Name = $"Category {i}",
				};
				Categories.Add(category);
			}
		}

		private static void GreateQuestions()
		{
			foreach(var cat in Categories)
			{
				for (int i = 1; i <= 50; i++)
				{
					var question = new QuestionEntity
					{
						Text = $"This is the {i} question for the category '{cat.Id}'",
						CategoryId = cat.Id
					};
					Questions.Add(question);
				}
			}
		}
		private static List<AnswerEntity> GreateAnswers()
		{
			var answers = new List<AnswerEntity>();
			foreach (var question in Questions)
			{
				for (int i = 1; i <= 4; i++)
				{
					var answer = new AnswerEntity
					{
						Text = $"Answer {i}",
						IsCorrect = (i == 1 ? true : false),
						QuestionId = question.Id
					};

					answers.Add(answer);
				}
			}
			
			return answers;
		}

		private static List<PlayerEntity> GreatePlayers()
		{
			var players = new List<PlayerEntity>();
			Random rnd = new Random();
			for (int i = 1; i <= 100; i++)
			{
				var player = new PlayerEntity
				{
					Name = $"Player {i}",
					Score = rnd.Next(0,700),
					LastGameDate = new DateTime(2020, rnd.Next(5, 7), rnd.Next(1, 28)),
					IsGameOrganizer = false,

			    };
				players.Add(player);
			}
			return players;
		}
	}
}
