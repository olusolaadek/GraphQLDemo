using Bogus.DataSets;
using GraphQLDemo.Schema.Queries;
using GraphQLDemo.Schema.Subscriptions;
using HotChocolate.Subscriptions;


namespace GraphQLDemo.Schema.Mutations
{
    public class Mutation
    {
        private readonly List<CourseResult> _courses;
        public Mutation()
        {
            _courses = new List<CourseResult>();
        }
        public async Task<CourseResult> CreateCourse(CourseInput courseInput,
            [Service] ITopicEventSender topicEventSender)
        {
            CourseResult course = new CourseResult()
            {
                Id = Guid.NewGuid(),
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId
            };

            _courses.Add(course);
            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);

            return course;
        }

        public async Task<CourseResult> UpdateCourse(Guid id, CourseInput courseInput,
            [Service] ITopicEventSender topicEventSender)
        {
            CourseResult? course = _courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                throw new GraphQLException(new Error("Course Not found", "COURSE_NOT_FOUND"));
                //  throw new Exception("Course Not found");
            }
            course.Name = courseInput.Name;
            course.Subject = courseInput.Subject;
            course.InstructorId = courseInput.InstructorId;

            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
            await topicEventSender.SendAsync(updateCourseTopic, course);

            return course;
        }

        public bool DeleteCourse(Guid id)
        {
            return _courses.RemoveAll(c => c.Id == id) >= 1;
        }
    }
}
