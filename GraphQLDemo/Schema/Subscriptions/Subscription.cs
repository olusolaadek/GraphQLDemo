using GraphQLDemo.Schema.Mutations;
using GraphQLDemo.Schema.Queries;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.Schema.Subscriptions
{
    public class Subscription
    {
        [Subscribe]
        public CourseResult CourseCreated([EventMessage] CourseResult course) => course;

        [SubscribeAndResolve]
        public ISourceStream<CourseResult> CourseUpdated(Guid courseId, ITopicEventReceiver topicEventReceiver)
        {
            string topicName = $"{courseId}_{nameof(Subscription.CourseUpdated)}";

            return topicEventReceiver.SubscribeAsync<CourseResult>(topicName).Result;
        }
    }
}
