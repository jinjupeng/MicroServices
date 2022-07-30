//using Castle.DynamicProxy;
//using Microsoft.Extensions.DependencyInjection;

//namespace LintCoder.Shared.Auditing
//{
//    public class AuditingInterceptor : IInterceptor
//    {
//        private readonly IServiceScopeFactory _serviceScopeFactory;
//        public AuditingInterceptor(IServiceScopeFactory serviceScopeFactory)
//        {
//            _serviceScopeFactory = serviceScopeFactory;
//        }

//        public void Intercept(IInvocation invocation)
//        {
//            var auditAttribute = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(AuditingAttribute), false);
//            var ignoreAuditAttribute = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(IgnoreAuditingAttribute), false);
//            if (auditAttribute != null && ignoreAuditAttribute == null)
//            {
//                // 添加审计日志
//                Console.WriteLine("审计日志");
//            }

//            invocation.Proceed();

//            /**
//             * 审计日志，必须可扩展
//             * 可以写入到数据库中，mongodb中，ElasticSearch等等
//             * https://stackoverflow.com/questions/60610580/net-core-default-dependency-injection-with-castle-dynamicproxy
//             * https://www.codeproject.com/Articles/5283541/Castle-Dynamic-Proxy-Interceptors-to-Track-Model-C
//             * https://gist.github.com/loosechainsaw/3415669
//             */
//        }
//    }
//}
