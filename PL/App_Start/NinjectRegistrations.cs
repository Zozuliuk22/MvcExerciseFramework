using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;
using DAL.Interfaces;
using DAL;

namespace PL
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IMeetingService>().To<MeetingService>().InSingletonScope();
            Bind<IHistoryService>().To<HistoryService>().InSingletonScope();
            Bind<IScenarioCreatorService>().To<ScenarioCreatorService>().InSingletonScope();
        }
    }
}