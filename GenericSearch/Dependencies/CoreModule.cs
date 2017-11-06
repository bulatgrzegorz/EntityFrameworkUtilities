using GenericSearch.Expressions;
using GenericSearch.Expressions.ExpressionStrategyHandlers;
using GenericSearch.Helpers;
using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace GenericSearch.Dependencies
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(
                x => x.FromAssemblyContaining<IPrepareExpressionBaseOnStrategy>()
                    .SelectAllClasses()
                    .InheritedFrom<IPrepareExpressionBaseOnStrategy>()
                    .BindSingleInterface()
                    .Configure(c => c.InSingletonScope()));

            Bind<IConvertTypeToPrecise>()
                .To<PreciseTypeConverter>()
                .InSingletonScope();
        }
    }
}