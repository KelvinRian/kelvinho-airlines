using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.CrewMembers;
using kelvinho_airlines.Entities.Places;
using kelvinho_airlines.Services.Interfaces;
using kelvinho_airlines.Utils;
using kelvinho_airlines.Utils.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kelvinho_airlines.Services
{
    public class TripInformerService : ITripInformerService
    {
        public void ShowBoardingInfo(params CrewMember[] crewMembers)
        {
            StringBuilder crewMembersInfo = new StringBuilder();

            foreach (var crewMember in crewMembers.Where(c => !c.IsNull()))
            {
                if (crewMembersInfo.Length > 0)
                    crewMembersInfo.Append(", ");

                crewMembersInfo.Append(crewMember);
            }

            Console.WriteLine($"Boarding ({crewMembersInfo})\n");
        }

        public void ShowDisembarkingInfo(IEnumerable<CrewMember> crewMembers)
        {
            StringBuilder crewMembersDisembarking = new StringBuilder();

            foreach (var crewMember in crewMembers)
            {
                if (crewMembersDisembarking.Length > 0)
                    crewMembersDisembarking.Append(", ");

                if (!crewMember.IsNull())
                    crewMembersDisembarking.Append(crewMember);
            }

            Console.WriteLine($"Disembarking ({crewMembersDisembarking})\n");
        }

        public void ShowMovementInfo(Place origin, Place destiny)
        {
            var originType = origin.GetType().Name;
            var destinyType = destiny.GetType().Name;

            Console.WriteLine($"Moving ({originType} => {destinyType})");
            Console.WriteLine($"\n{TextHelper.DividingLine}");
        }

        public void ShowStartMessage()
        {
            Console.WriteLine("Started\n");
        }

        public void ShowTripStateInfo(Place currentPlace, Place destinyPlace)
        {
            var defaultColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Location: {currentPlace.GetType().Name}\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{currentPlace.SmartFortwo}\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            var terminal = currentPlace is Terminal ? currentPlace : destinyPlace;
            Console.WriteLine(terminal);

            Console.ForegroundColor = ConsoleColor.Red;
            var airplane = currentPlace is Airplane ? currentPlace : destinyPlace;
            Console.WriteLine(airplane);

            Console.ForegroundColor = defaultColor;
            Console.WriteLine(TextHelper.DividingLine);
        }
    }
}
