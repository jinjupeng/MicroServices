//using Castle.Core;
//using Castle.MicroKernel.Proxy;
//using System.Reflection;

//namespace LintCoder.Shared.Auditing
//{
//    public class AuditInterceptorSelector : IModelInterceptorsSelector
//    {
//        public bool HasInterceptors(ComponentModel model)
//        {
//            var result = model.Implementation.GetMethods(BindingFlags.Public | BindingFlags.Instance)
//                .Any(method => method.GetCustomAttributes(false).Any(attribute => attribute.GetType() == typeof(AuditingAttribute)));
//            return result;
//        }

//        public InterceptorReference[] SelectInterceptors(ComponentModel model, InterceptorReference[] interceptors)
//        {
//            return new[]
//                        {
//                           InterceptorReference.ForType<AuditingInterceptor>()
//                       };
//        }
//    }
//}
