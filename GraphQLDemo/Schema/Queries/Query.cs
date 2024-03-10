using Bogus;

namespace GraphQLDemo.Schema.Queries
{
    public class Query
    {
        private readonly Faker<InstructorType> instructorFaker;
        private readonly Faker<StudentType> studentFaker;
        private readonly Faker<CourseType> courseFaker;

        public Query()
        {
            instructorFaker = new Faker<InstructorType>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.Salary, f => f.Random.Double(0, 100000));

            studentFaker = new Faker<StudentType>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.GPA, f => f.Random.Double(1, 4));

            courseFaker = new Faker<CourseType>()
               .RuleFor(c => c.Id, f => Guid.NewGuid())
               .RuleFor(c => c.Name, f => f.Name.JobTitle())
               .RuleFor(c => c.Subject, f => f.PickRandom<Subject>())
               .RuleFor(c => c.Instructor, f => instructorFaker.Generate())
               .RuleFor(c => c.Students, f => studentFaker.Generate(3));
        }
        public IEnumerable<CourseType> GetCourses() //=> new List<CourseType>
        {
            // Faker<CourseType> courseFaker = new Faker<CourseType>();
            List<CourseType> courses = courseFaker.Generate(5);

            return courses;

            //new CourseType()
            //{
            //    Id=   Guid.NewGuid(),
            //    Name = "Geometry",
            //    Subject = Subject.Mathematics,
            //    Instructor= new InstructorType()
            //    {
            //         Id = Guid.NewGuid(),

            //    }
            //   }
        }

        public async Task<CourseType> GetCourseById(Guid id)
        {
            await Task.Delay(1000);

            CourseType course = courseFaker.Generate();
            course.Id = id;
            return course;
        }

        [GraphQLDeprecated("This query is deprecated.")]
        public string Instructions => "This is a sampe string";
    }
}
