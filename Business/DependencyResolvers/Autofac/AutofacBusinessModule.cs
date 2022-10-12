using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Helpers.Senders.SmsSenders;
using Core.Utilities.Helpers.Senders.SmsSenders.Twilio;
using Core.Utilities.Helpers.Senders.SmtpSender;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;


namespace Business.DependencyResolvers.Autofac
{
   public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CoordinateManager>().As<ICoordinateService>().SingleInstance();
            builder.RegisterType<EfCoordinateDal>().As<ICoordinateDal>().SingleInstance();

            builder.RegisterType<CoordinateImageManager>().As<ICoordinateImageService>().SingleInstance();
            builder.RegisterType<EfCoordinateImageDal>().As<ICoordinateImageDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<SmtpEmailSender>().As<IEmailSender>();
            builder.RegisterType<TwilioSender>().As<ISmsSender>();

            builder.RegisterType<NoteManager>().As<INoteService>().SingleInstance();
            builder.RegisterType<EfNoteDal>().As<INoteDal>().SingleInstance();

            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(
                new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
