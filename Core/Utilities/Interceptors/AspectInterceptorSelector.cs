using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.NLog.Loggers;
using Core.Utilities.Interceptors;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector:IInterceptorSelector
    {
        //public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        //{
        //    var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
        //    //var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

        //    //var methodAttributes = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);


        //    var methodName = method?.Name;
        //    if (methodName != null && type != null)
        //    {
        //        var methodAttributes = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null)?.GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
        //        if (methodAttributes != null)
        //        {
        //            classAttributes.AddRange(methodAttributes);
        //        }
        //    }




        //    classAttributes.Add(new LogAspect(typeof(DatabaseLogger)));
        //    classAttributes.Add(new PerformanceAspect(3));

        //    return classAttributes.OrderBy(x => x.Priority).ToArray();
        //}
    
        
            public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
            {
                var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                    (true).ToList();
                var methodAttributes = type.GetMethod(method.Name)
                    .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
                classAttributes.AddRange(methodAttributes);
                classAttributes.Add(new LogAspect(typeof(DatabaseLogger)));

                return classAttributes.OrderBy(x => x.Priority).ToArray();
            }
        
    }
}
