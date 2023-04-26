using CatalogInfoCommonLibrary.Commands;
using CatalogInfoCommonLibrary.Factories;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;

namespace CatalogInfoService.Factories
{
    public class CommonCommandFactory : ICommonCommandFactory
    {
        /// <summary>
        /// Мапа создания команд
        /// </summary>
        private Dictionary<string, Func<object[], object>> ResolveList
            = new Dictionary<string, Func<object[], object>>();

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CommonCommandFactory(IServiceScopeFactory serviceScopeFactory) 
        {
            this._serviceScopeFactory = serviceScopeFactory;
        }


        public T Resolve<T>(string commandTitle, object[]? args)
        {
            Func<object[], object> c; 
            if(ResolveList.ContainsKey(commandTitle))
            {
                c = ResolveList[commandTitle];
            }
            else
            {
                c = CreateGenerateCommand(commandTitle);
                ResolveList.Add(commandTitle, c);
            }
            return (T)c(args);
        }

        private Func<object[], object> CreateGenerateCommand(string commandTitle)
        {
            var constructor = Type.GetType($"CatalogInfoCommonLibrary.{commandTitle}, CatalogInfoCommonLibrary")
                    ?.GetConstructor(new Type[] { typeof(IServiceScopeFactory), typeof(object[]) }) ?? throw new NullReferenceException();

            return (args) =>
            {
                var parameters = new List<object>();
                foreach (var p in constructor.GetParameters())
                {
                    if (p.ParameterType == args.GetType())
                    {
                        parameters.Add(args);
                    }
                    else
                    {
                        using (var scope = this._serviceScopeFactory.CreateScope())
                        {
                            parameters.Add(scope.ServiceProvider.GetService(p.ParameterType) ?? throw new NullReferenceException());
                        }
                    }
                }

                return (object)(constructor?.Invoke(parameters.ToArray()) ?? throw new NullReferenceException());
            };
        }
    }
}
