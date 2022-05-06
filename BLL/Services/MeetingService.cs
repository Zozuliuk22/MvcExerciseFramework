using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BLL.Interfaces;
using BLL.Guilds;
using BLL.Events;
using DAL.Interfaces;

namespace BLL.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly ThievesGuild _thievesGuild;
        private readonly BeggarsGuild _beggarsGuild;
        private readonly FoolsGuild _foolsGuild;
        private readonly AssassinsGuild _assassinsGuild;
        
        private List<MethodInfo> _methodsCreateGuild;        

        public MeetingService(IUnitOfWork unitOfWork)
        {
            _thievesGuild = new ThievesGuild();
            _beggarsGuild = new BeggarsGuild(unitOfWork);
            _foolsGuild = new FoolsGuild(unitOfWork);
            _assassinsGuild = new AssassinsGuild(unitOfWork);

            _methodsCreateGuild = typeof(MeetingService)
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.Name.StartsWith("Create")
                         && m.Name.EndsWith("Meeting")
                         && !m.Name.Contains("Random"))
                .ToList();
        }

        public Meeting CreateRandomGuildMeeting()
        {
            return (Meeting)_methodsCreateGuild[new Random()
                                                    .Next(0, _methodsCreateGuild.Count)]
                                                    .Invoke(this, null);
        }

        private Meeting CreateThievesGuildMeeting()
        {
            _thievesGuild.AddTheft();

            if (_thievesGuild.CurrentNumberThefts > _thievesGuild.MaxNumberThefts)
            {
                var method = _methodsCreateGuild.First(m => m.Name.Contains("Thieves"));
                _methodsCreateGuild.Remove(method);
                return CreateRandomGuildMeeting();
            }
            else
            {
                return new Meeting(_thievesGuild);
            }
        }

        private Meeting CreateBeggarsGuildMeeting()
        {
            return new Meeting(_beggarsGuild, _beggarsGuild.GetActiveNpc());
        }

        private Meeting CreateAssassinsGuildMeeting()
        {
            return new Meeting(_assassinsGuild);
        }

        private Meeting CreateFoolsGuildMeeting()
        {
            return new Meeting(_foolsGuild, _foolsGuild.GetActiveNpc());
        }

        public void Reset()
        {
            _thievesGuild.Reset();
            _assassinsGuild.Reset();
            _beggarsGuild.Reset();
            _foolsGuild.Reset();
        }
    }
}
