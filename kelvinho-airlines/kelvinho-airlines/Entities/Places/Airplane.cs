﻿using kelvinho_airlines.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kelvinho_airlines.Entities.Places
{
    public class Airplane : Place
    {
        public List<CrewMember> TechnicalCrew { get; protected set; }
        public List<CrewMember> CabinCrew { get; protected set; }
        public List<CrewMember> CommonCrew { get; protected set; }

        public Airplane()
        {
            TechnicalCrew = new List<CrewMember>();
            CabinCrew = new List<CrewMember>();
            CommonCrew = new List<CrewMember>();
        }

        public override void Board(HashSet<CrewMember> crewMembers)
        {
            foreach (var crewMember in crewMembers)
            {
                CrewMembers.Add(crewMember);

                if (crewMember.CrewType == CrewType.Technical)
                {
                    TechnicalCrew.Add(crewMember);
                }
                else if (crewMember.CrewType == CrewType.Cabin)
                {
                    CabinCrew.Add(crewMember);
                }
                else
                {
                    CommonCrew.Add(crewMember);
                }
            }
        }

        public override void Disembark(HashSet<CrewMember> crewMembers)
        {
            foreach (var crewMember in crewMembers)
            {
                CrewMembers.Remove(crewMember);

                if (crewMember.CrewType == CrewType.Technical)
                {
                    TechnicalCrew.Remove(crewMember);
                }
                else if (crewMember.CrewType == CrewType.Cabin)
                {
                    CabinCrew.Remove(crewMember);
                }
                else
                {
                    CommonCrew.Remove(crewMember);
                }
            }
        }

        public override string ToString()
        {
            StringBuilder technicalCrew = new StringBuilder();
            foreach(var crewMember in TechnicalCrew)
            {
                technicalCrew.AppendLine($"{crewMember.GetType().Name} : {crewMember.Name}");
            }

            StringBuilder cabinCrew = new StringBuilder();
            foreach (var crewMember in CabinCrew)
            {
                cabinCrew.AppendLine($"{crewMember.GetType().Name} : {crewMember.Name}");
            }

            StringBuilder commonCrew = new StringBuilder();
            foreach (var crewMember in CabinCrew)
            {
                commonCrew.AppendLine($"{crewMember.GetType().Name} : {crewMember.Name}");
            }

            return $"Airplane:\n\nTechnical Crew:\n\n{technicalCrew}\nCabin Crew:\n\n{cabinCrew}\nCommon Crew:\n\n{commonCrew}\n";
        }
    }
}
