using LinqPractice.DbOperations;
using LinqPractices.Entities;

// Name="Oğuzhan", Surname="Öksel", ClassId=1
// Name="AAA", Surname="EEE", ClassId=2
// Name="BBB", Surname="WWW", ClassId=3
// Name="CCC", Surname="QQQ", ClassId=2
// Name="AAA", Surname="XXX", ClassId=1
// Name="AAA", Surname="YYY", ClassId=1

internal class Program
{
	private static void Main(string[] args)
	{
		DataGenerator.Initialize();
		LinqDbContext _context = new LinqDbContext();
		var students = _context.Students.ToList();

		Student? student;
		// Find()
		/*Finds an entity with the given primary key values.*/
		Console.WriteLine("***Find()***");
		student = _context.Students.Find(3);
		Console.WriteLine(student.Name); // output: CCC

		// First()
		/*Returns the first element of a sequence that satisfies a specified condition.*/
		Console.WriteLine("***First()***");
		student = _context.Students.First(s => s.Name == "AAA");
		Console.WriteLine($"{student.Name} {student.Surname}"); // output: AAA EEE

		// FirstOrDefault()
		/*Returns the first element of a sequence that satisfies a specified condition
		or a default value if no such element is found.*/
		Console.WriteLine("***FirstOrDefault()***");
		student = _context.Students.FirstOrDefault(s => s.Name == "AAA");
		Console.WriteLine($"{student.Name} {student.Surname}"); // output: AAA EEE

		// SingleOrDefault()
		/*Returns the only element of a sequence that satisfies a specified condition
		or a default value if no such element exists; this method throws an exception
		if more than one element satisfies the condition.*/
		Console.WriteLine("***SingleOrDefault()***");
		student = _context.Students.SingleOrDefault(s => s.Surname == "QQQ");
		Console.WriteLine($"{student.Name} {student.Surname}"); // output: CCC QQQ

		// ToList()
		/*Creates a List<T> from an IEnumerable<out T>.*/
		Console.WriteLine("***ToList()***");
		var studentList = _context.Students.ToList();
		Console.WriteLine($"number of items: {studentList.Count()}"); // output: 6

		// OrderBy()
		/*Sorts the elements of a sequence in ascending order according to a key.*/
		Console.WriteLine("***OrderBy()***");
		var orderedList = studentList.OrderBy(s => s.Name);
		foreach(var item in orderedList){
			Console.Write($"{item.Name}, "); // output: AAA, AAA, AAA, BBB, CCC, Oguzhan,
		}

		// Anonymous Object Result
		/**/
		Console.WriteLine("***Anonymous Object Result***");
		var anonymousObject = _context.Students
		.Where(s => s.ClassId==1)
		.Select(s => new {
			Id = s.Id,
			FullName = $"{s.Name} {s.Surname}"
		});
		foreach(var item in anonymousObject){
			Console.Write($"{item.FullName}, "); // output: Oguzhan Öksel, AAA XXX, AAA YYY,
		}

	}
}
