using LinqPractices.Entities;

namespace LinqPractice.DbOperations
{
	public class DataGenerator
	{
		public static void Initialize()
		{
			using(var context = new LinqDbContext())
			{
				if(!context.Students.Any())
				{
					context.Students.AddRange(
						new Student(){Name="Oğuzhan", Surname="Öksel", ClassId=1},
						new Student(){Name="AAA", Surname="EEE", ClassId=2},
						new Student(){Name="BBB", Surname="WWW", ClassId=3},
						new Student(){Name="CCC", Surname="QQQ", ClassId=2},
						new Student(){Name="AAA", Surname="XXX", ClassId=1},
						new Student(){Name="AAA", Surname="YYY", ClassId=1}

					);

					context.SaveChanges();
				}
			}
		}
	}
}
