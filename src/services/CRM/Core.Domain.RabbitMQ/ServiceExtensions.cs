//using Core.Domain.RabbitMQ.Context;

//namespace Core.Domain.RabbitMQ
//{
//    public static class ServiceExtensions
//    {
//        public static void AddRabbitMqInfrastructure(this IServiceCollection services)
//        {

//            services.AddMassTransit(x =>
//            {
//                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
//                {
//                    config.Host(new Uri("rabbitmq://localhost"), h =>
//                    {
//                        h.Username("guest");
//                        h.Password("guest");
//                    });
//                }));

//            });
//            services.AddScoped<RabbitMqContext, RabbitMqContext>();
//            services.AddMassTransitHostedService();
//        }
//    }
//}
